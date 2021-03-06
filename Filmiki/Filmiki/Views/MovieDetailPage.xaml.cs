﻿using Filmiki.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Filmiki.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MovieDetailPage : ContentPage
	{
		public MovieDetailPage (MovieDetailViewModel viewModel)
		{
			InitializeComponent ();
            BindingContext = viewModel;
		}
	}
}