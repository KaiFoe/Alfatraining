using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Foundation;
using SQLite;
using UIKit;

namespace KontaktApp
{
    class DBHelper
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
                db.CreateTable<Contact>();
                db.Close();
            }
        }

        public void addContact(Contact contact)
        {
            db = new SQLiteConnection(dbPath);
            db.Insert(contact);
            db.Close();
        }

        public void deleteContact(Contact contact)
        {
            db = new SQLiteConnection(dbPath);
            //db.Query<Task>("DELETE * FROM Task WHERE ID = ?", task.ID);
            db.Delete(contact);
            db.Close();
        }

        public void updateContact(Contact contact)
        {
            db = new SQLiteConnection(dbPath);
            db.Update(contact);
            db.Close();
        }

        public void deleteAllContact()
        {
            db = new SQLiteConnection(dbPath);
            db.DeleteAll<Contact>();
            db.Close();
        }

        public List<Contact> getAllContact()
        {
            List<Contact> taskList = new List<Contact>();
            db = new SQLiteConnection(dbPath);
            taskList = db.Query<Contact>("SELECT * FROM CONTACT");
            db.Close();
            return taskList;
        }
    }
}