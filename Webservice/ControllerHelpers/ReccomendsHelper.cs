using BusinessLibrary.Models;
using DatabaseLibrary.Core;
using DatabaseLibrary.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Webservice.ControllerHelpers
{
    public class ReccomendsHelper
    {

        #region Converters

        /// <summary>
        /// Converts database models to a business logic object.
        /// </summary>
        public static BusinessLibrary.Models.Reccomends Convert(Reccomends_db instance)
        {
            if (instance == null)
                return null;
            return new BusinessLibrary.Models.Reccomends(instance.Reccomendation_address, instance.Media_id, instance.Reccomendation_card);
        }

        #endregion

        /// <summary>
        /// Signs up a Reccomends.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage Add(JObject data,
            DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Extract paramters
            string reccomendation_address = (data.ContainsKey("reccomendation_address")) ? data.GetValue("reccomendation_address").Value<string>() : null;
            int media_id = (data.ContainsKey("media_id")) ? data.GetValue("media_id").Value<int>() : -1;
            string reccomendation_card = (data.ContainsKey("reccomendation_card")) ? data.GetValue("reccomendation_card").Value<string>() : null;


            // Add instance to database
            var dbInstance = DatabaseLibrary.Helpers.ReccomendsHelper_db.Add(reccomendation_address, media_id, reccomendation_card,
                context, out StatusResponse statusResponse);

            // Get rid of detailed internal server error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while adding a new Reccomends.";

            // Return response
            var response = new ResponseMessage
                (
                    dbInstance != null,
                    statusResponse.Message,
                    Convert(dbInstance)
                );
            statusCode = statusResponse.StatusCode;
            return response;
        }

        /// <summary>
        /// Edits a Reccomends.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage Edit(JObject data,
            DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Extract paramters
            string reccomendation_address = (data.ContainsKey("reccomendation_address")) ? data.GetValue("reccomendation_address").Value<string>() : null;
            int media_id = (data.ContainsKey("media_id")) ? data.GetValue("media_id").Value<int>() : -1;
            string reccomendation_card = (data.ContainsKey("reccomendation_card")) ? data.GetValue("reccomendation_card").Value<string>() : null;

            // Add instance to database
            var dbInstance = DatabaseLibrary.Helpers.ReccomendsHelper_db.Edit(reccomendation_address, media_id, reccomendation_card,
                context, out StatusResponse statusResponse);

            // Get rid of detailed internal server error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while editing a Reccomends.";

            // Return response
            var response = new ResponseMessage
                (
                    dbInstance != null,
                    statusResponse.Message,
                    Convert(dbInstance)
                );
            statusCode = statusResponse.StatusCode;
            return response;

        }

        /// <summary>
        /// Deletes a Reccomends.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage Delete(JObject data,
            DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Extract paramters
            string reccomendation_address = (data.ContainsKey("reccomendation_address")) ? data.GetValue("reccomendation_address").Value<string>() : null;
            string reccomendation_card = (data.ContainsKey("reccomendation_card")) ? data.GetValue("reccomendation_card").Value<string>() : null;

            // Add instance to database
            DatabaseLibrary.Helpers.ReccomendsHelper_db.Delete(reccomendation_address, reccomendation_card, context, out StatusResponse statusResponse);

            bool failed = false;

            // Get rid of detailed internal server error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors) {
                statusResponse.Message = "Something went wrong while deleting a Reccomends.";
                failed = true;
            }

            // Return response
            var response = new ResponseMessage
                (
                    failed,
                    statusResponse.Message
                );
            statusCode = statusResponse.StatusCode;
            return response;
        }


        /// <summary>
        /// Gets a Reccomends.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage Get(string? reccomendation_address, string?  reccomendation_card,
        DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {

            // Get instances from database
            var dbInstance = DatabaseLibrary.Helpers.ReccomendsHelper_db.Get(reccomendation_address, reccomendation_card,
                context, out StatusResponse statusResponse);

            // Convert to business logic objects
            var instance = Convert(dbInstance);

            // Get rid of detailed error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while retrieving the Reccomends";

            // Return response
            var response = new ResponseMessage
                (
                    instance != null,
                    statusResponse.Message,
                    instance
                );
            statusCode = statusResponse.StatusCode;
            return response;
        }


        /// <summary>
        /// Gets list of Reccomends.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage GetCollection(
        DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Get instances from database
            var dbInstances = DatabaseLibrary.Helpers.ReccomendsHelper_db.GetCollection(
                context, out StatusResponse statusResponse);

            // Convert to business logic objects
            var instances = dbInstances?.Select(x => Convert(x)).ToList();

            // Get rid of detailed error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while retrieving the Reccomends";

            // Return response
            var response = new ResponseMessage
                (
                    instances != null,
                    statusResponse.Message,
                    instances
                );
            statusCode = statusResponse.StatusCode;
            return response;
        }

    }
}
