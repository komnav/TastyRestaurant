namespace Application.RwabbitMasstransit;

public record NotificationDto
(
    DateTime NotificationDate,
    string NotificationMessage,
    NotificationType NotificationType
);