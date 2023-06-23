using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewEmployeeNotificationServices.RequestModel
{
    public class AttachmentRequestDTO
    {
        public string attachmentTypeId { get; set; }
        public List<IFormFile> files { get; set; }

    }

    public class DeleteAttachmentRequestDTO
    {
        public string requestId { get; set; }
        public List<int> attachmentId { get; set; }
    }
}
