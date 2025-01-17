using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LavidaCoffee.Models;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace LavidaCoffeeTests.Mocks
{
	internal class RepositoryMocks
	{
		public static Mock<IEmailRequestRepository> GetEmailRequestRepository()
		{
			var emailRequests = new List<EmailRequest>
			{
				new EmailRequest
				{
					EmailRequestId = 0,
					Email = new Email
					{
						CustomerEmail = "john.appleseed@gmail.com",
						Subject = "Event booking request",
						Body = "Hi there, How much does it cost to book you for an 8hr event?"
					}
				},
				new EmailRequest
				{
					EmailRequestId = 1,
					Email = new Email
					{
						CustomerEmail = "tina.turner@hotmail.com",
						Subject = "I love the house blend!",
						Body = "Hi there, What is the house blend you use called? I'd love to buy the same beans if possible?"
					}
				},
				new EmailRequest
				{
					EmailRequestId = 2,
					Email = new Email
					{
						CustomerEmail = "mac.miller@icloud.com",
						Subject = "Sponsorship request",
						Body = "Hey, Do you guys sponsor charity events?"
					}
				}
			};

			var mockEmailRequestRepository = new Mock<IEmailRequestRepository>();
			mockEmailRequestRepository.Setup(repo => repo.AllEmailRequests()).Returns(emailRequests.ToList);
			mockEmailRequestRepository.Setup(repo => repo.GetEmailRequestById(It.IsAny<int>())).Returns((int id) => emailRequests.FirstOrDefault(e => e.EmailRequestId == id));
			return mockEmailRequestRepository;
		}
	}
}
