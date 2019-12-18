using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Json
{
    public class CustomCell : UITableViewCell
    {
        UILabel headingLabel, subheadingLabel;
        UIImageView imageView;

        public CustomCell(string cellId) : base(UITableViewCellStyle.Default, cellId)
        {
            ContentView.BackgroundColor = UIColor.FromRGB(218, 255, 127);
            imageView = new UIImageView();
            headingLabel = new UILabel()
            {
                Font = UIFont.FromName("Arial", 22f),
                TextColor = UIColor.FromRGB(127, 51, 0),
                BackgroundColor = UIColor.Clear
            };
            subheadingLabel = new UILabel()
            {
                Font = UIFont.FromName("Arial", 12f),
                TextColor = UIColor.FromRGB(127, 51, 120),
                BackgroundColor = UIColor.Clear,
                TextAlignment = UITextAlignment.Center
            };
            ContentView.AddSubviews(new UIView[] { headingLabel, subheadingLabel, imageView });
        }

        public void UpdateCell (string caption, string subtitle, UIImage image)
        {
            imageView.Image = image;
            headingLabel.Text = caption;
            subheadingLabel.Text = subtitle;
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            imageView.Frame = new CGRect(ContentView.Bounds.Width - 63, 5, 33, 33);
            headingLabel.Frame = new CGRect(5, 4, ContentView.Bounds.Width - 63, 25);
            subheadingLabel.Frame = new CGRect(110, 28, 100, 20);
        }
    }
}
        