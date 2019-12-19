using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using SQLite;
using UIKit;

namespace ToDoNavBar
{
    public class Task
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsDone { get; set; }
        public DateTime CreateDate { get; set; }

        public Task()
        {
        }
    }
}