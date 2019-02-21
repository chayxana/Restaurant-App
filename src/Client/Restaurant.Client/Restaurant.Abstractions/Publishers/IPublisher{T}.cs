using System;

namespace Restaurant.Abstractions.Publishers
{
    public interface IPublisher<T>
    {
        IObservable<T> Handler { get; }

        void Publish(T data);
    }
}