using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLibrary.Models
{
    public class DVD_db
    {

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DVD_db()
        {
        }

        /// <summary>
        /// Fields constructor.
        /// </summary>
        public DVD_db(int systemId, string title, string libraryAddress, string genre, DateTime publishingDate, int authorId,
            int? borrowerId, DateTime? dateOfCheckOut, DateTime? dueDate, int runTime)
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
            Run_Time = runTime;
        }

        #endregion

        #region Properties

        /// <summary>
        /// System id of cd.
        /// </summary>
        public int System_id { get; set; }

        /// <summary>
        /// title of cd.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Library address of cd.
        /// </summary>
        public string Library_address { get; set; }

        /// <summary>
        /// Genre of cd.
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// Publishing date of cd.
        /// </summary>
        public DateTime Publishing_date { get; set; }

        /// <summary>
        /// Author Id of cd.
        /// </summary>
        public int Author_id { get; set; }

        /// <summary>
        /// Borrower id of cd.
        /// </summary>
        public int? Borrower_id { get; set; }


        /// <summary>
        /// Date of checkout of cd.
        /// </summary>
        public DateTime? Date_of_check_out { get; set; }

        /// <summary>
        /// Due date of cd
        /// </summary>
        public DateTime? Due_date { get; set; }

        /// <summary>
        /// lenght of cd.
        /// </summary>
        public int Run_Time { get; set; }
        #endregion

    }
}
