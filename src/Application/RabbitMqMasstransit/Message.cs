using System.Text;
using RabbitMQ.Client;

namespace Application.RabbitMqMasstransit;

public class Message
{
    public static async Task SendMessage()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();


        await channel.QueueDeclareAsync(
            queue: "message",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        for (int i = 0; i < 10; i++)
        {
            var message = $"{DateTime.UtcNow} - {Guid.NewGuid()}";
            var body = Encoding.UTF8.GetBytes(message);

            await channel.BasicPublishAsync(
                exchange: string.Empty,
                routingKey: "message",
                mandatory: true,
                basicProperties: new BasicProperties() { Persistent = true },
                body: body);

            Console.WriteLine($"Sent message: {message}");
            await Task.Delay(2000);
        }
    }
}