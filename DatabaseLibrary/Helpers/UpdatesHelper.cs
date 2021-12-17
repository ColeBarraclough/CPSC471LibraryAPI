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
    public class UpdatesHelper_db
    {

        /// <summary>
        /// Adds a new instance into the database.
        /// </summary>
        public static Updates_db Add(int librarian_id, int media_id, DbContext context, out StatusResponse statusResponse)
        {
            try
            {

                // Generate a new instance
                Updates_db instance = new Updates_db
                    (
                        librarian_id, media_id
                    );

                // Add to database
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "INSERT INTO updates (librarian_id, media_id) values (@librarian_id, @media_id)",
                        parameters: new Dictionary<string, object>()
                        {
                            { "@librarian_id", instance.Librarian_id },
                            { "@media_id", instance.Media_id },
                        },
                        message: out string message
                    );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return value
                statusResponse = new StatusResponse("Update added successfully");
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
        public static void Delete(int librarian_id, int media_id, DbContext context, out StatusResponse statusResponse)
        {
            try
            {


                // Add to database
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "DELETE FROM updates WHERE librarian_id = @librarian_id and media_id = @media_id",
                        parameters: new Dictionary<string, object>()
                        {
                            {"@librarian_id", librarian_id },
                            {"@media_id", media_id }
                        },
                        message: out string message
                    );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return value
                statusResponse = new StatusResponse("Update deleted successfully");
            }
            catch (Exception exception)
            {
                statusResponse = new StatusResponse(exception);
            }

        }

        /// <summary>
        /// Retrieves an instance.
        /// </summary>
        public static Updates_db Get(int librarian_id, int media_id,
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Get from database
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT * FROM updates WHERE librarian_id = @librarian_id and media_id = @media_id",
                        parameters: new Dictionary<string, object>()
                        {
                            {"@librarian_id", librarian_id },
                            {"@media_id", media_id }
                        },
                        message: out string message
                    );
                if (table == null)
                    throw new Exception(message);

                DataRow row = table.Rows[0];

                // Parse data
                Updates_db instance = new Updates_db
                            (
                                librarianId: (int) row["libraria_id"],
                                mediaId: (int) row["media_id"]
                            );

                // Return value
                statusResponse = new StatusResponse("Udpate has been retrieved successfully.");
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
        public static List<Updates_db> GetCollection(
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Get from database
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT * FROM updates",
                        parameters: new Dictionary<string, object>()
                        {

                        },
                        message: out string message
                    );
                if (table == null)
                    throw new Exception(message);

                // Parse data
                List<Updates_db> instances = new List<Updates_db>();
                foreach (DataRow row in table.Rows)
                    instances.Add(new Updates_db
                            (
                                librarianId: (int)row["librarian_id"],
                                mediaId: (int) row["media_id"]
                            )
                        );

                // Return value
                statusResponse = new StatusResponse("Update list has been retrieved successfully.");
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
