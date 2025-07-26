namespace Application.RabbitMqMasstransit;

public record NotificationDto
(
    DateTime NotificationDate,
    string NotificationMessage,
    NotificationType NotificationType
);