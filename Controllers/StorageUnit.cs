using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using storageUnitAPi.Models;
using storageUnitAPi.Services;


namespace storageUnitAPi
{
    [ApiController]
    [Route("api/[controller]")]
    public class StorageUnitController : ControllerBase
    {
        private readonly ILogger<StorageUnitController> _logger;
        private StorageUnitService _storageUnitService;

        public StorageUnitController(ILogger<StorageUnitController> logger) {
            _logger = logger;
            _storageUnitService = new StorageUnitService();
        }

        // TODO: Change return type
        [HttpGet()]
        public void GetStorageUnits() {
            IEnumerable<StorageUnit> results = this._storageUnitService.GetStorageUnitsInInventory();
            
            IEnumerable<StorageUnit> reservedUnits = results.Where(unit => unit.Status is StorageUnitStatus.RESERVED);
            IEnumerable<StorageUnit> smallUnit = results.Where(unit => unit.Size == StorageUnitSize.SMALL);
        }

        // [HttpGet("{id:int}")]
        public void GetStorageUnit(int id) {
            var result = this._storageUnitService.GetStorageUnitById(id);
        }

        // [HttpGet("{ownerName:string}")]
        public void GetStorageUnitByOwner(string ownerName) {
            var result = this._storageUnitService.GetStorageUnitByOwner(ownerName);
        }

        [HttpPost()]
        public void CreateNewUnit() {
            // Assume that we can break out a body from the HTTP Post
            // ...
            StorageUnit unit = new StorageUnit();
            var result = this._storageUnitService.AddNewUnitToInventory(unit);
        }

        // [HttpPut("{unitId:int, StorageUnit:StorageUnit}")]
        // public void UpdateStorageUnit() {
        //     //
        // }
    }
}