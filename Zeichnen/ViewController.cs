using CoreGraphics;
using Foundation;
using System;
using UIKit;

namespace Zeichnen
{
    public partial class ViewController : UIViewController
    {
        class NoCaretField : UITextField
        {
            public NoCaretField() : base(new CGRect())
            {
                BorderStyle = UITextBorderStyle.Line;
            }

            public override CGRect GetCaretRectForPosition(UITextPosition position)
            {
                return new CGRect();
            }
        }
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            UIView contentView = new UIView()
            {
                BackgroundColor = UIColor.White
            };

            View = contentView;
            CGRect rect = UIScreen.MainScreen.Bounds;
            rect.Y += 0;
            rect.Height -= 50;


            UIStackView vertStackView = new UIStackView(rect)
            {
                Axis = UILayoutConstraintAxis.Vertical
            };
            contentView.Add(vertStackView);

            UIStackView horizStackView = new UIStackView(rect)
            {
                Axis = UILayoutConstraintAxis.Horizontal,
                Alignment = UIStackViewAlignment.Center,
                Distribution = UIStackViewDistribution.EqualSpacing
            };

            vertStackView.AddArrangedSubview(horizStackView);
            CanvasView canvasView = new CanvasView();
            vertStackView.AddArrangedSubview(canvasView);
            horizStackView.AddArrangedSubview(new UILabel(new CGRect(0, 0, 10, 10)));

            PickerDataModel<UIColor> colorModel = new PickerDataModel<UIColor>
            {
                Items =
                {
                    new NamedValue<UIColor>("Rot", UIColor.Red),
                    new NamedValue<UIColor>("Grün", UIColor.Green),
                    new NamedValue<UIColor>("Blau", UIColor.Blue),
                    new NamedValue<UIColor>("Gelb", UIColor.Yellow)
                }
            };

            UIPickerView colorPicker = new UIPickerView
            {
                Model = colorModel
            };

            PickerDataModel<float> thicknessModel = new PickerDataModel<float>
            {
                Items =
                {
                    new NamedValue<float>("Dünn", 2),
                    new NamedValue<float>("Medium", 10),
                    new NamedValue<float>("Dick", 20),
                    new NamedValue<float>("Fett", 50)
                }
            };

            UIPickerView thicknessPicker = new UIPickerView
            {
                Model = thicknessModel
            };

            var toolbar = new UIToolbar(new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, 45))
            {
                BarStyle = UIBarStyle.BlackTranslucent,
                Translucent = true
            };

            UIFont font = UIFont.SystemFontOfSize(24);

            UITextField colorTextField = new NoCaretField
            {
                Text = "Rot",
                InputView = colorPicker,
                InputAccessoryView = toolbar,
                Font = font
            };

            horizStackView.AddArrangedSubview(colorTextField);
            colorModel.ValueChanged += (sender, args) =>
            {
                colorTextField.Text = colorModel.SelectedItem.Name;
                canvasView.StrokeColor = colorModel.SelectedItem.Value.CGColor;
            };

            UITextField thicknessTextField = new NoCaretField
            {
                Text = "Dünn",
                InputView = thicknessPicker,
                InputAccessoryView = toolbar,
                Font = font
            };

            horizStackView.AddArrangedSubview(thicknessTextField);
            thicknessModel.ValueChanged += (sender, args) =>
            {
                thicknessTextField.Text = thicknessModel.SelectedItem.Name;
                canvasView.StrokeWidth = thicknessModel.SelectedItem.Value;
            };

            var spacer = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);
            var doneButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, (sender, args) =>
            {
                colorTextField.ResignFirstResponder();
                thicknessTextField.ResignFirstResponder();
            });

            toolbar.SetItems(new[] { spacer, doneButton }, true);

            UIButton button = new UIButton(UIButtonType.RoundedRect)
            {
                Font = font
            };

            horizStackView.AddArrangedSubview(button);

            button.Layer.BorderColor = UIColor.Blue.CGColor;
            button.Layer.BorderWidth = 1;
            button.Layer.CornerRadius = 10;
            button.SetTitle("Clear", UIControlState.Normal);
            button.SetTitleColor(UIColor.Black, UIControlState.Normal);

            button.TouchUpInside += (sender, args) =>
            {
                canvasView.clear();
            };


        }
    
        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}