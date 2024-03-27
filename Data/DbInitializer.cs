using System.Text.Json;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client.Extensions.Msal;
using Microsoft.VisualBasic.FileIO;


// using storageUnitAPi.Data;
using storageUnitAPi.Models;


namespace storageUnitAPi.Data {
    public class DbInitializer 
    {
        private readonly DataContext dataContext;
        private string connectionString;

        public DbInitializer(DataContext context) {
            this.dataContext = context;
        }

        public void Initialize(string connectionString) {
            this.connectionString = connectionString;
            if (!dataContext.Spells.Any())
            {
                InitializeSpells();
            }
        }
        
        private void InitializeSpells()
        {
            IEnumerable<Spell> spells = GetSpellsFromExternalDatabase();
            dataContext.Spells.AddRange(spells);
            var result = dataContext.SaveChanges();
            Console.WriteLine($"Result: {result}");
        }

        private IEnumerable<Spell> GetSpellsFromExternalDatabase() {
            string pathToSpellCsv = "/Users/blackcypher/code/storageUnitAPi/Data/spell_full.csv";
            IEnumerable<Spell> spells = ExternalCSVParser<Spell>.ParseSpellsFromCsv(
                ModelMappings.spellColumnToPropertyMapping,
                pathToSpellCsv);
            return spells;
        }
    }
}