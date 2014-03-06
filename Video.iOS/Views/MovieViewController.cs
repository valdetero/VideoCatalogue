using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using Video.Core.Models;
using Video.Core.ViewModels;
using Video.Core.Helpers;
using BigTed;
using Xamarin.Media;
using System.IO;
using System.Threading.Tasks;

namespace Video.iOS.Views
{
    public partial class MovieViewController : DialogViewController
    {
        private MovieViewModel viewModel;
        private Movie movie;
        private int id = 0;

        private StringElement title;
        private HtmlElement imdbId;
        private BooleanElement isFavorite;
        private StringElement runtime;
        private StringElement releaseDate;

        public MovieViewController(int id) : base(UITableViewStyle.Plain, null, true)
        {
            viewModel = ServiceContainer.Resolve<MovieViewModel>();
            this.id = id;
        }
        public MovieViewController(Movie movie) : base(UITableViewStyle.Plain, null, true)
        {
            this.movie = movie;
            viewModel = ServiceContainer.Resolve<MovieViewModel>();
            viewModel.Init(this.movie);
        }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

            if(id > 0)
            {
                BTProgressHUD.Show("Loading...");
                await viewModel.Init(id);
                BTProgressHUD.Dismiss();
            }

            var delete = UIButton.FromType(UIButtonType.RoundedRect);
            delete.SetTitle("Delete", UIControlState.Normal);
            delete.Frame = new System.Drawing.RectangleF(0, 0, 320, 44);
            delete.TouchUpInside += async (sender, e) => 
            {
                await viewModel.ExecuteDeleteMovieCommand(viewModel.Id);
                NavigationController.PopToRootViewController(true);
            };

            var save = UIButton.FromType(UIButtonType.RoundedRect);
            save.SetTitle("Save", UIControlState.Normal);
            save.Frame = new System.Drawing.RectangleF(0, 0, 320, 44);
            save.TouchUpInside += async (sender, e) => 
            {
                viewModel.IsFavorite = isFavorite.Value;
                await viewModel.ExecuteSaveMovieCommand();
                NavigationController.PopToRootViewController(true);
            };

            this.Root = new RootElement(viewModel.Title)
            {
                new Section("Movie Details")
                {
                    (title = new StringElement("Title")),
                    (imdbId = new HtmlElement("Imdb", string.Empty)),
                    (runtime = new StringElement("Runtime")),
                    (releaseDate = new StringElement("Release Date")),
                    (isFavorite = new BooleanElement("Favorite?", false)),
                },
                new Section()
                {
                    delete,
                    save
                }
            };

                      
            title.Value = viewModel.Title;
            imdbId.Url = string.Format("http://m.imdb.com/title/{0}", viewModel.ImdbId);
            runtime.Value = viewModel.Runtime.ToString();
            releaseDate.Value = viewModel.ReleaseDate.ToShortDateString();
            isFavorite.Value = viewModel.IsFavorite;

            this.NavigationItem.RightBarButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Camera, async delegate
            {
                var picker = new Xamarin.Media.MediaPicker();
                if (picker.PhotosSupported)
                {
                    try
                    {
                        MediaFile file = await picker.PickPhotoAsync();
                        if (file != null)
                        {
                            viewModel.Image = file.GetStream();
                            await viewModel.ExecuteSaveMovieCommand();
                            NavigationController.PopToRootViewController(true);
                        }
                    }
                    catch { }
                }
            });
        }
    }
}