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
   public class GroupListViewModel
    {
        public ObservableCollection<Group> Groups { get; set; }

        public ICommand startEmailCommand { get; set; }

        public bool IsBusy { get; set; }

        public ICommand startSMSCommand { get; set; }
               

        public GroupListViewModel()
        {
            Groups = new ObservableCollection<Group>();
            IsBusy = false;
          
            startEmailCommand = new Command<Group>((model) => HandleEmail(model));
            startSMSCommand = new Command<Group>((model) => HandleSMS(model));
        }

        public async Task PopulateGroups()
        {
            if (IsBusy == false)
            {
                IsBusy = true;
                Groups.Clear();

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

                List<Group> groups = await App.GroupDataBase.GetGroupsAsync();
                foreach (Group item in groups)
                {
                    Groups.Add(item);
                }
                    IsBusy = false;
            }
        }   
        
        public async Task deleteGroup(object groupObjc)
        {
            var group = (Group)groupObjc;
            await App.GroupDataBase.DeleteGroupAsync(group);
        }

        public NewGroupViewModel editGroup(object groupObjc)
        {
            var group = (Group)groupObjc;
            var _viewModel = new NewGroupViewModel();
            _viewModel.Name = group.Name;
            _viewModel.People = group.People;
            _viewModel.ID = group.ID;
            return _viewModel;
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
