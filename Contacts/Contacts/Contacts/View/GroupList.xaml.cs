using Contacts.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contacts.View
{

    public partial class GroupList : ContentPage
    {
        GroupListViewModel mv;
        public GroupList()
        {
            InitializeComponent();
            mv = new GroupListViewModel();
            BindingContext = mv;
        }

        private async void BtnContacts_Clicked(object sender, EventArgs e)
        {
            var root = Navigation.NavigationStack[0];
            Navigation.InsertPageBefore(new MainPage(), root);
            await Navigation.PopToRootAsync();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            GroupListView.BeginRefresh();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                GroupListView.ItemsSource = mv.Groups;
            else
                GroupListView.ItemsSource = mv.Groups.Where(i => i.Name.Contains(e.NewTextValue));

            GroupListView.EndRefresh();
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewGroup());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (mv.IsBusy == false && mv.Groups.Count == 0)
            {
                GroupListView.BeginRefresh();
                await mv.PopulateGroups();
                GroupListView.ItemsSource = mv.Groups;
                GroupListView.EndRefresh();
            }
        
        }

    }
}