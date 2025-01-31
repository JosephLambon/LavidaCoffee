using System.ComponentModel.DataAnnotations;
using LavidaCoffee.Models;

namespace LavidaCoffee.ViewModels
{
	public class AdminViewModel
	{
		public IEnumerable<Event> Events { get; }
		public List<EmailRequest> EmailRequests { get; }
		[Required]
		public DateTime Date { get; set; }
		[Required]
		public	string Title { get; set; }
		[Required]
		public string? ShortDescription { get; set; }
		[Required]
		public string? LongDescription { get; set; }
		[Required]
		public string Address { get; set; }
		[Required]
		public string? ImageUrl { get; set; }
		[Required]
		public string? ThumbnailUrl { get; set; }

		public AdminViewModel(IEnumerable<Event> events, List<EmailRequest> emailRequests)
		{
			Events = events;
			EmailRequests = emailRequests;
		}
	}
}
