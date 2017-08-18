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

        private void BtnContacts_Clicked(object sender, EventArgs e)
        {
            var root = Navigation.NavigationStack[0];
            Navigation.InsertPageBefore(new MainPage(), root);
            Navigation.PopToRootAsync();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
           /* GroupList.BeginRefresh();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                PeopleListView.ItemsSource = mv.Groups;
            else
                PeopleListView.ItemsSource = mv.Groups.Where(i => i.Name.Contains(e.NewTextValue));

            PeopleListView.EndRefresh();*/
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewGroup());
        }
    }
}