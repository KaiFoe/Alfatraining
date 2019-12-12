using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace ToDoNavBar
{
    public class TableSource : UITableViewSource
    {
        private string cellID;
        public Task currentTask;
        public List<Task> taskList { get; set; }

        public TableSource()
        {
            cellID = "cellID";
            taskList = new List<Task>();
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            int rowIndex = indexPath.Row;
            UITableViewCell cell = tableView.DequeueReusableCell(cellID);
            if (cell == null)
                cell = new UITableViewCell(UITableViewCellStyle.Subtitle, cellID);

            cell.TextLabel.Text = taskList[rowIndex].Name;
            cell.DetailTextLabel.Text = taskList[rowIndex].CreateDate.ToString();

            if (taskList[rowIndex].IsDone)
            {
                cell.BackgroundColor = UIColor.Green;
                cell.TextLabel.TextColor = UIColor.White;
                cell.Accessory = UITableViewCellAccessory.Checkmark;
            }
            else
            {
                cell.BackgroundColor = UIColor.White;
                cell.TextLabel.TextColor = UIColor.Black;
                cell.Accessory = UITableViewCellAccessory.None;
            }
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return taskList.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            currentTask = taskList[indexPath.Row];
            currentTask.IsDone = !currentTask.IsDone;

            tableView.ReloadData();
        }
    }
}