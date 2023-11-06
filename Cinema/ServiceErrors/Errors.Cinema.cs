using ErrorOr;

namespace Cinema.ServiceErrors;

public static class Errors
{
    public static class Session
    {
        public static Error NotFound => Error.NotFound(
            code: "Session.NotFound",
            description: "Session not found");

        public static Error InvalidRoom => Error.Validation(
        code: "Session.NotFound",
        description: $"Session room must be between {Models.Session.MinRoomNumber} and {Models.Session.MaxRoomNumber}");
    }
}