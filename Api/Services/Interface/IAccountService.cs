using System.Runtime.InteropServices.JavaScript;
using Api.DTO;

namespace Api.Services.Interface
{
    public interface IAccountService
    {
        Task RegisterAccountAsync(RegisterAccountDTO register);
        
        Task<TokenDTO> GetJwtTokenAsync(LoginDTO login);

        Task<string> GetAccessTokenAsync(string tokenId, int userId, DateTime lastModifiedTime);
    }
}
