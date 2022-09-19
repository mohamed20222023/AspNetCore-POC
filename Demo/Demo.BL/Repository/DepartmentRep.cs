using Demo.BL.Models;
using Demo.DAL.Database;
using Demo.DAL.Entity;
using Demo.BL.Interface;
using Microsoft.EntityFrameworkCore;

namespace Demo.BL.Repository
{
    public class DepartmentRep :IDepartmentRep
    {
        private readonly DemoContext db;

        public DepartmentRep(DemoContext db)
        {
            this.db = db;
        }

       
        public async Task<IEnumerable<Department>> GetAsync()
        {
            var data = await db.Department.Select(e => e).ToListAsync();

            return data;
        }

        public async Task<Department> GetByIdAsync(int id)
        {
            var data = await db.Department.Where(e => e.Id == id).FirstOrDefaultAsync();

            return data;
        }

        public async Task CreateAsync(Department obj)
        {
 
            await db.Department.AddAsync(obj);
            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Department obj)
        {

            db.Entry(obj).State = EntityState.Modified; 
            await db.SaveChangesAsync();
        }


        public async Task RemoveAsync(int id)
        {
            var oldData = await db.Department.FindAsync(id);
            db.Department.Remove(oldData);
            await db.SaveChangesAsync();
        }

        
    }
}
