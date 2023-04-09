using System;
using System.Collections.Generic;

namespace RepositoryLayer
{
    public partial class CounterTypesView
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int UnitOfMeasurement { get; set; }
    }
}
