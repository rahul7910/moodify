using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace App2
{
    public partial class App2Page : ContentPage
    {
        public App2Page()
        {
            InitializeComponent();
        }
		
		
//        private async void UploadPictureButton_Clicked(object sender, EventArgs e)
 //       {
   //         if (!CrossMedia.Current.IsPickPhotoSupported)
    //        {
   //             await DisplayAlert("No upload", "Picking a photo is not supported.", "OK");
   //             return;
   //         }
   //         var file = await CrossMedia.Current.PickPhotoAsync();
   //         if (file == null)
   //             return;

   //         Image1.Source = ImageSource.FromStream(() => file.GetStream());

//        }

        private async void TakePictureButton_Clicked(object sender, EventArgs e)
        {

            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            MediaFile file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                PhotoSize = PhotoSize.Medium,
                Directory = "Sample",
                Name = $"{DateTime.UtcNow}.jpg"
            });

            if (file == null)
                return;

            Image1.Source = ImageSource.FromStream(() =>
            {
                return file.GetStream();
            });

           // await postLocationAsync();

            await MakePredictionRequest(file);
        }

        static byte[] GetImageAsByteArray(MediaFile file)
        {
            var stream = file.GetStream();
            BinaryReader binaryReader = new BinaryReader(stream);
            return binaryReader.ReadBytes((int)stream.Length);
        }

        async Task MakePredictionRequest(MediaFile file)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Prediction-Key", "da8e855685e645ba93a3c7849b48b70f");

            //Where all the pictures are stored
            string url = "https://southcentralus.api.cognitive.microsoft.com/customvision/v1.0/Prediction/763c1ac4-c0d8-4876-944e-640a9d6fe57d/image?iterationId=56c0bd2c-8c37-41bc-bf82-9647dd4844fc";

            HttpResponseMessage response;

            byte[] byteData = GetImageAsByteArray(file);

            using (var content = new ByteArrayContent(byteData))
            {

                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(url, content);


                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    JObject rss = JObject.Parse(responseString);


                    //Get all Prediction Values
                    var Probability = from p in rss["Predictions"] select (string)p["Probability"];
                    var Tag = from p in rss["Predictions"] select (string)p["Tag"];

                    //Truncate values to labels in XAML
                    foreach (var item in Tag)
                    {
                        TagLabel.Text += item + ": \n";

                    }

                    foreach (var item in Probability)
                    {
                        PredicitionLabel.Text += item + "\n";
                    }

                }


                //Get rid of file once we have finished using it
                file.Dispose();
            }

        }
    }
}
