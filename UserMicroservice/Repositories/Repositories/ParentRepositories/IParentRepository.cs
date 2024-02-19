using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Repositories.Repositories.GenericRepository;

namespace Repositories.Repositories.ParentRepositories
{
    public interface IParentRepository : IRepository<Parent>
    {
        Task<List<Parent>> SearchByNameAsync(string name);
    }
}
