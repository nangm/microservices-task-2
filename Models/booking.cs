using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.Models
{
    public class booking
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 75)]
        //[Required]
        public string Date { get; set; }
        public string Time { get; set; }
        public string Venue { get; set; }
        public int TicketNo { get; set; }
        public decimal Amount { get; set; }
    }
}
