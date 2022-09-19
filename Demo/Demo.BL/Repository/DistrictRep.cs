﻿using Demo.BL.Interface;
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
    public class DistrictRep : IDistrictRep
    {

        private readonly DemoContext db;

        public DistrictRep(DemoContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<District>> GetAsync(Expression<Func<District, bool>> filter = null)
        {
            if (filter == null)
            {
                return await db.Districts.ToListAsync();
            }
            else
            {
                return await db.Districts.Where(filter).ToListAsync();
            }
        }
    }
}
