using System;
using System.Collections.Generic;

namespace RepositoryLayer
{
    public partial class LocInvTypeView
    {
        public int Id { get; set; }
        public int Location { get; set; }
        public int InvoiceType { get; set; }
        public bool Active { get; set; }
    }
}
