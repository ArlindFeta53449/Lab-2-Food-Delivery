using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Repositories.GenericRepository;

namespace Repositories.Repositories.ParentRepositories
{
    public class ParentRepository : Repository<Parent>, IParentRepository
    {
        public AppDbContext _context;
        public ParentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Parent>> SearchByNameAsync(string name)
        {
            return await _context.Parents
                .Where(p => p.Name.Contains(name))
                .ToListAsync();
        }
    }
}
