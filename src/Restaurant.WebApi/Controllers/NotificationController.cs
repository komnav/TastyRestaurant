using Application.RabbitMqMasstransit;
using Application.RabbitMqMasstransit.Repos;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Restaurant.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class NotificationController(IPublishEndpoint publishEndpoint) : ControllerBase
{
    public readonly IPublishEndpoint PublishEndpoint = publishEndpoint;

    [HttpPost]
    public async Task<IActionResult> Notify(NotificationDto notificationDto)
    {
        await PublishEndpoint.Publish<INotificationCreated>(new
        {
            NotificationDate = notificationDto.NotificationDate,
            NotificationMessage = notificationDto.NotificationMessage,
            NotificationType = notificationDto.NotificationType
        });
        
        return Ok();
    }
}