using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LavidaCoffee.Controllers;
using LavidaCoffee.Models;
using LavidaCoffeeTests.Mocks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LavidaCoffeeTests.Controllers
{
	public class Find_UsControllerTests
	{
		[Fact]
		public void Index_ReturnsView()
		{
			// Arrange
			var mockEventRepository = RepositoryMocks.GetEventRepository();
			var find_usController = new Find_UsController(mockEventRepository.Object);
			// Act
			var result = find_usController.Index();
			// Assert
			Assert.IsType<ViewResult>(result);
		}

		[Fact]
		public void EventDetails_ValidId_ReturnsView()
		{
			// Arrange
			var mockEventRepository = RepositoryMocks.GetEventRepository();
			var find_usController = new Find_UsController(mockEventRepository.Object);
			// Act
			var result = find_usController.EventDetails(1);
			// Assert
			var viewResult = Assert.IsType<ViewResult>(result);
			var model = Assert.IsType<Event>(viewResult.Model);
			Assert.Equal(1, model.EventId);
		}

		[Fact]
		public void EventDetails_InvalidId_ReturnsNotFound()
		{
			// Arrange
			// Act
			// Assert
		}
	}
}
