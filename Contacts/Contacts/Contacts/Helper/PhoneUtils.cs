using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Contacts.Helper
{
    enum PhoneActions
    {
        Call,
        SMS,
        Mail
        
    }

    static class PhoneUtils
    {
        public static void StartPhoneAction(String numberOrEmail, PhoneActions phoneAction)
        {
            switch (phoneAction)
            {
                case PhoneActions.Call:
                    Device.OpenUri(new Uri(String.Format("tel:{0}", numberOrEmail)));
                    break;
                case PhoneActions.SMS:
                    Device.OpenUri(new Uri(String.Format("smsto:{0}", numberOrEmail)));
                    break;
                case PhoneActions.Mail:
                    Device.OpenUri(new Uri(String.Format("mailto:{0}", numberOrEmail)));
                    break;
            }
            
        }
        
    }
}
