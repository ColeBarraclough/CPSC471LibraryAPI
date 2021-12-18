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
    public class RecommendsHelper_db
    {

        /// <summary>
        /// Adds a new instance into the database.
        /// </summary>
        public static Recommends_db Add(string recommendation_address, int media_id, string recommendation_card,
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Validate
                if (media_id < 0)
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a media id.");
                if (string.IsNullOrEmpty(recommendation_address?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a recommendation address.");
                if (string.IsNullOrEmpty(recommendation_card?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a recommendation card.");

                // Generate a new instance
                Recommends_db instance = new Recommends_db
                    (
                        recommendation_address, media_id, recommendation_card
                    );

                // Add to database
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "INSERT INTO recommends (Recommendation_address, Media_id, Recommendation_card) values (@recommendation_address, @media_id, @recommendation_card)",
                        parameters: new Dictionary<string, object>()
                        {
                            { "@recommendation_address", instance.Recommendation_address },
                            { "@media_id", instance.Media_id },
                            { "@recommendation_card", instance.Recommendation_card }
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
                statusResponse = new StatusResponse("Recommends added successfully");
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
        public static Recommends_db Edit(string recommendation_address, int media_id, string recommendation_card,
           DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Validate
                if (media_id < 0)
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a card id.");
                if (string.IsNullOrEmpty(recommendation_address?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a first name.");
                if (string.IsNullOrEmpty(recommendation_card?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a last name.");

                // Generate a new instance
                Recommends_db instance = new Recommends_db
                    (
                        recommendation_address, media_id, recommendation_card
                    );

                // Add to database
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "UPDATE recommends SET Recommendation_address = @recommendation_address, Media_id = @media_id, Recommendation_card = @recommendation_card",
                        parameters: new Dictionary<string, object>()
                        {
                            {"@recommendation_address", instance.Recommendation_address },
                            { "@media_id", instance.Media_id },
                            { "@recommendation_card", instance.Recommendation_card }
                        },
                        message: out string message
                    );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return value
                statusResponse = new StatusResponse("Recommends edited successfully");
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
        public static void Delete(string recommendation_address, string recommendation_card, string media_id, DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Validate
                if (recommendation_address == "")
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a recommendation address.");
                if (recommendation_card == "")
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a recommendation_card.");
                if (media_id == "")
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a media id.");


                // Add to database
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "DELETE FROM recommends WHERE recommendation_address = @recommendation_address and recommendation_card = @recommendation_card and media_id = @media_id",
                        parameters: new Dictionary<string, object>()
                        {
                            {"@recommendation_address", recommendation_address },
                            {"@recommendation_card", recommendation_card },
                            {"@media_id", media_id }
                        },
                        message: out string message
                    );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return value
                statusResponse = new StatusResponse("Recommends deleted successfully");
            }
            catch (Exception exception)
            {
                statusResponse = new StatusResponse(exception);
            }

        }

        /// <summary>
        /// Retrieves an instance.
        /// </summary>
        public static Recommends_db Get(string recommendation_address, string recommendation_card, string media_id,
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Get from database
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT * FROM recommends WHERE recommendation_address = @recommendation_address and recommendation_card = @recommendation_card and media_id = @media_id",
                        parameters: new Dictionary<string, object>()
                        {
                            {"@recommendation_address", recommendation_address },
                            {"@recommendation_card", recommendation_card },
                            {"@media_id", media_id }
                        },
                        message: out string message
                    );
                if (table == null)
                    throw new Exception(message);

                DataRow row = table.Rows[0];

                // Parse data
                Recommends_db instance = new Recommends_db
                            (
                                recommendation_address: row["Recommendation_address"].ToString(),
                                media_id: (int)row["Media_id"],
                                recommendation_card: row["Recommendation_card"].ToString()
                            );

                // Return value
                statusResponse = new StatusResponse("Recommends has been retrieved successfully.");
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
        public static List<Recommends_db> GetCollection(
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Get from database
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT * FROM recommends",
                        parameters: new Dictionary<string, object>()
                        {

                        },
                        message: out string message
                    );
                if (table == null)
                    throw new Exception(message);

                // Parse data
                List<Recommends_db> instances = new List<Recommends_db>();
                foreach (DataRow row in table.Rows)
                    instances.Add(new Recommends_db
                            (
                                recommendation_address: row["Recommendation_address"].ToString(),
                                media_id: (int)row["Media_id"],
                                recommendation_card: row["Recommendation_card"].ToString()
                            )
                        );

                // Return value
                statusResponse = new StatusResponse("Recommends list has been retrieved successfully.");
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
