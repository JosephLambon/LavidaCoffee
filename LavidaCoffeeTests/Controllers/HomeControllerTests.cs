using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LavidaCoffee.Controllers;
using LavidaCoffeeTests.Mocks;
using Microsoft.AspNetCore.Mvc;

namespace LavidaCoffeeTests.Controllers
{
	public class HomeControllerTests
	{
		[Fact]
		public void Index_ReturnsView()
		{
			// Arrange
			var homeController = new HomeController();

			// Act
			var result = homeController.Index();

			// Assert
			var viewResult = Assert.IsType<ViewResult>(result);
		}
	}
}
