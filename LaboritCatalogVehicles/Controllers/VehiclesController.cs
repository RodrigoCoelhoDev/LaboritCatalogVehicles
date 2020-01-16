using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LaboritCatalogVehicles.Models;

namespace LaboritCatalogVehicles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly CatalogContext _context;

        public VehiclesController(CatalogContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GetModels returns List of Vehicle
        /// </summary>        
        /// <returns>List of Vehicle</returns>  
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
        {
            return await _context.Vehicles.ToListAsync();
        }

        /// <summary>
        /// GetVehicle with specific ID
        /// </summary>
        /// <returns>Return specifc Vehicle</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicle(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return vehicle;
        }

        /// <summary>
        /// Update specific vehicle
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /Vehicle/id
        ///     {
		///         id: 1254
		///         value: "10.254,00",
		///         brand: "Acura",
		///         model: "Integra GS 1.8",
		///         yearModel: 1992,
		///         fuel: "Gasolina"
        ///     }
        ///
        /// </remarks>
        /// <param name="id">vehicle ID</param>
        /// <param name="vehicle">Name vehicle</param>
        /// <response code="404">IF vehicle not exists</response>
        /// <response code="400">Format of vehicle it's incorrect</response>  
        /// <response code="204">Product updated</response>  
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutVehicle(int id, Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return BadRequest();
            }

            _context.Entry(vehicle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Creates a vehicle.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Model
        ///     {
		///         value: "10.254,00",
		///         brand: "Acura",
		///         model: "Integra GS 1.8",
		///         yearModel: 1992,
		///         fuel: "Gasolina"
        ///     }
        ///
        /// </remarks>
        /// <param name="vehicle"></param>
        /// <returns>A newly created vehicle</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>  
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Vehicle>> PostVehicle(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicle", new { id = vehicle.Id }, vehicle);
        }

        /// <summary>
        /// Delete vehicle 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Returns removed vehicle</response>
        /// <response code="404">If vehicle not exists</response>  
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Vehicle>> DeleteVehicle(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return vehicle;
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehicles.Any(e => e.Id == id);
        }
    }
}
