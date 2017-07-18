using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App2.DataModels;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace App2
{
	public partial class AzureTable : ContentPage
	{
        void Handle_Clicked(object sender, System.EventArgs e)
        {
            throw new NotImplementedException();
        }

        Geocoder geoCoder;

		public AzureTable()
		{
			InitializeComponent();
			geoCoder = new Geocoder();

		}

		async void Handle_ClickedAsync(object sender, System.EventArgs e)
		{
			loading.IsRunning = true;
            List < EmployeeModel > EmployeeInformation = await AzureManager.AzureManagerInstance.GetEmployeeInformation();

            foreach (EmployeeModel model in EmployeeInformation)
			{
				var position = new Position(model.Latitude, model.Longitude);
				var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(position);
				foreach (var address in possibleAddresses)
					model.City = address;
			}

            EmployeeList.ItemsSource = EmployeeInformation;
			loading.IsRunning = false;
		}

        //For the maps button take to new page and then it will load the map 
        //Create new map content page 

        public async Task Handle_ClickAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MapPage());
        }

    }

	}

