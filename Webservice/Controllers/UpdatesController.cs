using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BusinessLibrary.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Webservice.ContextHelpers;
using Webservice.ControllerHelpers;
using Newtonsoft.Json.Linq;

namespace Webservice.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UpdatesController : ControllerBase
    {

        #region Initialization

        /// <summary>
        /// Reference to the hosting environment instance added in the Startup.cs.
        /// </summary>
        private readonly IHostingEnvironment HostingEnvironment;

        /// <summary>
        /// Reference to the app settings helper instance added in the Startup.cs.
        /// </summary>
        private readonly AppSettingsHelper AppSettings;

        /// <summary>
        /// Reference to the database context helper instance added in the Startup.cs.
        /// </summary>
        private readonly DatabaseContextHelper Database;

        /// <summary>
        /// Constructor called by the service provider.
        /// Using injection to get the arguments.
        /// </summary>
        public UpdatesController(IHostingEnvironment hostingEnvironment, AppSettingsHelper appSettings,
            DatabaseContextHelper database)
        {
            HostingEnvironment = hostingEnvironment;
            AppSettings = appSettings;
            Database = database;
        }

        #endregion


        // Gets an instance.
        [HttpGet]
        [Route("update")]
        public ResponseMessage GetUpdate(int? librarian_id, int? media_id)
        {
            if (librarian_id == null || media_id == null)
            {
                var response = UpdatesHelper.GetCollection(
                context: Database.DbContext,
                statusCode: out HttpStatusCode statusCode,
                includeDetailedErrors: HostingEnvironment.IsDevelopment());
                HttpContext.Response.StatusCode = (int)statusCode;
                return response;
            }
            else
            {
                var response = UpdatesHelper.Get(librarian_id, media_id,
                context: Database.DbContext,
                statusCode: out HttpStatusCode statusCode,
                includeDetailedErrors: HostingEnvironment.IsDevelopment());
                HttpContext.Response.StatusCode = (int)statusCode;
                return response;
            }

        }





        // Adds a new instance.
        [HttpPost]
        [Route("update")]
        public ResponseMessage AddUpdate([FromBody] JObject data)
        {

            var response = UpdatesHelper.Add(data,
                context: Database.DbContext,
                statusCode: out HttpStatusCode statusCode,
                includeDetailedErrors: HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)statusCode;
            return response;
        }


        // Deletes an instance
        [HttpDelete]
        [Route("update")]
        public ResponseMessage DeleteUpdate([FromBody] JObject data)
        {
            var response = UpdatesHelper.Delete(data,
                context: Database.DbContext,
                statusCode: out HttpStatusCode statusCode,
                includeDetailedErrors: HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)statusCode;
            return response;
        }
    }
}
