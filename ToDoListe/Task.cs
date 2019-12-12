using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace ToDoListe
{
    public class Task
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsDone { get; set; }
        public DateTime CreateDate { get; set; }

        //Standardkonstruktor
        public Task()
        {
        }

        //Überladener Konstruktor
        //Wird genutzt, wenn Task hinzugefügt wird
        public Task(string taskName)
        {
            this.Name = taskName;
            this.IsDone = false;
        }

        //Überladener Konstruktor
        //Wird genutzt, wenn Task bearbeitet wird
        public Task(string taskName, bool isDone)
        {
            this.Name = taskName;
            this.IsDone = isDone;
        }
    }
}