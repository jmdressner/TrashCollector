using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class Employee
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        [ForeignKey("Zipcode")]
        [Display(Name = "Work Route Zipcode")]
        public int ZipcodeID { get; set; }
        public Zipcode Zipcode { get; set; }

        public IEnumerable<Zipcode> Zipcodes { get; set; }
    }
}