using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class UserForNotificationDto
    {
        public string Id { get; set; }
        public bool? IsRead { get; set; } = false;
    }
}
