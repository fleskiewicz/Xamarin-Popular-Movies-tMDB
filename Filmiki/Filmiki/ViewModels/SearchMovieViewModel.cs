using Filmiki.Models;
using Filmiki.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;
using Xamarin.Forms;

namespace Filmiki.ViewModels
{
    public class SearchMovieViewModel : BaseViewModel
    {
        private ICommand _searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new Command<string>(async (text) =>
                {
                    ObservableCollection<Film> searchResults = SearchList(text);
                    var SearchResultsViewModel = new MovieViewModel("Wyszukiwanie")
                    {
                        MovieTitleList = searchResults,
                        CanRefresh = false
                    };
                    var searchResultsPage = new MoviePage(SearchResultsViewModel);
                    await Application.Current.MainPage.Navigation.PushAsync(searchResultsPage);
                }));
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

        ObservableCollection<Film> SearchList(string query)
        {
            ObservableCollection<Film> ApiMovieList = new ObservableCollection<Film>();
            TMDbClient client = new TMDbClient("7ab50ea9619bac2c618bbd67a7c80cc5");
            SearchContainer<SearchMovie> list = client.SearchMovieAsync(query).Result;
                foreach (SearchMovie movie in list.Results)
                {
                    Film film = new Film
                    {
                        Id = movie.Id,
                        Title = movie.Title,
                        Overview = movie.Overview,
                        BackdropPath = movie.BackdropPath,
                        PosterPath = movie.PosterPath,
                        VoteAverage = movie.VoteAverage.ToString(),
                        IsAddedToList = false
                    };
                    ApiMovieList.Add(film);
                }
            _movieTitleList = ApiMovieList;
            return _movieTitleList;
        }
    }
}
