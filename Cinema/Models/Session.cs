using Cinema.Contracts.Session;
using Cinema.ServiceErrors;
using ErrorOr;

namespace Cinema.Models;


public class Session
{
    public const int MinRoomNumber = 1;
    public const int MaxRoomNumber = 7;
    public Guid Id { get; }
    public string MovieName { get; }
    public string Synopsis { get; }
    public string Language { get; }
    public int Room { get; }
    public DateTime StartDateTime { get; }
    public DateTime EndDateTime { get; }
    public DateTime LastModifiedDateTime { get; }

    private Session(
        Guid id,
        string movieName,
        string synopsis,
        string language,
        int room,
        DateTime startDateTime,
        DateTime endDateTime,
        DateTime lastModifiedDateTime)
    {
        Id = id;
        MovieName = movieName;
        Synopsis = synopsis;
        Language = language;
        Room = room;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        LastModifiedDateTime = lastModifiedDateTime;
    }

    public static ErrorOr<Session> Create(
        string movieName,
        string synopsis,
        string language,
        int room,
        DateTime startDateTime,
        DateTime endDateTime,
        Guid? id = null
        )
    {
        List<Error> errors = new();

        if (room < MinRoomNumber || room > MaxRoomNumber)
        {
            errors.Add(Errors.Session.InvalidRoom);
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return new Session(
            id ?? Guid.NewGuid(),
            movieName,
            synopsis,
            language,
            room,
            startDateTime,
            endDateTime,
            DateTime.UtcNow);
    }

    public static ErrorOr<Session> From(CreateSessionRequest request)
    {
        return Create(
            request.MovieName,
            request.Synopsis,
            request.Language,
            request.Room,
            request.StartDateTime,
            request.EndDateTime
        );
    }

    public static ErrorOr<Session> From(Guid id, UpsertSessionRequest request)
    {
        return Create(
            request.MovieName,
            request.Synopsis,
            request.Language,
            request.Room,
            request.StartDateTime,
            request.EndDateTime,
            id
        );
    }
}