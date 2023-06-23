using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewEmployeeNotificationServices.RequestModel
{
    public class NENFormStatusRequestDTO
    {
        public bool? employeeDetails { get; set; }
        public bool? conditions { get; set; }
        public bool? payrollDetails { get; set; }
        public bool? payrollSupport { get; set; }
        public bool? remunerationBenefits { get; set; }
        public bool? attachments { get; set; }
        public bool? approver { get; set; }

    }

    public class UserDetails
    {
        public string firstName { get; set; }
        public string surName { get; set; }
        public string department { get; set; }
        public string positionTitle { get; set; }
        public string company { get; set; }
        public string office { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postCode { get; set; }
        public string country { get; set; }
        public string mobileNumber { get; set; }
        public string lineManager { get; set; }
        public string employeeType { get; set; }
        public string lineManagerEmployeeNumber { get; set; }
        public string ledgerCode { get; set; }
        public string departmentCode { get; set; }
        public string locationCode { get; set; }
        public string endDate { get; set; }
        public string positionNumber { get; set; }
        public string joinDate { get; set; }
        public string chrisId { get; set; }

    }

    public class CheerwellEntities
    {
        public string busObId { get; set; }
        public List<Field> fields { get; set; }
    }

    public class Field
    {
        public bool dirty { get; set; }
        public string fieldId { get; set; }
        public string value { get; set; }
    }

    public class Email
    {
        public string ManagerFirstname { get; set; }
        public string ManagerLastname { get; set; }
        public string ActionByFirstname { get; set; }
        public string ActionByLastname { get; set; }
        public string EmployeeFirstname { get; set; }
        public string EmployeeLastname { get; set; }
        public string EmployeeNumber { get; set; }
        public string EffectiveDate { get; set; }
        public string RequestorFirstname { get; set; }
        public string RequestorLastname { get; set; }
        public string formTitle { get; set; }
        public int RequestId { get; set; }
        public int DelegationId { get; set; }
        public string Comment { get; set; }
        public string TO { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public string Title { get; set; }
        public string SubHeading { get; set; }
        public string Attachment { get; set; }
    }
}
