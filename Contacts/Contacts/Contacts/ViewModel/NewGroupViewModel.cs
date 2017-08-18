using Contacts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.ViewModel
{
    class NewGroupViewModel
    {
        public string Name { get; set; }
        public List<Person> People { get; set; }


        public void AddToGroup()
        {
            Group grou = new Group();
            grou.Name = "teste2";
            grou.People = People;
            App.GroupDataBase.SaveGroupAsync(grou);
            
        }
    }
}
