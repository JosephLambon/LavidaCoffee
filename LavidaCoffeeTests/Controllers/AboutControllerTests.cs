using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LavidaCoffee.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace LavidaCoffeeTests.Controllers
{
	public class AboutControllerTests
	{
		[Fact]
		public void Index_ReturnsView()
		{
			// Arrange
			var aboutController = new HomeController();

			// Act
			var result = aboutController.Index();

			// Assert
			var viewResult = Assert.IsType<ViewResult>(result);
		}
	}
}
