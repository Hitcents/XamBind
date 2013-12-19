using System;
using Android.Content;
using Android.Views;
using Android.Widget;

namespace XamBind
{
	public static class BindingExtensions
    {
		public static void Bind<T>(this T bindable, View view)
			where T : Context, IBindable
		{
			Bind(bindable, bindable, view);
		}

		public static void Bind(this IBindable bindable, Context context, View view)
		{
			var viewModel = bindable.ViewModel;
			if (viewModel == null)
				return;

			var observer = new PropertyObserver(viewModel);
			bindable.Observer = observer;

			int id = view.Id;
			if (id == -1)
				return;

			string name = context.Resources.GetResourceName(id);

			var textView = view as TextView;
			if (textView != null)
			{
				bindable.Observer.Add<string>(name, text => textView.Text = text);
			}

			var viewGroup = view as ViewGroup;
			if (viewGroup != null)
			{
				BindChildren(bindable, context, viewGroup);
			}
		}

		private static void BindChildren(IBindable bindable, Context context, ViewGroup parent)
		{
			for (int i = 0; i < parent.ChildCount; i++)
			{
				var view = parent.GetChildAt(i);
				Bind(bindable, context, view);

				var viewGroup = view as ViewGroup;
				if (viewGroup != null)
				{
					BindChildren(bindable, context, viewGroup);
				}
			}
		}
    }
}

