using Data.Entities.StripeEntities;
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

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int RoleId { get; set; }

        public bool IsEmailVerified { get; set; }

        public string AccountVerificationToken { get; set; }

        public Role Role { get; set; }

        public IList<Order> Orders { get; set; }

        public string? StripeCustomerId { get; set; }

        public bool AgentHasOrder { get; set; } = false;

    }
}
