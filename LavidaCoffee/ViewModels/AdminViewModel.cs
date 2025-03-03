using System.ComponentModel.DataAnnotations;
using LavidaCoffee.Models;

namespace LavidaCoffee.ViewModels
{
	public class AdminViewModel
	{
		public List<Event>? Events { get; set; }
		public List<Email>? Emails { get; set; }
		public int TotalEmailCount { get; set; }

		public Event newEvent = new Event
		{
			Date = DateTime.Now,
			Title = string.Empty,
			Address = string.Empty,
			ShortDescription = string.Empty,
			LongDescription = string.Empty
		};
	}
}
