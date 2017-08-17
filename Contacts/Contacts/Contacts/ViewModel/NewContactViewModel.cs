using Contacts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.ViewModel
{
    class NewContactViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }


        public void AddToPeople()
        {
            Person person = new Person();
            person.Name = Name;
            person.Email = Email;
            person.Address = Address;
            person.PhoneNumber = PhoneNumber;
            App.PersonDatabase.SavePersonAsync(person);
        }

        
    }
}
