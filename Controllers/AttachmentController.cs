using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using urs_api.DbContexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NewEmployeeNotificationServices.RequestModel;
using NewEmployeeNotificationServices.ResponseModel;
using NLog;
using urs_api.Models;

namespace NewEmployeeNotificationServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        private readonly URSDbContext _attachmentContext;
        private URSDbContext _context;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IConfiguration GetConfig()
        {
            var Config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("AppSettings.json", optional: true, reloadOnChange: true);
            return Config.Build();
        }

        private URSDbContext InitializingContext()
        {
            URSDbContext Context = new URSDbContext();
            var Config = GetConfig();
            Context.Database.GetDbConnection().ConnectionString = Config.GetSection("ConnectionStrings").GetSection("OnlineFormDatabase").Value;
            return Context;
        }

        public AttachmentController(URSDbContext context)
        {
            _context = context;
        }

        #region Attachments

        /// <summary>
        /// Get All AttachmentsTypes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAttachmentTypes/{id}")]
        public ActionResult<IEnumerable<AttachmentTypeResponseDTO>> GetAttachmentTypes(string Id)
        {
            logger.Info("Received request for GetAttachmentTypes");
            _context = InitializingContext();
            try
            {
                    return _context.Attachment
                           .Where(x => x.Id == Convert.ToInt32(Id))
                           .Select(x => new AttachmentTypeResponseDTO
                           {
                               Id = Convert.ToInt64(x.Id),
                               AttachmentName = x.FileName,
                               DocUrl = x.OriginalFilePath,
                               CreatedDate = x.CreatedDate,
                               CreatedByUser = x.CreatedBy,
                               UpdatedDate = x.LastUpdateDate,
                               UpdatedByUser = x.LastUpdateBy
                           }).ToList();
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message, "Id=" + Id);
                throw ex;
            }
        }

        /// <summary>
        /// Get Saved Attachments
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAttachments/{Id}/")]
        public ActionResult<IEnumerable<AttachmentResponseDTO>> GetAttachments( string Id)
        {
            try {
                _context = InitializingContext();
                logger.Info("Received request for GetAttachments. and Id=" + Id);
            var attachments = _attachmentContext.Attachment
                            .Where(x => x.Id == Convert.ToInt32(Id))
                            .Select(x =>
                            new AttachmentResponseDTO
                            {
                               
                                Id = x.Id,
                                FileName = x.FileName,
                                ContentType = x.ContentType,
                                //FileContents = x.FileContents,
                                FileExt = x.FileExt,
                                FileSizeBytes = x.FileSizeBytes
                            }).ToList();

            attachments.ForEach(attachment =>
            {
                attachment.AttachmentName =Convert.ToString(_context.Attachment
                                        .FirstOrDefault(x => x.Id == Convert.ToInt32(Id)));

            });

            return Ok(attachments);
        }
              catch (Exception ex)
            {
                logger.Error(ex, ex.Message, "Form=" + Id);
                throw ex;
            }
        }

        /// <summary>
        /// Save Attachments
        /// </summary>
        /// <param name="attachmentModel">Attachments</param>
        /// <param name="formId">RequestId</param>
        /// <param name="requestId">RequestId</param>
        /// <returns>true</returns>
        [HttpPost("SaveAttachments/{formId}/{requestId?}")]
        public ActionResult SaveAttachments([FromForm] AttachmentRequestDTO attachmentModel, string formId, string Id)
        {
            try
            {
                logger.Info("Received request for SaveAttachments. Form=" + formId + " and RequestID=" + Id);
                if (attachmentModel != null && attachmentModel.files.Count > 0)
                {
                   
                    InsertAttachments(formId, attachmentModel);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message, "Form=" + formId + " and RequestID=" + Id);
                throw ex;
            }
            if (string.IsNullOrEmpty(Id))
            {
                return CreatedAtAction("SaveAttachments", attachmentModel.GetType());
            }
            return NoContent();
        }

        



        /// <summary>
        /// Delete Attachments by AttachmentId
        /// </summary>
        /// <param name="attachments">attachments</param>
        /// <returns>true</returns>
        [HttpPost("DeleteAttachments")]
        public ActionResult DeleteAttachments(List<int> attachments)
        {
            try
            {
                logger.Info("Received request for Delete Attachments ");
                attachments.ForEach(attachmentId =>
                {
                    var attachment = _attachmentContext.Attachment.FirstOrDefault(attachment => attachment.Id == attachmentId);
                    _attachmentContext.Attachment.Remove(attachment);
                });
                _attachmentContext.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                throw ex;
            }
            return Ok(attachments);
        }

        /// <summary>
        /// Download an Attachment by AttachmentID
        /// </summary>
        /// <param name="attachmentId">attachmentId</param>
        /// <returns>true</returns>
        [HttpGet("DownloadAttachment/{attachmentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DownloadAttachment(int attachmentId)
        {
            try
            {
                logger.Info("Received request for Download Attachment " + " attachment Id" + attachmentId);
                var attachment = _attachmentContext.Attachment.FirstOrDefault(x => x.Id == attachmentId);
                if (attachment != null)
                {
                    return new FileContentResult(attachment.FileContents, attachment.ContentType)
                    {
                        FileDownloadName = attachment.FileName,
                    };
                }
            }
           
            catch(Exception ex)
            {
                logger.Error(ex, ex.Message);
                throw ex; 
            }
            return NotFound($"The File with the specified attachmentId: {attachmentId} is not found");

        }

        #endregion

      
        private void InsertAttachments(string pastProgramId, AttachmentRequestDTO model)
        {
            logger.Info("Received request for InsertAttachments.  Form=" + pastProgramId);

            List<Attachments> attachments = new List<Attachments>();
            try
            {
                var attachmentTypeList = model.attachmentTypeId.Split(',').Select(x => Convert.ToInt32(x)).ToList();
                for (var i = 0; i < model.files.Count; i++)
                {
                    Attachments attachment = new Attachments();
                    attachment.FileName = Path.GetFileName(model.files[i].FileName);
                    attachment.FileExt = Path.GetExtension(model.files[i].FileName);
                    attachment.ContentType = model.files[i].ContentType;
                    attachment.FileSizeBytes = (int)model.files[i].Length;
                    using (var target = new MemoryStream())
                    {
                        model.files[i].CopyTo(target);
                        attachment.FileContents = target.ToArray();
                    }
                    attachments.Add(attachment);
                }
                _attachmentContext.Attachment.AddRange(attachments);
                _attachmentContext.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                throw ex;
            }
        }
    }
}