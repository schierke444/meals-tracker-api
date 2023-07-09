using Isopoh.Cryptography.Argon2;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace BuildingBlocks.Services;

public interface IPasswordService
{
    string HashPassword(string password, out string salt);

    bool VerifyPassword(string encodedPassword, string password, string salt);
}
public sealed class PasswordService : IPasswordService
{
    public PasswordService()
    {
    }

    public string HashPassword(string password, out string salt)
    {
        salt = RandomNumberGenerator.GetBytes(128 / 8).ToB64String();
        return Argon2.Hash(GetConfig(password, salt));
    }

    public bool VerifyPassword(string encodedPassword, string password, string salt)
    {
        return Argon2.Verify(encodedPassword, GetConfig(password, salt));
    }

    private Argon2Config GetConfig(string password, string salt)
    {
        return new Argon2Config
       {
            Type = Argon2Type.DataIndependentAddressing,
            Version = Argon2Version.Nineteen,
            TimeCost = 10,
            MemoryCost = 32768,
            Lanes = 5,
            Threads = Environment.ProcessorCount,
            Salt = Encoding.UTF8.GetBytes(salt),
            Password = Encoding.UTF8.GetBytes(password),
            HashLength = 20
       }; 
    }
}
