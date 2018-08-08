using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class PickUpModel
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Customer")]
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }

        public int Price { get; set; }

        [Display(Name = "Pickup Status")]
        public bool PickUpStatus { get; set; }
        
        [Display(Name = "Extra Pickup Status")]
        public bool ExtraPickUpStatus { get; set; }
    }
}