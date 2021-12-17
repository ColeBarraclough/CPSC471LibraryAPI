using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLibrary.Models
{
    public class Reservation
    {

        #region Contructors

        /// <summary>
        /// Default constructor. 
        /// Need for serialization purposes.
        /// </summary>
        public Reservation()
        {
        }

        /// <summary>
        /// Fields constructor.
        /// </summary>
        public Reservation(int librarianId, DateTime returnDate, DateTime pickupDate,int mediaId, int customerCardId)
        {
            LibrarianId = librarianId;
            PickupDate = pickupDate;
            ReturnDate = returnDate;
            CustomerCardId = customerCardId;
            MediaId = mediaId;


        }

        /// <summary>
        /// Clone/Copy constructor.
        /// </summary>
        /// <param name="instance">The object to clone from.</param>
        public Reservation(Reservation instance)
            : this(instance.LibrarianId, instance.ReturnDate, instance.PickupDate, instance.MediaId, instance.CustomerCardId)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// librarian id of reservation.
        /// </summary>
        [JsonProperty(PropertyName = "librarian_id")]
        public int LibrarianId { get; set; }

        /// <summary>
        /// return date of reservation.
        /// </summary>
        [JsonProperty(PropertyName = "return_date")]
        public DateTime ReturnDate { get; set; }

        /// <summary>
        /// pickup date of reservation.
        /// </summary>
        [JsonProperty(PropertyName = "pickup_date")]
        public DateTime PickupDate { get; set; }

        /// <summary>
        /// media id of reservation.
        /// </summary>
        [JsonProperty(PropertyName = "media_id")]
        public int MediaId { get; set; }


        /// <summary>
        /// customer card id of reservation.
        /// </summary>
        [JsonProperty(PropertyName = "customer_card_id")]
        public int CustomerCardId { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Overrides the .ToString method.
        /// </summary>
        public override string ToString()
        {
            return "" + LibrarianId;
        }

        #endregion

    }
}
