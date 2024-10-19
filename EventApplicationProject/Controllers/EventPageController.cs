using EventApplicationProject.Interface;
using EventApplicationProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventApplicationProject.Controllers
{
    public class EventPageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly IEventService _eventService;

        public EventPageController(IEventService eventService)
        {
            _eventService = eventService;
        }

        /// <summary>
        /// Retrieves a list of all events.
        /// </summary>
        /// <returns>A collection of <see cref="EventDto"/>.</returns>
        [HttpGet]
        public async Task<IActionResult> List()
        {
            return View(await _eventService.ListEvents());
        }

        /// <summary>
        /// Retrieves a specific event by ID.
        /// </summary>
        /// <param name="id">The ID of the event.</param>
        /// <returns>An <see cref="EventDto"/> if found; otherwise, null.</returns>
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            return View(await _eventService.GetEvent(id));
        }

        /// <summary>
        /// Displays a form to create a new event.
        /// </summary>
        /// <returns>A view for creating a new event.</returns>
        public ActionResult New()
        {
            return View();
        }

        /// <summary>
        /// Creates a new event.
        /// </summary>
        /// <param name="eventDto">The details of the event to create.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the creation.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(EventDto eventDto)
        {
            ServiceResponse response = await _eventService.CreateEvent(eventDto);

            if (response.Status == ServiceResponse.ServiceStatus.Created)
            {
                return RedirectToAction("Details", "EventPage", new { id = response.CreatedId });
            }
            else
            {
                return View("Error", new ErrorViewModel() { Errors = response.Messages });
            }
        }

        /// <summary>
        /// Displays a form to edit an existing event.
        /// </summary>
        /// <param name="id">The ID of the event to update.</param>
        /// <returns>A view for editing the event, or an error if not found.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            EventDto? EventDto = await _eventService.GetEvent(id);
            if (EventDto == null)
            {
                return View("Error");
            }
            else
            {
                return View(EventDto);
            }
        }

        /// <summary>
        /// Updates the details of an existing event.
        /// </summary>
        /// <param name="id">The ID of the event to update.</param>
        /// <param name="eventDto">The updated event details.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the update.</returns>
        [HttpPost]
        public async Task<IActionResult> Update(int id, EventDto eventDto)
        {
            ServiceResponse response = await _eventService.UpdateEventDetails(id, eventDto);

            if (response.Status == ServiceResponse.ServiceStatus.Updated)
            {
                return RedirectToAction("Details", "EventPage", new { id = id });
            }
            else
            {
                return View("Error", new ErrorViewModel() { Errors = response.Messages });
            }
        }

        /// <summary>
        /// Displays a confirmation page for deleting an event.
        /// </summary>
        /// <param name="id">The ID of the event to delete.</param>
        /// <returns>A view for confirming the deletion of the event, or an error if not found.</returns>
        [HttpGet]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            EventDto? eventDto = await _eventService.GetEvent(id);
            if (eventDto == null)
            {
                return View("Error");
            }
            else
            {
                return View(eventDto);
            }
        }

        /// <summary>
        /// Deletes an event by ID.
        /// </summary>
        /// <param name="id">The ID of the event to delete.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the deletion.</returns>
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResponse response = await _eventService.Deleteevent(id);

            if (response.Status == ServiceResponse.ServiceStatus.Deleted)
            {
                return RedirectToAction("List", "EventPage");
            }
            else
            {
                return View("Error");
            }
        }
    }
}
