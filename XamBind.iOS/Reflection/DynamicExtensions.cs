using System;
using System.Collections.Generic;
using System.Reflection;

namespace XamBind.Reflection
{
	public static class DynamicExtensions
    {
		private static readonly Dictionary<Type, Dictionary<string, PropertyInfo>> _properties = new Dictionary<Type, Dictionary<string, PropertyInfo>>();
		private static readonly Dictionary<Type, Dictionary<string, MethodInfo>> _methods = new Dictionary<Type, Dictionary<string, MethodInfo>>();

		private static PropertyInfo GetPropertyInfo(this object target, string propertyName)
		{
			var type = target.GetType();
			Dictionary<string, PropertyInfo> properties;
			if (!_properties.TryGetValue(type, out properties))
			{
				properties =
					_properties [type] = new Dictionary<string, PropertyInfo>();
			}

			PropertyInfo property;
			if (!properties.TryGetValue(propertyName, out property))
			{
				property = 
					properties[propertyName] = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
			}

			return property;
		}

		public static void Invoke(this object target, string methodName)
		{
			var type = target.GetType();
			Dictionary<string, MethodInfo> methods;
			if (!_methods.TryGetValue(type, out methods))
			{
				methods = 
					_methods [type] = new Dictionary<string, MethodInfo>();
			}

			MethodInfo method;
			if (!methods.TryGetValue(methodName, out method))
			{
				method =
					methods [methodName] = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
			}

			if (method != null)
				method.Invoke(target, null);
		}

		public static void SetProperty(this object target, string propertyName, object value)
		{
			var property = target.GetPropertyInfo(propertyName);
			if (property != null)
				property.SetValue(target, value);
		}

		public static object GetProperty(this object target, string propertyName)
		{
			return target.GetPropertyInfo(propertyName).GetValue(target);
		}
    }
}

