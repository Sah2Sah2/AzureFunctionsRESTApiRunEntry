using RESTApiAzureFunctions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTApiAzureFunctions
{
    // Momentary - move to DB later
    public static class RunDataStore
    {
        public static List<RunEntry> Runs { get; set; } = new List<RunEntry>();
    }
}
