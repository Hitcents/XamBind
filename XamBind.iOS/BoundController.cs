using System;
using System.Reflection;
using System.ComponentModel;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace XamBind
{
	[Register("BoundController")]
	public class BoundController : UIViewController
    {
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

					var label = view as UILabel;
					if (label != null)
					{
						Observer.Add(property.Name, value =>
						{
							string text = value as string;
							label.Text = text ?? string.Empty;
						});
						continue;
					}

					var button = view as UIButton;
					if (button != null)
					{
						Observer.Add(property.Name, value =>
						{
							string text = value as string;
							button.SetTitle(text ?? string.Empty, UIControlState.Normal);
						});
					}
				}
			}
		}
    }
}

