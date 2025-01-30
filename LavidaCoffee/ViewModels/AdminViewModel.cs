using System.ComponentModel.DataAnnotations;
using LavidaCoffee.Models;

namespace LavidaCoffee.ViewModels
{
	public class AdminViewModel
	{
		public IEnumerable<Event> Events { get; }
		public List<EmailRequest> EmailRequests { get; }

		public DateTime Date { get; set; }
		public	string Title { get; set; }
		public string? ShortDescription { get; set; }
		public string? LongDescription { get; set; }
		public string Address { get; set; }
		public string? ImageUrl { get; set; }
		public string? ThumbnailUrl { get; set; }

		public AdminViewModel(IEnumerable<Event> events, List<EmailRequest> emailRequests)
		{
			Events = events;
			EmailRequests = emailRequests;
		}
	}
}
