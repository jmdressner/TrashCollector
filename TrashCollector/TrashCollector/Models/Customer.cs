using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class Customer
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        [ForeignKey("Zipcode")]
        public int ZipcodeID { get; set; }
        public Zipcode Zipcode { get; set; }

        [ForeignKey("TrashDay")]
        [Display(Name = "Trash Pick Up Day")]
        public int TrashDayID { get; set; }
        public TrashDay TrashDay { get; set; }

        public bool PickUpStatus { get; set; }

        [ForeignKey("ExtraDay")]
        [Display(Name = "Extra Pick Up")]
        public int ExtraID { get; set; }
        public ExtraDay ExtraDay { get; set; }

        public IEnumerable<ExtraDay> ExtraDays { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}