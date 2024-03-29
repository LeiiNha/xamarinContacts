﻿using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Model
{
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string FirstName
        {
            get;
            set;
        }
        
        public string LastName { get; set; }

        public string Email
        {
            get;
            set;
        }
        public string Address
        {
            get;
            set;
        }

        public string PhoneNumbersBlobbed { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        [Ignore]
        public bool NoImage
        {
            get
            {
                if (String.IsNullOrEmpty(ImageSource)) { return true; } else { return false; }
            }
        }
        [Ignore]
        public bool HasImage
        {
            get
            {
                if (!String.IsNullOrEmpty(ImageSource)) { return true; } else { return false; }
            }
        }

        [Ignore]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        [Ignore]
        public string Initials
        {
            get
            {
                return FirstName.Substring(0,1).ToUpper() + LastName.Substring(0,1).ToUpper();
            }
        }

        [Ignore]
        private List<PhoneNumber> phoneNumber { get; set; }
        [Ignore]
        public List<PhoneNumber> PhoneNumber
        {
            get
            {
                if (PhoneNumbersBlobbed != null) { return JsonConvert.DeserializeObject<List<PhoneNumber>>(PhoneNumbersBlobbed); }
                return phoneNumber;

            }
            set
            {
                if (value.Count > 0) { PhoneNumbersBlobbed = JsonConvert.SerializeObject(value); }
                phoneNumber = value;
            }
        }
        public string ImageSource
        {
            get;
            set;
        }

        public string NameSort
        {
            get
            {
                if (string.IsNullOrEmpty(FirstName) || FirstName.Length == 0) return "?";

                return FirstName[0].ToString().ToUpper();
            }
        }



    }
}
