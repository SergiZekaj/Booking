using BookingLogger.Infrastructure.DependencyInjection;
using BookingLogger.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddInfrastructureServices(
    builder.Configuration.GetConnectionString("DefaultConnection")!);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LoggerDbContext>();
    db.Database.Migrate();
}


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();