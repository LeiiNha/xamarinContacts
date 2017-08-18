using Contacts.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Data
{
    public class GroupDatabase
    {
        readonly SQLiteAsyncConnection database;

        public GroupDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Group>().Wait();
        }

        public Task<List<Group>> GetGroupsAsync()
        {
            return database.Table<Group>().ToListAsync();
        }

        public Task<Group> GetGroupnAsync(int id)
        {
            return database.Table<Group>()
                .Where(i => i.ID == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveGroupAsync(Group group)
        {
            if (group.ID != 0)
            {
                return database.UpdateAsync(group);
            }
            else
            {
                return database.InsertAsync(group);
            }
        }

        public Task<int> DeleteGroupAsync(Group group)
        {
            return database.DeleteAsync(group);
        }
        
    }
}
