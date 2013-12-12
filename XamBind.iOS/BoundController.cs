using System;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using XamBind.Reflection;

namespace XamBind
{
	[Register("BoundController")]
	public class BoundController : UIViewController
	{
		private List<NSObject> _observers = new List<NSObject>();

        public BoundController()
        {
        }

		public BoundController(IntPtr handle) : base(handle)
		{
		}

		public PropertyObserver Observer
		{
			get;
			private set;
		}

		public INotifyPropertyChanged ViewModel
		{
			get;
			set;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			if (ViewModel == null)
				return;

			Observer = new PropertyObserver(ViewModel);

			var properties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.NonPublic);
			foreach (var property in properties)
			{
				var outlet = property.GetCustomAttribute<OutletAttribute>();
				if (outlet != null)
				{
					var view = property.GetValue(this) as UIView;

					//Label
					var label = view as UILabel;
					if (label != null)
					{
						Observer.Add<string>(property.Name, text =>
						{
							label.Text = text ?? string.Empty;
						});
						continue;
					}

					//Button
					var button = view as UIButton;
					if (button != null)
					{
						Observer.Add<bool>("Can" + property.Name, value =>
						{
							button.Enabled = value;
						});

						button.TouchUpInside += (sender, e) => 
						{
							Observer.InvokeMethod(property.Name);
						};

						continue;
					}

					//TextField
					var textField = view as UITextField;
					if (textField != null)
					{
						Observer.Add<string>(property.Name, text =>
						{
							textField.Text = text ?? string.Empty;
						});

						var observer = NSNotificationCenter.DefaultCenter.AddObserver(UITextField.TextFieldTextDidChangeNotification, n =>
						{
							ViewModel.SetProperty(property.Name, textField.Text);

						}, textField);
						_observers.Add(observer);

						continue;
					}
				}
			}
		}
    }
}

