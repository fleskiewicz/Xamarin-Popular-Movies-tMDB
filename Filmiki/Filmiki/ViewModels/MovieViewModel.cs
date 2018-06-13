using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Text;
using TMDbLib.Client;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;
using Xamarin.Forms;
using System.Threading.Tasks;
using Filmiki.Models;
using Filmiki.Views;
using System.Windows.Input;

namespace Filmiki.ViewModels
{
    public class MovieViewModel : BaseViewModel
    {
        public MovieViewModel()
        {
            _pageTitle = "Filmy na czasie";
            _movieTitleList = new ObservableCollection<Film>();
            _refreshCommand = new Command(() => RefreshList());
            _canDelete = false;
            Task.Run(() =>
            {
                IsBusy = true;
                MovieTitleList = PopulateList();
                IsBusy = false;
            }); 
        }

        public MovieViewModel(string title)
        {
            _pageTitle = title;
            _refreshCommand = new Command(() => RefreshList());
            _canDelete = true;
        }

        private bool _canDelete = false;
        public bool CanDelete
        {
            get
            {
                return _canDelete;
            }
            set
            {
                _canDelete = value;
                OnPropertyChanged("CanDelete");
            }
        }

        private string _pageTitle;
        public string PageTitle
        {
            get { return _pageTitle; }
            set { _pageTitle = value; }
        }

        private Boolean _canRefresh = true;
        public Boolean CanRefresh
        {
            get { return _canRefresh; }
            set { _canRefresh = value; }
        }

        void RefreshList()
        {
            if (_canRefresh == true) { 
                IsRefreshing = true;
                if (PageTitle == "Filmy na czasie")
                {
                    MovieTitleList = PopulateList();
                }
                if (PageTitle == "Moja lista")
                {
                    GoToOwnList.Execute(null);
                }
                IsRefreshing = false;
            }
        }



        private ObservableCollection<Film> _movieTitleList;
        public ObservableCollection<Film> MovieTitleList
        {
            get
            {
                return _movieTitleList;
            }
            set
            {
                _movieTitleList = value;
                OnPropertyChanged(nameof(MovieTitleList));
            }
        }

        ObservableCollection<Film> PopulateList()
        {
            ObservableCollection<Film> ApiMovieList = new ObservableCollection<Film>();
            TMDbClient client = new TMDbClient("7ab50ea9619bac2c618bbd67a7c80cc5");
            for (int i = 1; i < 5; i++)
            {
                SearchContainer<SearchMovie> list = client.GetMoviePopularListAsync("pl", i).Result;
                foreach (SearchMovie movie in list.Results)
                {
                    Film film = new Film
                    {
                        Id = movie.Id,
                        Title = movie.Title,
                        Overview = movie.Overview,
                        BackdropPath = movie.BackdropPath,
                        PosterPath = movie.PosterPath,
                        VoteAverage = movie.VoteAverage.ToString()
                    };
                    ApiMovieList.Add(film);
                }
            }
            _movieTitleList = ApiMovieList;
            return _movieTitleList;
        }

        readonly Command _refreshCommand;
        public Command RefreshCommand
        {
            get
            {
                return _refreshCommand;
            }
        }

        private Film _selectedMovie;

        public Film SelectedMovie
        {
            get { return _selectedMovie; }
            set
            {
                _selectedMovie = value;
                if (_selectedMovie == null)
                {
                    return;
                }
                SelectMovie.Execute(_selectedMovie);
                _selectedMovie = null;
                OnPropertyChanged("SelectedMovie");
            }
        }

        public Command SelectMovie
        {
            get
            {
                return new Command(async () =>
                {
                    var MovieDetailViewModel = new MovieDetailViewModel()
                    {
                        Id = SelectedMovie.Id,
                        Title = SelectedMovie.Title,
                        VoteAverage = SelectedMovie.VoteAverage,
                        Overview = SelectedMovie.Overview,
                        PosterPath = SelectedMovie.PosterPath,
                        BackdropPath = SelectedMovie.BackdropPath,
                        IsAddedToList = SelectedMovie.IsAddedToList
                    };
                    var movieDetailPage = new MovieDetailPage(MovieDetailViewModel);
                    await Application.Current.MainPage.Navigation.PushAsync(movieDetailPage);
                });
            }
        }

        public Command SearchForMovie
        {
            get
            {
                return new Command(async () =>
                {
                    var searchMovieViewModel = new SearchMovieViewModel() { };
                    var movieSearchPage = new MovieSearchPage(searchMovieViewModel);
                    await Application.Current.MainPage.Navigation.PushAsync(movieSearchPage);
                });
            }
        }

        public Command GoToOwnList
        {
            get
            {
                return new Command(async () =>
                {
                    var userList = await App.Database.GetFilmsAsync();
                    ObservableCollection<Film> ownList = new ObservableCollection<Film>(userList);
                    var ownListViewModel = new MovieViewModel("Moja lista")
                    {
                        MovieTitleList = ownList,
                        CanRefresh = false,
                        CanDelete = true
                    };
                    var ownListPage = new MoviePage(ownListViewModel);
                    await Application.Current.MainPage.Navigation.PushAsync(ownListPage);
                });
            }
        }
    }
}
