using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Demo.BL.Models
{
    public class LoginVM
    {

        [Required(ErrorMessage ="E-Mail Is Required")]
        [EmailAddress(ErrorMessage ="This Isn't E-Mail Address")]
        public string Email { get; set; }


        [Required(ErrorMessage ="Password Is Required")]
        [MinLength(6,ErrorMessage ="Min Length Is 6 ")]
        public string Password { get; set; }


        public bool RememberMe { get; set; }

    }
}
