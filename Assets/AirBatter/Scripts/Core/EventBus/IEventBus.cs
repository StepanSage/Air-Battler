using System;

public interface IEventBus : IService
{
    /// <summary>
    ///  Subsctibe a method to an event
    /// </summary>
    public void Subscribe<T>(Action<T> callback) where T : IEvent;

    /// <summary>
    /// Unsubscribr a metode to event
    /// </summary>
    public void Unsubscribe<T>(Action<T> callback) where T: IEvent;

    /// <summary>
    /// Notifies the subscribed method about an event
    /// </summary>
    public void Publish<T>(T eventData) where T : IEvent;
}
