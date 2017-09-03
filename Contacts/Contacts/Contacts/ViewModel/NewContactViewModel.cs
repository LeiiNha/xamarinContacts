using Contacts.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.ViewModel
{
    public class NewContactViewModel
    {
        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string ImageSource { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        //public List<PhoneNumber> PhoneNumber { get; set; }
        public ObservableCollection<PhoneNumber> PhoneNumber { get; set; }

        public NewContactViewModel()
        {
            if (PhoneNumber == null) { PhoneNumber = new ObservableCollection<PhoneNumber>(); }
            AddNewNumber();
        }

        public void fillPerson(Person person)
        {
            FirstName = person.FirstName;
            LastName = person.LastName;
            Email = person.Email;
            Address = person.Address;
            ImageSource = person.ImageSource;
            Longitude = person.Longitude;
            Latitude = person.Latitude;
            PhoneNumber = new ObservableCollection<PhoneNumber>(person.PhoneNumber);
            id = person.ID;
        }


        public void AddToPeople()
        {
            Person person = new Person();
            person.FirstName = FirstName;
            person.LastName = LastName;
            person.Email = Email;
            person.ID = id;
            person.Address = Address;
            person.ImageSource = ImageSource;
            person.Latitude = Latitude;
            person.Longitude = Longitude;
            person.PhoneNumber = new List<Model.PhoneNumber>(PhoneNumber);
            App.PersonDatabase.SavePersonAsync(person);
           
        }

        public void AddNewNumber()
        {
            PhoneNumber.Add(new PhoneNumber { Desc = "Casa", Number = "" });
        }

        
    }
}
