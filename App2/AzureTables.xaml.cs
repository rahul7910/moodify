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
		Geocoder geoCoder;

		public AzureTable()
		{
			InitializeComponent();
			geoCoder = new Geocoder();

		}

		async void Handle_ClickedAsync(object sender, EventArgs e)
		{
            List<EmployeeModel> EmployeeInformation = await AzureManager.AzureManagerInstance.GetEmployeeInformation();

            foreach (EmployeeModel model in EmployeeInformation)
			{
				var position = new Position(model.Latitude, model.Longitude);
				var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(position);
				foreach (var address in possibleAddresses)
					model.City = address;
			}

            EmployeeList.ItemsSource = EmployeeInformation;

		}

	}
}