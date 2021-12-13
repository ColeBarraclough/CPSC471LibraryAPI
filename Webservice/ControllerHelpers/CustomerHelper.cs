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
    public class CustomerHelper
    {

        #region Converters

        /// <summary>
        /// Converts database models to a business logic object.
        /// </summary>
        public static BusinessLibrary.Models.Customer Convert(Customer_db instance)
        {
            if (instance == null)
                return null;
            return new BusinessLibrary.Models.Customer(instance.Card_id, instance.First_name, instance.Last_name, instance.Password, instance.Address, instance.Date_of_birth);
        }

        #endregion

        /// <summary>
        /// Signs up a customer.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage Add(JObject data,
            DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Extract paramters
            string firstName = (data.ContainsKey("first_name")) ? data.GetValue("first_name").Value<string>() : null;
            string lastName = (data.ContainsKey("last_name")) ? data.GetValue("last_name").Value<string>() : null;
            string password = (data.ContainsKey("password")) ? data.GetValue("password").Value<string>() : null;
            string address = (data.ContainsKey("address")) ? data.GetValue("address").Value<string>() : null;
            DateTime dateOfBirth = (data.ContainsKey("date_of_birth")) ? data.GetValue("date_of_birth").Value<DateTime>() : new DateTime();


            // Add instance to database
            var dbInstance = DatabaseLibrary.Helpers.CustomerHelper_db.Add(0, firstName, lastName, password, address, dateOfBirth,
                context, out StatusResponse statusResponse);

            // Get rid of detailed internal server error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while adding a new customer.";

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
        /// Gets list of students.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage GetCollection(
            DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Get instances from database
            var dbInstances = DatabaseLibrary.Helpers.CustomerHelper_db.GetCollection(
                context, out StatusResponse statusResponse);

            // Convert to business logic objects
            var instances = dbInstances?.Select(x => Convert(x)).ToList();

            // Get rid of detailed error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while retrieving the customers";

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
