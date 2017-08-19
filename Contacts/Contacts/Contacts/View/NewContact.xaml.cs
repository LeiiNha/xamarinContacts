using Contacts.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contacts
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewContact : ContentPage
	{
        NewContactViewModel vm;

        

        public NewContact (NewContactViewModel vmPar)
		{
			InitializeComponent ();
            vm = vmPar;
            BindingContext = vm;
		}

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
          
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            vm.AddToPeople();
            Navigation.PopAsync();
            
        }

        private void AddNewNumber_Clicked(object sender, EventArgs e)
        {
            NumbersList.BeginRefresh();
            vm.AddNewNumber();
            NumbersList.EndRefresh();
        }

        private void PhoneDescPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            var selectedItem = (string)picker.SelectedItem;
            vm.PhoneNumber.Last().Desc = selectedItem;

        }
    }
}