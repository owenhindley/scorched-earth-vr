using System;

public static class EventExtensions 
{
	public static void Dispatch(this Action eventHandler)
	{
		if(eventHandler != null)
		{
			eventHandler();
		}
	}

	public static void Dispatch<T>(this Action<T> eventHandler, T payload)
	{
		if(eventHandler != null)
		{
			eventHandler(payload);
		}
	}

	public static void Dispatch<T, U>(this Action<T, U> eventHandler, T payloadA, U payloadB)
	{
		if (eventHandler != null)
		{
			eventHandler(payloadA, payloadB);
		}
	}
}
