using EventApplicationProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventApplicationProject.Interface
{
    /// <summary>
    /// Interface for location service operations.
    /// </summary>
    public interface ILocationService
    {
        /// <summary>
        /// Lists all locations.
        /// </summary>
        /// <returns>A collection of <see cref="LocationDto"/>.</returns>
        Task<IEnumerable<LocationDto>> ListLocations();

        /// <summary>
        /// Retrieves a specific location by ID.
        /// </summary>
        /// <param name="id">The ID of the location.</param>
        /// <returns>A <see cref="LocationDto"/> if found; otherwise, null.</returns>
        Task<LocationDto> GetLocation(int id);

        /// <summary>
        /// Creates a new location.
        /// </summary>
        /// <param name="locationDto">The details of the location to create.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the creation.</returns>
        Task<ServiceResponse> CreateLocation(LocationDto locationDto);

        /// <summary>
        /// Updates the details of an existing location.
        /// </summary>
        /// <param name="id">The ID of the location to update.</param>
        /// <param name="locationDto">The updated location details.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the update.</returns>
        Task<ServiceResponse> UpdateLocationDetails(int id, LocationDto locationDto);

        /// <summary>
        /// Deletes a location by ID.
        /// </summary>
        /// <param name="id">The ID of the location to delete.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the deletion.</returns>
        Task<ServiceResponse> DeleteLocation(int id);
    }
}
