using LavidaCoffee.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LavidaCoffee.ViewModels;
using System.Net;
using Microsoft.IdentityModel.Tokens;

namespace LavidaCoffee.Controllers
{
	public class AdminController : Controller
	{
		private readonly IEmailRepository _emailRepository;
		private readonly IEventRepository _eventRepository;

		public AdminController(IEmailRepository emailRepository, IEventRepository eventRepository)
		{
			_emailRepository = emailRepository;
			_eventRepository = eventRepository;
		}


		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Index()
		{
			IEnumerable<Email> emailRequests = await _emailRepository.GetAllEmailRequestsAsync();
			IEnumerable<Event> upcomingEvents = await _eventRepository.GetUpcomingEventsAsync();

			return View(new AdminViewModel(upcomingEvents, emailRequests));
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public async Task<IActionResult> DeleteEvent(int id)
		{
			try
			{
				var targetEvent = await _eventRepository.GetEventByIdAsync(id);
				_eventRepository.DeleteEvent(targetEvent);
			}
			catch(Exception ex)
			{
				TempData["errorMessage"] = $"Failed to delete event - no event with id={id} found. \n Error: {ex.Message}";
			}
			return RedirectToAction("Index");
		}
		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult CreateEvent(Event newEvent)
		{
			if (ModelState.IsValid)
			{
				_eventRepository.CreateEvent(newEvent);
			}
            return RedirectToAction("Index");
        }

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public async Task<IActionResult> EditEvent(Event updatedEventDetails)
		{
			var existingEvent = await _eventRepository.GetEventByIdAsync(updatedEventDetails.EventId);
			if (ModelState.IsValid)
			{
				existingEvent.Title = updatedEventDetails.Title;
				existingEvent.Date = updatedEventDetails.Date;
				existingEvent.ShortDescription = updatedEventDetails.ShortDescription;
				existingEvent.LongDescription = updatedEventDetails?.LongDescription;
				existingEvent.Address = updatedEventDetails.Address;
				existingEvent.ThumbnailUrl = updatedEventDetails.ThumbnailUrl;
				existingEvent.ImageUrl = updatedEventDetails.ImageUrl;

				_eventRepository.UpdateEvent(existingEvent);

			}
			return RedirectToAction("EventDetails", "Find_Us", new { id = existingEvent.EventId });
		}
	}
}