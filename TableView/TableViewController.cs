using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace TableView
{
    public partial class TableViewController : UITableViewController
    {

        public TableViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            table = new UITableView(View.Bounds);

            table.SeparatorEffect = UIBlurEffect.FromStyle(UIBlurEffectStyle.Dark);
            table.SeparatorColor = UIColor.Black;
            table.SeparatorStyle = UITableViewCellSeparatorStyle.DoubleLineEtched;

            table.SetEditing(true, true);

            List<string> items = new List<string>();
            items.Add("BMW");
            items.Add("Mercedes");
            items.Add("Rower");
            items.Add("Bugatti");
            items.Add("Ferrari");
            items.Add("Porsche");
      
            table.Source = new TableSource(items, this);
            Add(table);
        }
    }
}