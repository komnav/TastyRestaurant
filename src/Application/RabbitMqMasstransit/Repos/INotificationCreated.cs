namespace Application.RabbitMqMasstransit.Repos;

public interface INotificationCreated
{
    DateTime NotificationDate { get; }
    string NotificationMessage { get; }
    NotificationType NotificationType { get; }
}