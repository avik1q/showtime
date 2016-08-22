using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShowTime.Models
{
    public class PastShow
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "נא למלא את שדה השם")]
        public string Name { get; set; }
        [Required(ErrorMessage = "נא למלא את שדה הכתובת")]
        public string URL { get; set; }
    }
}