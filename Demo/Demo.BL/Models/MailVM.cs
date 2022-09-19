using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.Models
{
    public class MailVM
    {

        public string ToMail { get; set; }

        [Required(ErrorMessage = "Title Required")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Message Required")]
        public string Body { get; set; }



    }
}
