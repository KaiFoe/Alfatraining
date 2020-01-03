using Contacts.Helpers;
using Contacts.PickerDelegates;
using ContactsUI;
using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace Contacts
{
    public partial class ContactPickerController : UIViewController, IUITableViewDelegate, IUITableViewDataSource
    {
        private readonly List<Section> sections = new List<Section>();
        private ICNContactPickerDelegate contactDelegate;

        public ContactPickerController (IntPtr handle) : base (handle)
        {
        }
        public PickerMode Mode { get; set; }

        
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.tableView.Hidden = false;
            this.headerLabel.Hidden = false;

            
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            switch (Mode)
            {
                case PickerMode.SingleContact:
                    headerLabel.Text = "Selected Contract:";
                    descriptionLabel.Text = "This page allows you...";
                    break;
                case PickerMode.MultipleContacts:
                    headerLabel.Text = "Selected Contacts:";
                    break;
                case PickerMode.SingleProperty:
                    headerLabel.Text = "Selected Property:";
                    break;
                case PickerMode.MultiplePorperties:
                    headerLabel.Text = "Selected Properties:";
                    break;
            }
        }

        partial void ShowPicker(UIButton sender)
        {
            this.UpdateInterface(true);
            switch (this.Mode)
            {
                case PickerMode.SingleContact:
                    this.contactDelegate = new SingleContactPickerDelegate(Update);
                    var picker = new CNContactPickerViewController { Delegate = contactDelegate };
                    base.PresentViewController(picker, true, null);
                    break;

                case PickerMode.SingleProperty:
                    this.contactDelegate = new SinglePropertyPickerDelegate(Update);
                    var propertyPicker = new CNContactPickerViewController { Delegate = contactDelegate };
                    propertyPicker.DisplayedPropertyKeys = new NSString[] { CNContactKey.GivenName,
                                                                            CNContactKey.FamilyName,
                                                                            CNContactKey.EmailAddresses,
                                                                            CNContactKey.PhoneNumbers,
                                                                            CNContactKey.PostalAddresses};
                    base.PresentViewController(propertyPicker, true, null);
                    break;

                case PickerMode.MultipleContacts:
                    this.contactDelegate = new MultipleContactPickerDelegate(Update);
                    var contactPicker = new CNContactPickerViewController { Delegate = contactDelegate };
                    base.PresentViewController(contactPicker, true, null);
                    break;

                case PickerMode.MultiplePorperties:
                    this.contactDelegate = new MultiplePropertyPickerDelegate(Update);
                    var propertiesPicker = new CNContactPickerViewController { Delegate = contactDelegate };
                    propertiesPicker.PredicateForSelectionOfProperty = NSPredicate.FromFormat("key == 'emailadresses'");
                    base.PresentViewController(propertiesPicker, true, null);
                    break;
            }
        }

        private void UpdateInterface(bool hide)
        {
            this.tableView.Hidden = hide;
            this.headerLabel.Hidden = hide;

            
                this.tableView.ReloadData();
        }

        [Export("numberOfSectionsInTableView:")]
        public nint NumberOfSections(UITableView tableView)
        {
            return this.sections.Count;
        }

        public nint RowsInSection(UITableView tableView, nint section)
        {
            return this.sections[(int)section].Items.Count;
        }

        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var item = this.sections[indexPath.Section].Items[indexPath.Row];
            var cell = tableView.DequeueReusableCell("cellID");
            cell.TextLabel.Text = item;
            return cell;
        }

        private void Update(List<Section> items)
        {
            this.sections.Clear();
            this.sections.AddRange(items);
            this.UpdateInterface(false);
        }
    }
}