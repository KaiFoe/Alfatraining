using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contacts.Helpers;
using ContactsUI;
using Foundation;
using UIKit;

namespace Contacts.PickerDelegates
{
    public class SingleContactPickerDelegate : CNContactPickerDelegate
    {
        private readonly Action<List<Section>> callback;

        public SingleContactPickerDelegate(Action<List<Section>> callback)
        {
            this.callback = callback;
        }

        public override void DidSelectContact(CNContactPickerViewController picker, CNContact contact)
        {
            if (contact != null)
            {
                var sections = new List<Section>
                {
                    new Section {Items = new List<string> {contact.GetFormattedName()}}
                };

                callback(sections);
            }
        }
    }
}