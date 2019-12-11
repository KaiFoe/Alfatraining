using Foundation;
using System;
using UIKit;

namespace MultiView
{
    public partial class ThirdViewController : UIViewController
    {
        public ThirdViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            btnBackSecondView.TouchUpInside += (sender, args) =>
            {
                //Rücksprung nur zur vorherigen View möglich
                //this.DismissViewController(true, null);

                //über den NavigationController zur vorherigen View zurück
                this.NavigationController.PopViewController(true);
                //über den NavigationController zur RootView zurück
                //this.NavigationController.PopToRootViewController(true);
                //über den NavigationController zu einer beliebigen View zurück
                //this.NavigationController.PopToViewController("Controller der View", true);
            };
        }
    }
}