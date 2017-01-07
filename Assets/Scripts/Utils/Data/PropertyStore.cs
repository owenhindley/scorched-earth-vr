using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonsterLove.Data
{
    /// <summary>
    /// A data binding-esque strategy for unity variables. Allows you to create a data flow paradigms by raising events when the underlying variable is changed
    /// </summary>
    /// <typeparam name="T"></typeparam>
	public class PropertyStore<T>
	{
		public event Action<T> Changed;

		protected Func<T> ValueGetter;
		protected Action<T> ValueSetter;

		protected T value;

		public PropertyStore (T defaultValue)
		{
			value = defaultValue;
			
			ValueGetter = DefaultGetter;
			ValueSetter = DefaultSetter;
		}

		public PropertyStore ()
		{
			ValueGetter = DefaultGetter;
			ValueSetter = DefaultSetter;
		}

		protected T DefaultGetter()
		{
			return value;
		}

		protected void DefaultSetter(T newValue)
		{
			value = newValue;
			Changed.Dispatch(value);
		}

        /// <summary>
        /// Update the underlying variable, dispatching a change event to all bound listeners. 
        /// 
        /// C# isn't quite rich enough to specify the correct usage rights:
        /// Getter is public, and can be read from anywhere
        /// Setter should be private. i.e. only ever call "prop.Value = bar" from the class that created this property. 
        /// This class should then create a Method to change the property if you need to modify it from the outside world.
        /// </summary>
		public T Value
		{
			get { return ValueGetter(); }
			set { ValueSetter(value); }
		}

		/// <summary>
		/// Set the value directly without triggering notifications causing feedback loops. 
		/// Generally useful for when I view updates it's value directly
		/// </summary>
		/// <param name="newValue">New value to set the property to</param>
		/// <returns></returns>
		public void SetValueSilently(T newValue)
		{
			value = newValue;
		}

		//Used by Bind to make sure listening property stores always fire the correct ValueSetter
		private void Set(T newValue)
		{
			ValueSetter(newValue);
		}

		public virtual void Bind(Action<T> onChange)
		{
			Unbind(onChange); //Pattern to ensure an event is only ever subscribed once

			Changed += onChange;
			onChange(value);
		}

		public void Bind(PropertyStore<T> subscriber)
		{
			Unbind(subscriber); //Pattern to ensure an event is only ever subscribed once

			//Use Set() to make sure listening property stores always fires the correct ValueSetter
			Changed += subscriber.Set;
			subscriber.Set(value);
		}

		public void Unbind(Action<T> onChange)
		{
			Changed -= onChange;
		}

		public void Unbind(PropertyStore<T> subscriber)
		{
			Changed -= subscriber.Set;
		}

		protected void BroadcastChange()
		{
			Changed.Dispatch(value);
		}
	}
}
