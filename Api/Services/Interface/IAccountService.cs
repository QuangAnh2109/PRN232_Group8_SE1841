using Api.DTO;

namespace Api.Services.Interface
{
    public interface IAccountService
    {
        Task<bool> RegisterAccountAsync(RegisterAccountDTO register);

        Task<String> GetRefreshTokenAsync(LoginDTO login);

        Task<String> GetAccessTokenAsync(String refreshToken);
    }
}
