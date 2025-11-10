using Api.Models;
using Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository.Implement;

public class ClassRepository : IClassRepository
{
    private readonly ILogger<ClassRepository> _logger;
    private readonly AppDbContext _context;
    
    public ClassRepository(ILogger<ClassRepository> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    
    public async Task<IEnumerable<Class>> GetAllClassesAsync()
    {
        return await _context.Classes
            .Where(c => c.IsDeleted == false)
            .ToListAsync();
    }
}