using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLibrary.Models
{
    public class EBook_db
    {

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public EBook_db()
        {
        }

        /// <summary>
        /// Fields constructor.
        /// </summary>
        public EBook_db(int systemId, string title, string genre, DateTime publishingDate, int authorId, string link,
            int pages)
        {
            System_id = systemId;
            Title = title;
            Genre = genre;
            Publishing_date = publishingDate;
            Author_id = authorId;
            Link = link;
            Pages = pages;
        }

        #endregion

        #region Properties

        /// <summary>
        /// System id of book.
        /// </summary>
        public int System_id { get; set; }

        /// <summary>
        /// title of book.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Genre of book.
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// Publishing date of book.
        /// </summary>
        public DateTime Publishing_date { get; set; }

        /// <summary>
        /// Author Id of book.
        /// </summary>
        public int Author_id { get; set; }

        /// <summary>
        /// Link of Ebook.
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// pages of book.
        /// </summary>
        public int Pages { get; set; }
        #endregion

    }
}
