using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Video.Core.Helpers;
using Video.Core.Interfaces;

namespace Video.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Video.Core.ServiceRegistrar.Startup();

            var movieService = ServiceContainer.Resolve<IMovieService>();
            var movie = movieService.GetMovie(550).Result;

            System.Console.WriteLine(movie.Title);
            System.Console.ReadLine();
        }
    }
}
