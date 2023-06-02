using Isopoh.Cryptography.Argon2;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace BuildingBlocks.Services;

public interface IPasswordService
{
    string HashPassword(string password);

    bool VerifyPassword(string encodedPassword, string password);
}
public sealed class PasswordService : IPasswordService
{
    private readonly IConfiguration _config;
    private string Salt { get; set; }
    public PasswordService(IConfiguration config)
    {
        _config = config;
        Salt = _config["PasswordSettings:Salt"] ?? throw new ArgumentNullException();
    }

    public string HashPassword(string password)
    {

        return Argon2.Hash(PassConfig(password));
    }

    public bool VerifyPassword(string encodedPassword, string password)
    {
        return Argon2.Verify(encodedPassword,PassConfig(password));
    }

    private Argon2Config PassConfig(string password)
    {
        return new Argon2Config
        {
            Salt = Encoding.UTF8.GetBytes(Salt),
            Password = Encoding.UTF8.GetBytes(password)
        };
    }
}
