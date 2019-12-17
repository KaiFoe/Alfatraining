using Foundation;
using System;
using UIKit;

namespace Drawing
{
    public partial class ViewController : UIViewController
    {

        TransparentRegionView v;
        TextDrawingView textDrawingView;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            v = new TransparentRegionView();
            v.Frame = UIScreen.MainScreen.Bounds;
            View.AddSubview(v);

            textDrawingView = new TextDrawingView()
            {
                Frame = View.Frame
            };
            View.AddSubview(textDrawingView);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }
    }
}