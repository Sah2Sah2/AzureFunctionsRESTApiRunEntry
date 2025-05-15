using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTApiAzureFunctions.Models
{
    public class RunEntry
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public double DistanceKm { get; set; }
        public TimeSpan Duration { get; set; }
        public double PaceMinPerKm => Duration.TotalMinutes / DistanceKm;
        public string ShoesUsed { get; set; }
        public string Notes { get; set; }
    }
}
