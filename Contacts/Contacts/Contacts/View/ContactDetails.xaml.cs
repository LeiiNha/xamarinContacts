using Contacts.Model;
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
    public partial class ContactDetails : ContentPage
    {
        Person person;
        public ContactDetails(Person personPar)
        {
            InitializeComponent();
            person = personPar;
            DisplayAlert("Detalhes", person.Name, "OK");
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {

        }
    }
}