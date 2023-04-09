namespace DataTransferObjects
{
    public class MonthsVMDto
    {
        public string Description { get; set; }
        public List<InvoicesVMDto> Invoices { get; set; }
    }
}
