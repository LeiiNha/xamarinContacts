using Contacts.ViewModel;
using System;
using System.Collections.Generic;
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
		public NewContact ()
		{
			InitializeComponent ();
            vm = new NewContactViewModel();
            BindingContext = vm;
		}

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            vm.AddToPeople();
            Navigation.PushAsync(new MainPage());
        }
    }
}