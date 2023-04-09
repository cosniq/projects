using RepositoryLayer;
using DataTransferObjects;

namespace BusinessLayer
{
    public class CustomMapper
    {
        #region Users
        internal UserWithTokenDto Map(UsersView user)
        {
            return new UserWithTokenDto
            {
                Username = user.Username,
                UserId = user.Id,
            };
        }
        #endregion

        #region InvoiceTypes
        internal List<InvoiceTypeDto> Map(List<InvoiceTypesView> invoiceTypes, List<CounterTypesView> counterTypes)
        {
            var invoiceTypeDtos = new List<InvoiceTypeDto>();
            foreach (var invoiceType in invoiceTypes)
            {
                var elemToAdd = new InvoiceTypeDto();
                elemToAdd.Id = invoiceType.Id;
                elemToAdd.Name = invoiceType.Name;
                elemToAdd.CostOnlyDependentOnUsage = invoiceType.CostOnlyDependentOnUsage;
                elemToAdd.CounterType = counterTypes.FirstOrDefault(ct => ct.Id == invoiceType.CounterType).Name;
                invoiceTypeDtos.Add(elemToAdd);
            }
            return invoiceTypeDtos;
        }
        #endregion

        #region CounterTypes
        
        public List<CounterTypeDto> Map(List<CounterTypesView> counterTypes, List<UnitsOfMeasurementView> units)
        {
            var counterTypeDtos = new List<CounterTypeDto>();
            foreach (var counterType in counterTypes)
            {
                var elemToAdd = new CounterTypeDto();
                elemToAdd.Id = counterType.Id;
                elemToAdd.Name = counterType.Name;
                elemToAdd.UnitOfMeasurement = units.FirstOrDefault(u => u.Id == counterType.UnitOfMeasurement).Symbol;
                counterTypeDtos.Add(elemToAdd);
            }
            return counterTypeDtos;
        }

        #endregion

        #region LocationInvoiceTypeRelations

        public List<LocationInvoiceTypeRelationDto> Map (List<LocInvTypeView> relations, List<LocationsView> locations, List<InvoiceTypesView> invoiceTypes)
        {
            var results = new List<LocationInvoiceTypeRelationDto>();

            foreach (var relation in relations)
            {
                var elemToAdd = new LocationInvoiceTypeRelationDto();
                elemToAdd.Id = relation.Id;
                elemToAdd.LocationDescription = locations.FirstOrDefault(l => l.Id == relation.Location).Description;
                elemToAdd.InvoiceTypeName = invoiceTypes.FirstOrDefault(it => it.Id == relation.InvoiceType).Name;
                results.Add(elemToAdd);
            }

            return results;
        }

        #endregion

        #region ViewModel

        public ViewModelDto Map(int userId, List<LocationsView> locations, List<MonthsView> months, List<InvoicesView> invoices, List<CountersView> counters, List<IndexReadingsView> indexes, List<UnitsOfMeasurementView> uoms, List<CounterTypesView> counterTypes, List<InvoiceTypesView> invoiceTypes)
        {
            var results = new ViewModelDto();
            results.UserId = userId;
            results.Locations = Map(locations, months, invoices, counters, indexes, uoms, counterTypes, invoiceTypes);
            return results;
        }

        private List<LocationsVMDto> Map(List<LocationsView> locations, List<MonthsView> months, List<InvoicesView> invoices, List<CountersView> counters, List<IndexReadingsView> indexes, List<UnitsOfMeasurementView> uoms, List<CounterTypesView> counterTypes, List<InvoiceTypesView> invoiceTypes)
        {
            var results = new List<LocationsVMDto>();
            
            foreach (var location in locations)
            {
                var elemToAdd = new LocationsVMDto();
                elemToAdd.Address = location.Address;
                elemToAdd.Description = location.Description;
                elemToAdd.Months = Map(location.Id, months, invoices, counters, indexes, uoms, counterTypes, invoiceTypes);
                results.Add(elemToAdd);
            }

            return results;
        }

        private List<MonthsVMDto> Map(int locationId, List<MonthsView> months, List<InvoicesView> invoices, List<CountersView> counters, List<IndexReadingsView> indexes, List<UnitsOfMeasurementView> uoms, List<CounterTypesView> counterTypes, List<InvoiceTypesView> invoiceTypes)
        {
            var monthsOfCurrentLocation = months.Where(m => m.Location == locationId).ToList();
            var results = new List<MonthsVMDto>();

            foreach (var month in monthsOfCurrentLocation)
            {
                var elemToAdd = new MonthsVMDto();
                elemToAdd.Description = month.Description;
                elemToAdd.Invoices = Map(month.Id, invoices, counters, indexes, uoms, counterTypes, invoiceTypes);
                results.Add(elemToAdd);
            }

            return results;
        }

        private List<InvoicesVMDto> Map(int monthdId, List<InvoicesView> invoices, List<CountersView> counters, List<IndexReadingsView> indexes, List<UnitsOfMeasurementView> uoms, List<CounterTypesView> counterTypes, List<InvoiceTypesView> invoiceTypes)
        {
            var invoicesOfCurrentMonth = invoices.Where(i => i.Month == monthdId).ToList();
            var results = new List<InvoicesVMDto>();

            foreach (var invoice in invoicesOfCurrentMonth)
            {
                var elemToAdd = new InvoicesVMDto();
                elemToAdd.Price = invoice.Price;

                var counter = counters.SingleOrDefault(c => c.Id == invoice.Counter);
                
                if (counter is null)
                {
                    throw new Exception("Counter not found for invoice");
                }

                var invoiceType = invoiceTypes.SingleOrDefault(it => it.Id == invoice.InvoiceType);

                if (invoiceType is null)
                {
                    throw new Exception("InvoiceType not found for invoice");
                }

                elemToAdd.InvoiceTypeName = invoiceType.Name;
                elemToAdd.Counter = Map(counter, indexes, uoms, counterTypes);
                elemToAdd.IndexReading = Map(invoice.Id, indexes);
                elemToAdd.Paid = invoice.Paid;
                results.Add(elemToAdd);
            }

            return results;
        }

        private CounterVMDto Map(CountersView counter, List<IndexReadingsView> indexes, List<UnitsOfMeasurementView> uoms, List<CounterTypesView> counterTypes)
        {
            var result = new CounterVMDto();

            var counterTypeOfCounter = counterTypes.SingleOrDefault(ct => ct.Id == counter.CounterType);

            if (counterTypeOfCounter is null)
            {
                throw new Exception("CounterType of Counter wasn't found");
            }

            var uom = uoms.SingleOrDefault(uom => uom.Id == counterTypeOfCounter.UnitOfMeasurement);

            if (uom is null)
            {
                throw new Exception("Counter's Unit of Measurement wasn't found");
            }

            result.SerialNr = counter.SerialNr;
            result.UnitOfMeasurement = uom.Symbol;
            return result;
        }

        private IndexReadingWithUnitDto Map(int invoiceId, List<IndexReadingsView> indexes)
        {
            var indexOfInvoice = indexes.SingleOrDefault(i => i.Invoice == invoiceId);
            if (indexOfInvoice is null)
            {
                throw new Exception("IndexReading of Invoice wasn't found");
            }
            var result = new IndexReadingWithUnitDto();
            result.IndexReading = indexOfInvoice.NumberOnCounter;
            result.DateOfReading = indexOfInvoice.DateOfReading;

            return result;
        }

        #endregion

        #region Counters
    
        public List<CounterWithLocationAndTypeDto> Map(List<CountersView> counters, List<LocationsView> locations, List<CounterTypesView> counterTypes)
        {
            var results = new List<CounterWithLocationAndTypeDto>();

            foreach (var counter in counters)
            {
                var elemToAdd = new CounterWithLocationAndTypeDto();
                elemToAdd.Id = counter.Id;
                elemToAdd.SerialNumber = counter.SerialNr;

                var location = locations.FirstOrDefault(l => l.Id == counter.Location);
                if (location is null)
                {
                    throw new Exception("Location not found for counter");
                }

                elemToAdd.LocationDescription = location.Description;

                var counterType = counterTypes.FirstOrDefault(ct => ct.Id == counter.CounterType);
                if (counterType is null)
                {
                    throw new Exception("CounterType not found for counter");
                }
                
                elemToAdd.CounterType = counterType.Name;
                results.Add(elemToAdd);
            }

            return results;
        }
        
        #endregion
    }
}
