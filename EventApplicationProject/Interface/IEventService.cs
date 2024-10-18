using EventApplicationProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventApplicationProject.Interface
{
    /// <summary>
    /// Interface for event service operations.
    /// </summary>
    public interface IEventService
    {
        /// <summary>
        /// Lists all events.
        /// </summary>
        /// <returns>A collection of <see cref="EventDto"/>.</returns>
        Task<IEnumerable<EventDto>> ListEvents();

        /// <summary>
        /// Retrieves a specific event by ID.
        /// </summary>
        /// <param name="id">The ID of the event.</param>
        /// <returns>An <see cref="EventDto"/> if found; otherwise, null.</returns>
        Task<EventDto> GetEvent(int id);

        /// <summary>
        /// Creates a new event.
        /// </summary>
        /// <param name="eventDto">The details of the event to create.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the creation.</returns>
        Task<ServiceResponse> CreateEvent(EventDto eventDto);

        /// <summary>
        /// Updates the details of an existing event.
        /// </summary>
        /// <param name="id">The ID of the event to update.</param>
        /// <param name="eventDto">The updated event details.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the update.</returns>
        Task<ServiceResponse> UpdateEventDetails(int id, EventDto eventDto);

        /// <summary>
        /// Deletes an event by ID.
        /// </summary>
        /// <param name="id">The ID of the event to delete.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the deletion.</returns>
        Task<ServiceResponse> Deleteevent(int id);


    }
}
