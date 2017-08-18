using Contacts.Helper;
using Contacts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contacts.ViewModel
{

    public class ContactDetailsViewModel
    {
        public Person person { get; set; }
        public ICommand startCallCommand { get; set; }
        public ICommand startEmailCommand { get; set; }
        public ICommand startSMSCommand { get; set; }

        public ContactDetailsViewModel()
        {
            startEmailCommand = new Command(HandleEmail);            
            startSMSCommand = new Command<PhoneNumber>((model) => HandleSMS(model));
            startCallCommand = new Command<PhoneNumber>((model => HandleCall(model)));
        }

        private void HandleSMS(PhoneNumber phonenumber)
        {
            PhoneUtils.StartPhoneAction(phonenumber.Number, PhoneActions.SMS);
        }

        private void HandleEmail()
        {
            
            PhoneUtils.StartPhoneAction(person.Email, PhoneActions.Mail);
        }

        private void HandleCall(PhoneNumber phonenumber)
        {
            PhoneUtils.StartPhoneAction(phonenumber.Number, PhoneActions.Call);
        }
        
        public async Task<int> deleteContact()
        {
            return await App.PersonDatabase.DeletePersonAsync(person);
        }

    }
}
