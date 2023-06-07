using Auth.Extensions;
using Auth.Persistence.Seeds;
using BuildingBlocks.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Host.UseSerilog((ctx, cfg) => cfg.ReadFrom.Configuration(ctx.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Local")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Manually Seed Data thru CLI using "seed:user_data"
if (args.Length > 0)
{
    var seed = args.Any(x => x == "seed:user_data");
    if (seed)
        app.SeedData(builder.Configuration, new PasswordService(builder.Configuration));
}


app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
