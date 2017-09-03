using Contacts.Model;
using Contacts.View;
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
        public SelectMultipleBasePage<Person> MultiPage;
        public async Task<SelectMultipleBasePage<Person>> GetMultiplePage()
        {
            var items = new List<Person>();
            List<Person> people = await App.PersonDatabase.GetPeopleAsync();
            foreach (var item in people)
            {
                items.Add(item);

            }

            if (MultiPage == null)
            {
                MultiPage = new SelectMultipleBasePage<Person>(items) { Title = "Selecione as pessoas para o grupo" };
                
            }
            if (MultiPage != null && People != null)
            {
                for (int i = 0; i < MultiPage.WrappedItems.Count; i++)
                {
                    var person = People.Find(e => e.ID == MultiPage.WrappedItems[i].Item.ID);
                    if (person != null) { MultiPage.WrappedItems[i].IsSelected = true; }
                }
            }
            return MultiPage;
        }

        public void RepopulatePeople(List<Person> people)
        {
            People = new List<Person>();
            foreach (var a in people)
            {
                People.Add(a);
            }
        }
    }
}
