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
    public class BookHelper_db
    {

        /// <summary>
        /// Adds a new instance into the database.
        /// </summary>
        public static Book_db Add(int systemId, string title, string libraryAddress, string genre, DateTime publishingDate, int authorId,
            int? borrowerId, DateTime? dateOfCheckOut, DateTime? dueDate, int pages, DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Validate
                if (string.IsNullOrEmpty(title?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a title.");
                if (string.IsNullOrEmpty(libraryAddress?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a last name.");
                if (string.IsNullOrEmpty(genre?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a genre.");
                if (publishingDate == default(DateTime))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a publishing date.");
                if (pages < 0)
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide amount of pages.");

                // Generate a new instance
                Book_db instance = new Book_db
                    (
                        systemId: 0, //This can be ignored is PK in your DB is auto increment
                        title, libraryAddress, genre, publishingDate, authorId, borrowerId, dateOfCheckOut, dueDate, pages
                    );

                // Add to database

                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "INSERT INTO media (Genre, Author_id, Title, publishing_date) values (@genre, @author_id, @title, @publishing_date)",
                        parameters: new Dictionary<string, object>()
                        {
                            { "@genre", instance.Genre },
                            { "@author_id", instance.Author_id },
                            { "@title", instance.Title },
                            { "@publishing_date", instance.Publishing_date }
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
                        commandText: "INSERT INTO physical_media (System_id, Library_address, Borrower_id, Date_of_check_out, Due_date) values (@system_id, @library_address, @borrower_id, @date_of_check_out, @due_date)",
                        parameters: new Dictionary<string, object>()
                        {
                                            { "@system_id", instance.System_id },
                                            { "@library_address", instance.Library_address },
                                            { "@borrower_id", instance.Borrower_id },
                                            { "@date_of_check_out", instance.Date_of_check_out },
                                            { "@due_date", instance.Due_date }
                        },
                        message: out string message1
                    );
                if (rowsAffected == -1)
                    throw new Exception(message1);

                
                rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "INSERT INTO book (System_id, Pages) values (@system_id, @pages)",
                        parameters: new Dictionary<string, object>()
                        {
                                            { "@system_id", instance.System_id },
                                            { "@pages", instance.Pages }
                        },
                        message: out string message
                    );
                if (rowsAffected == -1)
                    throw new Exception(message);
                


                // Return value
                statusResponse = new StatusResponse("Book added successfully");
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
        public static Book_db Edit(int systemId, string title, string libraryAddress, string genre, DateTime publishingDate, int authorId,
            int? borrowerId, DateTime? dateOfCheckOut, DateTime? dueDate, int pages,
           DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Validate
                if (systemId < 0)
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide an id.");
                if (string.IsNullOrEmpty(title?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a title.");
                if (string.IsNullOrEmpty(libraryAddress?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a last name.");
                if (string.IsNullOrEmpty(genre?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a genre.");
                if (publishingDate == default(DateTime))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a publishing date.");
                if (pages < 0)
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide amount of pages.");

                // Generate a new instance
                Book_db instance = new Book_db
                    (
                        systemId,
                        title, libraryAddress, genre, publishingDate, authorId, borrowerId, dateOfCheckOut, dueDate, pages
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
                    commandText: "UPDATE physical_media SET library_address = @library_address, borrower_id = @borrower_id, date_of_check_out = @date_of_check_out, due_date = @due_date WHERE system_id = @system_id",
                    parameters: new Dictionary<string, object>()
                    {
                                        {"@system_id", instance.System_id },
                                        { "@library_address", instance.Library_address },
                                        { "@borrower_id", instance.Borrower_id },
                                        { "@date_of_check_out", instance.Date_of_check_out },
                                        { "@due_date", instance.Due_date }
                    },
                    message: out string message1
                );
                if (rowsAffected == -1)
                    throw new Exception(message1);


                rowsAffected = context.ExecuteNonQueryCommand
                (
                    commandText: "UPDATE book SET pages = @pages WHERE system_id = @system_id",
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
                statusResponse = new StatusResponse("Book edited successfully");
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
                        commandText: "DELETE FROM book WHERE system_id = @system_id",
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
                        commandText: "DELETE FROM physical_media WHERE system_id = @system_id",
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
        public static Book_db Get(int systemId,
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
                        commandText: "SELECT * FROM physical_media WHERE system_id = @system_id",
                        parameters: new Dictionary<string, object>()
                        {
                            {"@system_id", systemId },
                        },
                        message: out string message1
                    );
                if (table == null)
                    throw new Exception(message1);

                row = table.Rows[0];
                int? borrowerId = null;
                DateTime? dateOfCheckOut = null;
                DateTime? dueDate = null;
                string libraryAddress = row["library_address"].ToString();
                if (!row.IsNull("Borrower_id"))
                    borrowerId = (int?)row["Borrower_id"];
                if (!row.IsNull("date_of_check_out"))
                    dateOfCheckOut = (DateTime?)row["date_of_check_out"];
                if (!row.IsNull("due_date"))
                    dueDate = (DateTime?)row["due_date"];


                table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT * FROM book WHERE system_id = @system_id",
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
                Book_db instance = new Book_db
                            (
                                systemId: systemId,
                                genre: genre,
                                publishingDate: publishingDate,
                                authorId: authorId,
                                title: title,
                                libraryAddress: libraryAddress,
                                borrowerId: borrowerId,
                                dateOfCheckOut: dateOfCheckOut,
                                dueDate: dueDate,
                                pages: (int)row["pages"]
                            );

                // Return value
                statusResponse = new StatusResponse("Book has been retrieved successfully.");
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
        public static List<Book_db> GetCollection(
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Get from database
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT * FROM media INNER JOIN physical_media ON media.system_id = physical_media.system_id INNER JOIN book ON media.system_id = book.system_id",
                        parameters: new Dictionary<string, object>()
                        {

                        },
                        message: out string message
                    );
                if (table == null)
                    throw new Exception(message);

                // Parse data
                List<Book_db> instances = new List<Book_db>();
                foreach (DataRow row in table.Rows)
                {
                    int? borrowerId = null;
                    DateTime? dateOfCheckOut = null;
                    DateTime? dueDate = null;
                    if (!row.IsNull("Borrower_id"))
                        borrowerId = (int?)row["Borrower_id"];
                    if (!row.IsNull("date_of_check_out"))
                        dateOfCheckOut = (DateTime?)row["date_of_check_out"];
                    if (!row.IsNull("due_date"))
                        dueDate = (DateTime?)row["due_date"];
                    instances.Add(new Book_db
                            (
                                systemId: (int)row["System_id"],
                                genre: row["Genre"].ToString(),
                                publishingDate: (DateTime)row["publishing_date"],
                                authorId: (int)row["author_id"],
                                title: row["title"].ToString(),
                                libraryAddress: row["library_address"].ToString(),
                                borrowerId: borrowerId,
                                dateOfCheckOut: dateOfCheckOut,
                                dueDate: dueDate,
                                pages: (int)row["pages"]
                            )
                        );
                }

                // Return value
                statusResponse = new StatusResponse("Book list has been retrieved successfully.");
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
