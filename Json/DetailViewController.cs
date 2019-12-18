using Foundation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UIKit;

namespace Json
{
    public partial class DetailViewController : UIViewController
    {
        public List<Items> itemList = new List<Items>();
        public DetailViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            UIBarButtonItem saveButton = new UIBarButtonItem(UIBarButtonSystemItem.Save, (sender, args) =>
            {
                Items item = new Items();
                item.artist = artistTextField.Text;
                item.title = titleTextField.Text;
                item.image = imageTextField.Text;
                item.thumbnail_image = imageTextField.Text;
                itemList.Add(item);
                JsonWrite();
            });

            NavigationItem.SetRightBarButtonItem(saveButton, true);
        }
        protected bool JsonWrite()
        {
            try
            {
                string jsondata = JsonConvert.SerializeObject(itemList);
                File.WriteAllText("items.json", jsondata);
                Console.WriteLine("Object hinzugefügt");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler: " + ex.Message);
                return false;
            }
        }
    }
}