using Contacts.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
namespace Contacts.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactDetails : ContentPage
    {
        ContactDetailsViewModel vm;
        public ContactDetails(ContactDetailsViewModel viewmodel)
        {

            InitializeComponent();
            BindingContext = viewmodel;
            vm = viewmodel;
            emailImg.GestureRecognizers.Add(new TapGestureRecognizer
            {
                NumberOfTapsRequired = 1,
                Command = vm.startEmailCommand
            });
            var position = new Position(vm.person.Latitude, vm.person.Longitude);
            var map = new Map(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(0.3)))
            {
                IsShowingUser = true,
                HeightRequest = 300,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            var pin = new Pin
            {
                Type = PinType.Place,
                Position = position,
                Label = "Endereço de " + vm.person.FullName,
                Address = vm.person.Address
            };
            map.Pins.Add(pin);

            mapStackLayout.Children.Add(map);

          
        }

        private async void Share_Clicked(object sender, EventArgs e)
        {
            Plugin.Share.Abstractions.ShareMessage mssg = new Plugin.Share.Abstractions.ShareMessage();
            mssg.Title = "Contato";
            mssg.Text = vm.person.FullName + Environment.NewLine;
            mssg.Text += vm.person.Address + Environment.NewLine;
            foreach (var item in vm.person.PhoneNumber)
            {
                mssg.Text += item.Desc + " - " + item.Number + Environment.NewLine;
            }
                     

            await Plugin.Share.CrossShare.Current.Share(mssg);
        }

        private async void Edit_Clicked(object sender, EventArgs e)
        {
            var vmNewContact = new NewContactViewModel();
            vmNewContact.fillPerson(vm.person);            
            var newContact = new NewContact(vmNewContact);            
            await Navigation.PushAsync(newContact);   
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            await vm.deleteContact();
            await Navigation.PopAsync();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await vm.refreshContact();
            this.BindingContext = vm;

        }
    }
}