using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Contacts.Helpers
{
    public static class ContactExtensions
    {
        public static string GetFormattedName(this CNContact contact)
        {
            var name = contact != null ? CNContactFormatter.GetStringFrom(contact, CNContactFormatterStyle.FullName)?.Trim() : null;
            return !string.IsNullOrEmpty(name) ? name : "No Name";
        }
    }

    public static class ContactPropertyExtension
    {
        public static string GetNameMatchingKey(this CNContactProperty property)
        {
            switch (property.Key)
            {
                case "emailAddress":
                    return "Email address";
                case "phoneNumber":
                    return "Phone number";
                case "postalAddress":
                    return "Postal address";
                default:
                    return null;
            }
        }

        public static string GetNameMatchingValue (this CNContactProperty property)
        {
            switch (property.Key)
            {
                case "emailAddress":
                    return property.Value as NSString;
                case "phoneNumber":
                    if (property.Value is CNPhoneNumber phoneNumber)
                        return phoneNumber.StringValue;
                    break;
                case "postalAddress":
                    if (property.Value is CNPostalAddress postalAddress)
                        return postalAddress.GetFormattedPostalAddress();
                    break;
            }
            return null;
        }

        public static string GetNameMatchingLocalizedLabel(this CNContactProperty property)
        {
            var label = property?.Label;
            if (!string.IsNullOrEmpty(label))
            {
                var nativelabel = new NSString(label);
                switch (property.Label)
                {
                    case "emailAddresses":
                        return CNLabeledValue<NSString>.LocalizeLabel(nativelabel);
                    case "phoneNumbers":
                        return CNLabeledValue<NSString>.LocalizeLabel(nativelabel);
                    case "postalAddresses":
                        return CNLabeledValue<NSString>.LocalizeLabel(nativelabel);
                }
            }
            return null;
        }
    }

    public static class PostalAdressExtensions
    {
        public static string GetFormattedPostalAddress(this CNPostalAddress postalAddress)
        {
            string[] address = { postalAddress.Street, postalAddress.City, postalAddress.State, postalAddress.PostalCode };
            var filteredArray = address.Where(item => !string.IsNullOrEmpty(item)).ToArray();

            return filteredArray.Any() ? string.Join(", ", filteredArray) : null;
        }
    }
}