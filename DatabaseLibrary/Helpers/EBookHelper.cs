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
    public class EBookHelper_db
    {

        /// <summary>
        /// Adds a new instance into the database.
        /// </summary>
        public static EBook_db Add(int systemId, string title, string genre, DateTime publishingDate, int authorId, string link,
            int pages, DbContext context, out StatusResponse statusResponse)
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
                if (pages > 0)
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide amount of pages.");

                // Generate a new instance
                EBook_db instance = new EBook_db
                    (
                        systemId: 0, //This can be ignored is PK in your DB is auto increment
                        title, genre, publishingDate, authorId, link, pages
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
                        commandText: "INSERT INTO ebook (System_id, Pages) values (@system_id, @pages)",
                        parameters: new Dictionary<string, object>()
                        {
                            { "@system_id", instance.System_id },
                            { "@pages", instance.Pages }
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
                statusResponse = new StatusResponse("EBook added successfully");
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
        public static EBook_db Edit(int systemId, string title, string genre, DateTime publishingDate, int authorId, string link,
           int pages,
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
                if (pages > 0)
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide amount of pages.");

                // Generate a new instance
                EBook_db instance = new EBook_db
                    (
                        systemId,
                        title, genre, publishingDate, authorId, link, pages
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
                    commandText: "UPDATE ebook SET pages = @pages WHERE system_id = @system_id",
                    parameters: new Dictionary<string, object>()
                    {
                                        {"@system_id", instance.System_id },
                                        { "@pages", instance.Pages }
                    },
                    message: out string message2
                );
                if (rowsAffected == -1)
                    throw new Exception(message2);

                // Return value
                statusResponse = new StatusResponse("EBook edited successfully");
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
                        commandText: "DELETE FROM ebook WHERE system_id = @system_id",
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
        public static EBook_db Get(int systemId,
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
                        commandText: "SELECT * FROM ebook WHERE system_id = @system_id",
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
                EBook_db instance = new EBook_db
                            (
                                systemId: systemId,
                                genre: genre,
                                publishingDate: publishingDate,
                                authorId: authorId,
                                title: title,
                                link: link,
                                pages: (int)row["pages"]
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
        public static List<EBook_db> GetCollection(
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Get from database
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT * FROM media INNER JOIN digital_media ON media.system_id = digital_media.system_id INNER JOIN ebook ON media.system_id = ebook.system_id",
                        parameters: new Dictionary<string, object>()
                        {

                        },
                        message: out string message
                    );
                if (table == null)
                    throw new Exception(message);

                // Parse data
                List<EBook_db> instances = new List<EBook_db>();
                foreach (DataRow row in table.Rows)
                    instances.Add(new EBook_db
                            (
                                systemId: (int)row["System_id"],
                                genre: row["Genre"].ToString(),
                                publishingDate: (DateTime)row["publishing_date"],
                                authorId: (int)row["author_id"],
                                title: row["title"].ToString(),
                                link: row["link"].ToString(),
                                pages: (int)row["pages"]
                            )
                        );

                // Return value
                statusResponse = new StatusResponse("EBook list has been retrieved successfully.");
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
