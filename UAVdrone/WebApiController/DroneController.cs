using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Elmah;
using UAVdrone.Core.Repository;
using UAVdrone.Core.Repository.Interface;
using UAVdrone.Models;

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
    }
}