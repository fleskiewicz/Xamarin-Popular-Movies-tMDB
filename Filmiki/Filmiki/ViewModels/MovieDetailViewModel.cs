using Filmiki.Models;
using Filmiki.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Filmiki.ViewModels
{
    public class MovieDetailViewModel : BaseViewModel
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }
        public string PageTitle
        {
            get { return "Szczegóły"; }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        private string _overview;
        public string Overview
        {
            get { return _overview; }
            set
            {
                _overview = value;
                OnPropertyChanged("Overview");
            }
        }

        private bool _isAddedToList;
        public bool IsAddedToList
        {
            get { return _isAddedToList; }
            set
            {
                _isAddedToList = value;
                OnPropertyChanged("IsAddedToList");
            }
        }

        private string _posterPath;
        public string PosterPath
        {
            get { return _posterPath; }
            set
            {
                _posterPath = value;
                OnPropertyChanged("PosterPath");
            }
        }


        private string _backdropPath;
        public string BackdropPath
        {
            get { return _backdropPath; }
            set
            {
                _backdropPath = value;
                OnPropertyChanged("BackdropPath");
            }
        }

        private string _voteAverage;
        public string VoteAverage
        {
            get { return _voteAverage; }
            set
            {
                _voteAverage = value;
                OnPropertyChanged("VoteAverage");
            }
        }

        public Command FavoriteCommand
        {
            get
            {
                    return new Command(async () =>
                    {
                        Film filmToAdd = new Film
                        {
                            Id = _id,
                            Title = _title,
                            Overview = _overview,
                            BackdropPath = _backdropPath,
                            PosterPath = _posterPath,
                            VoteAverage = _voteAverage.ToString(),
                            IsAddedToList = true
                        };
                        await App.Database.SaveFilmAsync(filmToAdd);
                        await Application.Current.MainPage.DisplayAlert("Gotowe!", "Film został dodany.", "Ok");
                        await App.Current.MainPage.Navigation.PopAsync();
                    });
            }
        }

        private bool _canDeleteFromList;
        public bool CanDeleteFromList
        {
            get
            {
                if (_isAddedToList == true) return true;
                else return false;
            }
            set
            {
                _canDeleteFromList = value;
                OnPropertyChanged("CanDeleteFromList");
            }
        }

        public Command DeleteFilm
        {
            get
            {
                return new Command(async () =>
                {
                    Film filmToDelete = new Film
                    {
                        Id = _id,
                        Title = _title,
                        Overview = _overview,
                        BackdropPath = _backdropPath,
                        PosterPath = _posterPath,
                        VoteAverage = _voteAverage.ToString(),
                        IsAddedToList = true
                    };
                    await App.Database.DeleteFilmAsync(filmToDelete);
                    await App.Current.MainPage.Navigation.PopToRootAsync();
                });
            }
        }
    }
}
