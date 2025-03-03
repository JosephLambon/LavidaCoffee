using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LavidaCoffee;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using LavidaCoffee.Controllers.Api;
using LavidaCoffee.Models;
using LavidaCoffeeTests.Mocks;
using Microsoft.AspNetCore.Mvc;
using Xunit.Abstractions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using Microsoft.AspNetCore.Authorization;

namespace LavidaCoffeeTests.Apis
{
	public class EmailControllerTests
	{
		[Fact]
		public async Task Get_ReturnsOk()
		{
			// Arrange
			var mockEmailRequestRepository = RepositoryMocks.GetEmailRepository();
			var controller = new EmailController(mockEmailRequestRepository.Object);

			// Act
			var result = await controller.Get(1) as OkObjectResult;
			var emails = result.Value as List<Email>;
			// Assert
			Assert.NotNull(result);
			Assert.Equal(200, result.StatusCode);
			Assert.Equal(10, emails.Count);
			Assert.Equal(0, emails.First().EmailId);
			Assert.Equal(9, emails.Last().EmailId);
		}

		[Fact]
		public async Task requestForCurrentPage_ReturnsJson()
		{
			// Arrange
			var mockEmailRequestRepository = RepositoryMocks.GetEmailRepository();
			var controller = new EmailController(mockEmailRequestRepository.Object);

			// Act
			var result = await controller.IndexPaging(1) as JsonResult;

			// Assert
			Assert.NotNull(result);
			Assert.IsType<JsonResult>(result);
		}
	}
}
	
