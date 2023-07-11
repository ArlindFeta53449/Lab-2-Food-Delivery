using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.RabbitMQ
{
    public class UserPublishedDto
    {
        public string ExternalId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Event { get; set; }
    }
}
