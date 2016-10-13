using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAVdrone.Core.Model;
using UAVdrone.Core.Repository;
using UAVdrone.Core.Repository.Interface;
using UAVdrone.Helper.Constants;

namespace UAVdrone.Tests.DroneTestCases
{
    [TestClass]
    public class UnitTest1
    {
        IDroneRepository _repository;
        DroneControl _demoDrone;
        BattleField _demoBattleField;

        /// <summary>
        /// Sets up.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            _repository = new DroneRepository();
            _demoDrone = new DroneControl
            {
                InitPosition = new DronePosition
                {
                    XCoordinate = 0,
                    YCoordinate = 0,
                    FaceDirection = Constant.CompassPoint.N
                },
                CurrentPosition = new DronePosition
                {
                    XCoordinate = 0,
                    YCoordinate = 0,
                    FaceDirection = Constant.CompassPoint.N
                },
                Commands = new List<Constant.DroneCommand>
                {
                    Constant.DroneCommand.M,
                    Constant.DroneCommand.M,
                    Constant.DroneCommand.M,
                    Constant.DroneCommand.L,
                    Constant.DroneCommand.M,
                    Constant.DroneCommand.M
                }
            };

            _demoBattleField = new BattleField
            {
                Height = 5,
                Width = 5
            };
        }

        /// <summary>
        /// initial an invalid battlefield
        /// </summary>
        [TestMethod]
        public void Test_Initial_Invalid_Battlefield()
        {
            var battlefield = _repository.VerifyBattleFieldInit(0, 0);
            Assert.IsNull(battlefield);
        }

        /// <summary>
        /// initial a valid battlefield
        /// </summary>
        [TestMethod]
        public void Test_Initial_ValidBattlefield()
        {
            var battlefield = _repository.VerifyBattleFieldInit(5, 5);
            Assert.IsNotNull(battlefield);
            Assert.Equals(5, battlefield.Width);
            Assert.Equals(5, battlefield.Height);
        }

        /// <summary>
        /// move the drone
        /// expected result should be 0 3 W
        /// </summary>
        [TestMethod]
        public void Test_Move_Drone()
        {
            _demoDrone.ExecuteCommands(_demoBattleField);
            Assert.Equals(_demoDrone.CurrentPosition.XCoordinate, 0);
            Assert.Equals(_demoDrone.CurrentPosition.YCoordinate, 3);
            Assert.Equals(_demoDrone.CurrentPosition.FaceDirection, Constant.CompassPoint.W);
        }
    }
}
