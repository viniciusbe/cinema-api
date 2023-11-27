using Cinema.Data;
using Cinema.Helpers;
using Cinema.Services.Sessions;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddDbContext<DataContext>();
    builder.Services.AddControllers();
    builder.Services.AddScoped<ISessionService, SessionService>();
    builder.Services.AddScoped<IRepository, Repository>();
}

var app = builder.Build();
{
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
    await app.RunAsync();
}
