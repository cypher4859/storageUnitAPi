using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using storageUnitAPi.Data;
using storageUnitAPi.Interfaces;
using storageUnitAPi.Models;

namespace storageUnitAPi.Repositories
{
    public class SpellsRepository : ISpellsRepository
    {
        private readonly DataContext _context;
        public List<Spell> GetSpells()
        {
            return _context.Spells.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}