﻿using DatabaseLibrary.Core;
using DatabaseLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Net;
using System.Text;

namespace DatabaseLibrary.Helpers
{
    public class CustomerHelper_db
    {

        /// <summary>
        /// Adds a new instance into the database.
        /// </summary>
        public static Customer_db Add(int cardId, string firstName, string lastName, string password, string address, DateTime dateOfBirth,
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Validate
                if (string.IsNullOrEmpty(firstName?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a first name.");
                if (string.IsNullOrEmpty(lastName?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a last name.");
                if (string.IsNullOrEmpty(password?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a password.");
                if (string.IsNullOrEmpty(address?.Trim()))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a address.");
                if (dateOfBirth == default(DateTime))
                    throw new StatusException(HttpStatusCode.BadRequest, "Please provide a date of birth.");

                // Generate a new instance
                Customer_db instance = new Customer_db
                    (
                        cardId: 0, //This can be ignored is PK in your DB is auto increment
                        firstName, lastName, password, address, dateOfBirth
                    );

                // Add to database
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "INSERT INTO customer (First_name, Last_name, Password, Address, Date_of_birth) values (@first_name, @last_name, @password, @address, @date_of_birth)",
                        parameters: new Dictionary<string, object>()
                        {
                            { "@first_name", instance.First_name },
                            { "@last_name", instance.Last_name },
                            { "@password", instance.Password },
                            { "@address", instance.Address },
                            { "@date_of_birth", instance.Date_of_birth }
                        },
                        message: out string message
                    );
                if (rowsAffected == -1)
                    throw new Exception(message);

                // Return value
                statusResponse = new StatusResponse("Student added successfully");
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
        public static List<Customer_db> GetCollection(
            DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Get from database
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT * FROM customer",
                        parameters: new Dictionary<string, object>()
                        {

                        },
                        message: out string message
                    );
                if (table == null)
                    throw new Exception(message);

                // Parse data
                List<Customer_db> instances = new List<Customer_db>();
                foreach (DataRow row in table.Rows)
                    instances.Add(new Customer_db
                            (
                                cardId: (int) row["Card_id"],
                                firstName: row["First_name"].ToString(), 
                                lastName: row["Last_name"].ToString(),
                                password: row["Password"].ToString(),
                                address: row["Address"].ToString(),
                                dateOfBirth: (DateTime) row["Date_of_birth"]
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
