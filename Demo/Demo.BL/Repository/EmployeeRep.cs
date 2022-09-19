using Demo.BL.Interface;
using Demo.DAL.Database;
using Demo.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.Repository
{
    public class EmployeeRep:IEmployeeRep
    {
        private readonly DemoContext db;

        public EmployeeRep(DemoContext db)
        {
            this.db = db;
        }


        public async Task<IEnumerable<Employee>> GetAsync(Expression<Func<Employee, bool>> filter = null)
        {
            if (filter == null)
            {
                return await db.Employees.Select(e => e).Include("Department").Include("District").ToListAsync();
            }
            else
            {
                return await db.Employees.Select(e => e).Include("Department").Include("District").Where(filter).ToListAsync();
            }
        }

        public async Task<Employee> GetByIdAsync(Expression<Func<Employee, bool>> filter)
        {
            var data = await db.Employees.Where(filter).Select(e => e).Include("Department").FirstOrDefaultAsync();
            return data;
        }


        public async Task<Employee> CreateAsync(Employee obj)
        {
            await db.Employees.AddAsync(obj);
            await db.SaveChangesAsync();
            return db.Employees.OrderByDescending(a => a.Id).FirstOrDefault();
        }



        public async Task UpdateAsync(Employee obj)
        {

            db.Entry(obj).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }


        public async Task RemoveAsync(int id)
        {
            var oldData = await db.Employees.FindAsync(id);
            db.Employees.Remove(oldData);
            await db.SaveChangesAsync();
        }
    }
}
