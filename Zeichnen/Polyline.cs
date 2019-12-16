using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Zeichnen
{
    class Polyline
    {
        public CGColor Color { get; set; }
        public float StrokeWidth { get; set; }
        public CGPath Path {get;set;}

        public Polyline()
        {
            Path = new CGPath();
        }
    }
}