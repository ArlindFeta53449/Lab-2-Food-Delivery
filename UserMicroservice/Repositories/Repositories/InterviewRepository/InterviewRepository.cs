using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Repositories.EmployeeRepositories;
using Repositories.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.InterviewRepository
{
    public class InterviewRepository : Repository<Interview>, IInterviewRepository
    {
        public AppDbContext _context;
        public InterviewRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Interview>> SearchByIdAsync(int id)
        {
            return await _context.Interviews
                .Where(p => p.Id==id)
                .ToListAsync();
        }
    }
}
