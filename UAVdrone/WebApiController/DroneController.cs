using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Elmah;
using UAVdrone.Models;

namespace UAVdrone.WebApiController
{
    public class DroneController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage SetupBattlefield(BattleFieldModels battlefield)
        {
            try
            {
                if(!ModelState.IsValid) throw new Exception("Invalid battlefield width and height");
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}