using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XamBind
{	
	public class BindingActivity : Activity, IBindable
    {
		public virtual INotifyPropertyChanged ViewModel
		{
			get;
			set;
		}

		public virtual PropertyObserver Observer
		{
			get;
			set;
		}

		public override View OnCreateView(View parent, string name, Context context, Android.Util.IAttributeSet attrs)
		{
			var view = base.OnCreateView(parent, name, context, attrs);

			this.Bind(view);

			return view;
		}
    }
}

