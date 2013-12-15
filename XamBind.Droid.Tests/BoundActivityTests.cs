using Android.Content;
using Android.Test;
using Android.App;
using Android.OS;
using System;
using NUnit.Framework;
using XamBind.iOS.Tests;
using System.Threading.Tasks;

namespace XamBind.Droid.Tests
{
	[TestFixture]
	public class BoundActivityTests
    {
		private TestActivity _activity;
		private TestViewModel _viewModel;
		private InstrumentationTestRunner _instrumentation;
		private const int Times = 10000;

		[SetUp]
		public void SetUp()
		{
			_viewModel = new TestViewModel();


			_instrumentation = new InstrumentationTestRunner();

			_activity = new TestActivity();
			_instrumentation.CallActivityOnCreate(_activity, Bundle.Empty);
		}

		[Test]
		public void PropertyToTextView()
		{
			for (int i = 0; i < Times; i++)
			{
				_viewModel.Text = i.ToString();

				Assert.AreEqual(_viewModel.Text, _activity.GetTextView().Text);
			}
		}
    }
}

