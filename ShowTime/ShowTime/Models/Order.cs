using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ShowTime.Models
{
    public class Order
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public int Show { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}