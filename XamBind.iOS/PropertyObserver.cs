using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace XamBind
{
	public class PropertyObserver : IDisposable
    {
		private readonly INotifyPropertyChanged _object;
		private readonly Dictionary<string, List<Action<object>>> _actions = new Dictionary<string, List<Action<object>>>();
		private readonly Dictionary<string, PropertyInfo> _properties = new Dictionary<string, PropertyInfo>();
		private readonly Dictionary<string, Action> _methods = new Dictionary<string, Action>();

		public PropertyObserver(INotifyPropertyChanged obj)
        {
			_object = obj;
			_object.PropertyChanged += OnPropertyChanged;
        }

		~PropertyObserver ()
		{
			Dispose();
		}

		private PropertyInfo GetProperty(string propertyName)
		{
			PropertyInfo property;
			if (!_properties.TryGetValue(propertyName, out property))
			{
				property = 
					_properties[propertyName] = _object.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
			}
			return property;
		}

		private void OnPropertyChanged (object sender, PropertyChangedEventArgs e)
		{
			List<Action<object>> actions;
			if (_actions.TryGetValue(e.PropertyName, out actions))
			{
				var property = GetProperty(e.PropertyName);
				if (property != null)
				{
					foreach (var action in actions)
					{
						action(property.GetValue(_object));
					}
				}
			}
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
			var property = GetProperty(propertyName);
			if (property != null)
			{
				action(property.GetValue(_object));
			}
		}

		public void InvokeMethod(string methodName)
		{
			Action action;
			if (!_methods.TryGetValue(methodName, out action))
			{
				action =
					_methods [methodName] = Delegate.CreateDelegate(typeof(Action), _object, methodName) as Action;
			}
			if (action != null)
				action();
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);

			_object.PropertyChanged -= OnPropertyChanged;
			_actions.Clear();
		}
    }
}

