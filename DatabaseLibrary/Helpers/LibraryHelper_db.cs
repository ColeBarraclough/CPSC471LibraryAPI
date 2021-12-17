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
    public class LibraryHelper_db
    {

        /// <summary>
        /// Adds a new instance into the database.
        /// </summary>
        public static Library_db Add(string address, string name, string website_address, string admin_id,
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Validate
                if (string.IsNullOrEmpty(address?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide an address.");
                if (string.IsNullOrEmpty(name?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a last name.");
                if (string.IsNullOrEmpty(website_address?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a password.");
                if (string.IsNullOrEmpty(admin_id?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a address.");

                // Generate a new instance
                Library_db instance = new Library_db
                    (
                        address, name, website_address, admin_id
                    );

                // Add to database
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "INSERT INTO Library (Address, Name, Website_address, Admin_id) values (@Address, @Name, @Website_address, @Admin_id)",
                        parameters: new Dictionary<string, object>()
                        {
                            {"@address", instance.Address },
                            { "@name", instance.Name },
                            { "@website_address", instance.Website_address },
                            { "@admin_id", instance.Admin_id }
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

                //instance.Address = Convert.ToInt32(row[0]);

                // Return value
                statusResponse = new StatusResponse("Library added successfully");
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
        public static Library_db Edit(string address, string name, string website_address, string admin_id,
           DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Validate
                if (string.IsNullOrEmpty(address?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide an address.");
                if (string.IsNullOrEmpty(name?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a last name.");
                if (string.IsNullOrEmpty(website_address?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a url.");
                if (string.IsNullOrEmpty(admin_id?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide an id.");

                // Generate a new instance
                Library_db instance = new Library_db
                    (
                        address, name, website_address, admin_id
                    );

                // Add to database
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "UPDATE Library SET Name = @name, Website_address = @website_address WHERE address = @address",
                        parameters: new Dictionary<string, object>()
                        {
                            {"@address", instance.Address },
                            { "@name", instance.Name },
                            { "@website_address", instance.Website_address },
                            { "@admin_id", instance.Admin_id }
                        },
                        message: out string message
                    );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return value
                statusResponse = new StatusResponse("Library edited successfully");
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
        public static void Delete(string address, DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Validate
                if (address == "")
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide an address.");


                // Add to database
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "DELETE FROM Library WHERE address = @address",
                        parameters: new Dictionary<string, object>()
                        {
                            {"@address", address },
                        },
                        message: out string message
                    );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return value
                statusResponse = new StatusResponse("Library deleted successfully");
            }
            catch (Exception exception)
            {
                statusResponse = new StatusResponse(exception);
            }

        }

        /// <summary>
        /// Retrieves an instance.
        /// </summary>
        public static Library_db Get(string address,
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Get from database
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT * FROM Library WHERE address = @address",
                        parameters: new Dictionary<string, object>()
                        {
                            {"@address", address },
                        },
                        message: out string message
                    );
                if (table == null)
                    throw new Exception(message);

                DataRow row = table.Rows[0];

                // Parse data
                Library_db instance = new Library_db
                            (
                                address: row["Address"].ToString(),
                                name: row["Name"].ToString(),
                                website_address: row["Website_address"].ToString(),
                                admin_id: row["Admin_id"].ToString()
                            );

                // Return value
                statusResponse = new StatusResponse("Library has been retrieved successfully.");
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
        public static List<Library_db> GetCollection(
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Get from database
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT * FROM Library",
                        parameters: new Dictionary<string, object>()
                        {

                        },
                        message: out string message
                    );
                if (table == null)
                    throw new Exception(message);

                // Parse data
                List<Library_db> instances = new List<Library_db>();
                foreach (DataRow row in table.Rows)
                    instances.Add(new Library_db
                            (
                                address: row["Address"].ToString(), 
                                name: row["Name"].ToString(),
                                website_address: row["Website_address"].ToString(),
                                admin_id: row["Admin_id"].ToString()
                            )
                        );

                // Return value
                statusResponse = new StatusResponse("Library list has been retrieved successfully.");
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
