using System;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using XamBind.Reflection;

namespace XamBind
{
	[Register("BindingController")]
	public class BindingController : UIViewController, IBindable
	{
        public BindingController()
        {
        }

		public BindingController(IntPtr handle) : base(handle)
		{
		}

		public virtual PropertyObserver Observer
		{
			get;
			set;
		}

		public virtual INotifyPropertyChanged ViewModel
		{
			get;
			set;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			this.Bind();
		}
    }
}

