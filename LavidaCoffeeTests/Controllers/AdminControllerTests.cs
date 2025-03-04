using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LavidaCoffee.Controllers;
using LavidaCoffee.Models;
using LavidaCoffee.ViewModels;
using LavidaCoffeeTests.Mocks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using Org.BouncyCastle.Asn1.BC;

namespace LavidaCoffeeTests.Controllers
{
	public class AdminControllerTests
	{
		[Fact]
		public async Task Index_ReturnsView()
		{
			// Arrange
			var mockEmailRequestRepository = RepositoryMocks.GetEmailRepository();
			var mockEventRepository = RepositoryMocks.GetEventRepository();
			var adminController = new AdminController(mockEmailRequestRepository.Object, mockEventRepository.Object);
			// Aact
			var result = await adminController.Index();
			// Assert
			Assert.IsType<ViewResult>(result);
		}
		[Fact]
		public async Task DeleteEvent_ValidEvent_RemovesCorrectEvent()
		{
			// Arrange
			var mockEmailRequestRepository = RepositoryMocks.GetEmailRepository();
			var mockEventRepository = RepositoryMocks.GetEventRepository();
			var adminController = new AdminController(mockEmailRequestRepository.Object, mockEventRepository.Object);
			// Act
			var result = await adminController.DeleteEvent(1);
			var eventCount = await mockEventRepository.Object.GetAllEventsCountAsync();
			// Assert
			Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal(10, eventCount);
			Assert.Null( await mockEventRepository.Object.GetEventByIdAsync(1));
		}
		[Fact]
		public async Task DeleteEvent_InvalidEvent_ReturnsError()
		{
			// Arrange
			var mockEmailRequestRepository = RepositoryMocks.GetEmailRepository();
			var mockEventRepository = RepositoryMocks.GetEventRepository();
			var adminController = new AdminController(mockEmailRequestRepository.Object, mockEventRepository.Object);
			adminController.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
			// Act
			var result = await adminController.DeleteEvent(2147483647);
			// Assert
			var viewResult = Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal(11, await mockEventRepository.Object.GetAllEventsCountAsync());
			Assert.Equal($"Error: Failed to delete event - no event with id=2147483647 found.", adminController.TempData["errorMessage"]);
		}
		[Fact]
		public async Task CreateEvent_ValidEvent_AddsEvent()
		{
			// Arrange
			var mockEmailRequestRepository = RepositoryMocks.GetEmailRepository();
			var mockEventRepository = RepositoryMocks.GetEventRepository();
			var adminController = new AdminController(mockEmailRequestRepository.Object, mockEventRepository.Object);
			var newEvent = new Event
			{
				EventId = 12,
				Title = "Unit Test Festival",
				Date = DateTime.Now.AddDays(100),
				Address = "5 Apple Street, Banana village, Cranberry county",
				ShortDescription = "Brief overview of my unit test.",
				LongDescription = "This is a more extensive description of this Unit Test Festival. Lots of details to be included. You shouldn't let this pass you by... (please do)",
				ImageUrl = "https://example.com",
				ThumbnailUrl = "https://example.com"
			};
			// Act
			var result = await adminController.CreateEvent(newEvent);
			// Assert
			Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal(12, await mockEventRepository.Object.GetAllEventsCountAsync());
			Assert.Equal("Unit Test Festival", (await mockEventRepository.Object.GetEventByIdAsync(12)).Title);
		}
		
		[Fact]
		public async Task CreateEvent_InvalidEvent_ReturnsError()
		{
			// Arrange
			var mockEmailRequestRepository = RepositoryMocks.GetEmailRepository();
			var mockEventRepository = RepositoryMocks.GetEventRepository();
			var adminController = new AdminController(mockEmailRequestRepository.Object, mockEventRepository.Object);
			var newEvent = new Event
			{
				EventId = 12,
				Address = "5 Apple Street, Banana village, Cranberry county",
				Title = "",
				Date = DateTime.Now,
				ShortDescription = "Brief overview of my unit test.",
				LongDescription = "This is a more extensive description of this Unit Test Festival. Lots of details to be included. You shouldn't let this pass you by... (please do)",
				ImageUrl = "#",
				ThumbnailUrl = "#"
			};
			adminController.ModelState.AddModelError("Title", "Title is required.");
			// Act
			var result = await adminController.CreateEvent(newEvent);
			// Assert
			Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal(11, await mockEventRepository.Object.GetAllEventsCountAsync());
		}
		[Fact]
		public async Task EditEvent_ValidEvent_UpdatesEvent()
		{
			// Arrange
			var mockEmailRequestRepository = RepositoryMocks.GetEmailRepository();
			var mockEventRepository = RepositoryMocks.GetEventRepository();
			var adminController = new AdminController(mockEmailRequestRepository.Object, mockEventRepository.Object);
			var updatedEvent = new Event
			{
				EventId = 11,
				Title = "Craft Workshop - Advanced",
				Date = DateTime.Now.AddDays(70),
				Address = "Art Studio, Main Street",
				ShortDescription = "A hands-on craft workshop.",
				LongDescription = "Learn new crafting techniques and create your own handmade items.",
				ImageUrl = "https://images.pexels.com/photos/1109541/pexels-photo-1109541.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
				ThumbnailUrl = "https://images.pexels.com/photos/1109541/pexels-photo-1109541.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
			};
			// Act
			var result = await adminController.EditEvent(new Find_UsEventDetailsViewModel() { Event  = updatedEvent });
			// Assert
			Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal("Craft Workshop - Advanced", (await mockEventRepository.Object.GetEventByIdAsync(11)).Title);
		}
		[Fact]
		public async Task EditEvent_InvalidEvent_ReturnsError()
		{
			// Arrange
			var mockEmailRequestRepository = RepositoryMocks.GetEmailRepository();
			var mockEventRepository = RepositoryMocks.GetEventRepository();
			var adminController = new AdminController(mockEmailRequestRepository.Object, mockEventRepository.Object);
			var updatedEvent = new Event
			{
				EventId = 11,
				Title = "",
				Date = DateTime.Now.AddDays(70),
				Address = "Art Studio, Main Street",
				ShortDescription = "A hands-on craft workshop.",
				LongDescription = "Learn new crafting techniques and create your own handmade items.",
				ImageUrl = "https://images.pexels.com/photos/1109541/pexels-photo-1109541.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
				ThumbnailUrl = "https://images.pexels.com/photos/1109541/pexels-photo-1109541.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
			};
			var viewModel = new Find_UsEventDetailsViewModel
			{
				Event = updatedEvent
			};
			adminController.ModelState.AddModelError("Title", "Title is required.");
			// Act
			var result = await adminController.EditEvent(viewModel);
			// Assert
			Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal(11, await mockEventRepository.Object.GetAllEventsCountAsync());
		}
	}
}
