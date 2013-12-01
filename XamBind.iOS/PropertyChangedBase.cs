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
				var reference = _references.FirstOrDefault(r =>
				{
					PropertyChangedEventHandler target;
						return r.TryGetTarget(out target) && target == value;
				});
				if (reference != null)
					_references.Remove(reference);
			}
		}        

		protected virtual void OnPropertyChanged(string name)
		{
			var args = new PropertyChangedEventArgs(name);
			var indexes = new List<int>();

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
					indexes.Add(i);
				}
			}

			//Delete any stale references, backwards for-loop prevents index issues
			for (int i = indexes.Count - 1; i >= 0; i--)
			{
				_references.RemoveAt(i);
			}
		}
    }
}

