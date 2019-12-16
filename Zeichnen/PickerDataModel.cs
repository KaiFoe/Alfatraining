using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Zeichnen
{
    class NamedValue<T>
    {
        public string Name { get; set; }
        public T Value { get; set; }

        public NamedValue(string name, T value)
        {
            Name = name;
            Value = value;
        }
    }

    class PickerDataModel<T> : UIPickerViewModel
    {
        public event EventHandler<EventArgs> ValueChanged;
        int selectedIndex;
        public IList<NamedValue<T>> Items { private set; get; }

        public PickerDataModel()
        {
            Items = new List<NamedValue<T>>();
        }
       
        public NamedValue<T> SelectedItem
        {
            get
            {
                return Items != null &&
                    selectedIndex >= 0 &&
                    selectedIndex < Items.Count ? Items[selectedIndex] : null;
            }
        }

        public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
        {
            return Items != null ? Items.Count : 0;
        }

        public override string GetTitle(UIPickerView pickerView, nint row, nint component)
        {
            return Items != null && Items.Count > row ? Items[(int)row].Name : null;
        }

        public override nint GetComponentCount(UIPickerView pickerView)
        {
            return 1;
        }

        public override void Selected(UIPickerView pickerView, nint row, nint component)
        {
            selectedIndex = (int)row;
            ValueChanged?.Invoke(this, new EventArgs());
        }
    }
}