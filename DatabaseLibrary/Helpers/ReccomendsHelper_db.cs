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
    public class ReccomendsHelper_db
    {

        /// <summary>
        /// Adds a new instance into the database.
        /// </summary>
        public static Reccomends_db Add(string reccomendation_address, int media_id, string reccomendation_card,
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Validate
                if (media_id < 0)
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a media id.");
                if (string.IsNullOrEmpty(reccomendation_address?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a reccomendation address.");
                if (string.IsNullOrEmpty(reccomendation_card?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a reccomendation card.");

                // Generate a new instance
                Reccomends_db instance = new Reccomends_db
                    (
                        reccomendation_address, media_id, reccomendation_card
                    );

                // Add to database
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "INSERT INTO reccomends (Reccomendation_address, Media_id, Reccomendation_card) values (@reccomendation_address, @media_id, @reccomendation_card)",
                        parameters: new Dictionary<string, object>()
                        {
                            { "@reccomendation_address", instance.Reccomendation_address },
                            { "@media_id", instance.Media_id },
                            { "@reccomendation_card", instance.Reccomendation_card }
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

                //instance.Card_id = Convert.ToInt32(row[0]);

                // Return value
                statusResponse = new StatusResponse("Reccomends added successfully");
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
        public static Reccomends_db Edit(string reccomendation_address, int media_id, string reccomendation_card,
           DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Validate
                if (media_id < 0)
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a card id.");
                if (string.IsNullOrEmpty(reccomendation_address?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a first name.");
                if (string.IsNullOrEmpty(reccomendation_card?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a last name.");

                // Generate a new instance
                Reccomends_db instance = new Reccomends_db
                    (
                        reccomendation_address, media_id, reccomendation_card
                    );

                // Add to database
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "UPDATE reccomends SET Reccomendation_address = @reccomendation_address, Media_id = @media_id, Reccomendation_card = @reccomendation_card",
                        parameters: new Dictionary<string, object>()
                        {
                            {"@reccomendation_address", instance.Reccomendation_address },
                            { "@media_id", instance.Media_id },
                            { "@reccomendation_card", instance.Reccomendation_card }
                        },
                        message: out string message
                    );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return value
                statusResponse = new StatusResponse("Reccomends edited successfully");
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
        public static void Delete(string reccomendation_address, string reccomendation_card, DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Validate
                if (reccomendation_address == "")
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a reccomendation address.");
                if (reccomendation_card == "")
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a reccomendation_card.");


                // Add to database
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "DELETE FROM reccomends WHERE reccomendation_address = @reccomendation_address, reccomendation_card = @reccomendation_card",
                        parameters: new Dictionary<string, object>()
                        {
                            {"@reccomendation_address", reccomendation_address },
                            {"@reccomendation_card", reccomendation_card }
                        },
                        message: out string message
                    );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return value
                statusResponse = new StatusResponse("Reccomends deleted successfully");
            }
            catch (Exception exception)
            {
                statusResponse = new StatusResponse(exception);
            }

        }

        /// <summary>
        /// Retrieves an instance.
        /// </summary>
        public static Reccomends_db Get(string reccomendation_address, string reccomendation_card,
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Get from database
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT * FROM reccomends WHERE reccomendation_address = @reccomendation_address, reccomendation_card = @reccomendation_card",
                        parameters: new Dictionary<string, object>()
                        {
                            {"@reccomendation_address", reccomendation_address },
                            {"@reccomendation_card", reccomendation_card }
                        },
                        message: out string message
                    );
                if (table == null)
                    throw new Exception(message);

                DataRow row = table.Rows[0];

                // Parse data
                Reccomends_db instance = new Reccomends_db
                            (
                                reccomendation_address: row["Reccomendation_address"].ToString(),
                                media_id: (int)row["Media_id"],
                                reccomendation_card: row["Reccomendation_card"].ToString()
                            );

                // Return value
                statusResponse = new StatusResponse("Reccomends has been retrieved successfully.");
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
        public static List<Reccomends_db> GetCollection(
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Get from database
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT * FROM reccomends",
                        parameters: new Dictionary<string, object>()
                        {

                        },
                        message: out string message
                    );
                if (table == null)
                    throw new Exception(message);

                // Parse data
                List<Reccomends_db> instances = new List<Reccomends_db>();
                foreach (DataRow row in table.Rows)
                    instances.Add(new Reccomends_db
                            (
                                reccomendation_address: row["Reccomendation_address"].ToString(),
                                media_id: (int)row["Media_id"],
                                reccomendation_card: row["Reccomendation_card"].ToString()
                            )
                        );

                // Return value
                statusResponse = new StatusResponse("Reccomends list has been retrieved successfully.");
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
