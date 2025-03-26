using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Repositories.GenericRepository;
using Repositories.Repositories.ParentRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.PlanetRepository
{
    public class PlanetRepository : Repository<Planet>, IPlanetRepository
    {
        public AppDbContext _context;
        public PlanetRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Planet>> SearchByNameAsync(string name)
        {
            return await _context.Planets
                .Where(p => p.Name.Contains(name))
                .ToListAsync();
        }
    }
}
