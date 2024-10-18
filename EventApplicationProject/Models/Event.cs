using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EventApplicationProject.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        /// <summary>
        /// Gets or sets the name of the event.
        /// </summary>
        public string EventName { get; set; } = string.Empty; // Default to empty string

        /// <summary>
        /// Gets or sets the date of the event.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the description of the event.
        /// </summary>
        public string Description { get; set; } = string.Empty; // Default to empty string

        /// <summary>
        /// Foreign key for the location of the event.
        /// </summary>
        [ForeignKey("Location")]
        public int LocationId { get; set; }

        public virtual Location Location { get; set; }

        public virtual ICollection<Attendee> Attendees { get; set; } = new List<Attendee>(); // Initialize to an empty list
    }

    /// <summary>
    /// Data Transfer Object for event information.
    /// </summary>
    public class EventDto
    {
        public int EventId { get; set; }

        /// <summary>
        /// Gets or sets the name of the event.
        /// </summary>
        public string EventName { get; set; } = string.Empty; // Default to empty string

        /// <summary>
        /// Gets or sets the date of the event.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the description of the event.
        /// </summary>
        public string Description { get; set; } = string.Empty; // Default to empty string

        /// <summary>
        /// Foreign key for the location of the event.
        /// </summary>
        public int LocationId { get; set; }
    }
}
