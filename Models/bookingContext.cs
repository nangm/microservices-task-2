using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.Models
{
    public class bookingContext: DbContext
    {
        public bookingContext(DbContextOptions<bookingContext> options)
    : base(options)
        {
        }

        public DbSet<bookingDTO> bookingItems { get; set; }
    }
}
