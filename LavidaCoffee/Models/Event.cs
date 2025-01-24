using System.Drawing;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace LavidaCoffee.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public required DateTime Date { get; set; }
        public required string Title { get; set; }
        public string? ShortDescription { get; set; }
        public string? LongDescription { get; set; }
        public required string Address { get; set; }
        public string? ImageUrl { get; set; }
        public string? ThumbnailUrl { get; set; }
    }
}
