using System;
using System.Collections.Generic;

namespace RepositoryLayer
{
    public partial class InvoiceTypesView
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CounterType { get; set; }
        public bool CostOnlyDependentOnUsage { get; set; }
    }
}
