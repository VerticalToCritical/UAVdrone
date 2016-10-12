using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elmah;
using UAVdrone.Core.Model.Interface;
using UAVdrone.Helper.Constants;
using UAVdrone.Helper.Helper;

namespace UAVdrone.Core.Model
{
    public class DronePosition : IDronePositionTask
    {        
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }        

        public Constant.CompassPoint FaceDirection { get; set; }

        public void RotateDirection(Constant.DroneCommand nextCommand)
        {
            switch (nextCommand)
            {
                case Constant.DroneCommand.L:
                    //North => West
                    FaceDirection = (FaceDirection == Constant.CompassPoint.N)
                        ? Constant.CompassPoint.W
                        //West => South
                        : (FaceDirection == Constant.CompassPoint.W)
                            ? Constant.CompassPoint.S
                            //South => East
                            : (FaceDirection == Constant.CompassPoint.S)
                                ? Constant.CompassPoint.E
                                //East => North
                                : Constant.CompassPoint.N;
                    break;
                case Constant.DroneCommand.R:
                    //North =>East
                    FaceDirection = (FaceDirection == Constant.CompassPoint.N)
                        ? Constant.CompassPoint.E
                        //East => South
                        : (FaceDirection == Constant.CompassPoint.E)
                            ? Constant.CompassPoint.S
                            //South => West
                            : (FaceDirection == Constant.CompassPoint.S)
                                ? Constant.CompassPoint.W
                                //West => North
                                : Constant.CompassPoint.N;
                    break;
                //invalid command, only 'L' & 'R' are acceptable
                default:
                    break;
            }
        }

        public void MoveForward()
        {
            switch (FaceDirection)
            {
                case Constant.CompassPoint.N:
                    YCoordinate++;
                    break;
                case Constant.CompassPoint.S:
                    YCoordinate--;
                    break;
                case Constant.CompassPoint.W:
                    XCoordinate--;
                    break;
                case Constant.CompassPoint.E:
                    XCoordinate++;
                    break;
            }
        }
    }

    public class DroneControl : IDroneControlTask
    {
        public DronePosition CurrentPosition { get; set; }
        public List<Constant.DroneCommand> Commands { get; set; }
        public Dictionary<Constant.DroneCommand, string> ExecutionResult { get; set; }

        /// <summary>
        /// method to validate whether drone can move forward by one grid
        /// </summary>
        /// <param name="battleField"></param>
        /// <returns></returns>
        public bool ValidateMoveCommand(BattleField battleField)
        {
            try
            {
                if (battleField == null) throw new Exception("Battle field is not initialized");

                switch (CurrentPosition.FaceDirection)
                {
                    case Constant.CompassPoint.N:
                        return CurrentPosition.YCoordinate < battleField.Height;
                    case Constant.CompassPoint.S:
                        return CurrentPosition.YCoordinate > 0;
                    case Constant.CompassPoint.W:
                        return CurrentPosition.XCoordinate > 0;
                    case Constant.CompassPoint.E:
                        return CurrentPosition.XCoordinate < battleField.Width;
                    default:
                        return false;
                }
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                return false;
            }
        }

        /// <summary>
        /// method to move drone forward or change drone's facing direction
        /// and also log the result of each command
        /// </summary>
        /// <param name="battleField"></param>
        /// <param name="moveCommand"></param>
        public void MoveDrone(BattleField battleField, Constant.DroneCommand moveCommand)
        {
            try
            {
                if (moveCommand == Constant.DroneCommand.M)
                {
                    if (ValidateMoveCommand(battleField))
                    {
                        CurrentPosition.MoveForward();
                        AddToExecutionResult(moveCommand, $"Drone moved forward. Current position is X: {CurrentPosition.XCoordinate}, Y: {CurrentPosition.YCoordinate}, Direction: {CurrentPosition.FaceDirection.GetDescription()}");
                    }
                    else
                    {
                        AddToExecutionResult(moveCommand, $"Drone cannot move forward because it is on the boundary of battlefield. Position not changed and is still X: {CurrentPosition.XCoordinate}, Y: {CurrentPosition.YCoordinate}, Direction: {CurrentPosition.FaceDirection.GetDescription()}");
                    }
                }
                else
                {
                    CurrentPosition.RotateDirection(moveCommand);
                    AddToExecutionResult(moveCommand, $"Drone changed position. Current position is X: {CurrentPosition.XCoordinate}, Y: {CurrentPosition.YCoordinate}, Direction: {CurrentPosition.FaceDirection.GetDescription()}");
                }
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }

        public void ExecuteCommands(BattleField battleField)
        {
            ExecutionResult = new Dictionary<Constant.DroneCommand, string>();
            Commands.ForEach(command =>
            {
                MoveDrone(battleField, command);
            });
        }

        public void AddToExecutionResult(Constant.DroneCommand command, string result)
        {
            if (ExecutionResult.ContainsKey(command))
            {
                ExecutionResult[command] = result;
            }
            else
            {
                ExecutionResult.Add(command, result);
            }
        }
    }
}
