using System;

public class EventBinding<T> : IEventBinding<T> where T : IEvent
{
    Action<T> onEvent = _ => { };
    Action onEventNoArgs = () => { };

    Action<T> IEventBinding<T>.OnEvent 
    { 
        get => onEvent;
        set => onEvent = value; 
    }

    Action IEventBinding<T>.OnEventNoArgs 
    {
        get => onEventNoArgs; 
        set => onEventNoArgs = value; 
    }

    public EventBinding(Action<T> @event) => onEvent = @event;
    public EventBinding(Action @event) => onEventNoArgs = @event;

    public void Add(Action @event) => onEventNoArgs += @event;
    public void Remove(Action @event) => onEventNoArgs -= @event;

    public void Add(Action<T> @event) => onEvent += @event;
    public void Remove(Action<T> @event) => onEvent -= @event;
}