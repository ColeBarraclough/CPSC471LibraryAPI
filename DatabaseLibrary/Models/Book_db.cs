using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLibrary.Models
{
    public class Book_db
    {

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Book_db()
        {
        }

        /// <summary>
        /// Fields constructor.
        /// </summary>
        public Book_db(int systemId, string title, string libraryAddress, string genre, DateTime publishingDate, int authorId,
            int? borrowerId, DateTime? dateOfCheckOut, DateTime? dueDate, int pages)
        {
            System_id = systemId;
            Title = title;
            Library_address = libraryAddress;
            Genre = genre;
            Publishing_date = publishingDate;
            Author_id = authorId;
            Borrower_id = borrowerId;
            Date_of_check_out = dateOfCheckOut;
            Due_date = dueDate;
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
        /// Library address of book.
        /// </summary>
        public string Library_address { get; set; }

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
        /// Borrower id of book.
        /// </summary>
        public int? Borrower_id { get; set; }


        /// <summary>
        /// Date of checkout of book.
        /// </summary>
        public DateTime? Date_of_check_out { get; set; }

        /// <summary>
        /// Due date of book
        /// </summary>
        public DateTime? Due_date { get; set; }

        /// <summary>
        /// pages of book.
        /// </summary>
        public int Pages { get; set; }
        #endregion

    }
}
