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
    public class UpdatesHelper
    {

        #region Converters

        /// <summary>
        /// Converts database models to a business logic object.
        /// </summary>
        public static BusinessLibrary.Models.Updates Convert(Updates_db instance)
        {
            if (instance == null)
                return null;
            return new BusinessLibrary.Models.Updates(instance.Librarian_id, instance.Media_id, instance.Library_address);
        }

        #endregion

        /// <summary>
        /// Creates an update.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage Add(JObject data,
            DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Extract paramters
            int librarian_id = (data.ContainsKey("librarian_id")) ? data.GetValue("librarian_id").Value<int>() : -1;
            int media_id = (data.ContainsKey("media_id")) ? data.GetValue("media_id").Value<int>() : -1;
            string library_address = (data.ContainsKey("library_address")) ? data.GetValue("library_address").Value<string>() : null;


            // Add instance to database
            var dbInstance = DatabaseLibrary.Helpers.UpdatesHelper_db.Add(librarian_id, media_id, library_address,
                context, out StatusResponse statusResponse);

            // Get rid of detailed internal server error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while adding a new update.";

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
        /// Deletes an update.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage Delete(JObject data,
            DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Extract paramters
            int librarian_id = (data.ContainsKey("librarian_id")) ? data.GetValue("librarian_id").Value<int>() : -1;
            int media_id = (data.ContainsKey("media_id")) ? data.GetValue("media_id").Value<int>() : -1;
            string library_address = (data.ContainsKey("library_address")) ? data.GetValue("library_address").Value<string>() : null;

            // Add instance to database
            DatabaseLibrary.Helpers.UpdatesHelper_db.Delete(librarian_id, media_id, library_address, context, out StatusResponse statusResponse);

            bool failed = false;

            // Get rid of detailed internal server error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
            {
                statusResponse.Message = "Something went wrong while deleting an update.";
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
        /// Gets an update.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage Get(int? librarian_id, int? media_id,
        DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Extract paramters



            // Get instances from database
            var dbInstance = DatabaseLibrary.Helpers.UpdatesHelper_db.Get((int)librarian_id, (int) media_id,
                context, out StatusResponse statusResponse);

            // Convert to business logic objects
            var instance = Convert(dbInstance);

            // Get rid of detailed error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while retrieving the update";

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
        /// Gets list of updates.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage GetCollection(
        DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Get instances from database
            var dbInstances = DatabaseLibrary.Helpers.UpdatesHelper_db.GetCollection(
                context, out StatusResponse statusResponse);

            // Convert to business logic objects
            var instances = dbInstances?.Select(x => Convert(x)).ToList();

            // Get rid of detailed error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while retrieving the updates";

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
