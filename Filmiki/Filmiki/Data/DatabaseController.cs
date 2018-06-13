using Filmiki.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Filmiki.Data
{
    public class DatabaseController
    {
        readonly SQLiteAsyncConnection _database;

        public DatabaseController(string databasePath)
        {
            _database = new SQLiteAsyncConnection(databasePath);
            _database.CreateTableAsync<Film>().Wait();
        }

        public Task<List<Film>> GetFilmsAsync()
        {
            return _database.Table<Film>()
                .OrderBy(t => t.Title)
                .ThenBy(t => t.VoteAverage)
                .ToListAsync();
        }

        public Task<Film> GetFilmAsync(int id)
        {
            return _database.Table<Film>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveFilmAsync(Film film)
        {
                return _database.InsertAsync(film);
        }

        public Task<int> DeleteFilmAsync(Film film)
        {
            return _database.DeleteAsync(film);
        }
    }
}
