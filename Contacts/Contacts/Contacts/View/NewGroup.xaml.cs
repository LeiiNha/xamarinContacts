using Contacts.Model;
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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewGroup : ContentPage
    {
        NewGroupViewModel vm;
        public NewGroup()
        {
            InitializeComponent();
            vm = new NewGroupViewModel();
            BindingContext = vm;
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(NameEntry.Text) && (vm.People.Count != 0))
            {
                vm.AddToGroup();
                Navigation.PopAsync();
            }
            else
            {
                DisplayAlert("Campo", "preencha o nome e as pessoas do grupo!", "Ok");
            }
        }

        SelectMultipleBasePage<Person> multiPage;
        async void OnClick(object sender, EventArgs ea)
        {
            var items = new List<Person>();
            List<Person> people = await App.PersonDatabase.GetPeopleAsync();
            foreach (var item in people)
            {
                items.Add(item);
            }

            if (multiPage == null)
                multiPage = new SelectMultipleBasePage<Person>(items) { Title = "Selecione as pessoas para o grupo" };

            await Navigation.PushAsync(multiPage);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (multiPage != null)
            {
                results.Text = "";
                var answers = multiPage.GetSelection();
                vm.People = new List<Person>();
                foreach (var a in answers)
                {
                    vm.People.Add(a);
                    results.Text = a.FullName + Environment.NewLine;
                }
            }
            else
            {
                results.Text = "(none)";
            }
        }
    }
}