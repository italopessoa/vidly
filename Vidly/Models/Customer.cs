using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please insert the customer's name")]
        [StringLength(255)]
        public string Name { get; set; }
        
        [Display(Name = "Date of birth")]
        [Min18YearIfAMember]
        public DateTime? Birthdate { get; set; }
        
        public bool IsSubscribedToNewsletter { get; set; }
        
        public MemberShipType MemberShipType { get; set; }
        
        [Display(Name = "Membership Type")]
        public byte MemberShipTypeId { get; set; }
    }
}