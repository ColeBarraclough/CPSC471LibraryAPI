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
    public class RecommendsHelper
    {

        #region Converters

        /// <summary>
        /// Converts database models to a business logic object.
        /// </summary>
        public static BusinessLibrary.Models.Recommends Convert(Recommends_db instance)
        {
            if (instance == null)
                return null;
            return new BusinessLibrary.Models.Recommends(instance.Recommendation_address, instance.Media_id, instance.Recommendation_card);
        }

        #endregion

        /// <summary>
        /// Signs up a Recommends.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage Add(JObject data,
            DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Extract paramters
            string recommendation_address = (data.ContainsKey("recommendation_address")) ? data.GetValue("recommendation_address").Value<string>() : null;
            int media_id = (data.ContainsKey("media_id")) ? data.GetValue("media_id").Value<int>() : -1;
            string recommendation_card = (data.ContainsKey("recommendation_card")) ? data.GetValue("recommendation_card").Value<string>() : null;


            // Add instance to database
            var dbInstance = DatabaseLibrary.Helpers.RecommendsHelper_db.Add(recommendation_address, media_id, recommendation_card,
                context, out StatusResponse statusResponse);

            // Get rid of detailed internal server error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while adding a new Recommends.";

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
        /// Edits a Recommends.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage Edit(JObject data,
            DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Extract paramters
            string recommendation_address = (data.ContainsKey("recommendation_address")) ? data.GetValue("recommendation_address").Value<string>() : null;
            int media_id = (data.ContainsKey("media_id")) ? data.GetValue("media_id").Value<int>() : -1;
            string recommendation_card = (data.ContainsKey("recommendation_card")) ? data.GetValue("recommendation_card").Value<string>() : null;

            // Add instance to database
            var dbInstance = DatabaseLibrary.Helpers.RecommendsHelper_db.Edit(recommendation_address, media_id, recommendation_card,
                context, out StatusResponse statusResponse);

            // Get rid of detailed internal server error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while editing a Recommends.";

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
        /// Deletes a Recommends.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage Delete(JObject data,
            DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Extract paramters
            string recommendation_address = (data.ContainsKey("recommendation_address")) ? data.GetValue("recommendation_address").Value<string>() : null;
            string recommendation_card = (data.ContainsKey("recommendation_card")) ? data.GetValue("recommendation_card").Value<string>() : null;
            string media_id = (data.ContainsKey("media_id")) ? data.GetValue("media_id").Value<string>() : null;

            // Add instance to database
            DatabaseLibrary.Helpers.RecommendsHelper_db.Delete(recommendation_address, recommendation_card, media_id, context, out StatusResponse statusResponse);

            bool failed = false;

            // Get rid of detailed internal server error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors) {
                statusResponse.Message = "Something went wrong while deleting a Recommends.";
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
        /// Gets a Recommends.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage Get(string? recommendation_address, string?  recommendation_card, string? media_id,
        DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {

            // Get instances from database
            var dbInstance = DatabaseLibrary.Helpers.RecommendsHelper_db.Get(recommendation_address, recommendation_card, media_id,
                context, out StatusResponse statusResponse);

            // Convert to business logic objects
            var instance = Convert(dbInstance);

            // Get rid of detailed error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while retrieving the Recommends";

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
        /// Gets list of Recommends.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage GetCollection(
        DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Get instances from database
            var dbInstances = DatabaseLibrary.Helpers.RecommendsHelper_db.GetCollection(
                context, out StatusResponse statusResponse);

            // Convert to business logic objects
            var instances = dbInstances?.Select(x => Convert(x)).ToList();

            // Get rid of detailed error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while retrieving the Recommends";

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
