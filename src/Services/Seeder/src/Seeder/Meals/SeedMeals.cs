using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

public class SeedMeals 
{    
    public SeedMeals()
    {
    }

    public void InitializeMealData(IConfiguration config)
    {
        using(var con = new NpgsqlConnection(config.GetConnectionString("DB")))
        {
            con.QueryMultiple($@"
                INSERT INTO category (id, name, created_at, updated_at) VALUES ('{Guid.NewGuid()}', 'Breakfast', '{DateTime.UtcNow}', '{DateTime.UtcNow}');
                INSERT INTO category (id, name, created_at, updated_at) VALUES ('{Guid.NewGuid()}', 'Dinner', '{DateTime.UtcNow}', '{DateTime.UtcNow}');
                INSERT INTO category (id, name, created_at, updated_at) VALUES ('{Guid.NewGuid()}', 'Appetizer', '{DateTime.UtcNow}', '{DateTime.UtcNow}') ;
                INSERT INTO category (id, name, created_at, updated_at) VALUES ('{Guid.NewGuid()}', 'Desserts', '{DateTime.UtcNow}', '{DateTime.UtcNow}') ;
            ");
        }


        using(var con = new NpgsqlConnection(config.GetConnectionString("DB")))
        {
            con.QueryMultiple($@"
                INSERT INTO ingredients (id, name, created_at, updated_at) VALUES ('{Guid.NewGuid()}', 'salt', '{DateTime.UtcNow}', '{DateTime.UtcNow}');
                INSERT INTO ingredients (id, name, created_at, updated_at) VALUES ('{Guid.NewGuid()}', 'soy sauce', '{DateTime.UtcNow}', '{DateTime.UtcNow}');
                INSERT INTO ingredients (id, name, created_at, updated_at) VALUES ('{Guid.NewGuid()}', 'pepper', '{DateTime.UtcNow}', '{DateTime.UtcNow}') ;
                INSERT INTO ingredients (id, name, created_at, updated_at) VALUES ('{Guid.NewGuid()}', 'butter', '{DateTime.UtcNow}', '{DateTime.UtcNow}') ;
                INSERT INTO ingredients (id, name, created_at, updated_at) VALUES ('{Guid.NewGuid()}', 'egg', '{DateTime.UtcNow}', '{DateTime.UtcNow}') ;
                INSERT INTO ingredients (id, name, created_at, updated_at) VALUES ('{Guid.NewGuid()}', 'rice', '{DateTime.UtcNow}', '{DateTime.UtcNow}') ;
                INSERT INTO ingredients (id, name, created_at, updated_at) VALUES ('{Guid.NewGuid()}', 'sugar', '{DateTime.UtcNow}', '{DateTime.UtcNow}') ;
                INSERT INTO ingredients (id, name, created_at, updated_at) VALUES ('{Guid.NewGuid()}', 'water', '{DateTime.UtcNow}', '{DateTime.UtcNow}') ;
            ");
        }
    }
}