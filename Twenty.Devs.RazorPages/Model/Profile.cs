using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Twenty.Devs.RazorPages.Model
{
    public class Profile
    {
        [Required(ErrorMessage = "Enter Your Name")]
        public string   Name    { get; set; }
        
        [Required(ErrorMessage = "Enter Your Family")]
        [StringLength(5,ErrorMessage = "Minimum Length of Family must be 5 character")]
        public string   Family  { get; set; }

        [Range(15,120,ErrorMessage = "Your age must be in the range of 15 and 120")]
        public byte     Age     { get; set; }

        [Required(ErrorMessage = "Enter Your Email")]
        [EmailAddress(ErrorMessage = "Enter Valid Email Address")]
        public string   Email   { get; set; }
    }
}
