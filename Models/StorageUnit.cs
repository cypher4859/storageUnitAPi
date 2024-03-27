using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace storageUnitAPi.Models
{
    public class StorageUnit
    {
        public int Id {get;set;}
        public DateOnly ReservationStartDate {get;set;}
        public int? CurrentOwnerId {get;set;}
        public Customer? CurrentOwner {get;set;}
        public ICollection<int>? PreviousOwnersIds {get;set;}
        public ICollection<Customer>? PreviousOwners {get;set;}
        public StorageUnitSize Size {get;set;}
        public StorageUnitStatus Status {get;set;}
    }
}