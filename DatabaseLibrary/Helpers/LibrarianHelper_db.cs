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
    public class LibrarianHelper_db
    {

        /// <summary>
        /// Adds a new instance into the database.
        /// </summary>
        public static Librarian_db Add(string employee_id, string firstName, string lastName, string phone_no, string address, string social_insurance_no, string library_address,
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Validate
                if (string.IsNullOrEmpty(firstName?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a first name.");
                if (string.IsNullOrEmpty(lastName?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a last name.");
                if (string.IsNullOrEmpty(phone_no?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a phone number.");
                if (string.IsNullOrEmpty(address?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a address.");
                if (string.IsNullOrEmpty(social_insurance_no?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a social insurance number.");
                if (string.IsNullOrEmpty(social_insurance_no?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a social insurance number.");

                // Generate a new instance
                Librarian_db instance = new Librarian_db
                    (
                        employee_id, firstName, lastName, phone_no, address, social_insurance_no, library_address
                    );

                // Add to database
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "INSERT INTO librarian (First_name, Last_name, Password, Address, Date_of_birth) values (@first_name, @last_name, @phone_no, @address, @date_of_birth)",
                        parameters: new Dictionary<string, object>()
                        {
                            { "@first_name", instance.First_name },
                            { "@last_name", instance.Last_name },
                            { "@phone_no", instance.Phone_no },
                            { "@address", instance.Address },
                            { "@social_insurance_no", instance.Social_insurance_no },
                            { "@library_address", instance.Library_address }
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

                //instance.Employee_id;

                // Return value
                statusResponse = new StatusResponse("Customer added successfully");
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
        public static Librarian_db Edit(string employee_id, string firstName, string lastName, string phone_no, string address, string social_insurance_no, string library_address,
           DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Validate
                if (string.IsNullOrEmpty(employee_id?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide an employee id.");
                if (string.IsNullOrEmpty(firstName?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a first name.");
                if (string.IsNullOrEmpty(lastName?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a last name.");
                if (string.IsNullOrEmpty(phone_no?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a phone number.");
                if (string.IsNullOrEmpty(address?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide an address.");
                if (string.IsNullOrEmpty(social_insurance_no?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a social insurance number.");
                if (string.IsNullOrEmpty(library_address?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a social insurance number.");

                // Generate a new instance
                Librarian_db instance = new Librarian_db
                    (
                        employee_id, firstName, lastName, phone_no, address, social_insurance_no, library_address
                    );

                // Add to database
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "UPDATE customer SET First_name = @first_name, Last_name = @last_name, Password = @password, Address = @address, Date_of_birth = @date_of_birth WHERE card_id = @card_id",
                        parameters: new Dictionary<string, object>()
                        {
                            {"@card_id", instance.Employee_id },
                            { "@first_name", instance.First_name },
                            { "@last_name", instance.Last_name },
                            { "@password", instance.Phone_no },
                            { "@address", instance.Address },
                            { "@date_of_birth", instance.Social_insurance_no }
                        },
                        message: out string message
                    );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return value
                statusResponse = new StatusResponse("Customer edited successfully");
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
        public static void Delete(int cardId, DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Validate
                if (cardId < 0)
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a card id.");


                // Add to database
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "DELETE FROM customer WHERE card_id = @card_id",
                        parameters: new Dictionary<string, object>()
                        {
                            {"@card_id", cardId },
                        },
                        message: out string message
                    );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return value
                statusResponse = new StatusResponse("Customer deleted successfully");
            }
            catch (Exception exception)
            {
                statusResponse = new StatusResponse(exception);
            }

        }

        /// <summary>
        /// Retrieves an instance.
        /// </summary>
        public static Librarian_db Get(string employee_id,
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Get from database
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT * FROM customer WHERE card_id = @card_id",
                        parameters: new Dictionary<string, object>()
                        {
                            {"@employee_id", employee_id },
                        },
                        message: out string message
                    );
                if (table == null)
                    throw new Exception(message);

                DataRow row = table.Rows[0];

                // Parse data
                Librarian_db instance = new Librarian_db
                            (
                                employee_id: row["Employee_id"].ToString(),
                                firstName: row["First_name"].ToString(),
                                lastName: row["Last_name"].ToString(),
                                phone_no: row["Phone_no"].ToString(),
                                address: row["Address"].ToString(),
                                social_insurance_no: row["Social_insurance_number"].ToString(),
                                library_address: row["library_address"].ToString()
                            );

                // Return value
                statusResponse = new StatusResponse("Librarian has been retrieved successfully.");
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
        public static List<Librarian_db> GetCollection(
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Get from database
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT * FROM librarian",
                        parameters: new Dictionary<string, object>()
                        {

                        },
                        message: out string message
                    );
                if (table == null)
                    throw new Exception(message);

                // Parse data
                List<Librarian_db> instances = new List<Librarian_db>();
                foreach (DataRow row in table.Rows)
                    instances.Add(new Librarian_db
                            (
                                employee_id: row["Employee_id"].ToString(),
                                firstName: row["First_name"].ToString(),
                                lastName: row["Last_name"].ToString(),
                                phone_no: row["Phone_no"].ToString(),
                                address: row["Address"].ToString(),
                                social_insurance_no: row["Social_insurance_no"].ToString(),
                                library_address: row["Library_address"].ToString()
                            )
                        );

                // Return value
                statusResponse = new StatusResponse("Customer list has been retrieved successfully.");
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
