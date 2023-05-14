using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.Users
{
    public class UserCreateDto
    {

        public string Name { get; set; }

        public string Surname { get; set; }


        public string Email { get; set; }

        public string Password { get; set; }

        public int? RoleId { get; set; }

        public bool? IsEmailVerified { get; set; } = false;

        public string? AccountVerificationToken { get; set; }
    }
}
