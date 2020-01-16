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
    public class BrandsController : ControllerBase
    {
        private readonly CatalogContext _context;

        public BrandsController(CatalogContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GetBrand returns List of Brand
        /// </summary>        
        /// <returns>List of Brand</returns>  
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrand()
        {
            return await _context.Brands.ToListAsync();
        }

        /// <summary>
        /// GetBrand with specific ID
        /// </summary>
        /// <returns>Return specifc Brand</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrand(int id)
        {
            var brand = await _context.Brands.FindAsync(id);

            if (brand == null)
            {
                return NotFound();
            }

            return brand;
        }

        /// <summary>
        /// Update specific Brand
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /Brand/id
        ///     {
        ///        "id": 1,
        ///        "name": "Acura"
        ///     }
        ///
        /// </remarks>
        /// <param name="id">Brand ID</param>
        /// <param name="brand">Product Brand</param>
        /// <response code="404">IF product not exists</response>
        /// <response code="400">Format of product it's incorrect</response>  
        /// <response code="204">Product updated</response>  
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutBrand(int id, Brand brand)
        {
            if (id != brand.Id)
            {
                return BadRequest();
            }

            _context.Entry(brand).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandExists(id))
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
        /// Creates a Brand.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Brand
        ///     {
        ///        "name": "Acura"
        ///     }
        ///
        /// </remarks>
        /// <param name="brand"></param>
        /// <returns>A newly created Brand</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>  
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Brand>> PostBrand(Brand brand)
        {
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBrand", new { id = brand.Id }, brand);
        }

        /// <summary>
        /// Delete Brand 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Returns removed Model</response>
        /// <response code="404">If Model not exists</response>  
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Brand>> DeleteBrand(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();

            return brand;
        }

        private bool BrandExists(int id)
        {
            return _context.Brands.Any(e => e.Id == id);
        }
    }
}
