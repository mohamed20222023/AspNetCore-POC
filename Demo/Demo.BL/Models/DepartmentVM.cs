using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.Models
{
    public class DepartmentVM
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage ="Name Is Required")]
        [MinLength(3,ErrorMessage ="Minimum length : 3")]
        [MaxLength(20,ErrorMessage ="Max length : 20")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Code Is Required")]
        [MinLength(3, ErrorMessage = "Minimum length : 3")]
        public string? Code { get; set; }
    }

}
