using server.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IRepo, Repo>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var allowedOriginsConfig = builder.Configuration.GetValue<string>("allowedOrigins");

if (string.IsNullOrWhiteSpace(allowedOriginsConfig))
{
    throw new InvalidOperationException(
        "Missing required configuration value 'allowedOrigins'. Set it in appsettings.json or appsettings.Development.json"
    );
}

var allowedOrigins = allowedOriginsConfig
    .Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(allowedOrigins).AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
