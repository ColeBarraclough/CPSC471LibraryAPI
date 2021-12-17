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
    public class Library_cardHelper
    {

        #region Converters

        /// <summary>
        /// Converts database models to a business logic object.
        /// </summary>
        public static BusinessLibrary.Models.Library_card Convert(Library_card_db instance)
        {
            if (instance == null)
                return null;
            return new BusinessLibrary.Models.Library_card(instance.Id_no, instance.Issuer_address, instance.Date_of_expiration);
        }

        #endregion

        /// <summary>
        /// Signs up a Library_card.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage Add(JObject data,
            DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Extract paramters
            string issuer_address = (data.ContainsKey("issuer_address")) ? data.GetValue("issuer_address").Value<string>() : null;
            DateTime date_of_expiration = (data.ContainsKey("date_of_expiration")) ? data.GetValue("date_of_expiration").Value<DateTime>() : new DateTime();


            // Add instance to database
            var dbInstance = DatabaseLibrary.Helpers.Library_cardHelper_db.Add(issuer_address, date_of_expiration,
                context, out StatusResponse statusResponse);

            // Get rid of detailed internal server error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while adding a new Library_card.";

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
        /// Edits a Library_card.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage Edit(JObject data,
            DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Extract paramters
            int id_no = (data.ContainsKey("id_no")) ? data.GetValue("id_no").Value<int>() : -1;
            string issuer_address = (data.ContainsKey("issuer_address")) ? data.GetValue("issuer_address").Value<string>() : null;
            DateTime date_of_expiration = (data.ContainsKey("date_of_expiration")) ? data.GetValue("date_of_expiration").Value<DateTime>() : new DateTime();

            // Add instance to database
            var dbInstance = DatabaseLibrary.Helpers.Library_cardHelper_db.Edit(id_no, issuer_address, date_of_expiration,
                context, out StatusResponse statusResponse);

            // Get rid of detailed internal server error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while editing a Library_card.";

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
        /// Deletes a Library_card.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage Delete(JObject data,
            DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Extract paramters
            int id_no = (data.ContainsKey("id_no")) ? data.GetValue("id_no").Value<int>() : -1;

            // Add instance to database
            DatabaseLibrary.Helpers.Library_cardHelper_db.Delete(id_no, context, out StatusResponse statusResponse);

            bool failed = false;

            // Get rid of detailed internal server error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors) {
                statusResponse.Message = "Something went wrong while deleting a Library_card.";
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
        /// Gets a Library_card.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage Get(int? id,
        DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Extract paramters
            int id_no = (int) id;


            // Get instances from database
            var dbInstance = DatabaseLibrary.Helpers.Library_cardHelper_db.Get(id_no,
                context, out StatusResponse statusResponse);

            // Convert to business logic objects
            var instance = Convert(dbInstance);

            // Get rid of detailed error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while retrieving the Library_card";

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

        public static ResponseMessage Get(string? issuer_address,
DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {

            // Get instances from database
            var dbInstance = DatabaseLibrary.Helpers.Library_cardHelper_db.Get(issuer_address,
                context, out StatusResponse statusResponse);

            // Convert to business logic objects
            var instance = Convert(dbInstance);

            // Get rid of detailed error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while retrieving the Library_card";

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
        /// Gets list of Library_cards.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage GetCollection(
        DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Get instances from database
            var dbInstances = DatabaseLibrary.Helpers.Library_cardHelper_db.GetCollection(
                context, out StatusResponse statusResponse);

            // Convert to business logic objects
            var instances = dbInstances?.Select(x => Convert(x)).ToList();

            // Get rid of detailed error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while retrieving the Library_cards";

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
