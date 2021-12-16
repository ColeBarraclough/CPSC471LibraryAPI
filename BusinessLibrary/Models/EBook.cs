using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLibrary.Models
{
    public class EBook
    {

        #region Contructors

        /// <summary>
        /// Default constructor. 
        /// Need for serialization purposes.
        /// </summary>
        public EBook()
        {
        }

        /// <summary>
        /// Fields constructor.
        /// </summary>
        public EBook(int systemId, string title, string genre, DateTime publishingDate, int authorId, string link,
             int pages)
        {
            SystemId = systemId;
            Title = title;
            Genre = genre;
            PublishingDate = publishingDate;
            AuthorId = authorId;
            Link = link;
            Pages = pages;
        }

        /// <summary>
        /// Clone/Copy constructor.
        /// </summary>
        /// <param name="instance">The object to clone from.</param>
        public EBook(EBook instance)
            : this(instance.SystemId, instance.Title, instance.Genre, instance.PublishingDate, instance.AuthorId, instance.Link, instance.Pages)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// System id of cd.
        /// </summary>
        [JsonProperty(PropertyName = "system_id")]
        public int SystemId { get; set; }

        /// <summary>
        /// title of cd.
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Genre of cd.
        /// </summary>
        [JsonProperty(PropertyName = "genre")]
        public string Genre { get; set; }

        /// <summary>
        /// Publishing date of cd.
        /// </summary>
        [JsonProperty(PropertyName = "pulblishing_date")]
        public DateTime PublishingDate { get; set; }

        /// <summary>
        /// Author Id of cd.
        /// </summary>
        [JsonProperty(PropertyName = "author_id")]
        public int AuthorId { get; set; }


        /// <summary>
        /// Author Id of cd.
        /// </summary>
        [JsonProperty(PropertyName = "link")]
        public string Link { get; set; }

        /// <summary>
        /// length of cd.
        /// </summary>
        [JsonProperty(PropertyName = "pages")]
        public int Pages { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Overrides the .ToString method.
        /// </summary>
        public override string ToString()
        {
            return Title;
        }

        #endregion

    }
}
