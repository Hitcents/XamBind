// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace XamBind.iOS.Tests
{
	[Register ("TestController")]
	partial class TestController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton ButtonTitle { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel Text { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (Text != null) {
				Text.Dispose ();
				Text = null;
			}

			if (ButtonTitle != null) {
				ButtonTitle.Dispose ();
				ButtonTitle = null;
			}
		}
	}
}
