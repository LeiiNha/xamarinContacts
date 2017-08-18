using Contacts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.ViewModel
{
    public class NewContactViewModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public List<PhoneNumber> PhoneNumber { get; set; }

        public NewContactViewModel()
        {
            if (PhoneNumber == null) { PhoneNumber = new List<Model.PhoneNumber>(); }            
        }


        public void AddToPeople()
        {
            Person person = new Person();
            person.Name = Name;
            person.Email = Email;
            person.ID = id;
            person.Address = Address;
            person.PhoneNumber = PhoneNumber;
            App.PersonDatabase.SavePersonAsync(person);
        }

        public void AddNewNumber()
        {
            PhoneNumber.Add(new PhoneNumber { Desc = "Casa", Number = "" });
        }

        
    }
}
