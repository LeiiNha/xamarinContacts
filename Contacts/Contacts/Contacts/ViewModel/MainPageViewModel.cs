using Contacts.Helper;
using Contacts.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contacts.ViewModel
{
    class MainPageViewModel
    {
        public string Prompt { get; set; }

        public string Name { get; set; }

        public ObservableCollection<Person> People { get; set; } = new ObservableCollection<Person>();

        public ICommand startCallCommand { get; set; }

        public MainPageViewModel()
        {
            
            Person person = new Person();
            person.Name = "erica ";
            person.Address = " Quack";
            person.ImageSource = "man1.jpeg";
            person.PhoneNumber = "283772922";
            People.Add(person);

            PopulatePeople();
            
            startCallCommand = new Command<Person>((model) => HandleCall(model));
        }

        private async void PopulatePeople()
        {
            List<Person> people = await App.Database.GetPeopleAsync();
            foreach (Person person in people)
            {
                People.Add(person);
            }
        }

        private void HandleCall(Person person)
        {
            PhoneUtils.StartPhoneAction(person.PhoneNumber, PhoneActions.Call);

        }

       

 

    }

}
