using EtlProject.Core.Models;
using EtlProject.DataAccess.Interfaces;

namespace EtlProject.DataAccess.Repositories;

public class JsonMessageRepository : IJsonMessageRepository
{
    private readonly EtlDbContext _context;

    public JsonMessageRepository(EtlDbContext context)
    {
        _context = context;
    }
    
    public async Task AddJsonMessageAsync(JsonMessage jsonMessage)
    {
        await _context.JsonMessages.AddAsync(jsonMessage);
        await _context.SaveChangesAsync();
    }
}