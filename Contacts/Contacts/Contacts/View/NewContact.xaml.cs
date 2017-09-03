using Contacts.View;
using Contacts.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contacts
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewContact : ContentPage
	{
        NewContactViewModel vm;

        

        public NewContact (NewContactViewModel vmPar)
		{
			InitializeComponent ();
            vm = vmPar;
            BindingContext = vm;
		}

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            if ((!string.IsNullOrEmpty(FirstNameEntry.Text)) && (!string.IsNullOrEmpty(EmailEntry.Text)) && (vm.PhoneNumber.Count > 0)) {
                vm.AddToPeople();
                Navigation.PopAsync();
            } else
            {
                DisplayAlert("Campo obrigatório", "Preencha pelo menos o primeiro nome, email e número", "Ok");
            }
        }
        

        private void AddNewNumber_Clicked(object sender, EventArgs e)
        {
            NumbersList.BeginRefresh();
            vm.AddNewNumber();
            NumbersList.EndRefresh();
        }

        private void Camera_Clicked(object sender, EventArgs e)
        {
            var camera = new CameraView();
            camera.buttonCallback = takePicture;
            Navigation.PushAsync(camera);
        }

        private async void TakePicture_Clicked(object sender, EventArgs e)
        {
            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions() { });

            if (file != null)
            {
                PhotoImage.Source = file.Path;
                vm.ImageSource = file.Path;
            }
        }

        private async Task takePicture()
        {
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });

            if (photo != null)
            {
                PhotoImage.Source = photo.Path;
                vm.ImageSource = photo.Path;
            }
        }

        private async void Location_Clicked(object sender, EventArgs e)
        {
            var position = await Plugin.Geolocator.CrossGeolocator.Current.GetPositionAsync(timeout: new TimeSpan(0,0,0,5,0));
             if (position != null) {                
                vm.Latitude = position.Latitude;
                vm.Longitude = position.Longitude;
                try
                {
                    var possibleAddresses = await Plugin.Geolocator.CrossGeolocator.Current.GetAddressesForPositionAsync(position, "tVddRJQPKfkBxe8SYaX6~sa6b8cUD7r_nWF5KZwiiQw~AqktZmGWDGZgS6a3hiBnQnjLP4dOEoXfATQPwL7UW6TglpLA0AaebJyQw-9xD0hY");
                    var addr = possibleAddresses.FirstOrDefault();
                    AddressEntry.Text = addr.Thoroughfare + ", " + addr.SubThoroughfare + " - " + addr.SubLocality + " - " + addr.Locality + ", " + addr.CountryName;
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Geolocator", "Não foi possível usar o gps do celular: " + ex.Message, "Ok");
                }
                

            }
        }
    }
}