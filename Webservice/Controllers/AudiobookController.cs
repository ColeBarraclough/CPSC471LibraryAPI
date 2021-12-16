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
    public class AudiobookController : ControllerBase
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
        public AudiobookController(IHostingEnvironment hostingEnvironment, AppSettingsHelper appSettings,
            DatabaseContextHelper database)
        {
            HostingEnvironment = hostingEnvironment;
            AppSettings = appSettings;
            Database = database;
        }

        #endregion


        // Gets an instance.
        [HttpGet]
        [Route("audiobook")]
        public ResponseMessage GetBook(int? system_id)
        {
            if (system_id == null)
            {
                var response = AudiobookHelper.GetCollection(
                context: Database.DbContext,
                statusCode: out HttpStatusCode statusCode,
                includeDetailedErrors: HostingEnvironment.IsDevelopment());
                HttpContext.Response.StatusCode = (int)statusCode;
                return response;
            }
            else
            {
                var response = AudiobookHelper.Get(system_id,
                context: Database.DbContext,
                statusCode: out HttpStatusCode statusCode,
                includeDetailedErrors: HostingEnvironment.IsDevelopment());
                HttpContext.Response.StatusCode = (int)statusCode;
                return response;
            }

        }





        // Adds a new instance.
        [HttpPost]
        [Route("audiobook")]
        public ResponseMessage AddBook([FromBody] JObject data)
        {

            var response = AudiobookHelper.Add(data,
                context: Database.DbContext,
                statusCode: out HttpStatusCode statusCode,
                includeDetailedErrors: HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)statusCode;
            return response;
        }




        // Edits an instance.
        [HttpPut]
        [Route("audiobook")]
        public ResponseMessage EditBook([FromBody] JObject data)
        {
            var response = AudiobookHelper.Edit(data,
                context: Database.DbContext,
                statusCode: out HttpStatusCode statusCode,
                includeDetailedErrors: HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)statusCode;
            return response;
        }


        // Deletes an instance
        [HttpDelete]
        [Route("audiobook")]
        public ResponseMessage DeleteBook([FromBody] JObject data)
        {
            var response = AudiobookHelper.Delete(data,
                context: Database.DbContext,
                statusCode: out HttpStatusCode statusCode,
                includeDetailedErrors: HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)statusCode;
            return response;
        }
    }
}
