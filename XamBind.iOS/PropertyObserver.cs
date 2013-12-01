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

		public PropertyObserver(INotifyPropertyChanged obj)
        {
			_object = obj;
			_object.PropertyChanged += OnPropertyChanged;
        }

		~PropertyObserver ()
		{
			Dispose();
		}

		private void OnPropertyChanged (object sender, PropertyChangedEventArgs e)
		{
			List<Action<object>> actions;
			if (_actions.TryGetValue(e.PropertyName, out actions))
			{
				foreach (var action in actions)
				{
					var property = _object.GetType().GetProperty(e.PropertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
					if (property != null)
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
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);

			_object.PropertyChanged -= OnPropertyChanged;
			_actions.Clear();
		}
    }
}

