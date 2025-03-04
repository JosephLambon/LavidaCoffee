using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LavidaCoffee.Models;
using LavidaCoffeeTests.Mocks;
using LavidaCoffee.Controllers.Api;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace LavidaCoffeeTests.Apis
{
	public class EventControllerTests
	{
		[Fact]
		public async Task AllEvents_ReturnsOk()
		{
			// Arrange
			var eventRepository = RepositoryMocks.GetEventRepository();
			var eventController = new EventController(eventRepository.Object);
			// Act
			var result = await eventController.AllEvents() as OkObjectResult;
			var eventsReturned = result?.Value as IEnumerable<Event>;
			// Assert
			Assert.NotNull(result);
			Assert.Equal(11, eventsReturned.Count());
			Assert.Equal(1, eventsReturned.First().EventId);
			Assert.Equal(11, eventsReturned.Last().EventId);
		}

		[Fact]
		public async Task UpcomingEvents_ReturnsUpcoming()
		{
			// Arrange
			var eventRepository = RepositoryMocks.GetEventRepository();
			var eventController = new EventController(eventRepository.Object);
			// Act
			var result = await eventController.UpcomingEvents() as JsonResult;
			var eventsReturned = result?.Value as IEnumerable<Event>;
			// Assert
			Assert.NotNull(result);
			Assert.Equal(10, eventsReturned.Count());
			Assert.Equal(2, eventsReturned.First().EventId);
			Assert.All(eventsReturned, e => Assert.True(e.Date > DateTime.Now));
		}
		[Fact]
		public async Task IndexPaging_ValidPage_ReturnsPaged()
		{
			// Arrange
			var eventRepository = RepositoryMocks.GetEventRepository();
			var eventController = new EventController(eventRepository.Object);
			// Act
			var result = await eventController.IndexPaging(1) as JsonResult;
			var pagedEvents = result?.Value as IEnumerable<Event>;
			// Assert
			Assert.NotEmpty(pagedEvents);
			Assert.Equal(10, pagedEvents.Count());
			Assert.Equal(1, pagedEvents.First().EventId);
		}
		[Fact]
		public async Task IndexPaging_InvalidPage_ReturnsEmpty()
		{
			// Arrange
			var eventRepository = RepositoryMocks.GetEventRepository();
			var eventController = new EventController(eventRepository.Object);
			// Act
			var result = await eventController.IndexPaging(10000) as JsonResult;
			var pagedEvents = result?.Value as IEnumerable<Event>;
			// Assert
			Assert.Empty(pagedEvents);
		}
		[Fact]
		public async Task IndexPagingSorting_ValidPageDateDesc_ReturnsDateDescPaged()
		{
			// Arrange
			var eventRepository = RepositoryMocks.GetEventRepository();
			var eventController = new EventController(eventRepository.Object);
			var expectedDate = DateTime.Now.AddDays(70);
			// Act
			var result = await eventController.IndexPagingSorting(1, "date_descending") as JsonResult;
			var pagedEvents = result?.Value as IEnumerable<Event>;
			var actualDate = pagedEvents.First().Date;
			// Assert
			Assert.NotEmpty(pagedEvents);
			Assert.Equal(10, pagedEvents.Count());
			Assert.Equal(expectedDate.Date, actualDate.Date);
			Assert.Equal(expectedDate.Hour, actualDate.Hour);
			Assert.Equal(expectedDate.Minute, actualDate.Minute);
			Assert.Equal(expectedDate.Second, actualDate.Second);
		}
		[Fact]
		public async Task IndexPagingSorting_ValidPageEventIdDesc_ReturnsEventIdDescPaged()
		{
			// Arrange
			var eventRepository = RepositoryMocks.GetEventRepository();
			var eventController = new EventController(eventRepository.Object);
			// Act
			var result = await eventController.IndexPagingSorting(1, "eventid_descending") as JsonResult;
			var pagedEvents = result?.Value as IEnumerable<Event>;
			// Assert
			Assert.NotEmpty(pagedEvents);
			Assert.Equal(10, pagedEvents.Count());
			Assert.Equal(11, pagedEvents.First().EventId);
		}
		[Fact]
		public async Task IndexPagingSorting_ValidPageTitleDesc_ReturnsTitleDescPaged()
		{
			// Arrange
			var eventRepository = RepositoryMocks.GetEventRepository();
			var eventController = new EventController(eventRepository.Object);
			// Act
			var result = await eventController.IndexPagingSorting(1, "title_descending") as JsonResult;
			var pagedEvents = result?.Value as IEnumerable<Event>;
			// Assert
			Assert.NotEmpty(pagedEvents);
			Assert.Equal(10, pagedEvents.Count());
			Assert.Equal("Yoga in the Park", pagedEvents.First().Title);
		}
	}
}
