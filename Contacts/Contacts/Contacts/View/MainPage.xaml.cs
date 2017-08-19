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
                mv.createGrouping(mv.People);
                PeopleListView.ItemsSource = mv.GroupedPeople;
            }
            else
            {
                ObservableCollection<Person> list = new ObservableCollection<Person>(mv.People.Where(i => i.FullName.Contains(e.NewTextValue)));
                mv.createGrouping(list);
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
    }
}
