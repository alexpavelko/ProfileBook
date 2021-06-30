using ProfileBook.DefaultValues;
using ProfileBook.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ProfileBook.Services.Repository
{
    public class Repository : IRepository
    {
        private Lazy<SQLiteAsyncConnection> _database;
        public Repository()
        {
            _database = new Lazy<SQLiteAsyncConnection>(() =>
            {
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Values.DB_NAME);
                var database = new SQLiteAsyncConnection(path);

                database.CreateTableAsync<Profile>().Wait();

                database.CreateTableAsync<User>().Wait();

                return database;
            });
        }

        #region -- IRepository implementation --

        public Task DeleteAsync<T>(T entity) where T : IEntityBase, new()
        {
            return _database.Value.DeleteAsync(entity);
        }

        public async Task<List<T>> GetAllWithQueryAsync<T>(string sqlCommand) where T : IEntityBase, new()
        {
            return await _database.Value.QueryAsync<T>(sqlCommand);
        }

        public async Task<T> FindWithCommandAsync<T>(string sqlCommand) where T : IEntityBase, new()
        {
            return await _database.Value.FindWithQueryAsync<T>(sqlCommand);
        }

        public async Task AddAsync<T>(T entity) where T : IEntityBase, new()
        {
            await _database.Value.InsertAsync(entity);
        }

        public async Task UpdateAsync<T>(T entity) where T : IEntityBase, new()
        {
            await _database.Value.UpdateAsync(entity);
        }

        public async Task AddOrUpdateAsync<T>(T entity) where T : IEntityBase, new()
        {
            if (entity.Id == 0)
            {
                await AddAsync(entity);
            }
            else
            {
                await UpdateAsync(entity);
            }
        }

        #endregion
    }
}