using System;

namespace Restaurant.Abstractions.Facades
{
    public interface IDateTimeFacade
    {
        DateTime Now { get; }
    }
}
