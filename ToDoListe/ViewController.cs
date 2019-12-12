using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace ToDoListe
{
    public partial class ViewController : UIViewController
    {
        public ViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            

            bool canEdit = false;
            
            List<Task> taskList = new List<Task>();
            table.Source = new TableSource(taskList);
            Add(table);

            addBtn.TouchUpInside += (sender, args) =>
            {
                var newTask = new Task();
                newTask.Name = inputTask.Text;
                newTask.IsDone = false;
                newTask.CreateDate = DateTime.Now;

                taskList.Add(newTask);
                table.ReloadData();
                inputTask.Text = "";
            };

            editBtn.TouchUpInside += (sender, args) =>
            {
                if (canEdit == false)
                { 
                    table.SetEditing(true, true);
                    canEdit = !canEdit;
                }
                else
                { 
                    table.SetEditing(false, false);
                    canEdit = !canEdit;
                }
            };
        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
        }
    }
}