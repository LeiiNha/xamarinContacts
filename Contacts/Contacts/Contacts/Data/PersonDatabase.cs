using Contacts.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Data
{
    public class PersonDatabase
    {
        readonly SQLiteAsyncConnection database;

        public PersonDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Person>().Wait();
        }

        public Task<List<Person>> GetPeopleAsync()
        {
            return database.Table<Person>().ToListAsync();
        }

        public Task<Person> GetPersonAsync(int id)
        {
            return database.Table<Person>()
                .Where(i => i.ID == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SavePersonAsync(Person person)
        {
            if (person.ID != 0)
            {
                return database.UpdateAsync(person);
            } else
            {
                return database.InsertAsync(person);
            }
        }

        public Task<int> DeletePersonAsync(Person person)
        {
            return database.DeleteAsync(person);
        }
    }
}
