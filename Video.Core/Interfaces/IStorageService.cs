using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Video.Core.Models;

namespace Video.Core.Interfaces
{
    public interface IStorageService
    {
        Task<Movie> GetMovie(int id);
        Task<Movie> SaveMovie(Movie movie);
        Task<int> DeleteMovie(int id);
    }
}
