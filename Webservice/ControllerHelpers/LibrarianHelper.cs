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
    public class LibrarianHelper
    {

        #region Converters

        /// <summary>
        /// Converts database models to a business logic object.
        /// </summary>
        public static BusinessLibrary.Models.Librarian Convert(Librarian_db instance)
        {
            if (instance == null)
                return null;
            return new BusinessLibrary.Models.Librarian(instance.Employee_id, instance.Phone_no, instance.First_name, instance.Last_name, instance.Address, instance.Social_insurance_no, instance.Library_address, instance.Password);
        }

        #endregion

        /// <summary>
        /// Signs up a librarian.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage Add(JObject data,
            DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Extract paramters
            string firstName = (data.ContainsKey("first_name")) ? data.GetValue("first_name").Value<string>() : null;
            string lastName = (data.ContainsKey("last_name")) ? data.GetValue("last_name").Value<string>() : null;
            string phone_no = (data.ContainsKey("phone_no")) ? data.GetValue("phone_no").Value<string>() : null;
            string address = (data.ContainsKey("address")) ? data.GetValue("address").Value<string>() : null;
            string social_insurance_no = (data.ContainsKey("social_insurance_no")) ? data.GetValue("social_insurance_no").Value<string>() : null;
            string library_address = (data.ContainsKey("library_address")) ? data.GetValue("library_address").Value<string>() : null;
            string password = (data.ContainsKey("password")) ? data.GetValue("password").Value<string>() : null;


            // Add instance to database
            var dbInstance = DatabaseLibrary.Helpers.LibrarianHelper_db.Add("", firstName, lastName, phone_no, address, social_insurance_no, library_address, password,
                context, out StatusResponse statusResponse);

            // Get rid of detailed internal server error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while adding a new librarian.";

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
        /// Edits a librarian.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage Edit(JObject data,
            DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Extract paramters
            string employee_id = (data.ContainsKey("employee_id")) ? data.GetValue("employee_id").Value<string>() : null;
            string firstName = (data.ContainsKey("first_name")) ? data.GetValue("first_name").Value<string>() : null;
            string lastName = (data.ContainsKey("last_name")) ? data.GetValue("last_name").Value<string>() : null;
            string phone_no = (data.ContainsKey("phone_no")) ? data.GetValue("phone_no").Value<string>() : null;
            string address = (data.ContainsKey("address")) ? data.GetValue("address").Value<string>() : null;
            string social_insurance_no = (data.ContainsKey("social_insurance_no")) ? data.GetValue("social_insurance_no").Value<string>() : null;
            string library_address = (data.ContainsKey("library_address")) ? data.GetValue("library_address").Value<string>() : null;
            string password = (data.ContainsKey("password")) ? data.GetValue("password").Value<string>() : null;

            // Add instance to database
            var dbInstance = DatabaseLibrary.Helpers.LibrarianHelper_db.Edit(employee_id, firstName, lastName, phone_no, address, social_insurance_no, library_address, password,
                context, out StatusResponse statusResponse);

            // Get rid of detailed internal server error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while editing a librarian.";

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
        /// Deletes a librarian.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage Delete(JObject data,
            DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Extract paramters
            string employee_id = (data.ContainsKey("employee_id")) ? data.GetValue("employee_id").Value<string>() : null;

            // Add instance to database
            DatabaseLibrary.Helpers.LibrarianHelper_db.Delete(employee_id, context, out StatusResponse statusResponse);

            bool failed = false;

            // Get rid of detailed internal server error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors) {
                statusResponse.Message = "Something went wrong while deleting a librarian.";
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
        /// Gets a librarian.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage Get(string? id,
        DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Extract paramters
            string employee_id = id;


            // Get instances from database
            var dbInstance = DatabaseLibrary.Helpers.LibrarianHelper_db.Get(employee_id,
                context, out StatusResponse statusResponse);

            // Convert to business logic objects
            var instance = Convert(dbInstance);

            // Get rid of detailed error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while retrieving the librarian";

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
        /// Gets list of librarians.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage GetCollection(
        DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Get instances from database
            var dbInstances = DatabaseLibrary.Helpers.LibrarianHelper_db.GetCollection(
                context, out StatusResponse statusResponse);

            // Convert to business logic objects
            var instances = dbInstances?.Select(x => Convert(x)).ToList();

            // Get rid of detailed error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while retrieving the librarians";

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
