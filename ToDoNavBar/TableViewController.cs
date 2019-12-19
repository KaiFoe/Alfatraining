using Foundation;
using System;
using UIKit;

namespace ToDoNavBar
{
    public partial class TableViewController : UITableViewController
    {
        TableSource tableSource;
        DBHelper dbHelper = new DBHelper();

        public TableViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            tableView.Source = new TableSource();
            tableSource = (TableSource)tableView.Source;

            dbHelper.CreateDB();
            tableSource.taskList = dbHelper.getAllTasks();

            UIBarButtonItem buttonAdd = new UIBarButtonItem(UIBarButtonSystemItem.Add, (sender, args) =>
            {
                CreateAlertDialog(tableSource);
            });

            UIBarButtonItem buttonDeleteAll = new UIBarButtonItem(UIBarButtonSystemItem.Trash, (object sender, EventArgs args) =>
            {
                dbHelper.deleteAllTasks();
                tableSource.taskList.Clear();
                tableView.ReloadData();
            });

            UIBarButtonItem[] buttons = new UIBarButtonItem[] { buttonAdd, buttonDeleteAll };
            NavigationItem.SetRightBarButtonItems(buttons, false);

            UILongPressGestureRecognizer longPressGestureRecognizer = new UILongPressGestureRecognizer(LongPress);
            tableView.AddGestureRecognizer(longPressGestureRecognizer);

            UISwipeGestureRecognizer leftSwipeGesture = new UISwipeGestureRecognizer(SwipeLefttoRight) { Direction = UISwipeGestureRecognizerDirection.Right };
            tableView.AddGestureRecognizer(leftSwipeGesture);
        }

        private void CreateAlertDialog(TableSource tableSource)
        {
            UIAlertView alert = new UIAlertView();
            alert.AlertViewStyle = UIAlertViewStyle.PlainTextInput;
            alert.Title = "Neuen Task anlegen";
            alert.AddButton("Abbrechen");
            alert.AddButton("OK");
            alert.Show();
            alert.Clicked += (sender, args) =>
            {
                if (args.ButtonIndex == 1)
                {
                    var newTask = new Task();
                    newTask.Name = alert.GetTextField(0).Text;
                    newTask.IsDone = false;
                    newTask.CreateDate = DateTime.Now;
                    tableSource.taskList.Add(newTask);
                    tableView.ReloadData();

                    dbHelper.addTask(newTask);
                }
            };
        }

        private void EditAlertDialog(NSIndexPath indexPath)
        {
            Task currentTask = tableSource.taskList[indexPath.Row];
            UIAlertView alert = new UIAlertView();
            alert.Title = "Task bearbeiten";
            alert.AddButton("Abbrechen");
            alert.AddButton("OK");
            alert.AlertViewStyle = UIAlertViewStyle.PlainTextInput;
            alert.GetTextField(0).Text = currentTask.Name;
            alert.Show();
            alert.Clicked += (sender, args) =>
            {
                if (args.ButtonIndex == 1)
                {
                    currentTask.Name = alert.GetTextField(0).Text;
                    tableSource.taskList[indexPath.Row].Name = currentTask.Name;

                    dbHelper.updateTask(currentTask);

                    tableView.BeginUpdates();
                    tableView.ReloadRows(tableView.IndexPathsForVisibleRows, UITableViewRowAnimation.Automatic);
                    tableView.EndUpdates();
                }
            };
        }

        public void LongPress(UILongPressGestureRecognizer longPressGestureRecognizer)
        {
            if (longPressGestureRecognizer.State == UIGestureRecognizerState.Began)
            {
                var point = longPressGestureRecognizer.LocationInView(tableView);
                var indexPath = tableView.IndexPathForRowAtPoint(point);
                EditAlertDialog(indexPath);
            }
        }

        public void SwipeLefttoRight(UISwipeGestureRecognizer swipeGestureRecognizer)
        {
            var point = swipeGestureRecognizer.LocationInView(tableView);
            var indexPath = tableView.IndexPathForRowAtPoint(point);

            dbHelper.deleteTask(tableSource.taskList[indexPath.Row]);

            tableSource.taskList.RemoveAt(indexPath.Row);
            tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);  
        }
    }
}