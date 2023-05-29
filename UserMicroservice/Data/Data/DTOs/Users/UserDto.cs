using Data.Entities;
using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.Users
{
    public class UserDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }


        public string Email { get; set; }

        public string Role { get; set; }

        public bool IsEmailVerified { get; set; }

        public string AccountVerificationToken { get; set; }

        public string? StripeCustomerId { get; set; }
        public bool AgentHasOrder { get; set; }
    }
}
