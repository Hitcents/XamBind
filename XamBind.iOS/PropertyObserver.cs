using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using XamBind.Reflection;

namespace XamBind
{
	public class PropertyObserver : IDisposable
    {
		private readonly INotifyPropertyChanged _target;
		private readonly Dictionary<string, List<Action<object>>> _actions = new Dictionary<string, List<Action<object>>>();

		public PropertyObserver(INotifyPropertyChanged target)
        {
			_target = target;
			_target.PropertyChanged += OnPropertyChanged;
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
				var value = _target.GetProperty(e.PropertyName);
				foreach (var action in actions)
				{
					action(value);
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
			action(_target.GetProperty(propertyName));
		}

		public void InvokeMethod(string methodName)
		{
			_target.Invoke(methodName);
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);

			_target.PropertyChanged -= OnPropertyChanged;
			_actions.Clear();
		}
    }
}

