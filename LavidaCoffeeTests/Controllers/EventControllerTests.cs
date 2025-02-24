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
		public void AllEvents_ReturnsOk()
		{
			// Arrange
			var eventRepository = RepositoryMocks.GetEventRepository();
			var eventController = new EventController(eventRepository.Object);
			// Act
			var result = eventController.AllEvents() as OkObjectResult;
			var eventsReturned = result?.Value as IEnumerable<Event>;
			// Assert
			Assert.NotNull(result);
			Assert.Equal(11, eventsReturned.Count());
			Assert.Equal(1, eventsReturned.First().EventId);
			Assert.Equal(11, eventsReturned.Last().EventId);
		}

		[Fact]
		public void UpcomingEvents_ReturnsUpcoming()
		{
			// Arrange
			var eventRepository = RepositoryMocks.GetEventRepository();
			var eventController = new EventController(eventRepository.Object);
			// Act
			var result = eventController.UpcomingEvents() as JsonResult;
			var eventsReturned = result?.Value as IEnumerable<Event>;
			// Assert
			Assert.NotNull(result);
			Assert.Equal(10, eventsReturned.Count());
			Assert.Equal(2, eventsReturned.First().EventId);
			Assert.All(eventsReturned, e => Assert.True(e.Date > DateTime.Now));
		}
	}
}
