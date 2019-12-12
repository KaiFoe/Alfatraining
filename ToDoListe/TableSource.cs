using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace ToDoListe
{
    public class TableSource : UITableViewSource
    {

        List<Task> TaskList = new List<Task>();
        string CellIdentifier = "TableCell";
        public Task currentTask;

        public TableSource(List<Task> taskList)
        {
            TaskList = taskList;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return TaskList.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            int rowIndex = indexPath.Row;
            UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);
            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Subtitle, CellIdentifier);
            }
            cell.TextLabel.Text = TaskList[rowIndex].Name;
            cell.DetailTextLabel.Text = TaskList[rowIndex].CreateDate.ToString();
            if (TaskList[rowIndex].IsDone)
            {
                cell.BackgroundColor = UIColor.Cyan;
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

        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
        {
            switch (editingStyle)
            {
                case UITableViewCellEditingStyle.Delete:
                    TaskList.RemoveAt(indexPath.Row);
                    tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
                    break;
            }
        }

        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            return true; // return false if you wish to disable editing for a specific indexPath or for all rows
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            //Geklickten Task ermitteln
            currentTask = TaskList[indexPath.Row];
            //IsDone-Eigenschaft des gewählten Task-Objektes ändern
            currentTask.IsDone = !currentTask.IsDone;

            //Lange Variante um die IsDone-Eigenschaft des gewählten Task-Objektes zu ändern
            /*if (currentTask.IsDone == false)
            {
                currentTask.IsDone = true;
            } else
            {
                currentTask.IsDone = false;
            }*/
            //TableView neu laden
            tableView.ReloadData();
        }


    }
}