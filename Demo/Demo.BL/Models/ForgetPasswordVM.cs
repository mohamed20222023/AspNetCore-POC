using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Demo.BL.Models
{
    public class ForgetPasswordVM
    {

        [Required(ErrorMessage = "E-Mail Is Required")]
        [EmailAddress(ErrorMessage = "This Isn't E-Mail Address")]
        public string Email { get; set; }


    }
}
