using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace mvc.Models
{
    public class Grade
    {
        [Key, Column(Order = 0)]
        [Required (ErrorMessage ="this field is required")]
        public string username { get; set; }
        [Key, Column(Order = 1)]
        [Required(ErrorMessage = "this field is required")]
        public string cid { get; set; }
        [Required(ErrorMessage = "this field is required")]
        public string grade1 { get; set; }

    }
}