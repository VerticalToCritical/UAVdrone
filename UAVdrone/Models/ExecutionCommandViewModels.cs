using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UAVdrone.Models
{
    public class ExecutionCommandViewModels
    {
        public BattleFieldModels BattleField { get; set; }
        public List<DroneControlModels> Drones { get; set; }
    }
}