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
    public class CityRep : ICityRep
    {

        private readonly DemoContext db;
        public CityRep(DemoContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<City>> GetAsync(Expression<Func<City, bool>> filter = null)
        {
            if (filter == null)
            {
                return await db.Cities.ToListAsync();
            }
            else
            {
                return await db.Cities.Where(filter).ToListAsync();
            }
        }
    }
}
