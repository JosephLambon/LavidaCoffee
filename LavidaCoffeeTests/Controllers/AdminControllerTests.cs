using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LavidaCoffee.Controllers;
using LavidaCoffee.Models;
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
		public void Index_ReturnsView()
		{
			// Arrange
			var mockEmailRequestRepository = RepositoryMocks.GetEmailRequestRepository();
			var mockEventRepository = RepositoryMocks.GetEventRepository();
			var adminController = new AdminController(mockEmailRequestRepository.Object, mockEventRepository.Object);
			// Aact
			var result = adminController.Index();
			// Assert
			Assert.IsType<ViewResult>(result);
		}

		[Fact]
		public void DeleteEvent_ValidEvent_RemovesCorrectEvent()
		{
			// Arrange
			var mockEmailRequestRepository = RepositoryMocks.GetEmailRequestRepository();
			var mockEventRepository = RepositoryMocks.GetEventRepository();
			var adminController = new AdminController(mockEmailRequestRepository.Object, mockEventRepository.Object);
			// Act
			var result = adminController.DeleteEvent(1);
			// AssertS
			Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal(10, mockEventRepository.Object.AllEvents.Count());
			Assert.Null(mockEventRepository.Object.GetEventById(1));
		}
		[Fact]
		public void DeleteEvent_InvalidEvent_ReturnsError()
		{
			// Arrange
			var mockEmailRequestRepository = RepositoryMocks.GetEmailRequestRepository();
			var mockEventRepository = RepositoryMocks.GetEventRepository();
			var adminController = new AdminController(mockEmailRequestRepository.Object, mockEventRepository.Object);
			adminController.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
			// Act
			var result = adminController.DeleteEvent(2147483647);
			// Assert
			var viewResult = Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal(11, mockEventRepository.Object.AllEvents.Count());
			Assert.Equal("Error: Failed to delete event - no event with id=2147483647 found", adminController.TempData["errorMessage"]);
		}
		[Fact]
		public void CreateEvent_ValidEvent_AddsEvent()
		{
			// Arrange
			var mockEmailRequestRepository = RepositoryMocks.GetEmailRequestRepository();
			var mockEventRepository = RepositoryMocks.GetEventRepository();
			var adminController = new AdminController(mockEmailRequestRepository.Object, mockEventRepository.Object);
			var newEvent = new Event
			{
				EventId = 12,
				Title = "Unit Test Festival",
				Date = DateTime.Now,
				Address = "5 Apple Street, Banana village, Cranberry county",
				ShortDescription = "Brief overview of my unit test.",
				LongDescription = "This is a more extensive description of this Unit Test Festival. Lots of details to be included. You shouldn't let this pass you by... (please do)",
				ImageUrl = "#",
				ThumbnailUrl = "#"
			};
			// Act
			var result = adminController.CreateEvent(newEvent);
			// Assert
			Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal(12, mockEventRepository.Object.AllEvents.Count());
			Assert.Equal("Unit Test Festival", mockEventRepository.Object.GetEventById(12).Title);
		}
		
		[Fact]
		public void CreateEvent_InvalidEvent_ReturnsError()
		{
			// Arrange
			var mockEmailRequestRepository = RepositoryMocks.GetEmailRequestRepository();
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
			var result = adminController.CreateEvent(newEvent);
			// Assert
			Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal(11, mockEventRepository.Object.AllEvents.Count());
		}
		[Fact]
		public void EditEvent_ValidEvent_UpdatesEvent()
		{
			// Arrange
			var mockEmailRequestRepository = RepositoryMocks.GetEmailRequestRepository();
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
			var result = adminController.EditEvent(updatedEvent);
			// Assert
			Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal("Craft Workshop - Advanced", mockEventRepository.Object.GetEventById(11).Title);
		}
		[Fact]
		public void EditEvent_InvalidEvent_ReturnsError()
		{
			// Arrange
			var mockEmailRequestRepository = RepositoryMocks.GetEmailRequestRepository();
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
			adminController.ModelState.AddModelError("Title", "Title is required.");
			// Act
			var result = adminController.EditEvent(updatedEvent);
			// Assert
			Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal(11, mockEventRepository.Object.AllEvents.Count());
		}
	}
}
