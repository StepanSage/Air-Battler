using System;

public interface IEventBus : IService
{
    public void Subscribe<T>(Action<T> callback) where T : IEvent;
    public void Unsubscribe<T>(Action<T> callback) where T: IEvent;

    public void Publish<T>(T eventData) where T : IEvent;
}
