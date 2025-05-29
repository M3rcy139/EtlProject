using EtlProject.Core.Models;

namespace EtlProject.DataAccess.Interfaces;

public interface IJsonMessageRepository
{
    Task AddJsonMessageAsync(JsonMessage jsonMessage);
}