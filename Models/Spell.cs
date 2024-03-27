using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using storageUnitAPi.Interfaces;

namespace storageUnitAPi.Models
{
    public class Spell : IBaseItem
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string School { get; set; }
        public string Level { get; set; }
        public CastingTime CastTime { get; set; }
        public string Components { get; set; }
        public SpellRange Range { get; set; }
        public string Effect { get; set; }
        public string SavingThrow { get; set; }
        public bool SpellResistance { get; set; }
        public bool Known {  get; set; }
        public bool Prepared { get; set; }
        // public ICollection<CharacterSpell> ?CharacterSpells { get; set; }
        public enum CastingTime
        {
            StandardAction,
            FullRoundAction,
            OneRound,
            TenMinutes,
            Onehour,
            Varies
        }
        public enum SpellRange
        {
            Personal,
            Touch,
            Close,
            Medium,
            Long,
            Unlimited,
            Special
        }
        public enum SpellDuration
        {
            Instantaneous,
            Rounds,
            Minutes,
            Hours,
            Permanent,
        }
    }
}