using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elmah;
using UAVdrone.Core.Model;
using UAVdrone.Core.Repository.Interface;
using UAVdrone.Helper.Constants;

namespace UAVdrone.Core.Repository
{
    public class DroneRepository : IDroneRepository
    {
        public BattleField VerifyBattleFieldInit(int? width, int? height)
        {
            try
            {
                if (!width.HasValue || !height.HasValue) throw new Exception("Width and height are both required");

                if (width <= 0 || height <= 0) throw new Exception("Width and height must greater than 0");

                var battleField = new BattleField
                {
                    Width = width.Value,
                    Height = height.Value
                };
                return battleField;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }        

        public List<DronePosition> ExecuteListCommands(BattleField battleField, List<DroneControl> droneControls)
        {
            try
            {
                droneControls.ForEach(drone => {
                    drone.ExecuteCommands(battleField);
                });
                return droneControls.Select(p=>p.CurrentPosition).ToList();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }
    }
}
