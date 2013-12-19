using Android.Content;
using Android.Test;
using Android.App;
using Android.OS;
using System;
using NUnit.Framework;
using XamBind.iOS.Tests;
using System.Threading.Tasks;
using Android.Views;
using Android.Widget;

namespace XamBind.Droid.Tests
{
	[TestFixture]
	public class BoundActivityTests
    {
		private TestActivity _activity;
		private TestViewModel _viewModel;
		private View _view;
		private const int Times = 10000;

		[SetUp]
		public void SetUp()
		{
			var context = Application.Context;
			var layoutInflater = context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;

			_view = layoutInflater.Inflate(Resource.Layout.Test, null);
			_viewModel = new TestViewModel();
			_activity = new TestActivity
			{
				ViewModel = _viewModel,
			};
			_activity.Bind(context, _view);
		}

		[Test]
		public void PropertyToTextView()
		{
			var textView = _view.FindViewById<TextView>(Resource.Id.Text);
			for (int i = 0; i < Times; i++)
			{
				_viewModel.Text = i.ToString();

				Assert.AreEqual(_viewModel.Text, textView.Text);
			}
		}
    }
}

