using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAVdrone.Helper.Constants;

namespace UAVdrone.Core.Model.Interface
{
    interface IDronePositionTask
    {
        void RotateDirection(Constant.DroneCommand nextCommand);

        void MoveForward();
    }

    interface IDroneControlTask
    {
        bool ValidateMoveCommand(BattleField batteField);
        void MoveDrone(BattleField battleField, Constant.DroneCommand moveCommand);
        void ExecuteCommands(BattleField battleField);
        void AddToExecutionResult(Constant.DroneCommand command, string result);
    }
}
