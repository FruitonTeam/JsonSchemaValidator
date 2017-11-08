using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace JsonSchemaValidator
{
    internal class Program
    {
        public static int Main(string[] args)
        {
            if (args.Length != 2)
            {
                WriteHelp();
                return 1;
            }

            string schemaFile = args[0];
            string jsonFile = args[1];

            if (!CheckArgs(schemaFile, jsonFile))
                return 1;

            JSchema schema;
            using (StreamReader schemaSr = File.OpenText(schemaFile))
            {
                schema = JSchema.Parse(schemaSr.ReadToEnd());
            }

            JObject fileToCheck;
            using (StreamReader schemaSr = File.OpenText(jsonFile))
            {
                fileToCheck = JObject.Parse(schemaSr.ReadToEnd());
            }

            Console.WriteLine($"Validating {jsonFile} against {schemaFile}...");

            bool valid = fileToCheck.IsValid(schema, out IList<ValidationError> errors);

            if (valid)
            {
                Console.WriteLine($"{jsonFile} : {schemaFile} is VALID!");
                return 0;
            }
            else
            {
                Console.WriteLine($"{jsonFile} : {schemaFile} is INVALID!");
                foreach (ValidationError error in errors)
                {
                    Console.WriteLine($"[JSON ERROR] Line: {error.LineNumber}:{error.LinePosition} {error.Message}");
                }
                return 1;
            }
        }

        private static void WriteHelp()
        {
            Console.WriteLine("Ivalid usage 2 parameters expected. First schema, second file to check.");
        }

        private static bool CheckArgs(string schemaFile, string jsonFile)
        {
            if (!File.Exists(schemaFile))
            {
                Console.WriteLine($"Schema file {schemaFile} not found");
                return false;
            }

            if (!File.Exists(jsonFile))
            {
                Console.WriteLine($"Json file {jsonFile} not found");
                return false;
            }

            return true;
        }
    }
}
