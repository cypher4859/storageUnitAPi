using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace storageUnitAPi.DTO
{
    public class SpellDTO
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string School { get; set; }
        public string Level { get; set; }
        public string Components { get; set; }
        public string Effect { get; set; }
        public string SavingThrow { get; set; }
        public bool SpellResistance { get; set; }
    }
}