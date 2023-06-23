using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewEmployeeNotificationServices.ResponseModel
{
    public class AttachmentTypeResponseDTO
    {
        public Int64 Id { get; set; }
        public string AttachmentName { get; set; }
        public string Description { get; set; }
        public string DocUrl { get; set; }
        public bool Active { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedByUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedByUser { get; set; }
    }
}
