using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace XamBind.iOS.Tests
{
	public partial class TestController : BoundController
	{
		public TestController (IntPtr handle) : base (handle)
		{
		}

		public string GetText()
		{
			return Text.Text;
		}

		public string GetSearchTitle()
		{
			return Search.Title(UIControlState.Normal);
		}
	}
}
