using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewEmployeeNotificationServices.ResponseModel
{
    public class RequestResponseDTO
    {
        public int Id { get; set; }
        public int FormId { get; set; }
        public string FormName { get; set; }
        public string FormDescription { get; set; }
        
    }
}
