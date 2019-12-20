using Foundation;
using System;
using UIKit;

namespace KontaktApp
{
    public partial class ContactDetailViewController : UIViewController
    {
        public ContactDetailViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            UIBarButtonItem buttonSave = new UIBarButtonItem(UIBarButtonSystemItem.Save, (sender, args) =>
            {
                Contact contact = new Contact();
                contact.Vorname = txtVorname.Text;
                contact.Nachname = txtNachname.Text;
                contact.Email = txtEmail.Text;
                contact.Strasse = txtStrasse.Text;
                contact.PLZ = txtPLZ.Text;
                contact.Ort= txtOrt.Text;
                contact.Handy = txtHandy.Text;
                contact.Telefon = txtTelefon.Text;
            });



            UIBarButtonItem[] buttons = new UIBarButtonItem[] { buttonSave };
            NavigationItem.SetRightBarButtonItems(buttons, false);
        }
    }
}