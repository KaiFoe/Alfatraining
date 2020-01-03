using CoreLocation;
using MapKit;
using System;
using System.Linq;
using UIKit;
using Xamarin.Essentials;

namespace MapDemo
{
    public partial class MapViewController : UIViewController
    {
        private readonly CLLocationManager locationManager = new CLLocationManager();

        private UISearchController searchController;

        private MapViewDelegate mapViewDelegate;

        public CLLocationCoordinate2D newLocation;
        public MapViewController (IntPtr handle) : base (handle) { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            locationManager.RequestWhenInUseAuthorization();

            // set map center and region
            const double lat = 42.374260;
            const double lon = -71.120824;
            var mapCenter = new CLLocationCoordinate2D(lat, lon);
            var mapRegion = MKCoordinateRegion.FromDistance(mapCenter, 2000, 2000);
            map.CenterCoordinate = mapCenter;
            map.Region = mapRegion;

            // add an annotation
            map.AddAnnotation(new MKPointAnnotation
            {
                Title = "MyAnnotation",
                Coordinate = new CLLocationCoordinate2D(42.364260, -71.120824)
            });

            // set the map delegate
            mapViewDelegate = new MapViewDelegate();
            map.Delegate = mapViewDelegate;

            // add a custom annotation
            map.AddAnnotation(new MonkeyAnnotation("Xamarin", mapCenter));

            // add an overlay
            map.AddOverlay(MKCircle.Circle(mapCenter, 1000));

            // add search
            var searchResultsController = new SearchResultsViewController(map);

            var searchUpdater = new SearchResultsUpdator();
            searchUpdater.UpdateSearchResults += searchResultsController.UpdateSearchResults;

            // add the search controller
            searchController = new UISearchController(searchResultsController);
            searchController.SearchResultsUpdater = searchUpdater;

            searchController.SearchBar.SizeToFit();
            searchController.SearchBar.SearchBarStyle = UISearchBarStyle.Minimal;
            searchController.SearchBar.Placeholder = "Enter a search query";

            searchController.HidesNavigationBarDuringPresentation = false;
            NavigationItem.TitleView = searchController.SearchBar;
            DefinesPresentationContext = true;


            //adresse in koordinaten umwandeln
            getCoordinates("Chennai International Airport");
            mapCenter = newLocation;
            map.CenterCoordinate = mapCenter;

            getAdress();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if(searchController != null)
            {
                searchController.Dispose();
                searchController = null;
            }

            if (mapViewDelegate != null)
            {
                mapViewDelegate.Dispose();
                mapViewDelegate = null;
            }

            if (locationManager != null)
            {
                locationManager.Dispose();
            }
        }

        public async void getCoordinates (string adress)
        {
            try
            {
                var locations = await Geocoding.GetLocationsAsync(adress);

                var location = locations?.FirstOrDefault();
                if (location != null)
                {
                    newLocation = new CLLocationCoordinate2D(location.Latitude, location.Longitude);
                }
            } catch (FeatureNotEnabledException fnsEx)
            {
                Console.WriteLine("Fehler : " + fnsEx.Message);
            } catch (Exception ex)
            {
                Console.WriteLine("Fehler: " + ex.Message);
            }
        }

        public async void getAdress()
        {
            try
            {
                var lat = 47.673988;
                var lon = -122.121513;

                var placemarks = await Geocoding.GetPlacemarksAsync(lat, lon);

                var placemark = placemarks?.FirstOrDefault();
                if (placemark != null)
                {
                    var geocodeAddress =
                        $"AdminArea:       {placemark.AdminArea}\n" +
                        $"CountryCode:     {placemark.CountryCode}\n" +
                        $"CountryName:     {placemark.CountryName}\n" +
                        $"FeatureName:     {placemark.FeatureName}\n" +
                        $"Locality:        {placemark.Locality}\n" +
                        $"PostalCode:      {placemark.PostalCode}\n" +
                        $"SubAdminArea:    {placemark.SubAdminArea}\n" +
                        $"SubLocality:     {placemark.SubLocality}\n" +
                        $"SubThoroughfare: {placemark.SubThoroughfare}\n" +
                        $"Thoroughfare:    {placemark.Thoroughfare}\n";

                    Console.WriteLine(geocodeAddress);
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
            }
            catch (Exception ex)
            {
                // Handle exception that may have occurred in geocoding
            }
        }
    }
}