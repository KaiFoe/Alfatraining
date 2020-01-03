using Contacts.Helpers;
using ContactsUI;
using CoreFoundation;
using Foundation;
using System;
using System.Linq;
using UIKit;

namespace Contacts
{
    public partial class RootViewController : UITableViewController, ICNContactViewControllerDelegate
    {

        private readonly CNContactStore store = new CNContactStore();
        public RootViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            CheckContactsAccess();
        }


        private void CheckContactsAccess()
        {
            var status = CNContactStore.GetAuthorizationStatus(CNEntityType.Contacts);
            switch (status)
            {
                case CNAuthorizationStatus.Authorized:
                    Console.WriteLine("App is authorized");
                    break;
                case CNAuthorizationStatus.NotDetermined:
                    this.store.RequestAccess(CNEntityType.Contacts, (granted, error) =>
                    {
                        if (granted)
                            Console.WriteLine("App is authorized");
                    });
                    break;
                case CNAuthorizationStatus.Restricted:
                case CNAuthorizationStatus.Denied:
                    Console.WriteLine("Access denied or restricted");
                    break;
            }
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "PickerSegue")
            {
                if (segue.DestinationViewController is ContactPickerController contactPickerController)
                    contactPickerController.Mode = (PickerMode)(sender as NSNumber).Int32Value;
            }
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var selectedCell = tableView.CellAt(indexPath);

            if (indexPath.Section == 0)
            {
                PerformContactAction(selectedCell);
            } else if (indexPath.Section == 1)
            {
                PerformContactPickerAction(selectedCell);
            } else if (indexPath.Section == 2)
            {
                PerformContactPredicatePickerAction(selectedCell);
            }
            tableView.DeselectRow(indexPath, true);
        }

        #region Contact predicate picker logic (3. Section)
        private void PerformContactPredicatePickerAction(UITableViewCell selectedCell)
        {
            if (selectedCell == enableContactsPredicateCell)
            {
                base.PerformSegue("PredicatePickerSegue", new NSNumber((int)PredicatePickerMode.EnableContacts));
            } else if (selectedCell == selectContactsPredicateCell)
            {
                base.PerformSegue("PedicatePickerSegue", new NSNumber((int)PredicatePickerMode.SelectContacts));
            }
        }
        #endregion

        #region Contact picker logic (2. Section)
        private void PerformContactPickerAction(UITableViewCell selectedCell)
        {
            var mode = default(PickerMode);
            if (selectedCell == pickSingleContactCell)
                mode = PickerMode.SingleContact;
            else if (selectedCell == pickSinglePropertyCell)
                mode = PickerMode.SingleProperty;
            else if (selectedCell == pickMultipleContactsCell)
                mode = PickerMode.MultipleContacts;
            else if (selectedCell == pickMultiplePropertiesCell)
                mode = PickerMode.MultiplePorperties;

            base.PerformSegue("PickerSegue", new NSNumber((int)mode));
        }
        #endregion

        #region Regular contact logic (int section)
        private void PerformContactAction(UITableViewCell selectedCell)
        {
            if (selectedCell == createNewContactCell)
            {
                var contactViewController = CNContactViewController.FromNewContact(null);
                contactViewController.Delegate = this;
                base.NavigationController.PushViewController(contactViewController, true);
            }

            else if (selectedCell == createNewContactExistingDataCell)
            {
                var contact = new CNMutableContact
                {
                    FamilyName = Name.Family,
                    GivenName = Name.Given,
                };

                contact.PhoneNumbers = new CNLabeledValue<CNPhoneNumber>[]
                {
                    new CNLabeledValue<CNPhoneNumber>(PhoneNumber.IPhone, new CNPhoneNumber(PhoneNumber.IPhone)),
                    new CNLabeledValue<CNPhoneNumber>(PhoneNumber.Mobile, new CNPhoneNumber(PhoneNumber.Mobile))
                };

                var homeAddress = new CNMutablePostalAddress
                {
                    Street = Address.Street,
                    City = Address.City,
                    State = Address.State,
                    PostalCode = Address.PostalCode
                };
                contact.PostalAddresses = new CNLabeledValue<CNPostalAddress>[] { new CNLabeledValue<CNPostalAddress>(CNLabelKey.Home, homeAddress)};

                //Erstelle einen Kontakt-View-Controller mit unserem Kontakt
                var contactViewController = CNContactViewController.FromNewContact(contact);
                contactViewController.Delegate = this;
                base.NavigationController.PushViewController(contactViewController, true);
            }

            else if (selectedCell == editContactCell)
            {
                var contact = new CNMutableContact();

                contact.PhoneNumbers = new CNLabeledValue<CNPhoneNumber>[] { new CNLabeledValue<CNPhoneNumber>(CNLabelPhoneNumberKey.iPhone, new CNPhoneNumber(PhoneNumber.Mobile)) };
                var homeAddress = new CNMutablePostalAddress()
                {
                    Street = Address.Street,
                    City = Address.City,
                    State = Address.State,
                    PostalCode = Address.PostalCode
                };
                contact.PostalAddresses = new CNLabeledValue<CNPostalAddress>[] { new CNLabeledValue<CNPostalAddress>(CNLabelKey.Home, homeAddress) };

                var contactViewController = CNContactViewController.FromUnknownContact(contact);
                contactViewController.AllowsEditing = true;
                contactViewController.ContactStore = new CNContactStore();
                contactViewController.Delegate = this;

                base.NavigationController.PushViewController(contactViewController, true);
            }

            else if (selectedCell == displayEditCell)
            {
                var name = $"{Name.Given} {Name.Family}";
                FetchContact(name, (contacts) =>
                {
                    if (contacts.Any())
                    {
                        var contactViewController = CNContactViewController.FromContact(contacts[0]);
                        contactViewController.AllowsEditing = true;
                        contactViewController.AllowsActions = true;
                        contactViewController.Delegate = this;

                        var highlightedPropertyIdentifiers = contacts[0].PhoneNumbers.FirstOrDefault()?.Identifier;
                        if (!string.IsNullOrEmpty(highlightedPropertyIdentifiers))
                            contactViewController.HighlightProperty(new NSString("phoneNumbers"), highlightedPropertyIdentifiers);
                        else
                            this.ShowAlert($"Could not find {name} in Contacts.");

                    }
                });
            }
        }
        #endregion

        #region IContactViewControllerDelegate
        [Export("contactViewController:shouldPerformDefaultActionForContactProperty:")]
        public bool ShouldPerformDefaultAction(CNContactViewController viewController, CNContactProperty property)
        {
            return false;
        }

        [Export("contactViewController:didCompleteWithContact:")]
        public void DidComplete(CNContactViewController viewController, CNContact contact)
        {
            base.NavigationController.PopViewController(true);
            if (contact != null)
                base.NavigationController.ShowAlert($"{contact.GetFormattedName()} was successsfully added.");
        }
        #endregion

        private void FetchContact(string name, Action<CNContact[]> completion)
        {
            var result = store.GetUnifiedContacts(CNContact.GetPredicateForContacts(name),
                                    new ICNKeyDescriptor[] { CNContactViewController.DescriptorForRequiredKeys },
                                    out NSError error);
            if (error != null)
                Console.WriteLine($"Error: {error.LocalizedDescription}");
            else
                DispatchQueue.MainQueue.DispatchAsync(() => completion(result));
        }

        #region Hilfsklassen

        static class PhoneNumber
        {
            public static string IPhone { get; } = "+49 (0) 1234 123456789";
            public static string Mobile { get; } = "+49 (0) 160 12345000242";
        }

        static class Name
        {
            public static string Family { get; } = "Förstermann";
            public static string Given { get; } = "Kai";
        }

        static class Address
        {
            public static string Street { get; } = "Hauptstraße 11";
            public static string City { get; } = "Suderburg";
            public static string State { get; } = "Niedersachsen";
            public static string PostalCode { get; } = "29556";
        }
        #endregion
    }
}