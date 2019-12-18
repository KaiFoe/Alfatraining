using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundation;
using UIKit;

namespace Json
{
    public class TableSource : UITableViewSource
    {
        List<Items> ItemList;
        string CellIdentifier = "cellIdentifier";

        public TableSource(List<Items> itemList)
        {
            ItemList = itemList;
        }
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            //UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);
            Items item = ItemList[indexPath.Row];
            UIImage image = new UIImage();
            
            //if (cell == null)
               CustomCell cell = new CustomCell(CellIdentifier);
            cell.UpdateCell(item.title,
                            item.artist,
                            FromUrl(item.image));
                           
            return cell;

        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return ItemList.Count;
        }

        static UIImage FromUrl(string uri)
        {
            using (var url = new NSUrl(uri))
            using (var data = NSData.FromUrl(url))
                return UIImage.LoadFromData(data);
        }

    }
}