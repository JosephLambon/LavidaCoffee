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
			var mockEmailRequestRepository = RepositoryMocks.GetEmailRequestRepository();
			var contactController = new ContactController(mockEmailRequestRepository.Object);

			// Act
			var result = contactController.Index();

			// Assert
			var viewResult = Assert.IsType<ViewResult>(result);
		}
		[Fact]
		public void EmailRequest_ValidId_Returns_CorrectViewModel()
		{
			// Arrange
			var mockEmailRequestRepository = RepositoryMocks.GetEmailRequestRepository();
			var contactController = new ContactController(mockEmailRequestRepository.Object);
			var emailRequests = mockEmailRequestRepository.Object.AllEmailRequests();

			// Act
			var result = contactController.EmailRequest(1);

			// Assert
			var viewResult = Assert.IsType<ViewResult>(result);
			var model = Assert.IsType<EmailViewModel>(viewResult.Model);
			Assert.Equal(emailRequests[1], model.EmailRequest);

		}
		[Fact]
		public void EmailRequest_InvalidId_Returns_NotFound()
		{
			// Arrange
			var mockEmailRequestRepository = RepositoryMocks.GetEmailRequestRepository();
			var contactController = new ContactController(mockEmailRequestRepository.Object);
			var emailRequests = mockEmailRequestRepository.Object.AllEmailRequests();

			// Act
			var result = contactController.EmailRequest(-1);

			// Assert
			var viewResult = Assert.IsType<NotFoundResult>(result);
		}
		[Fact]
		public void SendRequest_NullEmail_ReturnsModelError()
		{
			// Arrange
			var mockEmailRequestRepository = RepositoryMocks.GetEmailRequestRepository();
			var contactController = new ContactController(mockEmailRequestRepository.Object);

			// Act
			var result = contactController.SendEmail(null);

			// Assert
			var viewResult = Assert.IsType<RedirectToActionResult>(result);
			Assert.False(contactController.ModelState.IsValid);
			Assert.True(contactController.ModelState.ContainsKey(""));
			var modelState = contactController.ModelState[""];
			Assert.Equal("Your email has no subject. Please assign one first.", Assert.Single(modelState.Errors).ErrorMessage);
		}
		[Fact]
		public void SendRequest_ValidEmail_RedirectsToRequestSent()
		{
			// Arrange
			var mockEmailRequestRepository = RepositoryMocks.GetEmailRequestRepository();
			var contactController = new ContactController(mockEmailRequestRepository.Object);

			// Act
			var result = contactController.SendEmail(mockEmailRequestRepository.Object.GetEmailRequestById(1).Email);

			// Assert
			var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal("RequestSent", redirectToActionResult.ActionName);
		}
		[Fact]
		public void RequestSent_ReturnsView()
		{
			// Arrange
			var mockEmailRequestRepository = RepositoryMocks.GetEmailRequestRepository();
			var contactController = new ContactController(mockEmailRequestRepository.Object);

			// Act
			var result = contactController.RequestSent();

			// Assert
			var viewResult = Assert.IsType<ViewResult>(result);
		}
	}
}
