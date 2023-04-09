namespace DataTransferObjects
{
    public class LocationsVMDto
    {
        public string Address { get; set; }
        public string Description { get; set; }
        public List<MonthsVMDto> Months { get; set; }
    }
}
