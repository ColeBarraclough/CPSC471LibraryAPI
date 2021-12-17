using DatabaseLibrary.Core;
using DatabaseLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Net;
using System.Text;

namespace DatabaseLibrary.Helpers
{
    public class ReservationHelper_db
    {

        /// <summary>
        /// Adds a new instance into the database.
        /// </summary>
        public static Reservation_db Add(int librarianId, DateTime returnDate, DateTime pickupDate, int mediaId, int customerCardId,
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Validate
                if (librarianId < 0)
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a librarian id.");
                if (mediaId < 0)
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a media id.");
                if (customerCardId < 0)
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a customer id.");
                if (pickupDate == default(DateTime))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a pickup date.");
                if (returnDate == default(DateTime))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a return date.");

                // Generate a new instance
                Reservation_db instance = new Reservation_db
                    (
                        librarianId, returnDate, pickupDate, mediaId, customerCardId
                    );

                // Add to database
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "INSERT INTO reservation (Librarian_id, media_id, customer_card_id, pickup_date, return_date) values (@librarian_id, @media_id, @customer_card_id, @pickup_date, @return_date)",
                        parameters: new Dictionary<string, object>()
                        {
                            { "@librarian_id", instance.Librarian_id },
                            { "@media_id", instance.Media_id },
                            { "@customer_card_id", instance.Customer_card_id },
                            { "@pickup_date", instance.Pickup_date },
                            { "@return_date", instance.Return_date }
                        },
                        message: out string message
                    );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return value
                statusResponse = new StatusResponse("Reservation added successfully");
                return instance;
            }
            catch (Exception exception)
            {
                statusResponse = new StatusResponse(exception);
                return null;
            }
        }

        /// <summary>
        /// Edits an instance in the database
        /// </summary>
        public static Reservation_db Edit(int librarianId, DateTime returnDate, DateTime pickupDate, int mediaId, int customerCardId,
           DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Validate
                if (librarianId < 0)
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a librarian id.");
                if (mediaId < 0)
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a media id.");
                if (customerCardId < 0)
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a customer id.");
                if (pickupDate == default(DateTime))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a pickup date.");
                if (returnDate == default(DateTime))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a return date.");

                // Generate a new instance
                Reservation_db instance = new Reservation_db
                    (
                        librarianId, returnDate, pickupDate, mediaId, customerCardId
                    );

                // Add to database
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "UPDATE reservation SET return_date = @return_date, pickup_date = @pickup_date WHERE librarian_id = @librarian_id and media_id = @media_id and customer_card_id = @customer_card_Id",
                        parameters: new Dictionary<string, object>()
                        {
                            {"@return_date", instance.Return_date },
                            { "@pickup_date", instance.Pickup_date },
                            { "@librarian_id", instance.Librarian_id },
                            { "@media_id", instance.Media_id },
                            { "@customer_card_id", instance.Customer_card_id },
                        },
                        message: out string message
                    );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return value
                statusResponse = new StatusResponse("Reservation edited successfully");
                return instance;
            }
            catch (Exception exception)
            {
                statusResponse = new StatusResponse(exception);
                return null;
            }

        }


        /// <summary>
        /// Deletes an instance in the database
        /// </summary>
        public static void Delete(int librarianId, int mediaId, int customerCardId, DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Validate
                if (librarianId < 0)
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a librarian id.");
                if (mediaId < 0)
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a media id.");
                if (customerCardId < 0)
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a customer id.");


                // Add to database
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "DELETE FROM reservation WHERE librarian_id = @librarian_id and media_id = @media_id and customer_card_id = @customer_card_Id",
                        parameters: new Dictionary<string, object>()
                        {
                            { "@librarian_id", librarianId },
                            { "@media_id", mediaId },
                            { "@customer_card_id", customerCardId },
                        },
                        message: out string message
                    );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return value
                statusResponse = new StatusResponse("Reservation deleted successfully");
            }
            catch (Exception exception)
            {
                statusResponse = new StatusResponse(exception);
            }

        }

        /// <summary>
        /// Retrieves an instance.
        /// </summary>
        public static Reservation_db Get(int librarianId, int mediaId, int customerCardId,
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Get from database
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT * FROM reservation WHERE librarian_id = @librarian_id and media_id = @media_id and customer_card_id = @customer_card_Id",
                        parameters: new Dictionary<string, object>()
                        {
                            { "@librarian_id", librarianId },
                            { "@media_id", mediaId },
                            { "@customer_card_id", customerCardId },
                        },
                        message: out string message
                    );
                if (table == null)
                    throw new Exception(message);

                DataRow row = table.Rows[0];

                // Parse data
                Reservation_db instance = new Reservation_db
                            (
                                librarianId: (int)row["librarian_id"],
                                mediaId: (int) row["media_id"],
                                customerCardId: (int) row["customer_card_id"],
                                returnDate: (DateTime)row["return_date"],
                                pickupDate: (DateTime)row["pickup_date"]
                            );

                // Return value
                statusResponse = new StatusResponse("Reservation has been retrieved successfully.");
                return instance;
            }
            catch (Exception exception)
            {
                statusResponse = new StatusResponse(exception);
                return null;
            }
        }



        /// <summary>
        /// Retrieves a list of instances.
        /// </summary>
        public static List<Reservation_db> GetCollection(
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Get from database
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT * FROM reservation",
                        parameters: new Dictionary<string, object>()
                        {

                        },
                        message: out string message
                    );
                if (table == null)
                    throw new Exception(message);

                // Parse data
                List<Reservation_db> instances = new List<Reservation_db>();
                foreach (DataRow row in table.Rows)
                    instances.Add(new Reservation_db
                            (
                                librarianId: (int)row["librarian_id"],
                                mediaId: (int)row["media_id"],
                                customerCardId: (int)row["customer_card_id"],
                                returnDate: (DateTime)row["return_date"],
                                pickupDate: (DateTime)row["pickup_date"]
                            )
                        );

                // Return value
                statusResponse = new StatusResponse("Reservation list has been retrieved successfully.");
                return instances;
            }
            catch (Exception exception)
            {
                statusResponse = new StatusResponse(exception);
                return null;
            }
        }

    }
}
