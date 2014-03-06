using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Video.Core.Models;

namespace Video.Core.Interfaces
{
    public interface IMovieService
    {
        Task<Movie> GetMovie(int id);
        Task<IEnumerable<Movie>> GetMovies(string title);
        Task<IEnumerable<Movie>> GetMoviesNowPlaying(int count = 0, int skip = 0);
        Task<IEnumerable<Movie>> GetMoviesPopular(int count = 0, int skip = 0);
    }
}
