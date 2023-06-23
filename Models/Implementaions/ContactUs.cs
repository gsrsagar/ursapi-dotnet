using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace urs_api.Models.Implementaions
{
    public class ContactUs
    {
        [Key]
        public int contactUsId { get; set; }
        [Required]
        public string emailId { get; set; }
        [Required]
        public string mobileNo { get; set; }
        [Required]
        public string query { get; set; }

        public static explicit operator ContactUs(EntityEntry<ContactUs> v)
        {
            throw new NotImplementedException();
        }

        public static explicit operator ContactUs(ValueTask<EntityEntry<ContactUs>> v)
        {
            throw new NotImplementedException();
        }
    }
}
