using Data.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class User: IdentityUser
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }

        public Gender Gender { get;set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public int RoleId { get; set; }

        public bool IsEmailVerified { get; set; }

        public Role Role { get; set; }
        
    }
}
