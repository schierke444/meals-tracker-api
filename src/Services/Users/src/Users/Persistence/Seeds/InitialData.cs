// using BuildingBlocks.Services;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// using Users.API.Entities;
// using Users.API.Persistence;

// namespace Auth.Persistence.Seeds;

// public static class InitialData
// {   
//     public static WebApplication SeedData(this WebApplication app, IConfiguration config, IPasswordService passwordService)
//     {
//         using(var scope = app.Services.CreateScope())
//         {
//             using (var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
//             {
//                 try
//                 {
//                     context.Database.EnsureCreated();

//                     var data = context.Users.FirstOrDefault();
//                     if(data == null)
//                     {
//                         User user = new()
//                         {
//                             Id = Guid.NewGuid(),
//                             Username = config["Seed:Admin_Username"] ?? string.Empty,
//                             Password = passwordService.HashPassword(config["Seed:Admin_Password"] ?? string.Empty),
//                             Email = config["Seed:Admin_Email"] ?? string.Empty,
//                             CreatedAt = DateTime.UtcNow,
//                             UpdatedAt = DateTime.UtcNow
//                         };

//                         context.Add(user);
//                         context.SaveChanges();
//                     }
//                 }
//                 catch(Exception)
//                 {
//                     throw;
//                 }
//             }
//         }
//         return app;
//     }
// }