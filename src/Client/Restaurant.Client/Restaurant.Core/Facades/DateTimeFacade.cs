using System;
using System.Diagnostics.CodeAnalysis;
using Restaurant.Abstractions.Facades;

namespace Restaurant.Core.Facades
{
    [ExcludeFromCodeCoverage]
    public class DateTimeFacade : IDateTimeFacade
    {
        public DateTime Now => DateTime.Now;
    }
}
