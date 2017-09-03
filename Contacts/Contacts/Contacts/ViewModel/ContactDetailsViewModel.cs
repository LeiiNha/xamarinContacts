using Contacts.Helper;
using Contacts.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contacts.ViewModel
{

    public class ContactDetailsViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Person _person { get; set; }
        public Person person {
            get
            {
                return _person;
            }
            set
            {
                _person = value;
                NotifyPropertyChanged("person");
            }
        }
        public ICommand startCallCommand { get; set; }
        public ICommand startEmailCommand { get; set; }
        public ICommand startSMSCommand { get; set; }

        public ContactDetailsViewModel()
        {
            startEmailCommand = new Command(HandleEmail);            
            startSMSCommand = new Command<PhoneNumber>((model) => HandleSMS(model));
            startCallCommand = new Command<PhoneNumber>((model => HandleCall(model)));
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
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

        public async Task refreshContact()
        {
            person = await App.PersonDatabase.GetPersonAsync(person.ID);            
        }
        

    }
}
