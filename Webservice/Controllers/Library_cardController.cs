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
    public class Library_cardController : ControllerBase
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
        public Library_cardController(IHostingEnvironment hostingEnvironment, AppSettingsHelper appSettings,
            DatabaseContextHelper database)
        {
            HostingEnvironment = hostingEnvironment;
            AppSettings = appSettings;
            Database = database;
        }

        #endregion


        // Gets an instance.
        [HttpGet]
        [Route("library_card")]
        public ResponseMessage GetLibrary_card(int? id_no)
        {
            if (id_no < 0)
            {
                var response = Library_cardHelper.GetCollection(
                context: Database.DbContext,
                statusCode: out HttpStatusCode statusCode,
                includeDetailedErrors: HostingEnvironment.IsDevelopment());
                HttpContext.Response.StatusCode = (int)statusCode;
                return response;
            } else
            {
                var response = Library_cardHelper.Get(id_no,
                context: Database.DbContext,
                statusCode: out HttpStatusCode statusCode,
                includeDetailedErrors: HostingEnvironment.IsDevelopment());
                HttpContext.Response.StatusCode = (int)statusCode;
                return response;
            }
            
        }

        



        // Adds a new instance.
        [HttpPost]
        [Route("library_card")]
        public ResponseMessage AddLibrary_card([FromBody] JObject data)
        {

            var response = Library_cardHelper.Add(data,
                context: Database.DbContext,
                statusCode: out HttpStatusCode statusCode,
                includeDetailedErrors: HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)statusCode;
            return response;
        }




        // Edits an instance.
        [HttpPut]
        [Route("library_card")]
        public ResponseMessage EditLibrary_card([FromBody] JObject data)
        {
            var response = Library_cardHelper.Edit(data,
                context: Database.DbContext,
                statusCode: out HttpStatusCode statusCode,
                includeDetailedErrors: HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)statusCode;
            return response;
        }


        // Deletes an instance
        [HttpDelete]
        [Route("library_card")]
        public ResponseMessage DeleteLibrary_card([FromBody] JObject data)
        {
            var response = Library_cardHelper.Delete(data,
                context: Database.DbContext,
                statusCode: out HttpStatusCode statusCode,
                includeDetailedErrors: HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)statusCode;
            return response;
        }
    }
}
