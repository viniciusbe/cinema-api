using Cinema.Contracts.Session;
using Cinema.ServiceErrors;
using ErrorOr;

namespace Cinema.Models;


public class Session
{
    public const int MinRoomNumber = 1;
    public const int MaxRoomNumber = 7;
    public Guid Id { get; set; }
    public string MovieName { get; set; }
    public string Synopsis { get; set; }
    public string Language { get; set; }
    public int Room { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public DateTime LastModifiedDateTime { get; set; }

    public Session(
        Guid id,
        string movieName,
        string synopsis,
        string language,
        int room,
        DateTime startDateTime,
        DateTime endDateTime,
        DateTime lastModifiedDateTime)
    {
        this.Id = id;
        this.MovieName = movieName;
        this.Synopsis = synopsis;
        this.Language = language;
        this.Room = room;
        this.StartDateTime = startDateTime;
        this.EndDateTime = endDateTime;
        this.LastModifiedDateTime = lastModifiedDateTime;
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