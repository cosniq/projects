using System;
using System.Collections.Generic;

namespace RepositoryLayer
{
    public partial class IndexReadingsView
    {
        public int Id { get; set; }
        public int NumberOnCounter { get; set; }
        public DateTime DateOfReading { get; set; }
        public int Counter { get; set; }
        public int Invoice { get; set; }
    }
}
