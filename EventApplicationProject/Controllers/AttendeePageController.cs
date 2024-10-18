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

        // GET: AttendeePage/Index
        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        // GET: AttendeePage/List
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var attendees = await _attendeeService.ListAttendees();
            return View(attendees);
        }

        // GET: AttendeePage/Details/{id}
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

        // GET: AttendeePage/New
        public IActionResult New()
        {
            return View();
        }

        // POST: AttendeePage/Add
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

        // GET: AttendeePage/Edit/{id}
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

        // POST: AttendeePage/Update/{id}
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

        // GET: AttendeePage/ConfirmDelete/{id}
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

        // POST: AttendeePage/Delete/{id}
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
