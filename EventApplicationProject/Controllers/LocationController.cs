using EventApplicationProject.Interface;
using EventApplicationProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventApplicationProject.Controllers
{
    /// <summary>
    /// Controller for managing event locations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LocationAPIController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationAPIController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        /// <summary>
        /// Retrieves a list of all locations.
        /// </summary>
        /// <returns>A collection of LocationDto objects.</returns>
        [HttpGet("List")]
        public async Task<IEnumerable<LocationDto>> ListLocations()
        {
            return await _locationService.ListLocations();
        }

        /// <summary>
        /// Retrieves a specific location by ID.
        /// </summary>
        /// <param name="id">The ID of the location to retrieve.</param>
        /// <returns>The requested LocationDto, or null if not found.</returns>
        [HttpGet("{id}")]
        public async Task<LocationDto> GetLocation(int id)
        {
            return await _locationService.GetLocation(id);
        }

        /// <summary>
        /// Creates a new location.
        /// </summary>
        /// <param name="locationDto">The location data transfer object.</param>
        /// <returns>A ServiceResponse indicating the result of the creation operation.</returns>
        [HttpPost("Add")]
        public async Task<ServiceResponse> CreateLocation(LocationDto locationDto)
        {
            return await _locationService.CreateLocation(locationDto);
        }

        /// <summary>
        /// Updates the details of an existing location.
        /// </summary>
        /// <param name="id">The ID of the location to update.</param>
        /// <param name="locationDto">The updated location data transfer object.</param>
        /// <returns>A ServiceResponse indicating the result of the update operation.</returns>
        [HttpPut("Update/{id}")]
        public async Task<ServiceResponse> UpdateLocationDetails(int id, LocationDto locationDto)
        {
            return await _locationService.UpdateLocationDetails(id, locationDto);
        }

        /// <summary>
        /// Deletes a location by its ID.
        /// </summary>
        /// <param name="id">The ID of the location to delete.</param>
        /// <returns>A ServiceResponse indicating the result of the deletion operation.</returns>
        [HttpDelete("Delete/{id}")]
        public async Task<ServiceResponse> DeleteLocation(int id)
        {
            return await _locationService.DeleteLocation(id);
        }
    }
}
