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
    public class AudiobookHelper_db
    {

        /// <summary>
        /// Adds a new instance into the database.
        /// </summary>
        public static Audiobook_db Add(int systemId, string title, string genre, DateTime publishingDate, int authorId, string link,
            int runTime, DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Validate
                if (string.IsNullOrEmpty(title?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a title.");
                if (string.IsNullOrEmpty(genre?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a genre.");
                if (string.IsNullOrEmpty(link?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a link.");
                if (publishingDate == default(DateTime))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a publishing date.");
                if (runTime > 0)
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a run time.");

                // Generate a new instance
                Audiobook_db instance = new Audiobook_db
                    (
                        systemId: 0, //This can be ignored is PK in your DB is auto increment
                        title, genre, publishingDate, authorId, link, runTime
                    );

                // Add to database

                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "INSERT INTO media (Genre, Author_id, Title) values (@genre, @author_id, @title)",
                        parameters: new Dictionary<string, object>()
                        {
                            { "@genre", instance.Genre },
                            { "@author_id", instance.Author_id },
                            { "@title", instance.Title },
                        },
                        message: out string message2
                    );
                if (rowsAffected == -1)
                    throw new Exception(message2);


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

                instance.System_id = Convert.ToInt32(row[0]);


                rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "INSERT INTO audiobook (System_id, Run_Time) values (@system_id, @run_time)",
                        parameters: new Dictionary<string, object>()
                        {
                            { "@system_id", instance.System_id },
                            { "@run_time", instance.Run_Time }
                        },
                        message: out string message
                    );
                if (rowsAffected == -1)
                    throw new Exception(message);

                rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "INSERT INTO digital_media (System_id, Link) values (@system_id, @link)",
                        parameters: new Dictionary<string, object>()
                        {
                                            { "@system_id", instance.System_id },
                                            { "@link", instance.Link },
                        },
                        message: out string message1
                    );
                if (rowsAffected == -1)
                    throw new Exception(message1);



                // Return value
                statusResponse = new StatusResponse("Audiobook added successfully");
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
        public static Audiobook_db Edit(int systemId, string title, string genre, DateTime publishingDate, int authorId, string link,
           int runTime,
           DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Validate
                if (systemId < 0)
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide an id.");
                if (string.IsNullOrEmpty(title?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a title.");
                if (string.IsNullOrEmpty(link?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a link.");
                if (string.IsNullOrEmpty(genre?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a genre.");
                if (publishingDate == default(DateTime))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a publishing date.");
                if (runTime > 0)
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a run time.");

                // Generate a new instance
                Audiobook_db instance = new Audiobook_db
                    (
                        systemId,
                        title, genre, publishingDate, authorId, link, runTime
                    );

                // Add to database
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "UPDATE media SET Genre = @genre, Publishing_date = @publishing_date, Author_id = @author_id, Title = @title WHERE system_id = @system_id",
                        parameters: new Dictionary<string, object>()
                        {
                            {"@system_id", instance.System_id },
                            { "@genre", instance.Genre },
                            { "@publishing_date", instance.Publishing_date },
                            { "@author_id", instance.Author_id },
                            { "@Title", instance.Title }
                        },
                        message: out string message
                    );
                if (rowsAffected == -1)
                    throw new Exception(message);


                rowsAffected = context.ExecuteNonQueryCommand
                (
                    commandText: "UPDATE digital_media SET Link = @link WHERE system_id = @system_id",
                    parameters: new Dictionary<string, object>()
                    {
                                        {"@system_id", instance.System_id },
                                        { "@link", instance.Link }
                    },
                    message: out string message1
                );
                if (rowsAffected == -1)
                    throw new Exception(message1);


                rowsAffected = context.ExecuteNonQueryCommand
                (
                    commandText: "UPDATE audiobook SET run_time = @run_time WHERE system_id = @system_id",
                    parameters: new Dictionary<string, object>()
                    {
                                        {"@system_id", instance.System_id },
                                        { "@run_time", instance.Run_Time }
                    },
                    message: out string message2
                );
                if (rowsAffected == -1)
                    throw new Exception(message2);

                // Return value
                statusResponse = new StatusResponse("Audiobook edited successfully");
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
        public static void Delete(int systemId, DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Validate
                if (systemId < 0)
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a system id.");


                // Add to database
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "DELETE FROM audiobook WHERE system_id = @system_id",
                        parameters: new Dictionary<string, object>()
                        {
                            {"@system_id", systemId },
                        },
                        message: out string message
                    );
                if (rowsAffected == -1)
                    throw new Exception(message);

                rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "DELETE FROM digital_media WHERE system_id = @system_id",
                        parameters: new Dictionary<string, object>()
                        {
                            {"@system_id", systemId },
                        },
                        message: out string message1
                    );
                if (rowsAffected == -1)
                    throw new Exception(message1);

                rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "DELETE FROM media WHERE system_id = @system_id",
                        parameters: new Dictionary<string, object>()
                        {
                            {"@system_id", systemId },
                        },
                        message: out string message2
                    );
                if (rowsAffected == -1)
                    throw new Exception(message2);

                // Return value
                statusResponse = new StatusResponse("Media deleted successfully");
            }
            catch (Exception exception)
            {
                statusResponse = new StatusResponse(exception);
            }

        }

        /// <summary>
        /// Retrieves an instance.
        /// </summary>
        public static Audiobook_db Get(int systemId,
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Get from database
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT * FROM media WHERE system_id = @system_id",
                        parameters: new Dictionary<string, object>()
                        {
                            {"@system_id", systemId },
                        },
                        message: out string message
                    );
                if (table == null)
                    throw new Exception(message);

                DataRow row = table.Rows[0];

                string genre = row["Genre"].ToString();
                DateTime publishingDate = (DateTime)row["Publishing_date"];
                int authorId = (int)row["author_id"];
                string title = row["title"].ToString();

                table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT * FROM digital_media WHERE system_id = @system_id",
                        parameters: new Dictionary<string, object>()
                        {
                            {"@system_id", systemId },
                        },
                        message: out string message1
                    );
                if (table == null)
                    throw new Exception(message1);

                row = table.Rows[0];

                string link = row["link"].ToString();


                table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT * FROM audiobook WHERE system_id = @system_id",
                        parameters: new Dictionary<string, object>()
                        {
                            {"@system_id", systemId },
                        },
                        message: out string message2
                    );
                if (table == null)
                    throw new Exception(message2);

                row = table.Rows[0];

                // Parse data
                Audiobook_db instance = new Audiobook_db
                            (
                                systemId: systemId,
                                genre: genre,
                                publishingDate: publishingDate,
                                authorId: authorId,
                                title: title,
                                link: link,
                                runTime: (int)row["run_time"]
                            );

                // Return value
                statusResponse = new StatusResponse("Media has been retrieved successfully.");
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
        public static List<Audiobook_db> GetCollection(
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Get from database
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT * FROM media INNER JOIN digital_media ON media.system_id = digital_media.system_id INNER JOIN audiobook ON media.system_id = book.system_id",
                        parameters: new Dictionary<string, object>()
                        {

                        },
                        message: out string message
                    );
                if (table == null)
                    throw new Exception(message);

                // Parse data
                List<Audiobook_db> instances = new List<Audiobook_db>();
                foreach (DataRow row in table.Rows)
                    instances.Add(new Audiobook_db
                            (
                                systemId: (int)row["System_id"],
                                genre: row["Genre"].ToString(),
                                publishingDate: (DateTime)row["publishing_date"],
                                authorId: (int)row["author_id"],
                                title: row["title"].ToString(),
                                link: row["link"].ToString(),
                                runTime: (int)row["run_time"]
                            )
                        );

                // Return value
                statusResponse = new StatusResponse("Audiobook list has been retrieved successfully.");
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
