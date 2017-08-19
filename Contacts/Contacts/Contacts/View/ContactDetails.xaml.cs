﻿using Contacts.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contacts.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactDetails : ContentPage
    {
        ContactDetailsViewModel vm;
        public ContactDetails(ContactDetailsViewModel viewmodel)
        {
            InitializeComponent();
            BindingContext = viewmodel;
            vm = viewmodel;
            emailImg.GestureRecognizers.Add(new TapGestureRecognizer
            {
                NumberOfTapsRequired = 1,
                Command = vm.startEmailCommand
            });
        }

        private async void Edit_Clicked(object sender, EventArgs e)
        {
            var vmNewContact = new NewContactViewModel();
            vmNewContact.fillPerson(vm.person);            
            var newContact = new NewContact(vmNewContact);            
            await Navigation.PushAsync(newContact);   
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            await vm.deleteContact();
            await Navigation.PopAsync();
        }
    }
}