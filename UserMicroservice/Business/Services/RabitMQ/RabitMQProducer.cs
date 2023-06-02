using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.RabitMQ
{
    public class RabitMQProducer:IRabitMQProducer
    {
        public void SendMessage<T>(string queue,T message)
        {
            //Here we specify the Rabbit MQ Server. we use rabbitmq docker image and use it
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672
            };
            //Create the RabbitMQ connection using connection factory details as i mentioned above
            var connection = factory.CreateConnection();
            //Here we create channel with session and model
            using
            var channel = connection.CreateModel();
            //declare the queue after mentioning name and a few property related to that
            channel.QueueDeclare(queue, exclusive: false);
            //Serialize the message
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            //put the data on to the product queue
            channel.BasicPublish(exchange: "", routingKey: queue, body: body);
        }
        /*public T ConsumeMessage<T>(string queue)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue, exclusive: false);

                var consumer = new EventingBasicConsumer(channel);
                T receivedMessage = default(T); // Default value if no message is received
                consumer.Received += (sender, args) =>
                {
                    var body = args.Body.ToArray();
                    var json = Encoding.UTF8.GetString(body);
                    receivedMessage = JsonConvert.DeserializeObject<T>(json);

                    channel.BasicAck(args.DeliveryTag, multiple: false);
                };

                channel.BasicConsume(queue, autoAck: false, consumer);

                // Keep the consumer running until it receives a message or is explicitly stopped
                while (receivedMessage == default(T)) { }

                channel.BasicCancel(consumer.ConsumerTags[0]);

                return receivedMessage;
            }*/
        }
}
