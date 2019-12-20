using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using SQLite;
using UIKit;

namespace KontaktApp
{
    public class Contact
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int ID { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Strasse { get; set; }
        public string PLZ { get; set; }
        public string Ort { get; set; }
        public string Handy { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
    }
}