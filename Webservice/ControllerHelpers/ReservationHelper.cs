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
    public class ReservationHelper
    {

        #region Converters

        /// <summary>
        /// Converts database models to a business logic object.
        /// </summary>
        public static BusinessLibrary.Models.Reservation Convert(Reservation_db instance)
        {
            if (instance == null)
                return null;
            return new BusinessLibrary.Models.Reservation(instance.Librarian_id, instance.Return_date, instance.Pickup_date, instance.Media_id, instance.Customer_card_id);
        }

        #endregion

        /// <summary>
        /// Creates a reservation.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage Add(JObject data,
            DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Extract paramters
            int librarian_id = (data.ContainsKey("librarian_id")) ? data.GetValue("librarian_id").Value<int>() : -1;
            int media_id = (data.ContainsKey("media_id")) ? data.GetValue("media_id").Value<int>() : -1;
            int customer_card_id = (data.ContainsKey("customer_card_id")) ? data.GetValue("customer_card_id").Value<int>() : -1;
            DateTime return_date = (data.ContainsKey("return_date")) ? data.GetValue("return_date").Value<DateTime>() : new DateTime();
            DateTime pickup_date = (data.ContainsKey("pickup_date")) ? data.GetValue("pickup_date").Value<DateTime>() : new DateTime();


            // Add instance to database
            var dbInstance = DatabaseLibrary.Helpers.ReservationHelper_db.Add(librarian_id, return_date, pickup_date, media_id, customer_card_id,
                context, out StatusResponse statusResponse);

            // Get rid of detailed internal server error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while adding a new reservation.";

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
        /// Edits a reservation.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage Edit(JObject data,
            DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Extract paramters
            int librarian_id = (data.ContainsKey("librarian_id")) ? data.GetValue("librarian_id").Value<int>() : -1;
            int media_id = (data.ContainsKey("media_id")) ? data.GetValue("media_id").Value<int>() : -1;
            int customer_card_id = (data.ContainsKey("customer_card_id")) ? data.GetValue("customer_card_id").Value<int>() : -1;
            DateTime return_date = (data.ContainsKey("return_date")) ? data.GetValue("return_date").Value<DateTime>() : new DateTime();
            DateTime pickup_date = (data.ContainsKey("pickup_date")) ? data.GetValue("pickup_date").Value<DateTime>() : new DateTime();

            // Add instance to database
            var dbInstance = DatabaseLibrary.Helpers.ReservationHelper_db.Edit(librarian_id, return_date, pickup_date, media_id, customer_card_id,
                context, out StatusResponse statusResponse);

            // Get rid of detailed internal server error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while editing a reservation.";

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
        /// Deletes a reservation.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage Delete(JObject data,
            DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Extract paramters
            int librarian_id = (data.ContainsKey("librarian_id")) ? data.GetValue("librarian_id").Value<int>() : -1;
            int media_id = (data.ContainsKey("media_id")) ? data.GetValue("media_id").Value<int>() : -1;
            int customer_card_id = (data.ContainsKey("customer_card_id")) ? data.GetValue("customer_card_id").Value<int>() : -1;

            // Add instance to database
            DatabaseLibrary.Helpers.ReservationHelper_db.Delete(librarian_id, media_id, customer_card_id, context, out StatusResponse statusResponse);

            bool failed = false;

            // Get rid of detailed internal server error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
            {
                statusResponse.Message = "Something went wrong while deleting a reservation.";
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
        /// Gets a reservation.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage Get(int? librarian_id, int? media_id, int? customer_card_id,
        DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Extract paramters


            // Get instances from database
            var dbInstance = DatabaseLibrary.Helpers.ReservationHelper_db.Get((int)librarian_id, (int)media_id, (int)customer_card_id,
                context, out StatusResponse statusResponse);

            // Convert to business logic objects
            var instance = Convert(dbInstance);

            // Get rid of detailed error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while retrieving the reservation";

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
        /// Gets list of reservations.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage GetCollection(
        DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            // Get instances from database
            var dbInstances = DatabaseLibrary.Helpers.ReservationHelper_db.GetCollection(
                context, out StatusResponse statusResponse);

            // Convert to business logic objects
            var instances = dbInstances?.Select(x => Convert(x)).ToList();

            // Get rid of detailed error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while retrieving the reservations";

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
