using System.ComponentModel.DataAnnotations;

namespace EventApplicationProject.Models
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }

        /// <summary>
        /// Gets or sets the name of the location.
        /// </summary>
        public string LocationName { get; set; }

        /// <summary>
        /// Gets or sets the address of the location.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the capacity of the location.
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// Navigation property for events associated with this location.
        /// </summary>
        public virtual ICollection<Event> Events { get; set; }
    }

    /// <summary>
    /// Data Transfer Object for location information.
    /// </summary>
    public class LocationDto
    {
        public int LocationId { get; set; }

        /// <summary>
        /// Gets or sets the name of the location.
        /// </summary>
        public string LocationName { get; set; }

        /// <summary>
        /// Gets or sets the address of the location.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the capacity of the location.
        /// </summary>
        public int Capacity { get; set; }
    }
}
