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
    public class CdHelper
    {

        #region Converters

        /// <summary>
        /// Converts database models to a business logic object.
        /// </summary>
        public static BusinessLibrary.Models.Cd Convert(Cd_db instance)
        {
            if (instance == null)
                return null;
            return new BusinessLibrary.Models.Cd(instance.System_id, instance.Title, instance.Library_address, instance.Genre, instance.Publishing_date, instance.Author_id, instance.Borrower_id, instance.Date_of_check_out, instance.Due_date, instance.Length);
        }

        #endregion

        /// <summary>
        /// Creates up a cd.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage Add(JObject data,
            DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Extract paramters
            string title = (data.ContainsKey("title")) ? data.GetValue("title").Value<string>() : null;
            string libraryAddress = (data.ContainsKey("library_address")) ? data.GetValue("library_address").Value<string>() : null;
            string genre = (data.ContainsKey("genre")) ? data.GetValue("genre").Value<string>() : null;
            DateTime publishingDate = (data.ContainsKey("publishing_date")) ? data.GetValue("publishing_date").Value<DateTime>() : new DateTime();
            int authorId = (data.ContainsKey("author_id")) ? data.GetValue("author_id").Value<int>() : -1;
            int? borrowerId = (data.ContainsKey("borrower_id")) ? data.GetValue("borrower_id").Value<int?>() : null;
            DateTime? dateOfCheckOut = (data.ContainsKey("date_of_check_out")) ? data.GetValue("date_of_check_out").Value<DateTime?>() : null;
            DateTime? dueDate = (data.ContainsKey("due_date")) ? data.GetValue("due_date").Value<DateTime?>() : null;
            int length = (data.ContainsKey("length")) ? data.GetValue("length").Value<int>() : -1;


            // Add instance to database
            var dbInstance = DatabaseLibrary.Helpers.CdHelper_db.Add(0, title, libraryAddress, genre, publishingDate, authorId, borrowerId, dateOfCheckOut, dueDate, length,
                context, out StatusResponse statusResponse);

            // Get rid of detailed internal server error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while adding a new book.";

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
        /// Edits a cd.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage Edit(JObject data,
            DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Extract paramters
            int systemId = (data.ContainsKey("system_id")) ? data.GetValue("system_id").Value<int>() : -1;
            string title = (data.ContainsKey("title")) ? data.GetValue("title").Value<string>() : null;
            string libraryAddress = (data.ContainsKey("library_address")) ? data.GetValue("library_address").Value<string>() : null;
            string genre = (data.ContainsKey("genre")) ? data.GetValue("genre").Value<string>() : null;
            DateTime publishingDate = (data.ContainsKey("publishing_date")) ? data.GetValue("publishing_date").Value<DateTime>() : new DateTime();
            int authorId = (data.ContainsKey("author_id")) ? data.GetValue("author_id").Value<int>() : -1;
            int? borrowerId = (data.ContainsKey("borrower_id")) ? data.GetValue("borrower_id").Value<int?>() : null;
            DateTime? dateOfCheckOut = (data.ContainsKey("date_of_check_out")) ? data.GetValue("date_of_check_out").Value<DateTime?>() : null;
            DateTime? dueDate = (data.ContainsKey("due_date")) ? data.GetValue("due_date").Value<DateTime?>() : null;
            int length = (data.ContainsKey("length")) ? data.GetValue("length").Value<int>() : -1;

            // Add instance to database
            var dbInstance = DatabaseLibrary.Helpers.CdHelper_db.Edit(systemId, title, libraryAddress, genre, publishingDate, authorId, borrowerId, dateOfCheckOut, dueDate, length,
                context, out StatusResponse statusResponse);

            // Get rid of detailed internal server error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while editing a book.";

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
        /// Deletes a cd.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage Delete(JObject data,
            DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Extract paramters
            int systemId = (data.ContainsKey("system_id")) ? data.GetValue("system_id").Value<int>() : -1;

            // Add instance to database
            DatabaseLibrary.Helpers.CdHelper_db.Delete(systemId, context, out StatusResponse statusResponse);

            bool failed = false;

            // Get rid of detailed internal server error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
            {
                statusResponse.Message = "Something went wrong while deleting a cd.";
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
        /// Gets a cd.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage Get(int? id,
        DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Extract paramters
            int system_id = (int)id;


            // Get instances from database
            var dbInstance = DatabaseLibrary.Helpers.CdHelper_db.Get(system_id,
                context, out StatusResponse statusResponse);

            // Convert to business logic objects
            var instance = Convert(dbInstance);

            // Get rid of detailed error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while retrieving the cd";

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
        /// Gets list of cds.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage GetCollection(
        DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Get instances from database
            var dbInstances = DatabaseLibrary.Helpers.CdHelper_db.GetCollection(
                context, out StatusResponse statusResponse);

            // Convert to business logic objects
            var instances = dbInstances?.Select(x => Convert(x)).ToList();

            // Get rid of detailed error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while retrieving the cds";

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
