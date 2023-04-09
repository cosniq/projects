using System;
using System.Collections.Generic;

namespace RepositoryLayer
{
    public partial class LocationsView
    {
        public int Id { get; set; }
        public string Address { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int UserId { get; set; }
    }
}
