using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using CoreText;
using Foundation;
using UIKit;

namespace Drawing
{
    public class TextDrawingView : UIView
    {
        public TextDrawingView()
        {

        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            var gctx = UIGraphics.GetCurrentContext();
            gctx.TranslateCTM(10, 0.5f * Bounds.Height);
            gctx.ScaleCTM(1, -1);
            gctx.RotateCTM((float)Math.PI * 315 / 180);

            gctx.SetFillColor(UIColor.Green.CGColor);
            string text = "Alfatraining";

            var attributedString = new NSAttributedString(text,
                new CoreText.CTStringAttributes
                {
                    ForegroundColorFromContext = true,
                    Font = new CTFont("Arial", 24)
                });

            using (var textLine = new CTLine(attributedString))
            {
                textLine.Draw(gctx);
            }
        }
    }
}