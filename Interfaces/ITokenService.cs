using BankingSystem.Entities;

namespace BankingSystem.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
