using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AGIMaster.Models
{
    public class EditAccount : AGIMaster.Models.Table
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string CompanyAddress { get; set; }

        [Required]
        public string PrimaryContactName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string PrimaryEmailAddress { get; set; }

        [Required]
        public string PrimaryPhone { get; set; }

        [Required]
        public string SecondaryContactName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string SecondaryEmailAddress { get; set; }

        [Required]
        public string SecondaryPhone { get; set; }

        [Required]
        public string ShippingAddress { get; set; }
    }
}