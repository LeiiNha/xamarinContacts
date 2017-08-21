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
        public NewGroup(NewGroupViewModel vmPar)
        {
            InitializeComponent();
            vm = vmPar;
            BindingContext = vm;
            LoadPeople();
        }
        private void LoadPeople()
        {
            if (vm.People != null)
            {
                foreach (var a in vm.People)
                {
                    results.Text = a.FullName + Environment.NewLine;
                }
            }
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
            {
                multiPage = new SelectMultipleBasePage<Person>(items) { Title = "Selecione as pessoas para o grupo" };
                for (int i = 0; i < multiPage.WrappedItems.Count; i++)
                {
                    var person = vm.People.Find(e => e.ID == multiPage.WrappedItems[i].Item.ID);
                    if (person != null) { multiPage.WrappedItems[i].IsSelected = true; }
                }
                    
            }
             

            

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
                results.Text = "Ninguém selecionado";
            }
        }
    }
}