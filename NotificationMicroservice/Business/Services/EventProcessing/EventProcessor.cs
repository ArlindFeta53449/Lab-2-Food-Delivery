using AutoMapper;
using Business.Services.NotificationHub;
using Data.DTOs;
using Data.Entities;
using Data.Enums;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using Repository.NotificationsRepository;
using Repository.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Business.Services.EventProcessing
{
    public class EventProcessor:IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;


        public EventProcessor(IServiceScopeFactory scopeFactory,IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermainEvent(message);
            switch(eventType)
            {
                case EventType.UserCreated:
                    addUser(message);
                    break;
                case EventType.OrderStatusChanged:
                    addOrderStatusNotification(message);
                    break;
                case EventType.OrderCreated:
                    addOrderCreatedNotification(message);
                    break;
                case EventType.OrderAccepted:
                    addOrderAcceptedNotification(message);
                    break;
                default:
                    break;
            }
        }

        private EventType DetermainEvent(string notificationMessage)
        {
            var eventType = JsonSerializer.Deserialize<GenericEvent>(notificationMessage);
            switch(eventType.Event) {
                case "User_Created":
                    return EventType.UserCreated;
                case "OrderStatus_Changed":
                    return EventType.OrderStatusChanged;
                case "Order_Created":
                    return EventType.OrderCreated;
                case "Order_Accepted":
                    return EventType.OrderAccepted;
                default:
                    return EventType.Undetermined;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------
        // Methods to create the notifications
        private void addUser(string userCreatedMessage)
        {
            using(var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IUserRepository>();

                var user = JsonSerializer.Deserialize<UserPublishedDto>(userCreatedMessage);
                try
                {
                    var userMapped = _mapper.Map<User>(user);
                    if (!repo.UserExists(userMapped.ExternalId))
                    {
                        repo.CreateUser(userMapped);
                    }
                }
                catch(Exception ex) 
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        private async void addOrderStatusNotification(string orderStatusMessage)
        {
            using(var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<INotificationRepository>();
                var notificationHub = scope.ServiceProvider.GetRequiredService<INotificationHub>();
                var order = JsonSerializer.Deserialize<OrderPublishedDto>(orderStatusMessage);
                try
                {
                    var notification = new Notification()
                    {
                        Title = "Your order's status has changed",
                        Message = $"Your order with Id:{order.Id} has new status of: {order.OrderStatus}",
                        Link = $"/menageOrder/{order.Id}",
                        Users = new List<UserForNotificationDto> {
                            new UserForNotificationDto{Id = order.UserId}
                        },
                        Created = DateTime.Now.ToString("HH:mm dd-MM-yyyy")
                    };
                    var notificationCreated= repo.Create(notification);
                    if(notificationCreated != null)
                    {
                        await notificationHub.SendNotification(notificationCreated);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        private async void addOrderAcceptedNotification(string orderMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<INotificationRepository>();
                var notificationHub = scope.ServiceProvider.GetRequiredService<INotificationHub>();
                var order = JsonSerializer.Deserialize<OrderPublishedDto>(orderMessage);
                try
                {
                    var notification = new Notification()
                    {
                        Title = "Your order was accepted by an agent",
                        Message = $"Your order with Id:{order.Id} has been accepted. You will recieve further updates about your order.",
                        Users = new List<UserForNotificationDto> {
                            new UserForNotificationDto{Id = order.UserId}
                        },
                        Created = DateTime.Now.ToString("HH:mm dd-MM-yyyy")
                    };
                    var notificationCreated = repo.Create(notification);
                    if (notificationCreated != null)
                    {
                        await notificationHub.SendNotification(notificationCreated);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        private async void addOrderCreatedNotification(string message)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<INotificationRepository>();
                var notificationHub = scope.ServiceProvider.GetRequiredService<INotificationHub>();
                var order = JsonSerializer.Deserialize<OrderCreatedDto>(message);
                try
                {
                    var users = order.Agents.Select(agent => new UserForNotificationDto { Id = agent }).ToList();
                    var notification = new Notification()
                    {
                        Title = "New order was made!",
                        Message = $"A new order was made with Id: {order.Id} to be delivered at this address: {order.DeliveryAddress}" +
                        $" with total: {order.Total/100}",
                        Link = $"/dashboard/listOrders",
                        Users = users,
                        Created = DateTime.Now.ToString("HH:mm dd-MM-yyyy")
                    };
                    var notificationCreated = repo.Create(notification);
                    if (notificationCreated != null)
                    {
                        await notificationHub.SendNotification(notificationCreated);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }

}
