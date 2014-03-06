using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Video.Core.Helpers;
using Video.Core.Interfaces;
using Video.Core.Models;
using Video.Core.Services;

namespace Video.Core.ViewModels
{
    public class MoviesViewModel : ViewModelBase
    {
        private IMovieService movieService;
        private IMessageDialog messageDialog;
        private IStorageService storageService;

        public MoviesViewModel()
        {
            movieService = ServiceContainer.Resolve<IMovieService>();
            messageDialog = ServiceContainer.Resolve<IMessageDialog>();
            storageService = ServiceContainer.Resolve<IStorageService>();
            NeedsUpdate = true;
        }

        /// <summary>
        /// Gets or sets if an update is needed
        /// </summary>
        public bool NeedsUpdate { get; set; }
        /// <summary>
        /// Gets or sets if we have loaded alert
        /// </summary>
        public bool LoadedAlert { get; set; }

        private ObservableCollection<Movie> movies = new ObservableCollection<Movie>();
        public ObservableCollection<Movie> Movies
        {
            get { return movies; }
            set { movies = value; OnPropertyChanged("Movies"); }
        }

        private List<Movie> moviesList = new List<Movie>();
        public List<Movie> MoviesList
        {
            get { return moviesList; }
            set { moviesList = value; OnPropertyChanged("MoviesList"); }
        }

        private RelayCommand loadMoviesCommand;
        public ICommand LoadMoviesCommand
        {
            get { return loadMoviesCommand ?? (loadMoviesCommand = new RelayCommand(async () => await ExecuteLoadMoviesCommand())); }
        }

        public async Task ExecuteLoadMoviesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            Movies.Clear();
            NeedsUpdate = false;
            try
            {
                var movies = await movieService.GetMoviesNowPlaying(20);

                foreach (var movie in movies)
                {
                    Movies.Add(movie);
                }
                MoviesList = movies.ToList();
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Unable to query and gather expenses");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private RelayCommand loadMoreCommand;
        public ICommand LoadMoreCommand
        {
            get { return loadMoreCommand ?? (loadMoreCommand = new RelayCommand(async () => await ExecuteLoadMoreCommand())); }
        }
        public async Task ExecuteLoadMoreCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            MoviesList.Clear();
            NeedsUpdate = false;
            try
            {
                var movies = await movieService.GetMoviesNowPlaying(20, Movies.Count);

                foreach (var movie in movies)
                {
                    Movies.Add(movie);
                }
                MoviesList = movies.ToList();
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Unable to query and gather expenses");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private RelayCommand<Movie> deleteMoviesCommand;
        public ICommand DeleteMovieCommand
        {
            get { return deleteMoviesCommand ?? (deleteMoviesCommand = new RelayCommand<Movie>(async (item) => await ExecuteDeleteMovieCommand(item))); }
        }

        public async Task ExecuteDeleteMovieCommand(Movie movie)
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                await storageService.DeleteMovie(movie.Id);
            }
            catch (Exception)
            {
                Debug.WriteLine("Unable to delete movie");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
