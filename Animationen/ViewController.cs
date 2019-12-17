using CoreAnimation;
using CoreGraphics;
using Foundation;
using System;
using System.Threading;
using UIKit;

namespace Animationen
{
    public partial class ViewController : UIViewController
    {
        CALayer layer;
        UIImageView imageView;
        UIImage image;

        public ViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            imageView = new UIImageView(new CGRect(0, 100, 50, 50));
            image = UIImage.FromBundle("Default.png");
            imageView.Image = image;
            imageView.Alpha = 0.25f;
            View.AddSubview(imageView);

            Action setCenterRight = () =>
            {
                var xpos = UIScreen.MainScreen.Bounds.Right - imageView.Frame.Width / 2;
                var ypos = imageView.Center.Y;
                imageView.Center = new CGPoint(xpos, ypos);
            };

            Action setCenterLeft = () =>
            {
                var xpos = UIScreen.MainScreen.Bounds.Left + imageView.Frame.Width / 2;
                var ypos = imageView.Center.Y;
                imageView.Center = new CGPoint(xpos, ypos);
            };

            Action setOpacity = () =>
            {
                imageView.Alpha = 1;
            };

            UIViewPropertyAnimator propertyAnimator = new UIViewPropertyAnimator(4, UIViewAnimationCurve.EaseInOut, setCenterRight);
            propertyAnimator.AddAnimations(setOpacity);

            Action<object> reversePosition = (o) =>
            {
                InvokeOnMainThread(() =>
                {
                    propertyAnimator.AddAnimations(setCenterLeft);
                });
            };

            TimerCallback abortPositionDelegate = new TimerCallback(reversePosition);
            Timer abortPosition = new Timer(abortPositionDelegate, null, 3000, Timeout.Infinite);
            propertyAnimator.StartAnimation();


            // Perform any additional setup after loading the view, typically from a nib.

            /*layer = new CALayer();
            layer.Bounds = new CGRect(0, 0, 80, 80);
            layer.Position = new CGPoint(100, 100);
            layer.Contents = UIImage.FromBundle("Default.png").CGImage;
            layer.ContentsGravity = CALayer.GravityResizeAspectFill;

            View.Layer.AddSublayer(layer);

            /*UIImageView imageView= new UIImageView(new CGRect(50, 50, 57, 57));
            UIImage image = UIImage.FromBundle("Default.png");
            imageView.Image = image;
            View.AddSubview(imageView);

            CGPoint point = imageView.Center;
            UIView.Animate(2, 0, UIViewAnimationOptions.CurveEaseInOut | UIViewAnimationOptions.Autoreverse,
                () =>
                {
                    imageView.Center = new CGPoint(UIScreen.MainScreen.Bounds.Right - imageView.Frame.Width / 2, imageView.Center.Y);
                },
                () =>
                {
                    imageView.Center = point;
                }
            );*/



        }
        #region implizite Animation
        /* public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            CATransaction.Begin();
            CATransaction.AnimationDuration = 2;
            layer.Position = new CoreGraphics.CGPoint(100, 400);
            CATransaction.Commit();
        } */
        #endregion

        #region explizite Animation
        /*public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            var position = layer.Position;
            layer.Position = new CGPoint(150, 350);


            CGPath path = new CGPath();
            path.AddLines(new CGPoint[] { position, layer.Position });

            CAKeyFrameAnimation animation = (CAKeyFrameAnimation)CAKeyFrameAnimation.FromKeyPath("position");
            animation.Path = path;

            layer.Transform = CATransform3D.MakeRotation((float)Math.PI * 2, 0, 0, 1);
            var animRotate = (CAKeyFrameAnimation)CAKeyFrameAnimation.FromKeyPath("transform");
            animRotate.Values = new NSObject[]
            {
                NSNumber.FromFloat (0f),
                NSNumber.FromFloat((float)Math.PI / 2f),
                NSNumber.FromFloat((float)Math.PI),
                NSNumber.FromFloat((float)Math.PI * 2f),
            };
            animRotate.ValueFunction = CAValueFunction.FromName(CAValueFunction.RotateX);

            var animationGroup = CAAnimationGroup.CreateAnimation();
            animationGroup.Duration = 5;
            animationGroup.Animations = new CAAnimation[] { animation, animRotate };

            var basicAnimation = CABasicAnimation.FromKeyPath("position)");
            
            basicAnimation.TimingFunction = CAMediaTimingFunction.FromName(CAMediaTimingFunction.EaseInEaseOut);
            basicAnimation.From = NSValue.FromCGPoint(position);
            basicAnimation.To = NSValue.FromCGPoint(new CGPoint(150, 350));
            basicAnimation.Duration = 10;

            layer.AddAnimation(animationGroup, null);
        }*/
        #endregion
        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}