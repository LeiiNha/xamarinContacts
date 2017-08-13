using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Contacts.View;
using Contacts.ViewModel;
using Contacts.Model;

namespace Contacts
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }
        
        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) { return; }
            Person person = e.SelectedItem as Person;
            Navigation.PushAsync(new ContactDetails(person));

        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewContact());
        }
    }
}
