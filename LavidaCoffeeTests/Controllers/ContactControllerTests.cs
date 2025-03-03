using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LavidaCoffee.Controllers;
using LavidaCoffee.Models;
using LavidaCoffee.ViewModels;
using LavidaCoffeeTests.Mocks;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.BC;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Pqc.Crypto.Lms;

namespace LavidaCoffeeTests.Controllers
{
	public class ContactControllerTests
	{
		[Fact]
		public void Index_ReturnsView()
		{
			// Arrange
			var mockEmailRepository = RepositoryMocks.GetEmailRepository();
			var contactController = new ContactController(mockEmailRepository.Object);

			// Act
			var result = contactController.Index();

			// Assert
			var viewResult = Assert.IsType<ViewResult>(result);
		}
		[Fact]
		public async Task EmailRequest_ValidId_Returns_CorrectViewModel()
		{
			// Arrange
			var mockEmailRepository = RepositoryMocks.GetEmailRepository();
			var contactController = new ContactController(mockEmailRepository.Object);
			var emailRequests = await mockEmailRepository.Object.GetAllEmailsAsync();

			// Act
			var result = await contactController.EmailRequest(1);

			// Assert
			var viewResult = Assert.IsType<ViewResult>(result);
			var model = Assert.IsType<EmailViewModel>(viewResult.Model);
			Assert.Equal(emailRequests.ElementAt(1), model.EmailRequest);
		}
		[Fact]
		public async Task EmailRequest_InvalidId_Returns_NotFound()
		{
			// Arrange
			var mockEmailRepository = RepositoryMocks.GetEmailRepository();
			var contactController = new ContactController(mockEmailRepository.Object);
			var emailRequests = await mockEmailRepository.Object.GetAllEmailsAsync();

			// Act
			var result = await contactController.EmailRequest(-1);

			// Assert
			var viewResult = Assert.IsType<NotFoundResult>(result);
		}
		[Fact]
		public async Task SendRequest_NullEmail_ReturnsModelError()
		{
			// Arrange
			var mockEmailRepository = RepositoryMocks.GetEmailRepository();
			var contactController = new ContactController(mockEmailRepository.Object);

			// Act
			var result = await contactController.SendEmail(null);

			// Assert
			var viewResult = Assert.IsType<RedirectToActionResult>(result);
			Assert.False(contactController.ModelState.IsValid);
			Assert.True(contactController.ModelState.ContainsKey(""));
			var modelState = contactController.ModelState[""];
			Assert.Equal("Your email has no subject. Please assign one first.", Assert.Single(modelState.Errors).ErrorMessage);
		}
		[Fact]
		public async Task SendRequest_ValidEmail_RedirectsToRequestSent()
		{
			// Arrange
			var mockEmailRepository = RepositoryMocks.GetEmailRepository();
			var contactController = new ContactController(mockEmailRepository.Object);

			// Act
			var result = await contactController.SendEmail(await mockEmailRepository.Object.GetEmailByIdAsync(1));

			// Assert
			var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal("RequestSent", redirectToActionResult.ActionName);
		}
		[Fact]
		public void RequestSent_ReturnsView()
		{
			// Arrange
			var mockEmailRepository = RepositoryMocks.GetEmailRepository();
			var contactController = new ContactController(mockEmailRepository.Object);

			// Act
			var result = contactController.RequestSent();

			// Assert
			var viewResult = Assert.IsType<ViewResult>(result);
		}
	}
}
