using Data.Entities;
using Repositories.Repositories.GenericRepository;
using Repositories.Repositories.LoanRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.LoanRepository
{
    public class LoanRepository : Repository<Loan>, ILoanRepository
    {
        public LoanRepository(AppDbContext context) : base(context)
        {

        }
    }
}
