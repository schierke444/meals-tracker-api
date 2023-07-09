using System.Data;
using BuildingBlocks.Services;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

public class SeedUsers
{    
    private readonly IPasswordService _passwordService;
    public SeedUsers(IPasswordService passwordService)
    {
        _passwordService = passwordService;
    }

    public void InitializeUserData(IConfiguration config)
    {
        // Role Admin and Member Ids
        var adminId = Guid.NewGuid();
        var memberId = Guid.NewGuid();

        // Admin User and Member User Ids
        var adminUserId = Guid.NewGuid();
        var memberUserId = Guid.NewGuid();

        using(var con = new NpgsqlConnection(config.GetConnectionString("DB")))
        {
            // Insert roles
            con.QueryMultiple($@"
                INSERT INTO roles (id, name, created_at, updated_at) VALUES ('{adminId}', 'Admin', '{DateTime.UtcNow}', '{DateTime.UtcNow}');
                INSERT INTO roles (id, name, created_at, updated_at) VALUES ('{memberId}', 'Member', '{DateTime.UtcNow}', '{DateTime.UtcNow}');
            ");
        };


        using(var con = new NpgsqlConnection(config.GetConnectionString("DB")))
        {
            var adminPassword = _passwordService.HashPassword(config["Seed:Admin_Password"] ?? throw new ArgumentNullException(), out string salt);
            var memberPassword = _passwordService.HashPassword(config["Seed:Member_Password"] ?? throw new ArgumentNullException(), out string salt1);

            con.QueryMultiple($@"
                INSERT INTO users (id, username, password, salt, role_id, created_at, updated_at) VALUES ('{adminUserId}', '{config["Seed:Admin_Username"]}', '{adminPassword}', '{salt}', '{adminId}', '{DateTime.UtcNow}', '{DateTime.UtcNow}');
                INSERT INTO users (id, username, password, salt, role_id, created_at, updated_at) VALUES ('{memberUserId}', '{config["Seed:Member_Username"]}', '{memberPassword}', '{salt1}', '{memberId}', '{DateTime.UtcNow}', '{DateTime.UtcNow}');
            ");
        }

        using(var con = new NpgsqlConnection(config.GetConnectionString("DB")))
        {
            // Insert User Info
            con.QueryMultiple($@"
                INSERT INTO users_info (id, first_name, last_name, email, bio, user_id, created_at, updated_at) VALUES ('{Guid.NewGuid()}', 'admin', 'admin', '{config["Seed:Admin_Email"]}', 'sample', '{adminUserId}', '{DateTime.UtcNow}', '{DateTime.UtcNow}');
                INSERT INTO users_info (id, first_name, last_name, email, bio, user_id, created_at, updated_at) VALUES ('{Guid.NewGuid()}', 'member', 'member', '{config["Seed:Member_Email"]}', 'sample', '{memberUserId}', '{DateTime.UtcNow}', '{DateTime.UtcNow}');
            ");
        }
    }
}