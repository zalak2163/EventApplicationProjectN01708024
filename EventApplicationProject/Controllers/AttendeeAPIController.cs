using EventApplicationProject.Interface;
using EventApplicationProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace EventApplicationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendeeAPIController : ControllerBase
    {
        private readonly IAttendeeService _attendeeService;

        public AttendeeAPIController(IAttendeeService attendeeService)
        {
            _attendeeService = attendeeService;
        }

        /// <summary>
        /// Retrieves a list of all attendees.
        /// </summary>
        /// <returns>A collection of <see cref="AttendeeDto"/>.</returns>
        [HttpGet]
        public async Task<IEnumerable<AttendeeDto>> ListAttendees()
        {
            return await _attendeeService.ListAttendees();
        }

        /// <summary>
        /// Retrieves a specific attendee by ID.
        /// </summary>
        /// <param name="id">The ID of the attendee.</param>
        /// <returns>The <see cref="AttendeeDto"/> if found; otherwise, null.</returns>
        [HttpGet("{id}")]
        public async Task<AttendeeDto> Getattendee(int id)
        {
            return await _attendeeService.Getattendee(id);
        }

        /// <summary>
        /// Creates a new attendee.
        /// </summary>
        /// <param name="attendeeDto">The attendee details.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the creation.</returns>
        [HttpPost("Add")]
        public async Task<ServiceResponse> CreateAttendee(AttendeeDto attendeeDto)
        {
            return await _attendeeService.CreateAttendee(attendeeDto);
        }

        /// <summary>
        /// Updates the details of an existing attendee.
        /// </summary>
        /// <param name="id">The ID of the attendee to update.</param>
        /// <param name="attendeeDto">The updated attendee details.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the update.</returns>
        [HttpPut("Update/{id}")]
        public async Task<ServiceResponse> UpdateAttendeeDetails(int id, AttendeeDto attendeeDto)
        {
            return await _attendeeService.UpdateAttendeeDetails(id, attendeeDto);
        }

        /// <summary>
        /// Deletes an attendee by ID.
        /// </summary>
        /// <param name="id">The ID of the attendee to delete.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the deletion.</returns>
        [HttpDelete("Delete/{id}")]
        public async Task<ServiceResponse> DeleteAttendee(int id)
        {
            return await _attendeeService.DeleteAttendee(id);
        }


    }
}