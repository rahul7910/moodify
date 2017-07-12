using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace App2
{
    public partial class FacePage : ContentPage
    {
        public FacePage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var viewModel = BindingContext as ViewModel;
            viewModel.Load();
        }
    }
}
