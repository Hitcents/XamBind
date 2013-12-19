using System;
using System.ComponentModel;

namespace XamBind
{
	public interface IBindable
    {
		INotifyPropertyChanged ViewModel
		{
			get;
		}

		PropertyObserver Observer
		{
			get;
			set;
		}
    }
}

