using System;

namespace Restaurant.Core.Models
{
    public class MasterItem
    {
        public string Title { get; set; }

        public string IconSource { get; set; }

        public Type NavigationType { get; set; }
    }
}