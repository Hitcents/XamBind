using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

[assembly: Instrumentation(Name = "android.test.InstrumentationTestRunner", TargetPackage = "com.xambind.tests")]

namespace XamBind.Droid.Tests
{
	[Activity(Label = "TestActivity")]			
	public class TestActivity : BoundActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

			SetContentView(Resource.Layout.Test);
        }

		public TextView GetTextView()
		{
			return FindViewById<TextView>(Resource.Id.Text);
		}
    }
}

