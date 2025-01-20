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

public class AdminControllerApiTests
    {
	[Fact]
    public async Task Get_ReturnsOk()
    {
        // Arrange
        var mockEmailRequestRepository = RepositoryMocks.GetEmailRequestRepository();
        var controller = new AdminController(mockEmailRequestRepository.Object);
        // Act
        var result = controller.Get(1) as OkObjectResult;
        var emailRequests = result.Value as List<EmailRequest>;
        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(10, emailRequests.Count);
        Assert.Equal(0, emailRequests.First().EmailRequestId);
		Assert.Equal(9, emailRequests.Last().EmailRequestId);
	}

    //[Fact]
    //public async Task Index_ReturnsFail_ForNonAdmin()
    //{
    //    // Arrange
            
    //    // Act
            
    //    // Assert
            
    //}
}
