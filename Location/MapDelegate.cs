using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using MapKit;
using UIKit;

namespace Location
{
    class MapDelegate : MKMapViewDelegate
    {

        public override MKOverlayView GetViewForOverlay(MKMapView mapView, IMKOverlay overlay)
        {

            MKPolygon polygon = overlay as MKPolygon;
            MKPolygonView mKPolygonView = new MKPolygonView(polygon);
            mKPolygonView.FillColor = UIColor.Red;
            mKPolygonView.StrokeColor = UIColor.Blue;
            return mKPolygonView;
        }

        //public override MKOverlayView GetViewForOverlay(MKMapView mapView, NSObject overlay)
        //{
        //    var circleOverlay = overlay as MKCircle;
        //    var circleView = new MKCircleView(circleOverlay);
        //    circleView.FillColor = UIColor.Blue;
        //    return circleView;
        //}
    }
}