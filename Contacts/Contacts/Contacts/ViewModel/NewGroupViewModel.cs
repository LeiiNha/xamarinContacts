using Contacts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.ViewModel
{
    public class NewGroupViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Person> People { get; set; }


        public void AddToGroup()
        {
            Group grou = new Group();
            grou.Name = Name;
            grou.ID = ID;
            grou.People = People;
            App.GroupDataBase.SaveGroupAsync(grou);
            
        }

    }
}
