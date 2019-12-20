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

namespace Location
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnlocalize { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnStop { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblOutput { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        MapKit.MKMapView mapView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnlocalize != null) {
                btnlocalize.Dispose ();
                btnlocalize = null;
            }

            if (btnStop != null) {
                btnStop.Dispose ();
                btnStop = null;
            }

            if (lblOutput != null) {
                lblOutput.Dispose ();
                lblOutput = null;
            }

            if (mapView != null) {
                mapView.Dispose ();
                mapView = null;
            }
        }
    }
}