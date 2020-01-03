using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Contacts.Helpers
{
    public static class Helper
    {
        public static void ShowAlert(this UIViewController controller, string message)
        {
            var alert = UIAlertController.Create("Status", message, UIAlertControllerStyle.ActionSheet);
            alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            controller.PresentViewController(alert, true, null);
        }
    }
}