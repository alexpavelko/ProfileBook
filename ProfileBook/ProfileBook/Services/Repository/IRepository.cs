using ProfileBook.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileBook.Services.Repository
{
    public interface IRepository
    {
        Task AddAsync<T>(T entity) where T : IEntityBase, new();
        Task UpdateAsync<T>(T entity) where T : IEntityBase, new();
        Task DeleteAsync<T>(T entity) where T : IEntityBase, new();
        Task<List<T>> GetAllAsync<T>(int user_id) where T : IEntityBase, new();
        Task<List<T>> GetAllWithQueryAsync<T>(string sqlCommand) where T : IEntityBase, new();
        Task AddOrUpdateAsync<T>(T entity) where T : IEntityBase, new();
    }
}
