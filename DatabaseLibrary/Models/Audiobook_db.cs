using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLibrary.Models
{
    public class Audiobook_db
    {

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Audiobook_db()
        {
        }

        /// <summary>
        /// Fields constructor.
        /// </summary>
        public Audiobook_db(int systemId, string title, string genre, DateTime publishingDate, int authorId, string link,
            int runTime)
        {
            System_id = systemId;
            Title = title;
            Genre = genre;
            Publishing_date = publishingDate;
            Author_id = authorId;
            Link = link;
            Run_Time = runTime;
        }

        #endregion

        #region Properties

        /// <summary>
        /// System id of Audiobook.
        /// </summary>
        public int System_id { get; set; }

        /// <summary>
        /// title of Audiobook.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Genre of Audiobook.
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// Publishing date of Audiobook.
        /// </summary>
        public DateTime Publishing_date { get; set; }

        /// <summary>
        /// Author Id of Audiobook.
        /// </summary>
        public int Author_id { get; set; }

        /// <summary>
        /// Link of Audiobook.
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// pages of Audiobook.
        /// </summary>
        public int Run_Time { get; set; }
        #endregion

    }
}
