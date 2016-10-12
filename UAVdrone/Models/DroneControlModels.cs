using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UAVdrone.Models
{
    public class DroneControlModels
    {
        public int? xcoordinate { get; set; }
        public int? ycoordinate { get; set; }
        public string direction { get; set; }

        public string commands { get; set; }
    }
}