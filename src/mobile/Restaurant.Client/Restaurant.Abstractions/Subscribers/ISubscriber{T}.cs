using System;
namespace Restaurant.Abstractions.Subscribers
{
    /// <summary>
    /// Abstraction of ISubscriber,
    /// Provides IObservable to be able subscribe for particular type publishers
    /// </summary>
    /// <typeparam name="T">Particular type of objects </typeparam>
    public interface ISubscriber<out T>
    {
        IDisposable Subscribe(IObserver<T> observer);
    }
}