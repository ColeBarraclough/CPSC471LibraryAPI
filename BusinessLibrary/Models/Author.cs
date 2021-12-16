﻿using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLibrary.Models
{
    public class Author
    {

        #region Contructors

        /// <summary>
        /// Default constructor. 
        /// Need for serialization purposes.
        /// </summary>
        public Author()
        {
        }

        /// <summary>
        /// Fields constructor.
        /// </summary>
        public Author(string author_id, string firstName, string lastName, DateTime dateOfBirth)
        {
            Author_id = author_id;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;

        }

        /// <summary>
        /// Clone/Copy constructor.
        /// </summary>
        /// <param name="instance">The object to clone from.</param>
        public Author(Author instance)
            : this(instance.Author_id, instance.FirstName, instance.LastName, instance.DateOfBirth)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// CardId generated by our system upon creation of a new instance.
        /// </summary>
        [JsonProperty(PropertyName = "author_id")]
        public string Author_id { get; set; }

        /// <summary>
        /// First name of the Author.
        /// </summary>
        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the Author.
        /// </summary>
        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        /// <summary>
        /// Date of birth of the Author.
        /// </summary>
        [JsonProperty(PropertyName = "date_of_birth")]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// FUll name of the Author.
        /// </summary>
        [JsonProperty(PropertyName = "full_name")]
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
