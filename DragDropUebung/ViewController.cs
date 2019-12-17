using CoreGraphics;
using Foundation;
using System;
using UIKit;

namespace DragDropUebung
{
    public partial class ViewController : UIViewController
    {
        private CGRect originalImageFrame = CGRect.Empty;

        public ViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            // Perform any additional setup after loading the view, typically from a nib.

            Title = "Gesture Recognizers";

            // Save initial state
            originalImageFrame = DragImage.Frame;


            WireUpDragGestureRecognizer();
        }


        private void HandleDrag(UIPanGestureRecognizer recognizer)
        {
            // If it's just began, cache the location of the image
            if (recognizer.State == UIGestureRecognizerState.Began)
            {
                originalImageFrame = DragImage.Frame;
            }

            // Move the image if the gesture is valid
            if (recognizer.State != (UIGestureRecognizerState.Cancelled | UIGestureRecognizerState.Failed
                | UIGestureRecognizerState.Possible))
            {
                // Move the image by adding the offset to the object's frame
                CGPoint offset = recognizer.TranslationInView(DragImage);
                CGRect newFrame = originalImageFrame;
                newFrame.Offset(offset.X, offset.Y);
                DragImage.Frame = newFrame;
            }
        }

        private void WireUpDragGestureRecognizer()
        {
            // Create a new tap gesture
            UIPanGestureRecognizer gesture = new UIPanGestureRecognizer();

            // Wire up the event handler (have to use a selector)
            gesture.AddTarget(() => HandleDrag(gesture));

            // Add the gesture recognizer to the view
            DragImage.AddGestureRecognizer(gesture);
        }

    }

    public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}