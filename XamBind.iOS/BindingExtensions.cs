using System;
using System.Reflection;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using XamBind.Reflection;

namespace XamBind
{
	public static class BindingExtensions
    {
		public static void Bind(this IBindable bindable)
		{
			var viewModel = bindable.ViewModel;
			if (viewModel == null)
				return;

			var observer = new PropertyObserver(viewModel);
			bindable.Observer = observer;

			var properties = bindable.GetType().GetProperties(BindingFlags.Instance | BindingFlags.NonPublic);
			foreach (var property in properties)
			{
				var outlet = property.GetCustomAttribute<OutletAttribute>();
				if (outlet != null)
				{
					var view = property.GetValue(bindable) as UIView;

					//Label
					var label = view as UILabel;
					if (label != null)
					{
						observer.Add<string>(property.Name, text =>
						{
							label.Text = text ?? string.Empty;
						});
						continue;
					}

					//Button
					var button = view as UIButton;
					if (button != null)
					{
						observer.Add<bool>("Can" + property.Name, value =>
						{
							button.Enabled = value;
						});

						button.TouchUpInside += (sender, e) => 
						{
							observer.InvokeMethod(property.Name);
						};

						continue;
					}

					//TextField
					var textField = view as UITextField;
					if (textField != null)
					{
						observer.Add<string>(property.Name, text =>
						{
							textField.Text = text ?? string.Empty;
						});

						NSNotificationCenter.DefaultCenter.AddObserver(UITextField.TextFieldTextDidChangeNotification, n =>
						{
							viewModel.SetProperty(property.Name, textField.Text);

						}, textField);

						continue;
					}
				}
			}
		}
    }
}

