using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }

        public byte MemberShipTypeId { get; set; }

        public MembershipTypeDto MemberShipType { get; set; }

        [Display]
        public DateTime? Birthdate { get; set; }
    }
}
