using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonsterLove.Data
{
	public class PropertyList<T> : PropertyStore<PropertyList<T>> 
	{
		private List<PropertyListItem<T>> list;

		private Queue<PropertyListItem<T>> pool; 

		public PropertyList() 
		{
			list = new List<PropertyListItem<T>>();
			pool = new Queue<PropertyListItem<T>>();

			value = this;
			
			ValueGetter = DefaultGetter;
			ValueSetter = DefaultSetter;
		}

		public void Add(T item)
		{
			PropertyListItem<T> listItem;
			if(pool.Count > 0)
			{
				listItem = pool.Dequeue();
				listItem.Value = item;
			}
			else
			{
				listItem = new PropertyListItem<T>(item);
			}
			listItem.Changed += OnItemChanged;
			list.Add(listItem);

			BroadcastChange();
		}

		public void Remove(T item)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if(EqualityComparer<T>.Default.Equals(list[i].Value, item))
				{
					var listItem = list[i];
					list.Remove(listItem);
					listItem.Changed -= OnItemChanged;
					listItem.Kill();
					pool.Enqueue(listItem);
					BroadcastChange();
					break;
				}
			}
		}

		public void Clear()
		{
			for (int i = list.Count - 1; i > -1; i--)
			{
				var listItem = list[i];
				list.Remove(listItem);
				listItem.Kill();
				pool.Enqueue(listItem);
			}

			BroadcastChange();
		}

		public int Count
		{
			get { return list.Count; }
		}

		public T this[int i]
		{
			get { return list[i].Value; }
			set
			{
				if (list[i] != null)
				{
					list[i].Value = value;
				}
				else
				{
					list[i] = new PropertyListItem<T>(value);
					BroadcastChange();
				}
			}
		}

		protected void OnItemChanged(T item)
		{
			BroadcastChange();
		}
	}
}
