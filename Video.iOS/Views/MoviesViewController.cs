using System;
using System.Linq;
using System.Windows;
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.Dialog;
using MonoTouch.UIKit;
using Video.Core.ViewModels;
using Video.Core.Helpers;
using BigTed;

namespace Video.iOS.Views
{
    public partial class MoviesViewController : DialogViewController
    {
        private Section section;
        private LoadMoreElement loadMore;

        private MoviesViewModel viewModel;
        public MoviesViewController() : base(UITableViewStyle.Plain, null)
        {
      
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            viewModel = ServiceContainer.Resolve<MoviesViewModel>();

            viewModel.IsBusyChanged = (busy) =>
            {
                if (busy)
                    BTProgressHUD.Show("Loading...");
                else
                    BTProgressHUD.Dismiss();
            };
        }

        public async override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if (viewModel.NeedsUpdate)
            {
                await viewModel.ExecuteLoadMoviesCommand();
                Root = new RootElement("Movies");
                Root.Add(section = new Section());
                loadMovies();
            }
        }

        private void loadMovies()
        {
            section.Remove(loadMore);
            section.AddAll(viewModel.MoviesList.Select(movie => (Element)new StringElement(movie.Title, () =>
                           {
                               this.NavigationController.PushViewController(new MovieViewController(movie.Id), true);
                           })));

            //section.Add(loadMore = new LoadMoreElement("Load more", "Loading...", async (element) =>
            //{
            //    await viewModel.ExecuteLoadMoreCommand();

            //    loadMovies();
            //}));
        }
       
    }
}