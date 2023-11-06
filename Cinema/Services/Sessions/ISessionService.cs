using Cinema.Models;
using ErrorOr;

namespace Cinema.Services.Sessions;

public interface ISessionService
{
    ErrorOr<Created> CreateSession(Session session);
    ErrorOr<Session> GetSession(Guid idn);
    ErrorOr<UpsertedSession> UpsertSession(Session session);
    ErrorOr<Deleted> DeleteSession(Guid id);
}