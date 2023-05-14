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
        private int CreateDefaultCustomerRole()
        {
            var role = new Role() { 
                Name = "Customer",
            };
            Context.Set<Role>().Add(role);
            Context.SaveChanges();
            return role.Id;
        }
        public int FindDefaultCustomerRole()
        {
            var role = Context.Set<Role>().Where(x => x.Name == "Customer").FirstOrDefault();
            if (role == null)
            {
                return this.CreateDefaultCustomerRole();
            }
            else
            {
                return role.Id;
            }
        }
    }
}
