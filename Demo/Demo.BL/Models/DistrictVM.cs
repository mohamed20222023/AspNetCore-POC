using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.DAL.Entity
{

    public class DistrictVM
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int CityId { get; set; }

        public City? City { get; set; }

    }
}
