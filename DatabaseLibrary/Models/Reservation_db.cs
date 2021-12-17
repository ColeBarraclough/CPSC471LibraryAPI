using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLibrary.Models
{
    public class Reservation_db
    {

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Reservation_db()
        {
        }

        /// <summary>
        /// Fields constructor.
        /// </summary>
        public Reservation_db(int librarianId, DateTime returnDate, DateTime pickupDate, int mediaId, int customerCardId)
        {
            Librarian_id = librarianId;
            Pickup_date = pickupDate;
            Return_date = returnDate;
            Customer_card_id = customerCardId;
            Media_id = mediaId;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Labrarian Id of the reservation
        /// </summary>
        public int Librarian_id { get; set; }


        /// <summary>
        /// Return_date of the reservation
        /// </summary>
        public DateTime Return_date { get; set; }


        /// <summary>
        /// Labrarian Id of the reservation
        /// </summary>
        public DateTime Pickup_date { get; set; }

        /// <summary>
        /// Media id of the reservation
        /// </summary>
        public int Media_id { get; set; }

        /// <summary>
        /// customer card id of the reservation
        /// </summary>
        public int Customer_card_id { get; set; }

        #endregion

    }
}
