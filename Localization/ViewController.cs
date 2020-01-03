using Foundation;
using System;
using UIKit;

namespace Localization
{
    public partial class ViewController : UIViewController
    {
        public ViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            
        }


        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            var localizedString = NSBundle.MainBundle.GetLocalizedString("Have a nice day!");
            txtOutput.Text = localizedString;
            
        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}