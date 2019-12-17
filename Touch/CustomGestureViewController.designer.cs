// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Touch
{
    [Register ("CustomGestureViewController")]
    partial class CustomGestureViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView CheckboxImage { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (CheckboxImage != null) {
                CheckboxImage.Dispose ();
                CheckboxImage = null;
            }
        }
    }
}