using EventApplicationProject.Data;
using EventApplicationProject.Interface;
using EventApplicationProject.Models;
using Microsoft.EntityFrameworkCore;

namespace EventApplicationProject.Services
{
    public class AttendeeService : IAttendeeService
    {
        private readonly ApplicationDbContext _context;

        public AttendeeService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lists all attendees.
        /// </summary>
        /// <returns>A collection of <see cref="AttendeeDto"/>.</returns>
        public async Task<IEnumerable<AttendeeDto>> ListAttendees()
        {
            var attendees = await _context.Attendees.ToListAsync();
            return attendees.Select(a => new AttendeeDto
            {
                AttendeeId = a.AttendeeId,
                AttendeeName = a.AttendeeName,
                Email = a.Email
            }).ToList();
        }

        /// <summary>
        /// Retrieves an attendee by ID.
        /// </summary>
        /// <param name="id">The ID of the attendee.</param>
        /// <returns>The <see cref="AttendeeDto"/> if found; otherwise, null.</returns>
        public async Task<AttendeeDto> Getattendee(int id)
        {
            var attendee = await _context.Attendees.FindAsync(id);
            if (attendee == null) return null;

            return new AttendeeDto
            {
                AttendeeId = attendee.AttendeeId,
                AttendeeName = attendee.AttendeeName,
                Email = attendee.Email
            };
        }

        /// <summary>
        /// Creates a new attendee.
        /// </summary>
        /// <param name="attendeeDto">The details of the attendee to create.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the creation.</returns>
        public async Task<ServiceResponse> CreateAttendee(AttendeeDto attendeeDto)
        {
            ServiceResponse serviceResponse = new();

            var newAttendee = new Attendee
            {
                AttendeeName = attendeeDto.AttendeeName,
                Email = attendeeDto.Email
            };

            _context.Attendees.Add(newAttendee);
            await _context.SaveChangesAsync();

            serviceResponse.Status = ServiceResponse.ServiceStatus.Created;
            serviceResponse.CreatedId = newAttendee.AttendeeId;
            serviceResponse.Messages.Add("Attendee created successfully.");

            return serviceResponse;
        }

        /// <summary>
        /// Updates an existing attendee's details.
        /// </summary>
        /// <param name="id">The ID of the attendee to update.</param>
        /// <param name="attendeeDto">The updated attendee details.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the update.</returns>
        public async Task<ServiceResponse> UpdateAttendeeDetails(int id, AttendeeDto attendeeDto)
        {
            ServiceResponse serviceResponse = new();

            var existingAttendee = await _context.Attendees.FindAsync(id);
            if (existingAttendee == null)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.NotFound;
                serviceResponse.Messages.Add("Attendee not found.");
                return serviceResponse;
            }

            existingAttendee.AttendeeName = attendeeDto.AttendeeName;
            existingAttendee.Email = attendeeDto.Email;

            await _context.SaveChangesAsync();

            serviceResponse.Status = ServiceResponse.ServiceStatus.Updated;
            serviceResponse.Messages.Add("Attendee updated successfully.");

            return serviceResponse;
        }

        /// <summary>
        /// Deletes an attendee by ID.
        /// </summary>
        /// <param name="id">The ID of the attendee to delete.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the deletion.</returns>
        public async Task<ServiceResponse> DeleteAttendee(int id)
        {
            ServiceResponse serviceResponse = new();

            var attendee = await _context.Attendees.FindAsync(id);
            if (attendee == null)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.NotFound;
                serviceResponse.Messages.Add("Attendee not found.");
                return serviceResponse;
            }

            _context.Attendees.Remove(attendee);
            await _context.SaveChangesAsync();

            serviceResponse.Status = ServiceResponse.ServiceStatus.Deleted;
            serviceResponse.Messages.Add("Attendee deleted successfully.");

            return serviceResponse;
        }

    }
}


