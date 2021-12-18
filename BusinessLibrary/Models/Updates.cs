using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLibrary.Models
{
    public class Updates
    {

        #region Contructors

        /// <summary>
        /// Default constructor. 
        /// Need for serialization purposes.
        /// </summary>
        public Updates()
        {
        }

        /// <summary>
        /// Fields constructor.
        /// </summary>
        public Updates(int librarianId, int mediaId, string library_address)
        {
            LibrarianId = librarianId;
            MediaId = mediaId;
            LibraryAddress = library_address;

        }

        /// <summary>
        /// Clone/Copy constructor.
        /// </summary>
        /// <param name="instance">The object to clone from.</param>
        public Updates(Updates instance)
            : this(instance.LibrarianId, instance.MediaId, instance.LibraryAddress)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// librarian id of update.
        /// </summary>
        [JsonProperty(PropertyName = "librarian_id")]
        public int LibrarianId { get; set; }

        /// <summary>
        /// media id of update.
        /// </summary>
        [JsonProperty(PropertyName = "media_id")]
        public int MediaId { get; set; }


        /// <summary>
        /// media id of update.
        /// </summary>
        [JsonProperty(PropertyName = "library_address")]
        public string LibraryAddress { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Overrides the .ToString method.
        /// </summary>
        public override string ToString()
        {
            return ""+ LibrarianId;
        }

        #endregion

    }
}
