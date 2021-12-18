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
    public class RecommendsController : ControllerBase
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
        public RecommendsController(IHostingEnvironment hostingEnvironment, AppSettingsHelper appSettings,
            DatabaseContextHelper database)
        {
            HostingEnvironment = hostingEnvironment;
            AppSettings = appSettings;
            Database = database;
        }

        #endregion


        // Gets an instance.
        [HttpGet]
        [Route("recommendation")]
        public ResponseMessage GetRecommends(string? recommendation_address, string? recommendation_card, string? media_id)
        {
            if (recommendation_address == null || recommendation_card == null || media_id == null)
            {
                var response = RecommendsHelper.GetCollection(
                context: Database.DbContext,
                statusCode: out HttpStatusCode statusCode,
                includeDetailedErrors: HostingEnvironment.IsDevelopment());
                HttpContext.Response.StatusCode = (int)statusCode;
                return response;
            } else
            {
                var response = RecommendsHelper.Get(recommendation_address, recommendation_card, media_id,
                context: Database.DbContext,
                statusCode: out HttpStatusCode statusCode,
                includeDetailedErrors: HostingEnvironment.IsDevelopment());
                HttpContext.Response.StatusCode = (int)statusCode;
                return response;
            }
            
        }

        



        // Adds a new instance.
        [HttpPost]
        [Route("recommendation")]
        public ResponseMessage AddRecommends([FromBody] JObject data)
        {

            var response = RecommendsHelper.Add(data,
                context: Database.DbContext,
                statusCode: out HttpStatusCode statusCode,
                includeDetailedErrors: HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)statusCode;
            return response;
        }




        // Edits an instance.
        [HttpPut]
        [Route("recommendation")]
        public ResponseMessage EditRecommends([FromBody] JObject data)
        {
            var response = RecommendsHelper.Edit(data,
                context: Database.DbContext,
                statusCode: out HttpStatusCode statusCode,
                includeDetailedErrors: HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)statusCode;
            return response;
        }


        // Deletes an instance
        [HttpDelete]
        [Route("recommendation")]
        public ResponseMessage DeleteRecommends([FromBody] JObject data)
        {
            var response = RecommendsHelper.Delete(data,
                context: Database.DbContext,
                statusCode: out HttpStatusCode statusCode,
                includeDetailedErrors: HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)statusCode;
            return response;
        }
    }
}
