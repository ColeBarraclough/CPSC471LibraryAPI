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
    public class AuthorHelper_db
    {

        /// <summary>
        /// Adds a new instance into the database.
        /// </summary>
        public static Author_db Add(string author_id, string firstName, string lastName, DateTime dateOfBirth,
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Validate
                if (string.IsNullOrEmpty(author_id?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a password.");
                if (string.IsNullOrEmpty(firstName?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a first name.");
                if (string.IsNullOrEmpty(lastName?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a last name.");
                if (dateOfBirth == default(DateTime))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a date of birth.");

                // Generate a new instance
                Author_db instance = new Author_db
                    (
                        author_id, firstName, lastName, dateOfBirth
                    );

                // Add to database
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "INSERT INTO Author (First_name, Last_name, Password, Address, Date_of_birth) values (@first_name, @last_name, @password, @address, @date_of_birth)",
                        parameters: new Dictionary<string, object>()
                        {
                            { "@author_id", instance.Author_id },
                            { "@first_name", instance.First_name },
                            { "@last_name", instance.Last_name },
                            { "@date_of_birth", instance.Date_of_birth }
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
                statusResponse = new StatusResponse("Author added successfully");
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
        public static Author_db Edit(string author_id, string firstName, string lastName, DateTime dateOfBirth,
           DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Validate
                if (string.IsNullOrEmpty(author_id?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a password.");
                if (string.IsNullOrEmpty(firstName?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a first name.");
                if (string.IsNullOrEmpty(lastName?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a last name.");
                if (dateOfBirth == default(DateTime))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a date of birth.");

                // Generate a new instance
                Author_db instance = new Author_db
                    (
                        author_id, firstName, lastName, dateOfBirth
                    );

                // Add to database
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "UPDATE Author SET First_name = @first_name, Last_name = @last_name, Password = @password, Address = @address, Date_of_birth = @date_of_birth WHERE card_id = @card_id",
                        parameters: new Dictionary<string, object>()
                        {
                            {"@author_id", instance.Author_id },
                            { "@first_name", instance.First_name },
                            { "@last_name", instance.Last_name },
                            { "@date_of_birth", instance.Date_of_birth }
                        },
                        message: out string message
                    );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return value
                statusResponse = new StatusResponse("Author edited successfully");
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
        public static void Delete(string author_id, DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Validate
                if (author_id == "")
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide an author id.");


                // Add to database
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "DELETE FROM Author WHERE author_id = @author_id",
                        parameters: new Dictionary<string, object>()
                        {
                            {"@author_id", author_id },
                        },
                        message: out string message
                    );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return value
                statusResponse = new StatusResponse("Author deleted successfully");
            }
            catch (Exception exception)
            {
                statusResponse = new StatusResponse(exception);
            }

        }

        /// <summary>
        /// Retrieves an instance.
        /// </summary>
        public static Author_db Get(string author_id,
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Get from database
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT * FROM Author WHERE author_id = @author_id",
                        parameters: new Dictionary<string, object>()
                        {
                            {"@author_id", author_id },
                        },
                        message: out string message
                    );
                if (table == null)
                    throw new Exception(message);

                DataRow row = table.Rows[0];

                // Parse data
                Author_db instance = new Author_db
                            (
                                author_id: row["Author_id"].ToString(),
                                firstName: row["First_name"].ToString(),
                                lastName: row["Last_name"].ToString(),
                                dateOfBirth: (DateTime)row["Date_of_birth"]
                            );

                // Return value
                statusResponse = new StatusResponse("Author has been retrieved successfully.");
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
        public static List<Author_db> GetCollection(
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Get from database
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT * FROM Author",
                        parameters: new Dictionary<string, object>()
                        {

                        },
                        message: out string message
                    );
                if (table == null)
                    throw new Exception(message);

                // Parse data
                List<Author_db> instances = new List<Author_db>();
                foreach (DataRow row in table.Rows)
                    instances.Add(new Author_db
                            (
                                author_id: row["Author_id"].ToString(),
                                firstName: row["First_name"].ToString(),
                                lastName: row["Last_name"].ToString(),
                                dateOfBirth: (DateTime)row["Date_of_birth"]
                            )
                        );

                // Return value
                statusResponse = new StatusResponse("Author list has been retrieved successfully.");
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
