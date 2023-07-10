using System.Data;
using BuildingBlocks.Commons.Interfaces;
using BuildingBlocks.Dapper;
using BuildingBlocks.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Seeder;

public class Program
{
    static void Main(string[] args)
    {
        var configuration =  new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.Development.json", false, true);
            
        var config = configuration.Build(); 

        var service = new ServiceCollection()
            .AddTransient<IPasswordService, PasswordService>()
            .AddTransient<SeedUsers>()
            .AddTransient<SeedMeals>()
            .BuildServiceProvider();

            
        
        foreach (var item in args)
        {
            string command = args[0];
            if(command == "seed_users")
            {
                var seed = service.GetRequiredService<SeedUsers>(); 
                seed.InitializeUserData(config); 
            }
            else if(command == "seed_meals")
            {
                var seed = service.GetRequiredService<SeedMeals>();
                seed.InitializeMealData(config);
            }  
            else 
            {
                Console.WriteLine("Command not Found");
            }
        }  
    }   
}
