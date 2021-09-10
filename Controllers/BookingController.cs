using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookingService.Models;
using System.Net.Mime;

namespace BookingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly bookingContext _context;

        public BookingController(bookingContext context)
        {
            _context = context;
        }

        // GET: api/bookingItems
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<IEnumerable<bookingDTO>>> ViewBookingItems()
        {
            //return await _context.bookingItems.ToListAsync();
            return await _context.bookingItems
                     .Select(x => ItemToDTO(x))
                        .ToListAsync();
        }

        // GET: api/bookingItems/5
        [HttpGet("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<bookingDTO>> ViewbookingItem(int id)
        {
            var bookingItem = await _context.bookingItems.FindAsync(id);

            if (bookingItem == null)
            {
                return NotFound();
            }
            return ItemToDTO(bookingItem);
        }

        //PUT: api/bookingItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> PutbookingItem(int id, bookingDTO bookingDTO)
        {
            if (id != bookingDTO.Id)
            {
                return BadRequest();
            }           

            var bookingItem = await _context.bookingItems.FindAsync(id);
            if (bookingItem == null)
            {
                return NotFound();
            }
            bookingItem.Id = bookingDTO.Id;
            bookingItem.Date = bookingDTO.Date;
            bookingItem.Time = bookingDTO.Time;
            bookingItem.Venue = bookingDTO.Venue;
            bookingItem.TicketNo = bookingDTO.TicketNo;
            bookingItem.Amount = bookingDTO.Amount;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!BookingItemExists(id))
            {
                return NotFound();
            }

            return Ok();
        }

        // POST: api/bookingItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]

        public async Task<ActionResult<bookingDTO>> CreatebookingItem(bookingDTO bookingItemDTO)
        {
            var booking = new bookingDTO
            {
                Id = bookingItemDTO.Id,
                Date = bookingItemDTO.Date,
                Time = bookingItemDTO.Time,
                Venue = bookingItemDTO.Venue,
                TicketNo = bookingItemDTO.TicketNo,
                Amount = bookingItemDTO.Amount
            };

            _context.bookingItems.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(ViewbookingItem),
                new { id = booking.Id },
                ItemToDTO(booking));
        }

        // DELETE: api/bookingItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletebookingItem(int id)
        {
            var todoItem = await _context.bookingItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.bookingItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool BookingItemExists(long id)
        {
            return _context.bookingItems.Any(e => e.Id == id);
        }
        private static bookingDTO ItemToDTO(bookingDTO bookingItem) =>
        new bookingDTO
        {
            Id = bookingItem.Id,
            Date = bookingItem.Date,
            Time = bookingItem.Time,
            Venue = bookingItem.Venue,
            TicketNo = bookingItem.TicketNo,
            Amount = bookingItem.Amount
        };
    }
}
