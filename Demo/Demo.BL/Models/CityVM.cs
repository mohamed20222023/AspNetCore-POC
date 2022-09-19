using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.DAL.Entity
{

    public class CityVM
    {
        public int Id { get; set; }


        public string? Name { get; set; }

        public int CountryId { get; set; }

        public Country? Country { get; set; }

    }
}
