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

namespace KontaktApp
{
    [Register ("ContactDetailViewController")]
    partial class ContactDetailViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgPerson { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtEmail { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtHandy { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtNachname { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtOrt { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtPLZ { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtStrasse { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtTelefon { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtVorname { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (imgPerson != null) {
                imgPerson.Dispose ();
                imgPerson = null;
            }

            if (txtEmail != null) {
                txtEmail.Dispose ();
                txtEmail = null;
            }

            if (txtHandy != null) {
                txtHandy.Dispose ();
                txtHandy = null;
            }

            if (txtNachname != null) {
                txtNachname.Dispose ();
                txtNachname = null;
            }

            if (txtOrt != null) {
                txtOrt.Dispose ();
                txtOrt = null;
            }

            if (txtPLZ != null) {
                txtPLZ.Dispose ();
                txtPLZ = null;
            }

            if (txtStrasse != null) {
                txtStrasse.Dispose ();
                txtStrasse = null;
            }

            if (txtTelefon != null) {
                txtTelefon.Dispose ();
                txtTelefon = null;
            }

            if (txtVorname != null) {
                txtVorname.Dispose ();
                txtVorname = null;
            }
        }
    }
}