using System.Runtime.InteropServices.JavaScript;
using Api.DTO;

namespace Api.Services.Interface
{
    public interface IAccountService
    {
        Task<bool> RegisterAccountAsync(RegisterAccountDTO register);

        Task<TokenDTO> GetJwtTokenAsync(LoginDTO login);

        Task<string> GetAccessTokenAsync(int userId, DateTime lastModifiedTime);
    }
}
