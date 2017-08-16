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
       MainPageViewModel mv;
        public MainPage()
        {
            InitializeComponent();
            mv = new MainPageViewModel();
            BindingContext = mv;
            MessagingCenter.Subscribe<MainPageViewModel>(new MainPageViewModel(), "ButtonClicked", (sender) =>
             {
                 DisplayAlert("Message", "Button Clicked!", "Ok");
             });
            
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

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            PeopleListView.BeginRefresh();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                PeopleListView.ItemsSource = mv.People;
            else
                PeopleListView.ItemsSource = mv.People.Where(i => i.Name.Contains(e.NewTextValue));

            PeopleListView.EndRefresh();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            
            DisplayAlert("Apertei butaum", "Button Clicked!", "Ok");
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            var root = Navigation.NavigationStack[0];
            Navigation.InsertPageBefore(new GroupList(), root);
            Navigation.PopToRootAsync();
        }
    }
}
