using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace storageUnitAPi.Data
{
    public static class ModelMappings
    {
        // Here the plan is to use a mapping of 
        // index of spell attribute -> column name -> object Property
        // The key is the name of column from the external CSV file
        // The value is the name of the property on our model.
        // CastTime and Range have been commented out because our model
        // uses an enum to denote these whereas we receive from the external db a string
        // This will probably mean breaking out the conditional logic in the ExternalCSVParser to not be generic
        public static Dictionary<string, string> spellColumnToPropertyMapping = new Dictionary<string, string>{
            {"name", "Name"},
            {"school", "School"},
            {"spell_level", "Level"},
            // {"casting_time","CastTime"},
            {"components","Components"},            
            // {"range","Range"},
            {"effect","Effect"},
            {"saving_throw","SavingThrow"},
            {"spell_resistance","SpellResistance"}
        };
    }
}