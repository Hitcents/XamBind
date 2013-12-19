using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace XamBind.Droid.Tests
{	
	public class TestActivity : IBindable
    {
		public INotifyPropertyChanged ViewModel
		{
			get;
			set;
		}

		public PropertyObserver Observer
		{
			get;
			set;
		}
    }
}

