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
    class GroupListViewModel
    {
        public ObservableCollection<Group> Groups { get; set; } = new ObservableCollection<Group>();

        public ICommand startEmailCommand { get; set; }

        public ICommand startSMSCommand { get; set; }

        public GroupListViewModel()
        {

            Group group = new Group();
            Person person = new Person();
            person.FirstName = "erica ";
            person.LastName = "Blah";
            person.Address = " Quack";
            person.ImageSource = "man1.jpeg";
            person.PhoneNumber = new List<PhoneNumber>();
            person.PhoneNumber.Add(new PhoneNumber { Desc = "Casa", Number = "4332232" });
            group.People = new List<Person>();
            group.People.Add(person);
            group.Name = "Grupo blabla";
            Groups.Add(group);

            PopulatePeople();

            startEmailCommand = new Command<Group>((model) => HandleEmail(model));
            startSMSCommand = new Command<Group>((model) => HandleSMS(model));
        }

        private async void PopulatePeople()
        {
            List<Group> groups = await App.GroupDataBase.GetGroupsAsync();
            foreach (Group group in groups)
            {
                Groups.Add(group);
            }
        }

        private void HandleSMS(Group group)
        {
            var phones = "";
            foreach (var person in group.People)
            {
                phones += person.PhoneNumber[0].Number + ';';
            }            
            PhoneUtils.StartPhoneAction(phones, PhoneActions.SMS);
        }

        private void HandleEmail(Group group)
        {
            var emails = "";
            if (group.People.Count > 1)
            {
                emails = group.People[0].Email + "?cc=";
            }
            for (int i = 1; i < group.People.Count; i++)
            {
                emails += group.People[i].Email + ";";
            }
            PhoneUtils.StartPhoneAction(emails, PhoneActions.Mail);
        }



    }
}
