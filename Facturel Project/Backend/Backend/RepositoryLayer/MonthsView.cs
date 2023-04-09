using System;
using System.Collections.Generic;

namespace RepositoryLayer
{
    public partial class MonthsView
    {
        public int Id { get; set; }
        public int Location { get; set; }
        public string Description { get; set; } = null!;
    }
}
