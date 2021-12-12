﻿using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLibrary.Models
{
    public class Customer
    {

        #region Contructors

        /// <summary>
        /// Default constructor. 
        /// Need for serialization purposes.
        /// </summary>
        public Customer()
        {
        }

        /// <summary>
        /// Fields constructor.
        /// </summary>
        public Customer(int cardId, string firstName, string lastName, string password, string address, DateTime dateOfBirth)
        {
            CardId = cardId;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            Address = address;
            DateOfBirth = dateOfBirth;

        }

        /// <summary>
        /// Clone/Copy constructor.
        /// </summary>
        /// <param name="instance">The object to clone from.</param>
        public Customer(Customer instance)
            : this(instance.CardId, instance.FirstName, instance.LastName, instance.Password, instance.Address, instance.DateOfBirth)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// CardId generated by our system upon creation of a new instance.
        /// </summary>
        [JsonProperty(PropertyName = "cardId")]
        public int CardId { get; set; }

        /// <summary>
        /// First name of the customer.
        /// </summary>
        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the customer.
        /// </summary>
        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        /// <summary>
        /// Password of the customer.
        /// </summary>
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        /// <summary>
        /// Address of the customer.
        /// </summary>
        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }

        /// <summary>
        /// Date of birth of the customer.
        /// </summary>
        [JsonProperty(PropertyName = "dateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// FUll name of the customer.
        /// </summary>
        [JsonProperty(PropertyName = "fullName")]
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Overrides the .ToString method.
        /// </summary>
        public override string ToString()
        {
            return FullName;
        }

        #endregion

    }
}
