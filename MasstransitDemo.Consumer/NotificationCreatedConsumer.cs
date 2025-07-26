using System.Text.Json;
using Application.RwabbitMasstransit.Repos;
using MassTransit;

namespace MasstransitDemo.Consumer;

public class NotificationCreatedConsumer : IConsumer<INotificationCreated>
{
    public async Task Consume(ConsumeContext<INotificationCreated> context)
    {
        var message = context.Message;
        var serialized = JsonSerializer.Serialize(message);
        Console.WriteLine($"Received message: {serialized}");
    }
}