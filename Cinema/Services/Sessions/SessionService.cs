using Cinema.Data;
using Cinema.Models;
using Cinema.ServiceErrors;
using ErrorOr;

namespace Cinema.Services.Sessions;

public class SessionService : ISessionService
{
    private readonly IRepository _repo;

    public SessionService(IRepository repo)
    {
        _repo = repo;
    }

    public ErrorOr<Created> CreateSession(Session session)
    {
        _repo.Add(session);
        if (_repo.SaveChanges())
        {
            return Result.Created;
        }

        return Errors.Session.NotCreated;

    }

    public ErrorOr<Deleted> DeleteSession(Guid id)
    {
        _repo.Delete(id);
        if (_repo.SaveChanges())
        {
            return Result.Deleted;
        }
        return Errors.Session.NotCreated;
    }

    public ErrorOr<Session> GetSession(Guid id)
    {

        Session? session = _repo.GetSessionById(id);
        if (session == null)
        {
            return Errors.Session.NotFound;
        }

        return session;
    }

    public ErrorOr<Session[]> GetAllSessions()
    {
        Session[] sessions = _repo.GetAllSessions();

        return sessions;


    }

    public ErrorOr<UpsertedSession> UpsertSession(Session session)
    {
        _repo.Update(session);

        if (_repo.SaveChanges())
        {
            return new UpsertedSession(false);
        }
        return Errors.Session.NotCreated;


        // return new UpsertedSession(IsNewlyCreated);
    }
}