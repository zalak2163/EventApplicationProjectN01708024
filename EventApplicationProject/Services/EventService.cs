using EventApplicationProject.Data;
using EventApplicationProject.Interface;
using EventApplicationProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventApplicationProject.Services
{
    /// <summary>
    /// Service class for managing event operations, including creation, retrieval, updating, and deletion of events.
    /// </summary>
    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _context;

        public EventService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lists all events.
        /// </summary>
        /// <returns>A collection of <see cref="EventDto"/>.</returns>
        public async Task<IEnumerable<EventDto>> ListEvents()
        {
            List<Event> events = await _context.Events
                .Include(e => e.Location) // Assuming a Location navigation property exists
                .ToListAsync();

            List<EventDto> eventDtos = new List<EventDto>();
            foreach (Event eventItem in events)
            {
                eventDtos.Add(new EventDto()
                {
                    EventId = eventItem.EventId,
                    EventName = eventItem.EventName,
                    Date = eventItem.Date,
                    Description = eventItem.Description,
                    LocationId = eventItem.LocationId,
                });
            }

            return eventDtos;
        }

        /// <summary>
        /// Retrieves a specific event by ID.
        /// </summary>
        /// <param name="id">The ID of the event.</param>
        /// <returns>An <see cref="EventDto"/> if found; otherwise, null.</returns>
        public async Task<EventDto> GetEvent(int id)
        {
            var eventItem = await _context.Events
                .Include(e => e.Location) // Include Location details
                .FirstOrDefaultAsync(e => e.EventId == id);

            if (eventItem == null)
            {
                return null;
            }

            return new EventDto()
            {
                EventId = eventItem.EventId,
                EventName = eventItem.EventName,
                Date = eventItem.Date,
                Description = eventItem.Description,
                LocationId = eventItem.LocationId,
            };
        }

        /// <summary>
        /// Creates a new event.
        /// </summary>
        /// <param name="eventDto">The details of the event to create.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the creation.</returns>
        public async Task<ServiceResponse> CreateEvent(EventDto eventDto)
        {
            ServiceResponse serviceResponse = new();

            var location = await _context.Locations.FindAsync(eventDto.LocationId);
            if (location == null)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add("Location not found. Please provide a valid LocationId.");
                return serviceResponse;
            }

            Event eventItem = new Event()
            {
                EventName = eventDto.EventName,
                Date = eventDto.Date,
                Description = eventDto.Description,
                LocationId = eventDto.LocationId
            };

            _context.Events.Add(eventItem);
            await _context.SaveChangesAsync();

            serviceResponse.Status = ServiceResponse.ServiceStatus.Created;
            serviceResponse.CreatedId = eventItem.EventId;
            serviceResponse.Messages.Add($"Event created successfully: {eventItem.EventName}");

            return serviceResponse;
        }

        /// <summary>
        /// Updates the details of an existing event.
        /// </summary>
        /// <param name="id">The ID of the event to update.</param>
        /// <param name="eventDto">The updated event details.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the update.</returns>
        public async Task<ServiceResponse> UpdateEventDetails(int id, EventDto eventDto)
        {
            ServiceResponse serviceResponse = new ServiceResponse();

            var existingEvent = await _context.Events.FindAsync(id);
            if (existingEvent == null)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.NotFound;
                serviceResponse.Messages.Add("Event not found.");
                return serviceResponse;
            }

            if (eventDto.LocationId != 0 && eventDto.LocationId != existingEvent.LocationId)
            {
                var location = await _context.Locations.FindAsync(eventDto.LocationId);
                if (location == null)
                {
                    serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                    serviceResponse.Messages.Add("Location not found.");
                    return serviceResponse;
                }
                existingEvent.LocationId = eventDto.LocationId;
            }

            existingEvent.EventName = eventDto.EventName ?? existingEvent.EventName;
            existingEvent.Date = eventDto.Date != default ? eventDto.Date : existingEvent.Date;
            existingEvent.Description = eventDto.Description ?? existingEvent.Description;

            await _context.SaveChangesAsync();

            serviceResponse.Status = ServiceResponse.ServiceStatus.Updated;
            serviceResponse.Messages.Add($"Event updated successfully: {existingEvent.EventName}");

            return serviceResponse;
        }

        /// <summary>
        /// Deletes an event by ID.
        /// </summary>
        /// <param name="id">The ID of the event to delete.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the deletion.</returns>
        public async Task<ServiceResponse> Deleteevent(int id)
        {
            ServiceResponse response = new();
            var eventItem = await _context.Events.FindAsync(id);
            if (eventItem == null)
            {
                response.Status = ServiceResponse.ServiceStatus.NotFound;
                response.Messages.Add("Event cannot be deleted because it does not exist.");
                return response;
            }

            _context.Events.Remove(eventItem);
            await _context.SaveChangesAsync();
            response.Status = ServiceResponse.ServiceStatus.Deleted;
            response.Messages.Add("Event deleted successfully.");

            return response;
        }

    }
}
