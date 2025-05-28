using EtlProject.Core.Models;

namespace EtlProject.DataAccess.Interfaces;

public interface IJsonMessageRepository
{
    Task AddJsonMessage(JsonMessage jsonMessage);
}