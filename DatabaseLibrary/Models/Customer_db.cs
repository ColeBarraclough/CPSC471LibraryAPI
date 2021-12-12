﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLibrary.Models
{
    public class Customer_db
    {

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Customer_db()
        {
        }

        /// <summary>
        /// Fields constructor.
        /// </summary>
        public Customer_db(int cardId, string firstName, string lastName, string password, string address, DateTime dateOfBirth)
        {
            CardId = cardId;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            Address = address;
            DateOfBirth = dateOfBirth;
        }

        #endregion

        #region Properties

        /// <summary>
        /// CardId generated by our system upon creation of a new instance.
        /// </summary>
        public int CardId { get; set; }

        /// <summary>
        /// First name used of the customer.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name used of the customer.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Password used of the customer.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Address used of the customer.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Date of birth used of the customer.
        /// </summary>
        public DateTime DateOfBirth { get; set; }
        #endregion

    }
}
