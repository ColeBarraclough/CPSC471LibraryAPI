﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLibrary.Models
{
    public class Reccomends_db
    {

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Reccomends_db()
        {
        }

        /// <summary>
        /// Fields constructor.
        /// </summary>
        public Reccomends_db(string reccomendation_address, int media_id, string reccomendation_card)
        {
            Reccomendation_address = reccomendation_address;
            Media_id = media_id;
            Reccomendation_card = reccomendation_card;
        }

        #endregion

        #region Properties

        /// <summary>
        /// CardId generated by our system upon creation of a new instance.
        /// </summary>
        public string Reccomendation_address { get; set; }

        /// <summary>
        /// First name used of the Reccomends.
        /// </summary>
        public int Media_id { get; set; }

        /// <summary>
        /// Last name used of the Reccomends.
        /// </summary>
        public string Reccomendation_card { get; set; }
        #endregion

    }
}
