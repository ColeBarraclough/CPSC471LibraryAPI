using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLibrary.Models
{
    public class DVD
    {

        #region Contructors

        /// <summary>
        /// Default constructor. 
        /// Need for serialization purposes.
        /// </summary>
        public DVD()
        {
        }

        /// <summary>
        /// Fields constructor.
        /// </summary>
        public DVD(int systemId, string title, string libraryAddress, string genre, DateTime publishingDate, int authorId,
            int? borrowerId, DateTime? dateOfCheckOut, DateTime? dueDate, int runTime)
        {
            SystemId = systemId;
            Title = title;
            LibraryAddress = libraryAddress;
            Genre = genre;
            PublishingDate = publishingDate;
            AuthorId = authorId;
            BorrowerId = borrowerId;
            DateOfCheckOut = dateOfCheckOut;
            DueDate = dueDate;
            RunTime = runTime;
        }

        /// <summary>
        /// Clone/Copy constructor.
        /// </summary>
        /// <param name="instance">The object to clone from.</param>
        public DVD(DVD instance)
            : this(instance.SystemId, instance.Title, instance.LibraryAddress, instance.Genre, instance.PublishingDate, instance.AuthorId, instance.BorrowerId, instance.DateOfCheckOut, instance.DueDate, instance.RunTime)
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
        /// Library address of cd.
        /// </summary>
        [JsonProperty(PropertyName = "library_address")]
        public string LibraryAddress { get; set; }

        /// <summary>
        /// Genre of cd.
        /// </summary>
        [JsonProperty(PropertyName = "genre")]
        public string Genre { get; set; }

        /// <summary>
        /// Publishing date of cd.
        /// </summary>
        [JsonProperty(PropertyName = "publishing_date")]
        public DateTime PublishingDate { get; set; }

        /// <summary>
        /// Author Id of cd.
        /// </summary>
        [JsonProperty(PropertyName = "author_id")]
        public int AuthorId { get; set; }

        /// <summary>
        /// Borrower id of cd.
        /// </summary>
        [JsonProperty(PropertyName = "borrower_id")]
        public int? BorrowerId { get; set; }


        /// <summary>
        /// Date of checkout of cd.
        /// </summary>
        [JsonProperty(PropertyName = "date_of_check_out")]
        public DateTime? DateOfCheckOut { get; set; }

        /// <summary>
        /// Due date of cd
        /// </summary>
        [JsonProperty(PropertyName = "due_date")]
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// length of cd.
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
