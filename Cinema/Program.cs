using Cinema.Helpers;
using Cinema.Services.Sessions;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddDbContext<DataContext>();
    builder.Services.AddControllers();
    builder.Services.AddScoped<ISessionService, SessionService>();
}

var app = builder.Build();
{
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}
