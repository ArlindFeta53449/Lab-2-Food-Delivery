using Data.Entities;
using Repositories.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.InterviewRepository
{
    public interface IInterviewRepository : IRepository<Interview>
    {
        Task<List<Interview>> SearchByIdAsync(int id);
    }
}
