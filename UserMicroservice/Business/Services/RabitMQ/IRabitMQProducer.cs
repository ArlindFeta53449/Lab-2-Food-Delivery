using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.RabitMQ
{
    public interface IRabitMQProducer
    {
        void SendMessage<T>(string queue, T message);
    }
}
