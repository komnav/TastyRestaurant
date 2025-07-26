using MassTransit;
using MasstransitDemo.Consumer;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((hostContext, services) =>
{
    services.AddMassTransit(x =>
    {
        x.AddConsumer<NotificationCreatedConsumer>();

        x.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host("localhost", "/", h =>
            {
                h.Username("guest");
                h.Password("guest");
            });

            cfg.ReceiveEndpoint("notification-created-queue", e =>
            {
                e.ConfigureConsumer<NotificationCreatedConsumer>(context);
            });
        });
    });
});

var app = builder.Build();

await app.RunAsync();