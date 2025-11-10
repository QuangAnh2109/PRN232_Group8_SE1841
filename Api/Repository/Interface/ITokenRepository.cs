using Api.Models;

namespace Api.Repository.Interface
{
    public interface ITokenRepository
    {
        Task<Token> GetTokenByIdAsync(Guid id);
        Task AddTokenAsync(Token token);
    }
}
