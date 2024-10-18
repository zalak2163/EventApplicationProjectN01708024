using EventApplicationProject.Data;
using EventApplicationProject.Interface;
using EventApplicationProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace EventApplicationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventAPIController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventAPIController(IEventService eventService)
        {
            _eventService = eventService;
        }

        /// <summary>
        /// Retrieves a list of all events.
        /// </summary>
        /// <returns>A collection of <see cref="EventDto"/>.</returns>
        [HttpGet("List")]
        public async Task<IEnumerable<EventDto>> ListEvents()
        {
            return await _eventService.ListEvents();
        }

        /// <summary>
        /// Retrieves a specific event by ID.
        /// </summary>
        /// <param name="id">The ID of the event.</param>
        /// <returns>An <see cref="EventDto"/> if found; otherwise, null.</returns>
        [HttpGet("{id}")]
        public async Task<EventDto> GetEvent(int id)
        {
            return await _eventService.GetEvent(id);
        }

        /// <summary>
        /// Creates a new event.
        /// </summary>
        /// <param name="eventDto">The details of the event to create.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the creation.</returns>
        [HttpPost("Add")]
        public async Task<ServiceResponse> CreateEvent(EventDto eventDto)
        {
            return await _eventService.CreateEvent(eventDto);
        }

        /// <summary>
        /// Updates the details of an existing event.
        /// </summary>
        /// <param name="id">The ID of the event to update.</param>
        /// <param name="eventDto">The updated event details.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the update.</returns>
        [HttpPut("Update/{id}")]
        public async Task<ServiceResponse> UpdateEventDetails(int id, EventDto eventDto)
        {
            return await _eventService.UpdateEventDetails(id, eventDto);
        }

        /// <summary>
        /// Deletes an event by ID.
        /// </summary>
        /// <param name="id">The ID of the event to delete.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the deletion.</returns>
        [HttpDelete("Delete/{id}")]
        public async Task<ServiceResponse> Deleteevent(int id)
        {
            return await _eventService.Deleteevent(id);
        }



    }
}
