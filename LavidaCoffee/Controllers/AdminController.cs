using LavidaCoffee.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LavidaCoffee.ViewModels;
using System.Net;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
		public async Task<IActionResult> Index(string sortBy, int? pageNumber)
		{
			ViewData["CurrentSort"] = sortBy;

			ViewData["EventIdSortParam"] = String.IsNullOrEmpty(sortBy) || sortBy == "eventid_descending" ? "eventid" : "eventid_descending";
			ViewData["TitleSortParam"] = sortBy == "title" ? "title_descending" : "title";
			ViewData["DateSortParam"] = sortBy == "date" ? "date_descending" : "date";

			AdminViewModel model = new()
			{
				Events = (await _eventRepository.GetEventsPagedAndSortedAsync(sortBy,1,10)).ToList(),
				Emails = (await _emailRepository.GetEmailsPagedAsync(1,10)).ToList(),
				TotalEmailCount = await _emailRepository.GetAllEmailsCountAsync(),
				TotalEventCount = await _eventRepository.GetAllEventsCountAsync()
			};

			return View(model);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public async Task<IActionResult> DeleteEvent(int id)
		{
			var targetEvent = await _eventRepository.GetEventByIdAsync(id);
			if (targetEvent == null)
			{
				TempData["errorMessage"] = $"Error: Failed to delete event - no event with id={id} found.";	
			}
			else
			{
				await _eventRepository.DeleteEventAsync(targetEvent);
			}
			return RedirectToAction("Index");
			
		}
		[Authorize(Roles = "Admin")]
		[HttpPost]
		public async Task<IActionResult> CreateEvent(Event newEvent)
		{
			if (ModelState.IsValid)
			{
				await _eventRepository.CreateEventAsync(newEvent);
			}
            return RedirectToAction("Index");
        }

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public async Task<IActionResult> EditEvent(Find_UsEventDetailsViewModel viewModel)
		{
			var updatedEvent = viewModel.Event;
			var existingEvent = await _eventRepository.GetEventByIdAsync(updatedEvent.EventId);
			Console.WriteLine("Event parsed:" + updatedEvent.EventId);
			Console.WriteLine("Event got:" + existingEvent.EventId);
			try
			{
				if (ModelState.IsValid)
				{
					existingEvent.Title = updatedEvent.Title;
					existingEvent.Date = updatedEvent.Date;
					existingEvent.ShortDescription = updatedEvent.ShortDescription;
					existingEvent.LongDescription = updatedEvent?.LongDescription;
					existingEvent.Address = updatedEvent.Address;
					existingEvent.ThumbnailUrl = updatedEvent.ThumbnailUrl;
					existingEvent.ImageUrl = updatedEvent.ImageUrl;

					await _eventRepository.UpdateEventAsync(existingEvent);
					return RedirectToAction("EventDetails", "Find_Us", new { id = existingEvent.EventId });
				}
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", $"Updating the category failed, try again. \n Error: {ex.Message}");	
			}
			return RedirectToAction("EventDetails", "Find_Us", new { id = existingEvent.EventId });
		}
	}
}