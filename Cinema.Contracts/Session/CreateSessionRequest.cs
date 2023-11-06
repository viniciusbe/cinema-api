namespace Cinema.Contracts.Session;

public record CreateSessionRequest(
    String MovieName,
    String Synopsis,
    String Language,
    int Room,
    DateTime StartDateTime,
    DateTime EndDateTime
);