using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace XamBind
{
	/// <summary>
	/// A base class for INotifyPropertyChanged
	/// </summary>
	public class PropertyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

		protected virtual void OnPropertyChanged(string name)
		{
            PropertyChanged(this, new PropertyChangedEventArgs(name));
		}
    }
}

