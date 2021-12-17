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
    public class Library_cardHelper_db
    {

        /// <summary>
        /// Adds a new instance into the database.
        /// </summary>
        public static Library_card_db Add(string issuer_address, DateTime date_of_expiration,
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                if (string.IsNullOrEmpty(issuer_address?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide an issuer address.");
                if (date_of_expiration == default(DateTime))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a date of expiration.");

                // Generate a new instance
                Library_card_db instance = new Library_card_db
                    (
                        id_no: 0, //This can be ignored is PK in your DB is auto increment
                        issuer_address, date_of_expiration
                    );

                // Add to database
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "INSERT INTO Library_card (Issuer_Address, Date_of_expiration) values (@issuer_address, @date_of_expiration)",
                        parameters: new Dictionary<string, object>()
                        {
                            { "@issuer_address", instance.Issuer_address },
                            { "@date_of_expiration", instance.Date_of_expiration }
                        },
                        message: out string message
                    );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Get from database
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT LAST_INSERT_ID();",
                        parameters: new Dictionary<string, object>()
                        {

                        },
                        message: out string getMessage
                    );
                if (table == null)
                    throw new Exception(getMessage);

                DataRow row = table.Rows[0];

                instance.Id_no = Convert.ToInt32(row[0]);

                // Return value
                statusResponse = new StatusResponse("Library_card added successfully");
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
        public static Library_card_db Edit(int id_no, string issuer_address, DateTime date_of_expiration,
           DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Validate
                if (id_no < 0)
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a card id.");
                if (string.IsNullOrEmpty(issuer_address?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a first name.");
                if (date_of_expiration == default(DateTime))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a date of birth.");

                // Generate a new instance
                Library_card_db instance = new Library_card_db
                    (
                        id_no, issuer_address, date_of_expiration
                    );

                // Add to database
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "UPDATE Library_card SET Issuer_address = @issuer_address, Date_of_expiration = @date_of_expiration WHERE Id_no = @id_no",
                        parameters: new Dictionary<string, object>()
                        {
                            {"@id_no", instance.Id_no },
                            { "@issuer_address", instance.Issuer_address },
                            { "@date_of_expiration", instance.Date_of_expiration }
                        },
                        message: out string message
                    );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return value
                statusResponse = new StatusResponse("Library_card edited successfully");
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
        public static void Delete(int id_no, DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Validate
                if (id_no < 0 || id_no == null)
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a card id.");


                // Add to database
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "DELETE FROM Library_card WHERE id_no = @id_no",
                        parameters: new Dictionary<string, object>()
                        {
                            {"@id_no", id_no},
                        },
                        message: out string message
                    );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return value
                statusResponse = new StatusResponse("Library_card deleted successfully");
            }
            catch (Exception exception)
            {
                statusResponse = new StatusResponse(exception);
            }

        }

        /// <summary>
        /// Retrieves an instance.
        /// </summary>
        public static Library_card_db Get(int id_no,
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Get from database
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT * FROM Library_card WHERE id_no = @id_no",
                        parameters: new Dictionary<string, object>()
                        {
                            {"@id_no", id_no },
                        },
                        message: out string message
                    );
                if (table == null)
                    throw new Exception(message);

                DataRow row = table.Rows[0];

                // Parse data
                Library_card_db instance = new Library_card_db
                            (
                                id_no: (int)row["Id_no"],
                                issuer_address: row["Issuer_address"].ToString(),
                                date_of_expiration: (DateTime)row["Date_of_expiration"]
                            );

                // Return value
                statusResponse = new StatusResponse("Library_card has been retrieved successfully.");
                return instance;
            }
            catch (Exception exception)
            {
                statusResponse = new StatusResponse(exception);
                return null;
            }
        }


        /// <summary>
        /// Retrieves an instance.
        /// </summary>
        public static Library_card_db Get(string issuer_address,
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Get from database
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT * FROM Library_card WHERE issuer_address = @issuer_address",
                        parameters: new Dictionary<string, object>()
                        {
                            {"@issuer_address", issuer_address },
                        },
                        message: out string message
                    );
                if (table == null)
                    throw new Exception(message);

                DataRow row = table.Rows[0];

                // Parse data
                Library_card_db instance = new Library_card_db
                            (
                                id_no: (int)row["Id_no"],
                                issuer_address: row["Issuer_address"].ToString(),
                                date_of_expiration: (DateTime)row["Date_of_expiration"]
                            );

                // Return value
                statusResponse = new StatusResponse("Library_card has been retrieved successfully.");
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
        public static List<Library_card_db> GetCollection(
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Get from database
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT * FROM Library_card",
                        parameters: new Dictionary<string, object>()
                        {

                        },
                        message: out string message
                    );
                if (table == null)
                    throw new Exception(message);

                // Parse data
                List<Library_card_db> instances = new List<Library_card_db>();
                foreach (DataRow row in table.Rows)
                    instances.Add(new Library_card_db
                            (
                                id_no: (int)row["Id_no"],
                                issuer_address: row["Issuer_address"].ToString(),
                                date_of_expiration: (DateTime)row["Date_of_expiration"]
                            )
                        );

                // Return value
                statusResponse = new StatusResponse("Library_card list has been retrieved successfully.");
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
