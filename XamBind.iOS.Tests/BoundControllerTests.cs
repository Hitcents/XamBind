using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using NUnit.Framework;

namespace XamBind.iOS.Tests
{
	[TestFixture]
    public class BoundControllerTests
    {
		private TestController _controller;
		private TestViewModel _viewModel;

		[SetUp]
		public void SetUp()
		{
			_viewModel = new TestViewModel();

			var storyboard = UIStoryboard.FromName("TestStoryboard", NSBundle.MainBundle);
			_controller = storyboard.InstantiateViewController("TestController") as TestController;
			_controller.ViewModel = _viewModel;
			_controller.View.ToString(); //Load view
		}

		[Test]
		public void Label()
		{
			_viewModel.Text = "WOOT";

			Assert.AreEqual(_viewModel.Text, _controller.GetText());
		}

		[Test]
		public void Button()
		{
			_viewModel.ButtonTitle = "WOOT";

			Assert.AreEqual(_viewModel.ButtonTitle, _controller.GetButtonTitle());
		}
    }
}

