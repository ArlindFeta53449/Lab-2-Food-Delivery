using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.Users
{
    public class UserLoginResponseDto
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public string Token { get; set; }

        public string? StripeCustomerId { get; set; }

        public bool AgentHasOrder { get; set; }
    }
}
