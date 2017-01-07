using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonsterLove.Data;

namespace MonsterLove.Data
{
	public class PropertyListItem<T> : PropertyStore<T>
	{
		public Action<T> Killed;

		public PropertyListItem(T defaultValue) : base(defaultValue)
		{

		}

		public void Kill()
		{
			Killed.Dispatch(value);
			value = default(T);
		}

	}
}
