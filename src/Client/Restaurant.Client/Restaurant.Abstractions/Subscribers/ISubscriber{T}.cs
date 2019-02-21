using System;
namespace Restaurant.Abstractions.Subscribers
{
    public interface ISubscriber<T>
    {
        IObservable<T> Handler { get; }
    }
}