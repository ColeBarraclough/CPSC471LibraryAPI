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
    public class AudiobookHelper
    {

        #region Converters

        /// <summary>
        /// Converts database models to a business logic object.
        /// </summary>
        public static BusinessLibrary.Models.Audiobook Convert(Audiobook_db instance)
        {
            if (instance == null)
                return null;
            return new BusinessLibrary.Models.Audiobook(instance.System_id, instance.Title, instance.Genre, instance.Publishing_date, instance.Author_id, instance.Link, instance.Run_Time);
        }

        #endregion

        /// <summary>
        /// Creates up a book.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage Add(JObject data,
            DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Extract paramters
            string title = (data.ContainsKey("title")) ? data.GetValue("title").Value<string>() : null;
            string link = (data.ContainsKey("link")) ? data.GetValue("link").Value<string>() : null;
            string genre = (data.ContainsKey("genre")) ? data.GetValue("genre").Value<string>() : null;
            DateTime publishingDate = (data.ContainsKey("publishing_date")) ? data.GetValue("publishing_date").Value<DateTime>() : new DateTime();
            int authorId = (data.ContainsKey("author_id")) ? data.GetValue("author_id").Value<int>() : -1;
            int run_time = (data.ContainsKey("run_time")) ? data.GetValue("run_time").Value<int>() : -1;


            // Add instance to database
            var dbInstance = DatabaseLibrary.Helpers.AudiobookHelper_db.Add(0, title, genre, publishingDate, authorId, link, run_time,
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
        /// Edits a book.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage Edit(JObject data,
            DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Extract paramters
            int systemId = (data.ContainsKey("system_id")) ? data.GetValue("system_id").Value<int>() : -1;
            string title = (data.ContainsKey("title")) ? data.GetValue("title").Value<string>() : null;
            string link = (data.ContainsKey("link")) ? data.GetValue("link").Value<string>() : null;
            string genre = (data.ContainsKey("genre")) ? data.GetValue("genre").Value<string>() : null;
            DateTime publishingDate = (data.ContainsKey("publishing_date")) ? data.GetValue("publishing_date").Value<DateTime>() : new DateTime();
            int authorId = (data.ContainsKey("author_id")) ? data.GetValue("author_id").Value<int>() : -1;
            int run_time = (data.ContainsKey("run_time")) ? data.GetValue("run_time").Value<int>() : -1;

            // Add instance to database
            var dbInstance = DatabaseLibrary.Helpers.AudiobookHelper_db.Edit(systemId, title, genre, publishingDate, authorId, link, run_time,
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
        /// Deletes a book.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage Delete(JObject data,
            DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Extract paramters
            int systemId = (data.ContainsKey("system_id")) ? data.GetValue("system_id").Value<int>() : -1;

            // Add instance to database
            DatabaseLibrary.Helpers.AudiobookHelper_db.Delete(systemId, context, out StatusResponse statusResponse);

            bool failed = false;

            // Get rid of detailed internal server error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
            {
                statusResponse.Message = "Something went wrong while deleting a book.";
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
        /// Gets a book.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage Get(int? id,
        DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Extract paramters
            int system_id = (int)id;


            // Get instances from database
            var dbInstance = DatabaseLibrary.Helpers.AudiobookHelper_db.Get(system_id,
                context, out StatusResponse statusResponse);

            // Convert to business logic objects
            var instance = Convert(dbInstance);

            // Get rid of detailed error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while retrieving the book";

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
        /// Gets list of books.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage GetCollection(
        DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Get instances from database
            var dbInstances = DatabaseLibrary.Helpers.AudiobookHelper_db.GetCollection(
                context, out StatusResponse statusResponse);

            // Convert to business logic objects
            var instances = dbInstances?.Select(x => Convert(x)).ToList();

            // Get rid of detailed error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while retrieving the books";

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
