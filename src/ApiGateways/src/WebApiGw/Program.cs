using Ocelot.DependencyInjection;
using Ocelot.Cache.CacheManager;
using Ocelot.Middleware;
using Serilog;
using BuildingBlocks.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(cfg => {
    cfg.AddPolicy("CorsPolicy", policy => {
        policy.WithOrigins(builder.Configuration["ClientURL"]!)
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddJwtExtensions(builder.Configuration);

builder.Services.AddOcelot().AddCacheManager(opt =>
{
    opt.WithDictionaryHandle();
});

builder.Configuration
    .AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", true, true);


builder.Host.UseSerilog((ctx, cfg) => cfg.ReadFrom.Configuration(ctx.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseSerilogRequestLogging();

app.UseAuthentication();
app.UseAuthorization();

await app.UseOcelot();

app.Run();

