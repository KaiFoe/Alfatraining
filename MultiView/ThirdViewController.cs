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
                //R�cksprung nur zur vorherigen View m�glich
                //this.DismissViewController(true, null);

                //�ber den NavigationController zur vorherigen View zur�ck
                this.NavigationController.PopViewController(true);
                //�ber den NavigationController zur RootView zur�ck
                //this.NavigationController.PopToRootViewController(true);
                //�ber den NavigationController zu einer beliebigen View zur�ck
                //this.NavigationController.PopToViewController("Controller der View", true);
            };
        }
    }
}