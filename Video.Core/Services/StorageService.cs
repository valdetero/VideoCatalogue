using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Video.Core.DataLayer;
using Video.Core.Models;

namespace Video.Core.Services
{
    public class StorageService : Interfaces.IStorageService
    {
        MovieDatabase db = null;
        protected static string dbLocation;

        public StorageService(SQLite.Net.SQLiteConnection conn)
        {
            db = new MovieDatabase(conn);
        }

        public Task<List<Movie>> GetMovies()
        {
            return Task.Factory.StartNew(() => db.GetItems<Movie>().ToList());
        }

        public Task<Movie> GetMovie(int id)
        {
            return Task.Factory.StartNew(() => db.GetItem<Movie>(id));
        }

        public Task<Movie> SaveMovie(Movie movie)
        {
            return Task.Factory.StartNew(() =>
            {
                db.SaveItem<Movie>(movie);
                return movie;
            });
        }

        public Task<int> DeleteMovie(int id)
        {
            return Task.Factory.StartNew(() => db.DeleteItem<Movie>(id));
        }
    }
}
