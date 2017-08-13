using Contacts.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.ViewModel
{
    class MainPageViewModel
    {
        public string Prompt { get; set; }

        public string Name { get; set; }

        public ObservableCollection<Person> People { get; set; } = new ObservableCollection<Person>();

        public MainPageViewModel()
        {
            Prompt = "Full Name: ";
            Name = "Jesse Liberty";

            for (int i = 0; i < 5; i++)
            {
                Person person = new Person();
                person.Name = "erica " + i.ToString();
                person.Address = i.ToString() + " Quack";
                person.ImageSource = "man" + i.ToString() + ".jpeg";
                People.Add(person);
            }
        }
    }
}
