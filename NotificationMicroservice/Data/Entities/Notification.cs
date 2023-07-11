using Data.DTOs;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    [BsonIgnoreExtraElements]
    public class Notification
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("message")]
        public string Message { get; set; }

        [BsonElement("created")]
        public string Created { get; set; }

        [BsonElement("link")]
        public string? Link { get; set; }

        [BsonElement("users")]
        public List<UserForNotificationDto> Users { get; set; }

    }
}
