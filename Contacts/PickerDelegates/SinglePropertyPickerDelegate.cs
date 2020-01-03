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
    public class SinglePropertyPickerDelegate : CNContactPickerDelegate
    {
        private readonly Action<List<Section>> callback;

        public SinglePropertyPickerDelegate(Action<List<Section>> callback)
        {
            this.callback = callback;
        }

        public override void DidSelectContactProperty(CNContactPickerViewController picker, CNContactProperty contactProperty)
        {
            if (contactProperty != null)
            {
                var sections = new List<Section>();
                var section = new Section { Items = new List<string>() };

                var nameKey = contactProperty.GetNameMatchingKey();
                if (!string.IsNullOrEmpty(nameKey))
                {
                    section.Items.Add($"Contact: {contactProperty.Contact.GetFormattedName()}");
                    section.Items.Add($"Key: {nameKey}");
                }

                var localizedLabel = contactProperty.GetNameMatchingLocalizedLabel();
                if (!string.IsNullOrEmpty(localizedLabel))
                {
                    section.Items.Add($"Label: {localizedLabel}");
                }

                var value = contactProperty.GetNameMatchingValue();
                if (!string.IsNullOrEmpty(value))
                {
                    section.Items.Add($"Value: {value}");
                }

                sections.Add(section);
                callback(sections);
            }
        }
    }
}