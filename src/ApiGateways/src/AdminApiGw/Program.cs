using BuildingBlocks.Jwt;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("YARP"));

builder.Services.AddJwtExtensions(builder.Configuration);

builder.Services.AddAuthorization(opt => {
    opt.AddPolicy("adminPolicy", policy => {
        policy.RequireAuthenticatedUser()
            .RequireRole("Admin");
    });
});


builder.Host.UseSerilog((ctx, cfg) => cfg.ReadFrom.Configuration(ctx.Configuration));

var app = builder.Build();

app.UseSerilogRequestLogging();

app.MapReverseProxy();

app.Run();