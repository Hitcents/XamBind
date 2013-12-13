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
    public class BoundActivity : Activity
    {
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

		public override View OnCreateView(View parent, string name, Context context, Android.Util.IAttributeSet attrs)
		{
			var view = base.OnCreateView(parent, name, context, attrs);

			if (ViewModel == null)
				return view;

			Observer = new PropertyObserver(ViewModel);

			Bind(view);

			var viewGroup = view as ViewGroup;
			if (viewGroup != null)
			{
				BindChildren(viewGroup);
			}

			return view;
		}

		private void BindChildren(ViewGroup parent)
		{
			for (int i = 0; i < parent.ChildCount; i++)
			{
				var view = parent.GetChildAt(i);
				Bind(view);

				var viewGroup = view as ViewGroup;
				if (viewGroup != null)
				{
					BindChildren(viewGroup);
				}
			}
		}

		private void Bind(View view)
		{
			int id = view.Id;
			if (id == -1)
				return;

			string name = Resources.GetResourceName(id);

			var textView = view as TextView;
			if (textView != null)
			{
				Observer.Add<string>(name, text => textView.Text = text);
			}
		}
    }
}

