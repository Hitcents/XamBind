using System;
using System.Linq;
using Android.Content;
using Android.Views;
using Android.Widget;
using XamBind.Reflection;

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

            var viewGroup = view as ViewGroup;
            if (viewGroup != null)
            {
                BindChildren(bindable, context, viewGroup);
            }

			int id = view.Id;
			if (id == -1)
				return;

			string name = context.Resources.GetResourceName(id);
            name = name.Split('/').Last();

            var button = view as Button;
            if (button != null)
            {
                bindable.Observer.Add<bool>("Can" + name, enabled => button.Enabled = enabled);

                button.Click += (sender, e) => bindable.Observer.InvokeMethod(name);
                return;
            }

            var editText = view as EditText;
            if (editText != null)
            {
                bindable.Observer.Add<string>(name, text => editText.Text = text);

                //editText.TextChanged += (sender, e) => viewModel.SetProperty(name, editText.Text);
                return;
            }

            var textView = view as TextView;
            if (textView != null)
            {
                bindable.Observer.Add<string>(name, text => textView.Text = text);
                return;
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

