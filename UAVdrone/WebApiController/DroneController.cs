using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Elmah;
using UAVdrone.Core.Model;
using UAVdrone.Core.Repository;
using UAVdrone.Core.Repository.Interface;
using UAVdrone.Helper.Constants;
using UAVdrone.Models;
using WebGrease.Css.Extensions;

namespace UAVdrone.WebApiController
{
    public class DroneController : ApiController
    {
        private IDroneRepository repo;
        [HttpPost]
        public HttpResponseMessage SetupBattlefield(BattleFieldModels battlefield)
        {
            try
            {
                if(battlefield == null || !ModelState.IsValid) throw new Exception("Invalid battlefield width and height");

                repo = new DroneRepository();
                var result = repo.VerifyBattleFieldInit(battlefield.Width, battlefield.Height);
                if(result == null) throw new Exception("Initialize battlefield failed. Invalid battlefield size.");

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetCompassItems()
        {
            try
            {
                repo = new DroneRepository();
                var result = repo.GetCompassItems().Select(p=>new
                {
                    p.Key,
                    p.Value
                }).ToList();
                                
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetCommands()
        {
            try
            {
                repo = new DroneRepository();
                var result = repo.GetCommands().Select(p => new
                {
                    p.Key,
                    p.Value
                }).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage RunDroneCommands(ExecutionCommandViewModels model)
        {
            try
            {
                Func<string, Constant.CompassPoint> parseCompass = (input) =>
                {
                    Constant.CompassPoint direction;
                    return Enum.TryParse(input, true, out direction) ? direction : Constant.CompassPoint.N;
                };

                Func<string, List<Constant.DroneCommand>> parseCommand = (input) =>
                {
                    var commands = new List<Constant.DroneCommand>();
                    var commandLetters = input.ToCharArray();
                    commandLetters.ForEach(p =>
                    {
                        Constant.DroneCommand current;
                        if(Enum.TryParse(p.ToString(), true, out current)) commands.Add(current);
                    });
                    return commands;
                };

                repo = new DroneRepository();
                if(model?.BattleField == null) throw new Exception("Battlefield is not initialized.");

                var battlefield = new BattleField
                {
                    Width = model.BattleField.Width ?? 0,
                    Height = model.BattleField.Height ?? 0
                };

                var drones = model.Drones.Select(p => new DroneControl
                {
                    
                    CurrentPosition = new DronePosition
                    {
                        XCoordinate = p.xcoordinate ?? 0,
                        YCoordinate = p.ycoordinate ?? 0,
                        FaceDirection = parseCompass(p.direction)
                    },
                    Commands = parseCommand(p.commands)
                }).ToList();

                var result = repo.ExecuteListCommands(battlefield, drones);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}