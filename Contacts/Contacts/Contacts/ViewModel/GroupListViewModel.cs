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
            person.Name = "erica ";
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
                phones += phones + person.PhoneNumber + ';';
            }            
            PhoneUtils.StartPhoneAction(phones, PhoneActions.SMS);
        }

        private void HandleEmail(Group group)
        {
            var emails = "";
            foreach (var person in group.People)
            {
                emails += emails + person.Email + ';';
            }
            PhoneUtils.StartPhoneAction(emails, PhoneActions.Mail);
        }



    }
}
