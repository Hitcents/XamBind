using System;
using NUnit.Framework;
using XamBind.iOS.Tests;
using Android.Content;
using Android.Test;

namespace XamBind.Droid.Tests
{
	[TestFixture]
    public class BoundActivityTests
    {
		private TestActivity _activity;
		private TestViewModel _viewModel;
		private InstrumentationTestRunner _runner;
		private const int Times = 10000;

		[SetUp]
		public void SetUp()
		{
			_runner = new InstrumentationTestRunner();
			_viewModel = new TestViewModel();
			_activity = _runner.StartActivitySync(new Intent(_runner.Context, typeof(TestActivity))) as TestActivity;
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

