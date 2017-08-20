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

        private void PhoneDescPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            var selectedItem = (string)picker.SelectedItem;
            vm.PhoneNumber.Last().Desc = selectedItem;

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
    }
}