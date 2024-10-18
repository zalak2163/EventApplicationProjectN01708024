using EventApplicationProject.Data;
using EventApplicationProject.Interface;
using EventApplicationProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventApplicationProject.Services
{
    /// <summary>
    /// Service class for managing location operations, including creation, retrieval, updating, and deletion.
    /// </summary>
    public class LocationService : ILocationService
    {
        private readonly ApplicationDbContext _context;

        public LocationService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a list of all locations.
        /// </summary>
        /// <returns>A collection of LocationDto objects.</returns>
        public async Task<IEnumerable<LocationDto>> ListLocations()
        {
            var locations = await _context.Locations.ToListAsync();
            var locationDtos = new List<LocationDto>();

            foreach (var location in locations)
            {
                locationDtos.Add(new LocationDto
                {
                    LocationId = location.LocationId,
                    LocationName = location.LocationName,
                    Address = location.Address,
                    Capacity = location.Capacity
                });
            }

            return locationDtos;
        }

        /// <summary>
        /// Retrieves a location by its ID.
        /// </summary>
        /// <param name="id">The ID of the location.</param>
        /// <returns>A LocationDto containing the location details, or null if not found.</returns>
        public async Task<LocationDto> GetLocation(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            return location == null ? null : new LocationDto
            {
                LocationId = location.LocationId,
                LocationName = location.LocationName,
                Address = location.Address,
                Capacity = location.Capacity
            };
        }

        /// <summary>
        /// Creates a new location and saves it to the database.
        /// </summary>
        /// <param name="locationDto">The location data transfer object.</param>
        /// <returns>A ServiceResponse indicating the result of the creation operation.</returns>
        public async Task<ServiceResponse> CreateLocation(LocationDto locationDto)
        {
            var location = new Location
            {
                LocationName = locationDto.LocationName,
                Address = locationDto.Address,
                Capacity = locationDto.Capacity
            };

            _context.Locations.Add(location);
            await _context.SaveChangesAsync();

            return new ServiceResponse
            {
                Status = ServiceResponse.ServiceStatus.Created,
                CreatedId = location.LocationId
            };
        }

        /// <summary>
        /// Updates the details of an existing location.
        /// </summary>
        /// <param name="id">The ID of the location to update.</param>
        /// <param name="locationDto">The updated location data transfer object.</param>
        /// <returns>A ServiceResponse indicating the result of the update operation.</returns>
        public async Task<ServiceResponse> UpdateLocationDetails(int id, LocationDto locationDto)
        {
            var existingLocation = await _context.Locations.FindAsync(id);
            if (existingLocation == null)
            {
                return new ServiceResponse
                {
                    Status = ServiceResponse.ServiceStatus.NotFound,
                    Messages = { "Location not found." }
                };
            }

            existingLocation.LocationName = locationDto.LocationName ?? existingLocation.LocationName;
            existingLocation.Address = locationDto.Address ?? existingLocation.Address;
            existingLocation.Capacity = locationDto.Capacity != 0 ? locationDto.Capacity : existingLocation.Capacity;

            await _context.SaveChangesAsync();

            return new ServiceResponse
            {
                Status = ServiceResponse.ServiceStatus.Updated
            };
        }

        /// <summary>
        /// Deletes a location by its ID.
        /// </summary>
        /// <param name="id">The ID of the location to delete.</param>
        /// <returns>A ServiceResponse indicating the result of the deletion operation.</returns>
        public async Task<ServiceResponse> DeleteLocation(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return new ServiceResponse
                {
                    Status = ServiceResponse.ServiceStatus.NotFound,
                    Messages = { "Location cannot be deleted because it does not exist." }
                };
            }

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();

            return new ServiceResponse
            {
                Status = ServiceResponse.ServiceStatus.Deleted
            };
        }
    }
}
