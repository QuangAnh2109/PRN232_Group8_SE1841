using Api.Models;
using Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository.Implement;

public class TokenRepository : ITokenRepository
{
    private readonly ILogger<TokenRepository> _logger;
    private readonly AppDbContext _context;

    public TokenRepository(AppDbContext context, ILogger<TokenRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public async Task<Token> GetTokenByIdAsync(Guid id)
    {
        try
        {
            return await _context.Tokens
                .Where(t => t.Id == id && t.IsDeleted == false && t.IsActive == true)
                .FirstAsync();
        }
        catch (Exception e)
        {
            _logger.LogError("Lỗi khi lấy token theo ID: {Message}", e.Message);
            throw;
        }
    }

    public async Task AddTokenAsync(Token token)
    {
        try
        {
            await _context.Tokens.AddAsync(token);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError("Lỗi khi thêm token: {Message}", e.Message);
            throw;
        }
    }
}