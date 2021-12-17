using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLibrary.Models
{
    public class Updates_db
    {

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Updates_db()
        {
        }

        /// <summary>
        /// Fields constructor.
        /// </summary>
        public Updates_db(int librarianId, int mediaId)
        {
            Librarian_id = librarianId;
            Media_id = mediaId;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Labrarian Id of the update
        /// </summary>
        public int Librarian_id { get; set; }

        /// <summary>
        /// Media id of the system
        /// </summary>
        public int Media_id { get; set; }
        #endregion

    }
}
