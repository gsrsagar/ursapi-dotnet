using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewEmployeeNotificationServices.ResponseModel
{
    public class NENFormStatusResponseDTO
    {
        public bool? employeeDetails { get; set; }
        public bool? conditions { get; set; }
        public bool? payrollDetails { get; set; }
        public bool? payrollSupport { get; set; }
        public bool? remunerationBenefits { get; set; }
        public bool? attachments { get; set; }
        public bool? approver { get; set; }

    }

    public class CherwellResponse
    {
        public string busObPublicId { get; set; }
        public string busObRecId { get; set; }
        public string cacheKey { get; set; }
        public string errorCode { get; set; }
        public string errorMessage { get; set; }
        public bool hasError { get; set; }
        public List<fieldValidationErrors> fieldValidationErrors { get; set; }
        public List<notificationTriggers> notificationTriggers { get; set; }
    }

    public class fieldValidationErrors
    {
        public string error { get; set; }
        public string errorCode { get; set; }
        public string fieldId { get; set; }
    }

    public class notificationTriggers
    {
        public string sourceType { get; set; }
        public string sourceId { get; set; }
        public string sourceChange { get; set; }
        public string key { get; set; }
    }
}
