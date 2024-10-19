using EventApplicationProject.Interface;
using EventApplicationProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventApplicationProject.Controllers
{
    public class AttendeePageController : Controller
    {
        private readonly IAttendeeService _attendeeService;

        // Dependency injection of attendee service
        public AttendeePageController(IAttendeeService attendeeService)
        {
            _attendeeService = attendeeService;
        }

        /// <summary>
        /// Redirects to the list of attendees.
        /// </summary>
        /// <returns>Redirects to List action.</returns>
        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        /// <summary>
        /// Retrieves and displays a list of all attendees.
        /// </summary>
        /// <returns>A view displaying the list of attendees.</returns>
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var attendees = await _attendeeService.ListAttendees();
            return View(attendees);
        }

        /// <summary>
        /// Retrieves and displays details of a specific attendee.
        /// </summary>
        /// <param name="id">The ID of the attendee.</param>
        /// <returns>A view displaying the details of the attendee.</returns>
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var attendee = await _attendeeService.Getattendee(id);
            if (attendee == null)
            {
                return View("Error", new ErrorViewModel() { Errors = new List<string> { "Could not find attendee" } });
            }
            return View(attendee);
        }

        /// <summary>
        /// Displays a form for creating a new attendee.
        /// </summary>
        /// <returns>A view with the attendee creation form.</returns>
        public IActionResult New()
        {
            return View();
        }

        /// <summary>
        /// Handles the creation of a new attendee.
        /// </summary>
        /// <param name="attendeeDto">The attendee data transfer object.</param>
        /// <returns>Redirects to the details of the newly created attendee or shows an error.</returns>
        [HttpPost]
        public async Task<IActionResult> Add(AttendeeDto attendeeDto)
        {
            var response = await _attendeeService.CreateAttendee(attendeeDto);
            if (response.Status == ServiceResponse.ServiceStatus.Created)
            {
                return RedirectToAction("Details", "AttendeePage", new { id = response.CreatedId });
            }
            return View("Error", new ErrorViewModel() { Errors = response.Messages });
        }

        /// <summary>
        /// Displays a form for editing an existing attendee.
        /// </summary>
        /// <param name="id">The ID of the attendee to edit.</param>
        /// <returns>A view with the attendee editing form.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var attendee = await _attendeeService.Getattendee(id);
            if (attendee == null)
            {
                return View("Error");
            }
            return View(attendee);
        }

        /// <summary>
        /// Handles the update of an existing attendee's details.
        /// </summary>
        /// <param name="id">The ID of the attendee to update.</param>
        /// <param name="attendeeDto">The updated attendee data transfer object.</param>
        /// <returns>Redirects to the attendee's details or shows an error.</returns>
        [HttpPost]
        public async Task<IActionResult> Update(int id, AttendeeDto attendeeDto)
        {
            var response = await _attendeeService.UpdateAttendeeDetails(id, attendeeDto);
            if (response.Status == ServiceResponse.ServiceStatus.Updated)
            {
                return RedirectToAction("Details", "AttendeePage", new { id });
            }
            return View("Error", new ErrorViewModel() { Errors = response.Messages });
        }

        /// <summary>
        /// Displays a confirmation page for deleting an attendee.
        /// </summary>
        /// <param name="id">The ID of the attendee to delete.</param>
        /// <returns>A view for confirming the deletion of the attendee.</returns>
        [HttpGet]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var attendee = await _attendeeService.Getattendee(id);
            if (attendee == null)
            {
                return View("Error");
            }
            return View(attendee);
        }

        /// <summary>
        /// Handles the deletion of an attendee.
        /// </summary>
        /// <param name="id">The ID of the attendee to delete.</param>
        /// <returns>Redirects to the list of attendees or shows an error.</returns>
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _attendeeService.DeleteAttendee(id);
            if (response.Status == ServiceResponse.ServiceStatus.Deleted)
            {
                return RedirectToAction("List", "AttendeePage");
            }
            return View("Error", new ErrorViewModel() { Errors = response.Messages });
        }
    }
}
