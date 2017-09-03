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
                    results.Text += a.FullName + Environment.NewLine;
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

        
        async void OnClick(object sender, EventArgs ea)
        {

            var multiPage = await vm.GetMultiplePage();       

            await Navigation.PushAsync(multiPage);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (vm.MultiPage != null)
            {
                results.Text = "";
                var answers = vm.MultiPage.GetSelection();                
                vm.RepopulatePeople(answers);
                foreach (var a in answers)
                {
                    results.Text += a.FullName + Environment.NewLine;
                }
            }
        }
    }
}