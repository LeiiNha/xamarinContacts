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

        public bool IsBusy { get; set; }

        public ObservableCollection<Person> People { get; set; }

        public ObservableCollection<Grouping<string, Person>> GroupedPeople { get; set; }

        public ICommand startCallCommand { get; set; }

        public MainPageViewModel()
        {
            People = new ObservableCollection<Person>();
            IsBusy = false;
            startCallCommand = new Command<Person>((model) => HandleCall(model));
        }

        public async Task PopulatePeople()
        {
            if (IsBusy == false)
            {
                IsBusy = true;
                People.Clear();
            
            List<Person> people = await App.PersonDatabase.GetPeopleAsync();
                foreach (var item in people)
                {
                    People.Add(item);
                }
            
                Person person = new Person();
                person.FirstName = "erica ";
                person.LastName = "Blah";
                person.Address = " Quack";
                person.ImageSource = "icon.png";
                person.PhoneNumber = new List<PhoneNumber>();
                person.PhoneNumber.Add(new PhoneNumber { Desc = "Casa", Number = "4332232" });
                People.Add(person);
                person = new Person();
                person.FirstName = "Erica";
                person.LastName = "Geraldes";
                person.Address = "Brasil";
                person.ImageSource = null;
                person.PhoneNumber = new List<PhoneNumber>();
                person.PhoneNumber.Add(new PhoneNumber { Desc = "Trabalho", Number = "2333222" });
                People.Add(person);
                createGrouping(false);
                IsBusy = false;
            }
        }

        public ContactDetailsViewModel getDetailsModel(object o)
        {
            Person person = (Person)o;
            var vmDetails = new ContactDetailsViewModel();
            vmDetails.person = person;
            return vmDetails;
        }
        
        public void createGrouping(bool filtered, string stringToSearch = "")
        {
            var list = People;
            if (filtered) 
                list = new ObservableCollection<Person>(People.Where(i => i.FullName.Contains(stringToSearch)));
            
            var sorted = from personn in list
                         orderby personn.FirstName
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
