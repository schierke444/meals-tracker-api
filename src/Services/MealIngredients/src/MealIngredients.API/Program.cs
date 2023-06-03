using System.Reflection;
using BuildingBlocks.Commons.Models;
using BuildingBlocks.Jwt;
using MassTransit;
using MealIngredients.API.Consumers;
using MealIngredients.API.Persistence;
using MealIngredients.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddScoped<IMealIngredientsRepository, MealIngredientsRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddJwtExtensions(builder.Configuration);
builder.Services.AddMassTransit((cfg) =>
{
    cfg.AddConsumer<CreateMealAndIngredientConsumer>();
    cfg.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);    
        
        cfg.ReceiveEndpoint(EventBusConstants.SubmitMealsIngredientQueue, (cfg) =>
        {
            cfg.ConfigureConsumer<CreateMealAndIngredientConsumer>(ctx);
        });
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();