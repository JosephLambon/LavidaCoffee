using LavidaCoffee.Models;

namespace LavidaCoffee.ViewModels
{
	public class AdminViewModel
	{
		public IEnumerable<Event> Events { get; }
		public List<EmailRequest> EmailRequests { get; }

		public AdminViewModel(IEnumerable<Event> events, List<EmailRequest> emailRequests)
		{
			Events = events;
			EmailRequests = emailRequests;
		}
	}
}
