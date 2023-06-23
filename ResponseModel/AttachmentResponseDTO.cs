using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewEmployeeNotificationServices.ResponseModel
{
    public class AttachmentResponseDTO
    {
        public long Id { get; set; }
        public string AttachmentName { get; set; }
        public int AttachmentTypeId { get; set; }
        public int? RequestId { get; set; }
        public int? FormId { get; set; }
        public string FileName { get; set; }
        public string FileExt { get; set; }
        public string ContentType { get; set; }
        public byte[] FileContents { get; set; }
        public int? FileSizeBytes { get; set; }
    }
}
