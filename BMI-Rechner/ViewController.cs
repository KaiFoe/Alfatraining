using Foundation;
using System;
using UIKit;

namespace BMI_Rechner
{
    public partial class ViewController : UIViewController
    {
        #region Fielddeclaration
        double weight, height;
        double bmi;
        #endregion

        public ViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            sldrWeight.ValueChanged += (object sender, EventArgs args) =>
            {
                weight = sldrWeight.Value;
            };

            sldrHeight.ValueChanged += (sender, args) =>
            {
                height = sldrHeight.Value;
            };

            btnCalculate.TouchUpInside += (sender, args) =>
            {
                CalculateBMI(weight, height);
                lblResult.Text = "Ihr BMI liegt bei " + bmi + "!";
            };

        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
            // Release any cached data, images, etc that aren't in use.
        }

        private void CalculateBMI(double weight, double height)
        {
            bmi = weight / (height / 100 * height / 100);
        }
    }
}