using SQLite;
using System;
using System.Collections.Generic;
using System.IO;

namespace ToDoNavBar
{
    public class DBHelper
    {
        static string DBName = "ToDOList.db3";
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), DBName);
        
        SQLiteConnection db;

        public void CreateDB()
        {
            Console.WriteLine(dbPath);
            if (!File.Exists(dbPath))
            {
                db = new SQLiteConnection(dbPath);
                db.CreateTable<Task>();
                db.Close();
            }
        }

        public void addTask(Task task)
        {
            db = new SQLiteConnection(dbPath);
            db.Insert(task);
            db.Close();
        }

        public void deleteTask(Task task)
        {
            db = new SQLiteConnection(dbPath);
            //db.Query<Task>("DELETE * FROM Task WHERE ID = ?", task.ID);
            db.Delete(task);
            db.Close();
        }

        public void updateTask(Task task)
        {
            db = new SQLiteConnection(dbPath);
            db.Update(task);
            db.Close();
        }

        public void deleteAllTasks()
        {
            db = new SQLiteConnection(dbPath);
            db.DeleteAll<Task>();
            db.Close();
        }

        public List<Task> getAllTasks()
        {
            List<Task> taskList = new List<Task>();
            db = new SQLiteConnection(dbPath);
            taskList = db.Query<Task>("SELECT * FROM TASK");
            db.Close();
            return taskList;
        }
    }
}