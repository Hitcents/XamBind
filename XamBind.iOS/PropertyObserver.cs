using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using XamBind.Reflection;

namespace XamBind
{
	public class PropertyObserver
    {
		private readonly WeakReference<INotifyPropertyChanged> _target;
		private readonly Dictionary<string, List<Action<object>>> _actions = new Dictionary<string, List<Action<object>>>();

		public PropertyObserver(INotifyPropertyChanged target)
        {
            target.PropertyChanged += (sender, e) =>
		    {
			    List<Action<object>> actions;
			    if (_actions.TryGetValue(e.PropertyName, out actions))
			    {
                    INotifyPropertyChanged obj;
                    if (_target.TryGetTarget(out obj))
                    {
                        var value = obj.GetProperty(e.PropertyName);
                        foreach (var action in actions)
                        {
                            action(value);
                        }
                    }
			    }
		    };

            _target = new WeakReference<INotifyPropertyChanged>(target);
        }

		public void Add<T>(string propertyName, Action<T> action)
		{
			Add(propertyName, value => action((T)value));
		}

		public void Add(string propertyName, Action<object> action)
		{
			List<Action<object>> actions;
			if (!_actions.TryGetValue(propertyName, out actions))
			{
				actions =
					_actions [propertyName] = new List<Action<object>>();
			}
			actions.Add(action);

			//Fire initial event
            INotifyPropertyChanged target;
            if (_target.TryGetTarget(out target))
            {
                action(target.GetProperty(propertyName));
            }
		}

		public void InvokeMethod(string methodName)
		{
            INotifyPropertyChanged target;
            if (_target.TryGetTarget(out target))
            {
                target.Invoke(methodName);
            }
		}
    }
}

