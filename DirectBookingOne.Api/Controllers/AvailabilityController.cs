using DirectBookingOne.Api.Models;
using DirectBookingOne.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DirectBookingOne.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailabilityController : ControllerBase
    {
        private readonly AvailabilityService _availabilityService;

        public AvailabilityController(AvailabilityService availabilityService)
        {
            _availabilityService = availabilityService;
        }

        // POST: api/Availability (Create Availability)
        [HttpPost]
        public async Task<IActionResult> CreateAvailability([FromBody] Availability availability)
        {
            if (availability == null)
            {
                return BadRequest("Availability information is required.");
            }

            // Check availability
            if (!await _availabilityService.CheckAvailability(availability))
            {
                return BadRequest("Availability overlaps with existing records.");
            }

            var createdAvailability = await _availabilityService.CreateAsync(availability);

            if (createdAvailability == null)
            {
                return StatusCode(500, "Error creating availability.");
            }

            return CreatedAtAction(nameof(GetAvailability), new { id = createdAvailability.Id }, createdAvailability);
        }

        // GET: api/Availability/{id} (Get Availability by ID)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAvailability(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Please provide a valid availability ID.");
            }

            var availability = await _availabilityService.GetByIdAsync(id);

            if (availability == null)
            {
                return NotFound("Availability not found.");
            }

            return Ok(availability);
        }

        // DELETE: api/Availability/{id} (Delete Availability)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAvailability(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Please provide a valid availability ID.");
            }

            await _availabilityService.DeleteAsync(id);

            return NoContent(); // Indicates successful deletion without content
        }

        // POST: api/Availability/Check (Check Availability)
        [HttpPost("check")]
        public async Task<IActionResult> CheckAvailability([FromBody] Availability availability)
        {
            if (availability == null)
            {
                return BadRequest("Availability information is required.");
            }

            var isAvailable = await _availabilityService.CheckAvailability(availability);

            return Ok(new { IsAvailable = isAvailable });
        }
    }
}
