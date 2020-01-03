using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace KontaktApp
{
    public partial class TableViewController : UITableViewController
    {
        DBHelper dbHelper = new DBHelper();
        TableSource tableSource = new TableSource();
        List<Contact> contactList = new List<Contact>();
        public TableViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            dbHelper.CreateDB();
            UIBarButtonItem buttonAdd = new UIBarButtonItem(UIBarButtonSystemItem.Add, (sender, args) =>
            {
                UIStoryboard storyboard = this.Storyboard;
                var contactDetailController = storyboard.InstantiateViewController("ContactDetail") as ContactDetailViewController;
                NavigationController.PushViewController(contactDetailController, true);
            });

            UIBarButtonItem buttonDeleteAll = new UIBarButtonItem(UIBarButtonSystemItem.Trash, (object sender, EventArgs args) =>
            {
                dbHelper.deleteAllContact();
                tableSource.contactList.Clear();
                TableView.ReloadData();
            });

            UIBarButtonItem[] buttons = new UIBarButtonItem[] { buttonAdd, buttonDeleteAll };
            NavigationItem.SetRightBarButtonItems(buttons, false);
        }
    }
}