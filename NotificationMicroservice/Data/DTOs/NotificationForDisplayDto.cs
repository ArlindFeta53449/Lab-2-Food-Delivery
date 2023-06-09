using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class NotificationForDisplayDto
    {
        public string? Id { get; set; }
       
        public string Title { get; set; }

        public string Message { get; set; }

        public string Created { get; set; }

        public string? Link { get; set; }

       public string UserId { get; set; }
        public bool? IsRead { get; set; }
    }
}
