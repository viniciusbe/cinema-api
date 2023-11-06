namespace Cinema.Contracts.Session;

public record UpsertSessionRequest(
    String MovieName,
    String Synopsis,
    String Language,
    int Room,
    DateTime StartDateTime,
    DateTime EndDateTime
);