using Serilog;
using TrainingApp.Infrastructure_Layer.Extensions;

var builder = WebApplication.CreateBuilder(args);

ConfigureLogging(builder);

builder.Services.AddApplicationServices();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureApiVersioning();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? "Data Source=identity.db";

builder.Services.AddIdentityAndJwtAuthentication("YourSecretKeyHere", connectionString);

var app = builder.Build();

ConfigureMiddleware(app);

app.Run();


// Serilog setup
void ConfigureLogging(WebApplicationBuilder builder)
{
    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .CreateLogger();
    builder.Host.UseSerilog();
}

// Middleware setup
void ConfigureMiddleware(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
}