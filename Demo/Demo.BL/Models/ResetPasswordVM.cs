using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Demo.BL.Models
{
    public class ResetPasswordVM
    {

        [Required(ErrorMessage = "Password Is Required")]
        [MinLength(6, ErrorMessage = "Min Length Is 6 ")]
        public string Password { get; set; }


        [Compare("Password", ErrorMessage = "Passord Not Matching")]
        [Required(ErrorMessage = "Password Is Required")]
        [MinLength(6, ErrorMessage = "Min Length Is 6 ")]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }



    }
}
