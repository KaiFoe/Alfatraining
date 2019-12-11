using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace TableView
{
    public class TableSource : UITableViewSource
    {
        UITableViewController Controller;
        List<string> Items;
        string CellIdentifier = "CellIdentifier";

        public TableSource(List<string> items, UITableViewController controller)
        {
            Items = items;
            Controller = controller;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);
            string item = Items[indexPath.Row];
            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier);
            }
            cell.TextLabel.Text = item;
            //cell.BackgroundColor = UIColor.Blue;
            cell.Accessory = UITableViewCellAccessory.Checkmark;

            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return Items.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            UIAlertController alertController = UIAlertController.Create("Row selected", Items[indexPath.Row], UIAlertControllerStyle.Alert);
            alertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            Controller.PresentViewController(alertController, true, null);
            tableView.DeselectRow(indexPath, true);
        }

        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            return true;
        }

        public override UITableViewCellEditingStyle EditingStyleForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return UITableViewCellEditingStyle.Delete;
        }

        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
        {
            switch (editingStyle)
            {
                case UITableViewCellEditingStyle.Delete:
                    Items.RemoveAt(indexPath.Row);
                    tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
                    break;
            }
        }


    }
}