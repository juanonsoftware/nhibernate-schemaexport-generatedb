using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NhibMigrations.Console
{
    internal class Helper
    {
        public static IEnumerable<string> GetMappingAssemblies()
        {
            return File.ReadAllLines("MappingAssemblies.txt").Where(x => !string.IsNullOrWhiteSpace(x));
        }
    }
}
