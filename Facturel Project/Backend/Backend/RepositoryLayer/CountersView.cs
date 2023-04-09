using System;
using System.Collections.Generic;

namespace RepositoryLayer
{
    public partial class CountersView
    {
        public int Id { get; set; }
        public int CounterType { get; set; }
        public int Location { get; set; }
        public string SerialNr { get; set; } = null!;
    }
}
