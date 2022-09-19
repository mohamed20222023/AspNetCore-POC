using Demo.DAL.Entity;
using Demo.DAL.Extend;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Database
{
    public class DemoContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Department> Department { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DemoContext(DbContextOptions<DemoContext> opt):base(opt)
        {

        }
    }
}
