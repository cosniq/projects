using System;
using System.Collections.Generic;

namespace RepositoryLayer
{
    public partial class InvoicesView
    {
        public int Id { get; set; }
        public int Counter { get; set; }
        public int Month { get; set; }
        public int InvoiceType { get; set; }
        public double? Price { get; set; }
        public bool Paid { get; set; }
    }
}
