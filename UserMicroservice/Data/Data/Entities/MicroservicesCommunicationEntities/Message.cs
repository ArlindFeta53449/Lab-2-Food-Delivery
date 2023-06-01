using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.MicroservicesCommunicationEntities
{
    public abstract class Message
    {
        public string MessageId { get; set; } 
        public DateTime Timestamp { get; set; } 
        public string Sender { get; set; } 
        public string Receiver { get; set; } 
        public string RoutingKey { get; set; } 
        public string CorrelationId { get; set; }
    }
}
