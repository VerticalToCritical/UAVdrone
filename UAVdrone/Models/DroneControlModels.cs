using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UAVdrone.Models
{
    public class DroneControlModels
    {
        public int? XCoordinate { get; set; }
        public int? YCoordinate { get; set; }
        public string DirectionLetter { get; set; }

        public List<string> CommandList { get; set; }
    }
}