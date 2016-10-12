using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAVdrone.Core.Model;
using UAVdrone.Helper.Constants;

namespace UAVdrone.Core.Repository.Interface
{
    public interface IDroneRepository
    {
        BattleField VerifyBattleFieldInit(int? width, int? height);
        List<DronePosition> ExecuteListCommands(BattleField battleField, List<DroneControl> droneControls);

        Dictionary<string, string> GetCompassItems();

        Dictionary<string, string> GetCommands();
    }
}
