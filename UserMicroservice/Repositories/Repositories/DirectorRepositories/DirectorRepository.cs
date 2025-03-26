using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Repositories.GenericRepository;
using Repositories.Repositories.ParentRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.DirectorRepositories
{
    public class DirectorRepository : Repository<Director>, IDirectorRepository
    {
        public AppDbContext _context;
        public DirectorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Director>> SearchByNameAsync(string name)
        {
            return await _context.Directors
                .Where(p => p.Name.Contains(name))
                .ToListAsync();
        }
    }
}


