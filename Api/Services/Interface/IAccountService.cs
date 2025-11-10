using System.Runtime.InteropServices.JavaScript;
using Api.DTO;

namespace Api.Services.Interface
{
    public interface IAccountService
    {
        //Register new account and center
        Task RegisterAccountAsync(RegisterAccountDTO register);
        
        //Generate JWT token based on login info
        Task<TokenDTO> GetJwtTokenAsync(LoginDTO login);

        //Create access token based on userId and lastModifiedTime
        Task<string> GetAccessTokenAsync(string tokenId, int userId, DateTime lastModifiedTime);
    }
}
