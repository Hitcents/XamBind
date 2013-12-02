using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace XamBind
{
	/// <summary>
	/// A base class for INotifyPropertyChanged, using WeakReference
	/// </summary>
	public class PropertyChangedBase : INotifyPropertyChanged
    {
		private readonly List<WeakReference<PropertyChangedEventHandler>> _references = new List<WeakReference<PropertyChangedEventHandler>>();

		public event PropertyChangedEventHandler PropertyChanged
		{
			add
			{
				_references.Add(new WeakReference<PropertyChangedEventHandler>(value, false));
			}
			remove
			{
				//Doesn't have to do anything
			}
		}        

		protected virtual void OnPropertyChanged(string name)
		{
			List<int> indexes = null;
			var args = new PropertyChangedEventArgs(name);

			//Fire the events
			WeakReference<PropertyChangedEventHandler> reference;
			for (int i = 0; i < _references.Count; i++)
			{
				reference = _references [i];
				PropertyChangedEventHandler target;
				if (reference.TryGetTarget(out target) && target != null)
				{
					target(this, args);
				}
				else
				{
					if (indexes == null)
						indexes = new List<int>();
					indexes.Add(i);
				}
			}

			//Delete any stale references, backwards for-loop prevents index issues
			if (indexes != null)
			{
				for (int i = indexes.Count - 1; i >= 0; i--)
				{
					_references.RemoveAt(i);
				}
			}
		}
    }
}

