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
		public async Task IndexPaging_ValidPage_ReturnsJson()
		{
			// Arrange
			var mockEmailRequestRepository = RepositoryMocks.GetEmailRepository();
			var controller = new EmailController(mockEmailRequestRepository.Object);

			// Act
			var result = await controller.IndexPaging(1) as JsonResult;
			var pagedEmails= result?.Value as IEnumerable<Email>;

			// Assert
			Assert.NotNull(result);
			Assert.IsType<JsonResult>(result);
			Assert.NotEmpty(pagedEmails);
			Assert.Equal(10, pagedEmails.Count());
			Assert.Equal(0, pagedEmails.First().EmailId);
		}
		[Fact]
		public async Task IndexPaging_InvalidPage_ReturnsEmpty()
		{
			// Arrange
			var emailRepository = RepositoryMocks.GetEmailRepository();
			var emailController = new EmailController(emailRepository.Object);
			// Act
			var result = await emailController.IndexPaging(10000) as JsonResult;
			var pagedEmails = result?.Value as IEnumerable<Email>;
			// Assert
			Assert.Empty(pagedEmails);
		}
		[Fact]
		public async Task IndexPagingSorting_ValidPageEmailIdDesc_ReturnsEmailIdDescPaged()
		{
			// Arrange
			var emailRepository = RepositoryMocks.GetEmailRepository();
			var emailController = new EmailController(emailRepository.Object);
			// Act
			var result = await emailController.IndexPagingSorting(1, "emailid_descending") as JsonResult;
			var pagedEmails = result?.Value as IEnumerable<Email>;
			// Assert
			Assert.Equal(10, pagedEmails.Count());
			Assert.Equal(22, pagedEmails.First().EmailId);
		}
		[Fact]
		public async Task IndexPagingSorting_ValidPageCustomerEmailDesc_ReturnsCustomerEmailDescPaged()
		{
			// Arrange
			var emailRepository = RepositoryMocks.GetEmailRepository();
			var emailController = new EmailController(emailRepository.Object);
			// Act
			var result = await emailController.IndexPagingSorting(1, "customeremail_descending") as JsonResult;
			var pagedEmails = result?.Value as IEnumerable<Email>;
			// Assert
			Assert.Equal(10, pagedEmails.Count());
			Assert.Equal("tony.stark@starkindustries.com", pagedEmails.First().CustomerEmail);
		}
		[Fact]
		public async Task IndexPagingSorting_ValidPageSubjectDesc_ReturnsSubjectDescPaged()
		{
			// Arrange
			var emailRepository = RepositoryMocks.GetEmailRepository();
			var emailController = new EmailController(emailRepository.Object);
			// Act
			var result = await emailController.IndexPagingSorting(1, "subject_descending") as JsonResult;
			var pagedEmails = result?.Value as IEnumerable<Email>;
			// Assert
			Assert.Equal(10, pagedEmails.Count());
			Assert.Equal("Wizard chess tournament", pagedEmails.First().Subject);
		}
	}
}
	
