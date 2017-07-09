using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;

//This is the Front Page 

namespace App2
{
    public partial class MyPage : ContentPage
    {
        public MyPage()
        {
            InitializeComponent();
        }
		private async void NavigateButton_OnClicked(object sender, EventArgs e)
		{
            await Navigation.PushAsync(new App2Page());
		}
        private async void Navigate_OnClicked(object sender, EventArgs e ){
            await Navigation.PushAsync(new AzureTable());
        }

    }

}
