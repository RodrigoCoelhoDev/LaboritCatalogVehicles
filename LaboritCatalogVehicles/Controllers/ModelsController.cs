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
    public class ModelsController : ControllerBase
    {
        private readonly CatalogContext _context;

        public ModelsController(CatalogContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GetModels returns List of Model
        /// </summary>        
        /// <returns>List of Model</returns>  
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Model>>> GetModels()
        {
            return await _context.Models.ToListAsync();
        }

        /// <summary>
        /// GetModel with specific ID
        /// </summary>
        /// <returns>Return specifc Model</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Model>> GetModel(int id)
        {
            var model = await _context.Models.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return model;
        }

        /// <summary>
        /// GetModelBrand with specific brandId
        /// </summary>
        /// <returns>Return specifc Model</returns>
        [HttpGet("{brandId}")]
        public async Task<ActionResult<Model>> GetModelBrand(int brandId)
        {
            var model = await _context.Models.FindAsync(brandId);

            if (model == null)
            {
                return NotFound();
            }

            return model;
        }

        /// <summary>
        /// Update specific Brand
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /Model/id
        ///     {
        ///        "id": 1,
        ///        "name": "AMAROK CD2.0 16V/S CD2.0 16V TDI 4x2 Die"
        ///     }
        ///
        /// </remarks>
        /// <param name="id">Model ID</param>
        /// <param name="model">Name Model</param>
        /// <response code="404">IF product not exists</response>
        /// <response code="400">Format of product it's incorrect</response>  
        /// <response code="204">Product updated</response>  
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutModel(int id, Model model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModelExists(id))
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
        /// Creates a Model.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Model
        ///     {
        ///        "name": "AMAROK CD2.0 16V/S CD2.0 16V TDI 4x2 Die"
        ///     }
        ///
        /// </remarks>
        /// <param name="model"></param>
        /// <returns>A newly created model</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>  
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Model>> PostModel(Model model)
        {
            _context.Models.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetModel", new { id = model.Id }, model);
        }

        /// <summary>
        /// Delete Model 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Returns removed Model</response>
        /// <response code="404">If Model not exists</response>  
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Model>> DeleteModel(int id)
        {
            var model = await _context.Models.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            _context.Models.Remove(model);
            await _context.SaveChangesAsync();

            return model;
        }

        private bool ModelExists(int id)
        {
            return _context.Models.Any(e => e.Id == id);
        }
    }
}
