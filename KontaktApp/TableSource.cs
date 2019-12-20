using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace KontaktApp
{
    class TableSource : UITableViewSource
    {
        private string cellID;
        public Contact currentContact;
        public List<Contact> contactList { get; set; }

        public TableSource()
        {
            cellID = "cellID";
            contactList = new List<Contact>();
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            int rowIndex = indexPath.Row;
            UITableViewCell cell = tableView.DequeueReusableCell(cellID);
            if (cell == null)
                cell = new UITableViewCell(UITableViewCellStyle.Subtitle, cellID);

            cell.TextLabel.Text = contactList[rowIndex].Vorname + " " + contactList[rowIndex].Nachname;
 
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return contactList.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            ContactDetailViewController cdvc = new ContactDetailViewController();
            cdvc.contact = contactList[indexPath.Row];
        }
    }
}