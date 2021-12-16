using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLibrary.Models
{
    public class Audiobook
    {

        #region Contructors

        /// <summary>
        /// Default constructor. 
        /// Need for serialization purposes.
        /// </summary>
        public Audiobook()
        {
        }

        /// <summary>
        /// Fields constructor.
        /// </summary>
        public Audiobook(int systemId, string title, string genre, DateTime publishingDate, int authorId, string link,
             int runTime)
        {
            SystemId = systemId;
            Title = title;
            Genre = genre;
            PublishingDate = publishingDate;
            AuthorId = authorId;
            Link = link;
            RunTime = runTime;
        }

        /// <summary>
        /// Clone/Copy constructor.
        /// </summary>
        /// <param name="instance">The object to clone from.</param>
        public Audiobook(EBook instance)
            : this(instance.SystemId, instance.Title, instance.Genre, instance.PublishingDate, instance.AuthorId, instance.Link, instance.Pages)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// System id of Audiobook.
        /// </summary>
        [JsonProperty(PropertyName = "system_id")]
        public int SystemId { get; set; }

        /// <summary>
        /// title of Audiobook.
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Genre of Audiobook.
        /// </summary>
        [JsonProperty(PropertyName = "genre")]
        public string Genre { get; set; }

        /// <summary>
        /// Publishing date of Audiobook.
        /// </summary>
        [JsonProperty(PropertyName = "pulblishing_date")]
        public DateTime PublishingDate { get; set; }

        /// <summary>
        /// Author Id of Audiobook.
        /// </summary>
        [JsonProperty(PropertyName = "author_id")]
        public int AuthorId { get; set; }


        /// <summary>
        /// Author Id of Audiobook.
        /// </summary>
        [JsonProperty(PropertyName = "link")]
        public string Link { get; set; }

        /// <summary>
        /// length of Audiobook.
        /// </summary>
        [JsonProperty(PropertyName = "run_time")]
        public int RunTime { get; set; }

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
