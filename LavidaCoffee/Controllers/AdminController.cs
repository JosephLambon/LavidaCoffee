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
		private readonly IEmailRequestRepository _emailRequestRepository;
		private readonly IEventRepository _eventRepository;

		public AdminController(IEmailRequestRepository emailRequestRepository, IEventRepository eventRepository)
		{
			_emailRequestRepository = emailRequestRepository;
			_eventRepository = eventRepository;
		}


		[Authorize(Roles = "Admin")]
		public IActionResult Index()
		{
			List<EmailRequest> emailRequests = _emailRequestRepository.AllEmailRequests();
			IEnumerable<Event> upcomingEvents = _eventRepository.AllEvents.Where(e => e.Date > DateTime.Today);

			if(!TempData.IsNullOrEmpty())
			{
				ViewBag.Message = TempData["errorMessage"].ToString();
			}
			return View(new AdminViewModel(upcomingEvents, emailRequests));
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult DeleteEvent(int id)
		{
			var targetEvent = _eventRepository.GetEventById(id);
			if(targetEvent!=null)
			{
				_eventRepository.DeleteEvent(targetEvent);
				return RedirectToAction("Index");
			}
			TempData["errorMessage"] = $"Error: Failed to delete event - no event with id={id} found";
			return RedirectToAction("Index");
		}
		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult AddEvent(Event newEvent)
		{
			if (ModelState.IsValid)
			{
				_eventRepository.CreateEvent(newEvent);
			}
            return RedirectToAction("Index");
        }

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult EditEvent(Event updatedEventDetails)
		{
			var existingEvent = _eventRepository.GetEventById(updatedEventDetails.EventId);
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