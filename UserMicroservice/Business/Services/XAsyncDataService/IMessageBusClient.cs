using Data.DTOs.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.XAsyncDataService
{
    public interface IMessageBusClient
    {
        void PublishNewUser(UserPublishedDto user);
    }
}
