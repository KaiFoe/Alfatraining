using Foundation;
using System;
using UIKit;

namespace MultiView
{
    public partial class SecondViewController : UIViewController
    {
        IntPtr handle;
        public SecondViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            btnThirdView.TouchUpInside += (sender, args) =>
            {
                UIStoryboard storyboard = this.Storyboard;
                //ThirdViewController thirdViewController = new ThirdViewController(handle);

                var thirdViewController = storyboard.InstantiateViewController("ThirdViewController") as UIViewController;
                //this.PresentViewController(thirdViewController, true, null);

                this.NavigationController.PushViewController(thirdViewController, true);
            };
        }
    }
}