using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Repositories.GenericRepository;
using Repositories.Repositories.CostumerRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.CostumerRepository
{
    public class CostumerRepository : Repository<Costumer>, ICostumerRepository
    {
        public AppDbContext _context;
        public CostumerRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Costumer>> SearchByNameAsync(string name)
        {
            return await _context.Costumers
                .Where(p => p.Name.Contains(name))
                .ToListAsync();
        }
    }
}
