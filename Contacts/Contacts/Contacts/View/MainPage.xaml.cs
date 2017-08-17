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
           /* MessagingCenter.Subscribe<MainPageViewModel>(new MainPageViewModel(), "ButtonClicked", (sender) =>
             {
                 DisplayAlert("Message", "Button Clicked!", "Ok");
             });

            PeopleListView.ItemsSource = mv.GroupedPeople;
            PeopleListView.IsGroupingEnabled = true;
            PeopleListView.GroupDisplayBinding = new Binding("Key");
            PeopleListView.GroupShortNameBinding = new Binding("Key");
            */
             
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
            {
                mv.createGrouping(mv.People);
                PeopleListView.ItemsSource = mv.GroupedPeople;
            }
            else
            {
                ObservableCollection<Person> list = new ObservableCollection<Person>(mv.People.Where(i => i.Name.Contains(e.NewTextValue)));
                mv.createGrouping(list);
                PeopleListView.ItemsSource = mv.GroupedPeople;


            }

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
