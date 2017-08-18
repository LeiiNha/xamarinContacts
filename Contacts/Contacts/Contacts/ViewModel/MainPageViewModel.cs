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

        public ObservableCollection<Grouping<string, Person>> GroupedPeople { get; set; }

        public ICommand startCallCommand { get; set; }

        public MainPageViewModel()
        {
            
            

            PopulatePeople();

            
            
            startCallCommand = new Command<Person>((model) => HandleCall(model));
        }

        private async void PopulatePeople()
        {
            List<Person> people = await App.PersonDatabase.GetPeopleAsync();
            Person person = new Person();
            person.Name = "erica ";
            person.Address = " Quack";
            person.ImageSource = "man1.jpeg";
            person.PhoneNumber = new List<PhoneNumber>();
            person.PhoneNumber.Add(new PhoneNumber { Desc = "Casa", Number = "4332232" });
            People.Add(person);
            foreach (Person person2 in people)
            {
                People.Add(person2);
            }
            createGrouping(People);  
        }

        public void createGrouping(ObservableCollection<Person> list)
        {
            var sorted = from personn in list
                         orderby personn.Name
                         group personn by personn.NameSort into personGroup
                         select new Grouping<string, Person>(personGroup.Key, personGroup);
            GroupedPeople = new ObservableCollection<Grouping<string, Person>>(sorted);
        }

        private void HandleCall(Person person)
        {
            PhoneUtils.StartPhoneAction(person.PhoneNumber[0].Number, PhoneActions.Call);

        }

       

 

    }

}
