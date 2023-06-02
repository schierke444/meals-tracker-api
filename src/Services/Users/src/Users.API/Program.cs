using BuildingBlocks.Events;
using BuildingBlocks.Jwt;
using BuildingBlocks.Services;
using BuildingBlocks.Web;
using MassTransit;
using Serilog;
using Users.API.Persistence;
using Users.API.Repositories;
using Users.API.RequestConsumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddJwtExtensions(builder.Configuration);
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

builder.Services.AddMassTransit(cfg =>
{
    cfg.SetKebabCaseEndpointNameFormatter();
    cfg.AddConsumer<GetUserByIdConsumer>();

    cfg.AddRequestClient<GetUserByIdRecord>(new Uri("exchange:getuser-queue"));

    // Specifying the Event to be consume
    cfg.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        cfg.ReceiveEndpoint("getuser-queue", (c) =>
        {
            c.ConfigureConsumer<GetUserByIdConsumer>(ctx);
        });
    });
});

builder.Host.UseSerilog((ctx, cfg) => cfg.ReadFrom.Configuration(ctx.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
