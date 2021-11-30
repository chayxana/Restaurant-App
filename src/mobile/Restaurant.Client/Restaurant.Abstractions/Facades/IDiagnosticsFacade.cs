using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Abstractions.Facades
{
    public interface IDiagnosticsFacade
    {
        void TrackError(Exception exception, IDictionary<string, string> properties = null);

        void TrackEvent(string name, IDictionary<string, string> properties = null);
    }
}
