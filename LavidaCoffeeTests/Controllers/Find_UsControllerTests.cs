using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LavidaCoffee.Controllers;
using LavidaCoffee.ViewModels;
using LavidaCoffeeTests.Mocks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LavidaCoffeeTests.Controllers
{
	public class Find_UsControllerTests
	{
		[Fact]
		public async Task Index_ReturnsView()
		{
			// Arrange
			var mockEventRepository = RepositoryMocks.GetEventRepository();
			var find_usController = new Find_UsController(mockEventRepository.Object);
			// Act
			var result = await find_usController.Index();
			// Assert
			Assert.IsType<ViewResult>(result);
		}

		[Fact]
		public async Task EventDetails_ValidId_ReturnsView()
		{
			// Arrange
			var mockEventRepository = RepositoryMocks.GetEventRepository();
			var find_usController = new Find_UsController(mockEventRepository.Object);
			// Act
			var result = await find_usController.EventDetails(1);
			// Assert
			var viewResult = Assert.IsType<ViewResult>(result);
			var model = Assert.IsType<Find_UsEventDetailsViewModel>(viewResult.Model);
			Assert.Equal(1, model.Event.EventId);
		}

		[Fact]
		public async Task EventDetails_InvalidId_ReturnsNotFound()
		{
			// Arrange
			var mockEventRepository = RepositoryMocks.GetEventRepository();
			var find_usController = new Find_UsController(mockEventRepository.Object);
			// Act
			var result = await find_usController.EventDetails(2147483647);
			// Assert
			Assert.IsType<NotFoundResult>(result);
		}
	}
}
