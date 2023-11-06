using Cinema.Models;
using Cinema.ServiceErrors;
using ErrorOr;

namespace Cinema.Services.Sessions;

public class SessionService : ISessionService
{
    private static readonly Dictionary<Guid, Session> _sessions = new();
    public ErrorOr<Created> CreateSession(Session session)
    {
        _sessions.Add(session.Id, session);

        return Result.Created;
    }

    public ErrorOr<Deleted> DeleteSession(Guid id)
    {
        _sessions.Remove(id);

        return Result.Deleted;
    }

    public ErrorOr<Session> GetSession(Guid id)
    {
        if (_sessions.TryGetValue(id, out var session))
        {
            return session;
        }
        return Errors.Session.NotFound;
    }

    public ErrorOr<UpsertedSession> UpsertSession(Session session)
    {
        var IsNewlyCreated = !_sessions.ContainsKey(session.Id);
        _sessions[session.Id] = session;

        return new UpsertedSession(IsNewlyCreated);
    }
}