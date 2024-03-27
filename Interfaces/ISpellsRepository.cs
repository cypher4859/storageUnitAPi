using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using storageUnitAPi.Models;

namespace storageUnitAPi.Interfaces
{
    public interface ISpellsRepository
    {
        List<Spell> GetSpells();
        bool Save();
    }
}