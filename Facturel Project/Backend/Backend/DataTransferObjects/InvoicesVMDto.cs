namespace DataTransferObjects
{
    public class InvoicesVMDto
    {
        public double? Price { get; set; }
        public CounterVMDto Counter { get; set; }
        public string InvoiceTypeName { get; set; }
        public IndexReadingWithUnitDto IndexReading { get; set; }
        public bool Paid { get; set; }
    }
}
