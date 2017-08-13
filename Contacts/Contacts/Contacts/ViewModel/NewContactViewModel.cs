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

        public NewContactViewModel()
        {
            

            for (int i = 0; i < 5; i++)
            {
                Person person = new Person();
                person.Name = "fernando " + i.ToString();
                person.Address = i.ToString() + " Omelete";
                person.ImageSource = "man" + i.ToString() + ".jpeg";
            }
        }
    }
}
