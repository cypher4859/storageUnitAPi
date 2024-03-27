using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using storageUnitAPi.Interfaces;
using storageUnitAPi.Data;
using CsvHelper;
using Microsoft.VisualBasic.FileIO;

namespace storageUnitAPi.Data
{
    public static class ExternalCSVParser<T> {
        public static IEnumerable<T> ParseSpellsFromCsv(Dictionary<string, string> fieldsMapping, string pathToCsv) {
            using (TextFieldParser parser = new TextFieldParser(@pathToCsv))
            {
                // This setups up the things we need to keep track of.
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                List<string> fieldsFromExternalSource = new List<string>(); // All the column names in the external db, e.g. name, school, spell_level
                List<string> mappedKeys = fieldsMapping.Keys.ToList(); // These keys are the column names from the external db that exist on our model
                List<T> resultObjects = new List<T>(); // Will hold our objects at the end of processing


                while (!parser.EndOfData) 
                {
                    // Will create an object from our model that we will set properties on, e.g. name, school, etc.
                    T currentModelObject = (T)Activator.CreateInstance(typeof(T), new object[] { });

                    // Here we are using reflection to get the properties that exist on our model
                    var modelType = currentModelObject.GetType();
                    //Processing row
                    string[] rowEntries = parser.ReadFields();
                    foreach (var (column, index) in rowEntries.Select((value, i) => (value, i))) 
                    {
                        // Line 2 is the name of the columns
                        if (parser.LineNumber == 2) {
                            fieldsFromExternalSource = rowEntries.ToList();
                        }
                        else if (parser.LineNumber > 2){
                            // This checks if the data in the field from external data (e.g. name, spell_level) exists
                            // on our Model then it will proceed to map it to the model
                            if (mappedKeys.Contains(fieldsFromExternalSource[index])){
                                // This looks tricky; we are basically using the column number to grab name of the column, e.g. name
                                // and then using that on our dictionary mapping to get the model's property, e.g. Name
                                var modelProperty = modelType.GetProperty(fieldsMapping[fieldsFromExternalSource[index]]);
                                if (modelProperty != null){
                                    // We only care about a subset of properties; whatever key/values are on the ModelMapping
                                    // we're using are the only ones this will grab

                                    // This checks if the model's property expects a string, int, bool, etc.
                                    // NOTE: If/When you decide to get Range, CastTime, and other custom types
                                    // then this logic will probably need to split out and this function should no longer be totally generic
                                    if (modelProperty.PropertyType == typeof(string)) {
                                        modelProperty.SetValue(currentModelObject, column);
                                    } else if (modelProperty.PropertyType == typeof(int)) {
                                        //
                                    } else if (modelProperty.PropertyType == typeof(bool)) {
                                        // convert column to bool appropriate value
                                        bool propertyValue = false;
                                        if (column == "no") {
                                            propertyValue = false;
                                        } else {
                                            propertyValue = true;
                                        }
                                    } else {
                                        // I think this will skip out of the conditional and continue onwards
                                        continue;
                                    }
                                }
                            }
                            
                        }
                    }

                    // Everything should have a name so used an interface here
                    // to make the compiler "assume" just that for the sake of generic-ness
                    if ((currentModelObject as IBaseItem).Name != null){
                        resultObjects.Add(currentModelObject);
                    }
                }
                
                // return whatever objects we build from our model
                return resultObjects; 
            }
        }
    }
}