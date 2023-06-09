
using Data.DTOs.RabbitMQ;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Business.Services.XAsyncDataService
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _config;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBusClient(IConfiguration config)
        {
            _config = config;
            var factory = new ConnectionFactory()
            {
                HostName = _config["RabbitMQHost"],
                Port = int.Parse(_config["RabbitMQPort"])
            };
            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutDown;

                Console.WriteLine("--> Connected to MessageBus");
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void PublishNewUser(UserPublishedDto user)
        {
            var message = JsonSerializer.Serialize(user);
            if (_connection.IsOpen)
            {
                SendMessage(message);
            }
        }
        public void PublishOrderStatus(OrderPublishedDto order)
        {
            var message = JsonSerializer.Serialize(order);
            if (_connection.IsOpen)
            {
                SendMessage(message);
            }
        }
        public void PublishMessage<T>(T message)
        {
            var messageSerialized = JsonSerializer.Serialize(message);
            if (_connection.IsOpen)
            {
                SendMessage(messageSerialized);
            }
        }
        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(
                exchange: "trigger",
                routingKey: "",
                basicProperties:null,
                body:body);
        }
        public void Dispose()
        {
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }
        private void RabbitMQ_ConnectionShutDown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("--> RabbitMQ Connection Shutdown");
        }
    }
}
