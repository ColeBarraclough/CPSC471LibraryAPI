using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLibrary.Models
{
    public class Recommends_db
    {

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Recommends_db()
        {
        }

        /// <summary>
        /// Fields constructor.
        /// </summary>
        public Recommends_db(string recommendation_address, int media_id, string recommendation_card)
        {
            Recommendation_address = recommendation_address;
            Media_id = media_id;
            Recommendation_card = recommendation_card;
        }

        #endregion

        #region Properties

        /// <summary>
        /// CardId generated by our system upon creation of a new instance.
        /// </summary>
        public string Recommendation_address { get; set; }

        /// <summary>
        /// First name used of the Reccomends.
        /// </summary>
        public int Media_id { get; set; }

        /// <summary>
        /// Last name used of the Reccomends.
        /// </summary>
        public string Recommendation_card { get; set; }
        #endregion

    }
}
