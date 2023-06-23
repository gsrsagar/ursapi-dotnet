using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace urs_api.Models.Implementaions
{
    public class Location
    {
        [Key]
        public int locationId { get; set; }
        [Required]
        public string locationName { get; set; }
        [Required]
        public string cityName { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        public string stateName { get; set; }
        [Required]
        [MinLength(5),MaxLength(8)]
        public Int32 pinCode { get; set; }

        public static explicit operator Location(EntityEntry<Location> v)
        {
            throw new NotImplementedException();
        }

        public static explicit operator Location(ValueTask<EntityEntry<Location>> v)
        {
            throw new NotImplementedException();
        }
    }
}
