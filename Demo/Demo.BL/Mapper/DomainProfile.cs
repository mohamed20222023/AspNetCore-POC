using AutoMapper;
using Demo.BL.Models;
using Demo.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.Mapper
{
    public class DomainProfile:Profile
    {

        public DomainProfile()
        {
            CreateMap<DepartmentVM , Department>();
            CreateMap<Department , DepartmentVM>();

            CreateMap<EmployeeVM, Employee>();
            CreateMap<Employee, EmployeeVM>();

            CreateMap<CountryVM, Country>();
            CreateMap<Country, CountryVM>();

            CreateMap<CityVM, City>();
            CreateMap<City, CityVM>();

            CreateMap<DistrictVM, District>();
            CreateMap<District, DistrictVM>();
        }

    }
}
