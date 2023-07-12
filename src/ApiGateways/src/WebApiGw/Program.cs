using BuildingBlocks.Jwt;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("YARP"));

builder.Services.AddJwtExtensions(builder.Configuration);

builder.Services.AddAuthorization(opt => {
    opt.AddPolicy("memberPolicy", policy => {
        policy.RequireAuthenticatedUser()
            .RequireRole("Member");
    });
});

builder.Host.UseSerilog((ctx, cfg) => cfg.ReadFrom.Configuration(ctx.Configuration));

var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseAuthentication();
app.UseAuthorization();

app.MapReverseProxy();

app.Run();
