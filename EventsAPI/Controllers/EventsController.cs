using System;
using EventsAPI.Models;
using EventsAPI.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventsAPI.Helpers;

namespace EventsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class EventsController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;
        private readonly IEventsService _eventService;

        public EventsController(ILogger<EventsController> logger, IEventsService eventService)
        {
            _logger = logger;
            _eventService = eventService;
        }


        /// <summary>
        /// return a paged list of all avaiable events.
        /// </summary>
        [HttpGet("List")]
        public async Task<IActionResult> List(int pageNo = 1, int pageSize = 25, CancellationToken cancellationToken = default)
        {
            if (pageNo < 1)
            {
                pageNo = 1;
            }

            var result = await _eventService.ListEvents(pageNo, pageSize, cancellationToken);

            if (result.Succeeded)
            {
                return Ok(result.Data);
            }

            return BadRequest();
        }

        /// <summary>
        /// Get an Event by Id.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken = default)
        {
            var result = await _eventService.GetEvent(id, cancellationToken);

            if (result.Succeeded)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Create a new Event.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post(EventModel model, CancellationToken cancellationToken = default)
        {
            // Validatiion is handeled by the ValidationFilter.

            var result = await _eventService.AddEvent(model, cancellationToken);

            if (result.Succeeded)
            {
                return CreatedAtAction(nameof(Get), new { id = result.Data });
            }

            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Update and Exsisting event.
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Put(EventModel model, CancellationToken cancellationToken = default)
        {
            // Validatiion is handeled by the ValidationFilter.

            var result = await _eventService.UpdateEvent(model, cancellationToken);

            if (result.Succeeded)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Remove an event.
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            var result = await _eventService.DeleteEvent(id, cancellationToken);

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(result.Errors);
        }
    }
}
