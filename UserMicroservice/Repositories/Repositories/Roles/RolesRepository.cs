using Data.Entities;
using Repositories.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.Roles
{
    public class RolesRepository:Repository<Role>, IRolesRepository
    {
        public RolesRepository(AppDbContext context):base(context)
        {
            
        }

        
    }
}
