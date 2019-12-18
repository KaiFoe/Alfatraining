using Foundation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UIKit;


namespace Json
{
    public partial class TableViewController : UITableViewController
    {

        public int count = 0;
        private const string url = "http://rallycoding.herokuapp.com/api/music_albums";
        //private HttpClient httpClient = new HttpClient();
        List<Items> itemList = new List<Items>();

        public TableViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            OnGetList();
           

            UIBarButtonItem addButton = new UIBarButtonItem(UIBarButtonSystemItem.Add, (sender, args) =>
            {
                var detailViewController = Storyboard.InstantiateViewController("DetailViewController") as UIViewController;
                
                NavigationController.PushViewController(detailViewController, true);
            });
            NavigationItem.SetRightBarButtonItem(addButton, true);

           
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            TableView.ReloadData();
        }

        protected void OnGetList()
        {
            try
            {
                var content = File.ReadAllText("items.json");
                itemList = JsonConvert.DeserializeObject<List<Items>>(content);
                TableView.Source = new TableSource(itemList);
                TableView.ReloadData();
                //ObservableCollection<Items> trends = new ObservableCollection<Items>(tr);
                
                //TableView.Source = trends;
            } catch (Exception ex)
            {
                Console.WriteLine("Fehler: " + ex.Message);
            }
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);

            var detailViewController = segue.DestinationViewController as DetailViewController;

            //if (detailViewController != null)
            //{
                detailViewController.itemList = itemList;
            //}
        }
    }
}