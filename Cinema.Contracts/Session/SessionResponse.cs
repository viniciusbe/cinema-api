namespace Cinema.Contracts.Session;

public record SessionResponse(
    Guid Id,
    String MovieName,
    String Synopsis,
    String Language,
    int Room,
    DateTime StartDateTime,
    DateTime EndDateTime,
    DateTime LastModifiedDateTime
);