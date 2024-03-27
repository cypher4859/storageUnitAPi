using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using storageUnitAPi.Models;

namespace storageUnitAPi.Services
{
    public class StorageUnitService
    {
        public Func<StorageUnit, bool> customFilter;
        private IEnumerable<StorageUnit> _currentInventory = [];
        public object GetStorageUnitById(int id)
        {
            customFilter = storageUnit => storageUnit.Id == id;
            return _GetStorageUnitByFilter(customFilter);
        }

        public object GetStorageUnitByOwner(string ownerName)
        {
            customFilter = storageUnit => (storageUnit.CurrentOwner != null && storageUnit.CurrentOwner.Name == ownerName) || (storageUnit.PreviousOwners != null && storageUnit.PreviousOwners.Where(s => s.Name == ownerName).Count() > 0);
            return _GetStorageUnitByFilter(customFilter);
        }

        public IEnumerable<StorageUnit> GetStorageUnitsInInventory()
        {
            return _GetStorageUnitsInInventory();
        }

        public object AddNewUnitToInventory(StorageUnit unit)
        {
            return _AddNewUnitToInventory(unit);
        }

        public object _AddNewUnitToInventory(StorageUnit unit){
            return new object();
        }

        private IEnumerable<StorageUnit> _GetStorageUnitByFilter(Func<StorageUnit, bool> filter) {
            IEnumerable<StorageUnit> currentUnits = _GetStorageUnitsInInventory();
            return currentUnits.Where(filter);
        }

        private IEnumerable<StorageUnit> _GetStorageUnitsInInventory() {
            return _currentInventory;
        }
    }
}