using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Repositories.Repositories.GenericRepository;


namespace Repositories.Repositories.ChildRepositories
{
    public class ChildRepository : Repository<Child>, IChildRepository
    {
        public ChildRepository(AppDbContext context) : base(context)
        {

        }
    }
}
