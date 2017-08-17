using Contacts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Contacts
{
    public partial class App : Application
    {
        static PersonDatabase personDatabase;
        static GroupDatabase groupDatabase;
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage()); 
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static PersonDatabase PersonDatabase
        {
            get
            {
                if (personDatabase == null)
                {
                    personDatabase = new PersonDatabase(
                        DependencyService.Get<IFileHelper>().GetLocalFilePath("PersonSQLite.db3"));

                }
                return personDatabase;
            }
        }

        public static GroupDatabase GroupDataBase
        {
            get
            {
                if (groupDatabase == null)
                {
                    groupDatabase = new GroupDatabase(
                        DependencyService.Get<IFileHelper>().GetLocalFilePath("GroupSQLite.db3"));

                }
                return groupDatabase;
            }
        }
    }
}
