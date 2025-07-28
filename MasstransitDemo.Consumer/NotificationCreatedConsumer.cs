using System.Text.Json;
using Application.RabbitMqMasstransit.Repos;
using MassTransit;

namespace MasstransitDemo.Consumer;

public class NotificationCreatedConsumer : IConsumer<INotificationCreated>
{
    public async Task Consume(ConsumeContext<INotificationCreated> context)
    {
        await ReceivedMessage.Received();
        var message = context.Message;
        var serialized = JsonSerializer.Serialize(message);
        Console.WriteLine($"Received message: {serialized}");
    }
}