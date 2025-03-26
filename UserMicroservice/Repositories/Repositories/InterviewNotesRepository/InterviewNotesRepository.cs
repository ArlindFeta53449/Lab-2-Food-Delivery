using Data.Entities;
using Repositories.Repositories.ContractRepositories;
using Repositories.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.InterviewNotesRepository
{
    public class InterviewNotesRepository : Repository<InterviewNotes>, IInterviewNotesRepository
    {
        public InterviewNotesRepository(AppDbContext context) : base(context)
        {

        }
    }
}
