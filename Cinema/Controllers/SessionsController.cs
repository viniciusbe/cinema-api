using Cinema.Contracts.Session;
using Cinema.Models;
using Cinema.ServiceErrors;
using Cinema.Services.Sessions;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers;


public class SessionsController : ApiController
{
    private readonly ISessionService _sessionService;

    public SessionsController(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }

    [HttpPost]
    public IActionResult CreateSession(CreateSessionRequest request)
    {
        ErrorOr<Session> requestToSessionResult = Session.From(request);

        if (requestToSessionResult.IsError)
        {
            return Problem(requestToSessionResult.Errors);
        }

        var session = requestToSessionResult.Value;
        ErrorOr<Created> createSessionResult = _sessionService.CreateSession(session);

        return createSessionResult.Match(
            created => CreatedAsGetSession(session),
            errors => Problem(errors)
        );
    }

    [HttpGet()]
    public IActionResult GetAllSessions()
    {
        ErrorOr<Session[]> getSessionResult = _sessionService.GetAllSessions();

        return getSessionResult.Match(
            sessions => Ok(sessions.Select(session => MapSessionResponse(session))),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetSession(Guid id)
    {
        ErrorOr<Session> getSessionResult = _sessionService.GetSession(id);

        return getSessionResult.Match(
            session => Ok(MapSessionResponse(session)),
            errors => Problem(errors)
        );
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpsertSession(Guid id, UpsertSessionRequest request)
    {
        ErrorOr<Session> requestToSessionResult = Session.From(id, request);

        if (requestToSessionResult.IsError)
        {
            return Problem(requestToSessionResult.Errors);
        }

        var session = requestToSessionResult.Value;
        ErrorOr<UpsertedSession> upsertSessionResult = _sessionService.UpsertSession(session);

        return upsertSessionResult.Match(
            upserted => upserted.IsNewlyCreated ? CreatedAsGetSession(session) : NoContent(),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteSession(Guid id)
    {
        ErrorOr<Deleted> deleteSessionResult = _sessionService.DeleteSession(id);

        return deleteSessionResult.Match(
            deleted => NoContent(),
            errors => Problem(errors)
        );
    }


    private IActionResult CreatedAsGetSession(Session session)
    {
        return CreatedAtAction(
            actionName: nameof(GetSession),
            routeValues: new { id = session.Id },
            value: MapSessionResponse(session)
        );
    }

    private static SessionResponse MapSessionResponse(Session session)
    {
        return new SessionResponse(
            session.Id,
            session.MovieName,
            session.Synopsis,
            session.Language,
            session.Room,
            session.StartDateTime,
            session.EndDateTime,
            session.LastModifiedDateTime
        );
    }
}