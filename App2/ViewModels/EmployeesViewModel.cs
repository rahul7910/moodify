﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Acr.UserDialogs;

using Microsoft.ProjectOxford.Face;

namespace EmployeeDirectory
{
	public class EmployeesViewModel : BaseViewModel
	{
		string personGroupId;

		public EmployeesViewModel()
		{
			Title = "Employees";

			Employees = new ObservableCollection<Employee>
			{
				new Employee { Name = "Bill Gates", Title = "CEO", PhotoUrl = "https://pbs.twimg.com/profile_images/558109954561679360/j1f9DiJi.jpeg" },
				new Employee { Name = "Steve Wozniak", Title = "CTO", PhotoUrl = "http://www.afr.com/content/dam/images/1/m/4/e/c/f/image.related.afrArticleLead.620x350.1m3xxk.png/1427131665373.jpg" },
				new Employee { Name = "Steve Jobs", Title = "VP of Developer Relations", PhotoUrl = "http://images.huffingtonpost.com/2015-07-17-1437155465-1424656-SteveJobs.jpg" },
				new Employee { Name = "James Montemagno", Title = "Developer Evangelist", PhotoUrl = "http://www.gravatar.com/avatar/7d1f32b86a6076963e7beab73dddf7ca?s=256" },
				new Employee { Name = "Pierce Boggan", Title = "Software Engineer", PhotoUrl = "https://avatars3.githubusercontent.com/u/1091304?v=3&s=460" },
			};

		//	RegisterEmployees();
		}

		ObservableCollection<Employee> employees;
		public ObservableCollection<Employee> Employees
		{
			get { return employees; }
			set { employees = value; OnPropertyChanged("Employees"); }
		}

		Command findSimilarFaceCommand;
		public Command FindSimilarFaceCommand
		{
			get { return findSimilarFaceCommand ?? (findSimilarFaceCommand = new Command(async () => await ExecuteFindSimilarFaceCommandAsync())); }
		}

		async Task ExecuteFindSimilarFaceCommandAsync()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				MediaFile photo;

				await CrossMedia.Current.Initialize();

				// Take photo
				if (CrossMedia.Current.IsCameraAvailable)
				{
					photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
					{
						Directory = "Employee Directory",
						Name = "photo.jpg"
					});
				}
				else
				{
					photo = await CrossMedia.Current.PickPhotoAsync();
				}

				// Upload to cognitive services
				using (var stream = photo.GetStream())
				{
					var faceServiceClient = new FaceServiceClient("22e49721a20e457880a32138afc9e027");

					// Step 4 - Upload our photo and see who it is!
					var faces = await faceServiceClient.DetectAsync(stream);
					var faceIds = faces.Select(face => face.FaceId).ToArray();

					var results = await faceServiceClient.IdentifyAsync(personGroupId, faceIds);
					var result = results[0].Candidates[0].PersonId;

					var person = await faceServiceClient.GetPersonAsync(personGroupId, result);

					UserDialogs.Instance.ShowSuccess($"Person identified is {person.Name}.");
				}
			}
			catch (Exception ex)
			{
				UserDialogs.Instance.ShowError(ex.Message);
			}
			finally
			{
				IsBusy = false;
			}
		}

		async Task RegisterEmployees()
		{
			var faceServiceClient = new FaceServiceClient("22e49721a20e457880a32138afc9e027");

			// Step 1 - Create Face List
			personGroupId = Guid.NewGuid().ToString();
			await faceServiceClient.CreatePersonGroupAsync(personGroupId, "Xamarin Employees");

			// Step 2 - Add people to face list
			foreach (var employee in Employees)
			{
				var p = await faceServiceClient.CreatePersonAsync(personGroupId, employee.Name);
				await faceServiceClient.AddPersonFaceAsync(personGroupId, p.PersonId, employee.PhotoUrl);
			}

			// Step 3 - Train face group
			await faceServiceClient.TrainPersonGroupAsync(personGroupId);
		}
	}
}