using EventApplicationProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventApplicationProject.Interface
{
    /// <summary>
    /// Interface for attendee service operations.
    /// </summary>
    public interface IAttendeeService
    {
        /// <summary>
        /// Lists all attendees.
        /// </summary>
        /// <returns>A collection of <see cref="AttendeeDto"/>.</returns>
        Task<IEnumerable<AttendeeDto>> ListAttendees();

        /// <summary>
        /// Retrieves a specific attendee by ID.
        /// </summary>
        /// <param name="id">The ID of the attendee.</param>
        /// <returns>An <see cref="AttendeeDto"/> if found; otherwise, null.</returns>
        Task<AttendeeDto> Getattendee(int id);

        /// <summary>
        /// Creates a new attendee.
        /// </summary>
        /// <param name="attendeeDto">The details of the attendee to create.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the creation.</returns>
        Task<ServiceResponse> CreateAttendee(AttendeeDto attendeeDto);

        /// <summary>
        /// Updates the details of an existing attendee.
        /// </summary>
        /// <param name="id">The ID of the attendee to update.</param>
        /// <param name="attendeeDto">The updated attendee details.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the update.</returns>
        Task<ServiceResponse> UpdateAttendeeDetails(int id, AttendeeDto attendeeDto);

        /// <summary>
        /// Deletes an attendee by ID.
        /// </summary>
        /// <param name="id">The ID of the attendee to delete.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the deletion.</returns>
        Task<ServiceResponse> DeleteAttendee(int id);


    }
}
