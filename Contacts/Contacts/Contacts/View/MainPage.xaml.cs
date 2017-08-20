using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Contacts.View;
using Contacts.ViewModel;
using Contacts.Model;
using System.Collections.ObjectModel;

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
            
        }
        
        async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
           if (e.SelectedItem == null) { return; }
           var vmDetails = mv.getDetailsModel(e.SelectedItem);
           var contactDetails = new ContactDetails(vmDetails);         
            
           await Navigation.PushAsync(contactDetails);

        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewContact(new NewContactViewModel()));
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            PeopleListView.BeginRefresh();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                mv.createGrouping(false);
                PeopleListView.ItemsSource = mv.GroupedPeople;
            }
            else
            {                
                mv.createGrouping(true, e.NewTextValue);
                PeopleListView.ItemsSource = mv.GroupedPeople;
            }

            PeopleListView.EndRefresh();
        }
        
        private void Button_Clicked_1(object sender, EventArgs e)
        {
            var root = Navigation.NavigationStack[0];
            Navigation.InsertPageBefore(new GroupList(), root);
            Navigation.PopToRootAsync();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (mv.IsBusy == false && mv.People.Count == 0) {
                PeopleListView.BeginRefresh();
                await mv.PopulatePeople();
                PeopleListView.ItemsSource = mv.GroupedPeople;
                PeopleListView.EndRefresh();
            }
        }
    }
}
