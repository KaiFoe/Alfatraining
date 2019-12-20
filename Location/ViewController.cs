using CoreLocation;
using Foundation;
using MapKit;
using System;
using UIKit;

namespace Location
{
    public partial class ViewController : UIViewController
    {
        public static double AccuracyHundredMeters { get; }
        private CLLocationManager locationManager;
        private CLLocation location;
        //public MKCoordinateRegion region;

        public ViewController(IntPtr handle) : base(handle)
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            MapDelegate mapDelegate = new MapDelegate();
            mapView.Delegate = mapDelegate;


            MKCoordinateRegion region = new MKCoordinateRegion();

            #region Location ausgeben (Text)
            locationManager = new CLLocationManager();

            locationManager.RequestAlwaysAuthorization();

            locationManager.LocationsUpdated += (sender, args) =>
            {
                location = args.Locations[0];
                double latitude = Math.Round(location.Coordinate.Latitude, 4);
                double longitude = Math.Round(location.Coordinate.Longitude, 4);
                double accuracy = Math.Round(location.HorizontalAccuracy, 0);


                lblOutput.Text = string.Format("Latitude: {0}\nLongitude: {1}\nAccuracy: {2}m", latitude, longitude, accuracy);
                Console.WriteLine("Zeitstempel Standort {0}", location.Timestamp);

                //region = new MKCoordinateRegion();
                //CLLocation secondlocation = new CLLocation(48.88370, 2.294481);
                region.Center = location.Coordinate;

                mapView.SetRegion(region, true);


            };

            locationManager.Failed += (sender, args) =>
            {
                lblOutput.Text = string.Format("Standortbestimmung fehlgeschlagen! \nFehlermeldung: {0}", args.Error.LocalizedDescription);
            };

            btnlocalize.TouchUpInside += (sender, args) =>
            {
                lblOutput.Text = "Bestimme Standort...";
                locationManager.StartUpdatingLocation();
            };

            btnStop.TouchUpInside += (sender, args) =>
            {
                locationManager.StopUpdatingLocation();
                lblOutput.Text = "Standortbestimmung beendet...";
            };
            #endregion

            #region Location in Map ausgeben

            //Aktuelle Position anzeigen
            mapView.ShowsUserLocation = true;
            mapView.ZoomEnabled = true;

            mapView.MapType = MKMapType.Standard;

            //Karte auf der Position zentrieren
            //MKCoordinateRegion region = new MKCoordinateRegion();
            CLLocation secondlocation = new CLLocation(37.687834, -122.406417);
            region.Center = secondlocation.Coordinate;

            //Zoom-Level setzen
            MKCoordinateSpan span = new MKCoordinateSpan();
            span.LatitudeDelta = 0.5;
            span.LongitudeDelta = 0.5;
            region.Span = span;

            mapView.SetRegion(region, true);

            //Pin auf Karte setzen
            mapView.AddAnnotation(new MKPointAnnotation()
            {
                Title = "Sehenswürdigkeit",
                Coordinate = new CLLocationCoordinate2D(37.687834, -122.406417)
            });

            MKPointAnnotation myAnnotation = new MKPointAnnotation();
            myAnnotation.Coordinate = new CLLocationCoordinate2D(37.687834, -122.406417);
            myAnnotation.Title = "Unsere Position";

            MKPointAnnotation mysecondAnnotation = new MKPointAnnotation();
            mysecondAnnotation.Coordinate = new CLLocationCoordinate2D(37.787834, -122.406417);
            mysecondAnnotation.Title = "Unsere Position";
            mapView.AddAnnotations(new IMKAnnotation[] { myAnnotation, mysecondAnnotation });

            //mapView.GetViewForAnnotation += (mapView, annotation) =>
            //{
            //    string pID = "PinAnnotation";
            //    if (annotation is MKPointAnnotation)
            //    {
            //        MKPinAnnotationView pinView = (MKPinAnnotationView)mapView.DequeueReusableAnnotation(pID);
            //        if (pinView == null)
            //            pinView = new MKPinAnnotationView(annotation, pID);

            //        pinView.PinColor = MKPinAnnotationColor.Green;
            //        pinView.CanShowCallout = true;
            //        pinView.RightCalloutAccessoryView = UIButton.FromType(UIButtonType.DetailDisclosure);
            //        pinView.LeftCalloutAccessoryView = new UIImageView(UIImage.FromBundle("Default.png"));
            //        return pinView;
            //    }
            //    else
            //    {
            //        return null;
            //    }
            //};

            //var circleOverlay = MKCircle.Circle(secondlocation.Coordinate, 1000);
            //mapView.AddOverlay(circleOverlay);

            //var circleView = new MKCircleView(circleOverlay);
            //circleView.FillColor = UIColor.Blue;
            //mapView.AddOverlay(mapView,  circleView);

            MKPolygon hotelOverlay = MKPolygon.FromCoordinates(
                new CLLocationCoordinate2D[]
                {
                    new CLLocationCoordinate2D(37.787834, -122.406417),
                    new CLLocationCoordinate2D(37.797834, -122.406417),
                    new CLLocationCoordinate2D(37.797834, -122.416417),
                    new CLLocationCoordinate2D(37.787834, -122.416417),
                    new CLLocationCoordinate2D(37.787834, -122.406417),
                });
            mapView.AddOverlay(hotelOverlay);
            #endregion

            #region Suchleiste in der Map
            //var searchResultContainer = new SearchResultsViewController(mapView);
            var searchUpdater = new UISearchResultsUpdating();

            UISearchController searchController = new UISearchController(searchResultsController: null);
            
            #endregion
        }


        //string pID = "PinAnnotation";
        //protected override MKAnnotationView GetViewForAnnotation(MKMapView mapView, NSObject annotation)
        //{
        //    if (annotation is MKUserLocation)
        //        return null;

        //    MKAnnotationView pinView = (MKPinAnnotationView)mapView.DequeueReusableAnnotation(pID);

        //    if (pinView == null)
        //        pinView = new MKPinAnnotationView(annotation, pID);

        //    ((MKPinAnnotationView)pinView).PinColor = MKPinAnnotationColor.Green;
        //    pinView.CanShowCallout = true;

        //    return pinView;

        //   }
        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }


    }
}