namespace DataTransferObjects
{
    public class InvoiceTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CounterType { get; set; }
        public bool CostOnlyDependentOnUsage { get; set; }
    }
}
