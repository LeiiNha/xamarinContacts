using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Newtonsoft.Json;

namespace Contacts.Model
{
    public class Group
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string PeopleBlobbed { get; set; }

             
        [Ignore]
        private List<Person> people { get; set; }
        [Ignore]
        public List<Person> People
        {
            get
            {
                if (PeopleBlobbed != null) { return JsonConvert.DeserializeObject<List<Person>>(PeopleBlobbed); }
                return people;

            }
            set
            {
                if (value.Count > 0) { PeopleBlobbed = JsonConvert.SerializeObject(value); }
                people = value;
            }
        }

        public string Name { get; set; }

    }
}
