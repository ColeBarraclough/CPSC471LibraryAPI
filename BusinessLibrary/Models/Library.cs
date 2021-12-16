﻿using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLibrary.Models
{
    public class Library
    {

        #region Contructors

        /// <summary>
        /// Default constructor. 
        /// Need for serialization purposes.
        /// </summary>
        public Library()
        {
        }

        /// <summary>
        /// Fields constructor.
        /// </summary>
        public Library(string address, string name, string website_address, string admin_id)
        {
            Address = address;
            Name = name;
            Website_address = website_address;
            Admin_id = admin_id;

        }

        /// <summary>
        /// Clone/Copy constructor.
        /// </summary>
        /// <param name="instance">The object to clone from.</param>
        public Library(Library instance)
            : this(instance.Address, instance.Name, instance.Website_address, instance.Admin_id)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// CardId generated by our system upon creation of a new instance.
        /// </summary>
        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }

        /// <summary>
        /// First name of the customer.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Last name of the customer.
        /// </summary>
        [JsonProperty(PropertyName = "website_address")]
        public string Website_address { get; set; }

        /// <summary>
        /// Password of the customer.
        /// </summary>
        [JsonProperty(PropertyName = "admin_id")]
        public string Admin_id { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Overrides the .ToString method.
        /// </summary>
        public override string ToString()
        {
            return Name;
        }

        #endregion

    }
}
