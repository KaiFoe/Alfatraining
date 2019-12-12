// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace ToDoListe
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton addBtn { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton editBtn { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField inputTask { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView table { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (addBtn != null) {
                addBtn.Dispose ();
                addBtn = null;
            }

            if (editBtn != null) {
                editBtn.Dispose ();
                editBtn = null;
            }

            if (inputTask != null) {
                inputTask.Dispose ();
                inputTask = null;
            }

            if (table != null) {
                table.Dispose ();
                table = null;
            }
        }
    }
}