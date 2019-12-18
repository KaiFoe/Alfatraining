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
        MyRequest request;

        public TableViewController (IntPtr handle) : base (handle)
        {
            request = new MyRequest();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            OnGetList();
            

            UIBarButtonItem addButton = new UIBarButtonItem(UIBarButtonSystemItem.Add, (sender, args) =>
            {
                var detailViewController = Storyboard.InstantiateViewController("DetailViewController") as DetailViewController;
                detailViewController.itemList = itemList;
                NavigationController.PushViewController(detailViewController, true);
            });
            NavigationItem.SetRightBarButtonItem(addButton, true);

            request.getRequest("103919");
           
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
            } catch (Exception ex)
            {
                Console.WriteLine("Fehler: " + ex.Message);
            }
        }
    }
}