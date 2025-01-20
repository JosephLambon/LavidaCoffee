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
				},
				new EmailRequest
				{
					EmailRequestId = 3,
					Email = new Email
					{
						CustomerEmail = "alice.wonderland@yahoo.com",
						Subject = "Catering services inquiry",
						Body = "Hello, Can you provide catering services for a wedding reception?"
					}
				},
				new EmailRequest
				{
					EmailRequestId = 4,
					Email = new Email
					{
						CustomerEmail = "bob.builder@gmail.com",
						Subject = "Construction project proposal",
						Body = "Hi, I would like to discuss a potential construction project with your team."
					}
				},
				new EmailRequest
				{
					EmailRequestId = 5,
					Email = new Email
					{
						CustomerEmail = "charlie.brown@outlook.com",
						Subject = "Event photography rates",
						Body = "Hello, What are your rates for event photography?"
					}
				},
				new EmailRequest
				{
					EmailRequestId = 6,
					Email = new Email
					{
						CustomerEmail = "diana.prince@icloud.com",
						Subject = "Fitness training sessions",
						Body = "Hi, Do you offer personal fitness training sessions?"
					}
				},
				new EmailRequest
				{
					EmailRequestId = 7,
					Email = new Email
					{
						CustomerEmail = "edward.scissorhands@hotmail.com",
						Subject = "Haircut appointment",
						Body = "Hello, Can I book a haircut appointment for next week?"
					}
				},
				new EmailRequest
				{
					EmailRequestId = 8,
					Email = new Email
					{
						CustomerEmail = "fiona.shrek@gmail.com",
						Subject = "Makeup artist availability",
						Body = "Hi, Are you available for a makeup session on the 15th?"
					}
				},
				new EmailRequest
				{
					EmailRequestId = 9,
					Email = new Email
					{
						CustomerEmail = "george.jetson@yahoo.com",
						Subject = "Tech support needed",
						Body = "Hello, I'm having issues with my computer. Can you help?"
					}
				},
				new EmailRequest
				{
					EmailRequestId = 10,
					Email = new Email
					{
						CustomerEmail = "harry.potter@hogwarts.com",
						Subject = "Magic show booking",
						Body = "Hi, How much do you charge for a magic show performance?"
					}
				},
				new EmailRequest
				{
					EmailRequestId = 11,
					Email = new Email
					{
						CustomerEmail = "isabella.swan@vampire.com",
						Subject = "Vampire-themed party",
						Body = "Hello, Do you offer services for themed parties?"
					}
				},
				new EmailRequest
				{
					EmailRequestId = 12,
					Email = new Email
					{
						CustomerEmail = "jack.sparrow@pirates.com",
						Subject = "Boat rental inquiry",
						Body = "Hi, How much does it cost to rent a boat for a day?"
					}
				},
				new EmailRequest
				{
					EmailRequestId = 13,
					Email = new Email
					{
						CustomerEmail = "katniss.everdeen@district12.com",
						Subject = "Archery lessons",
						Body = "Hello, Do you offer archery lessons for beginners?"
					}
				},
				new EmailRequest
				{
					EmailRequestId = 14,
					Email = new Email
					{
						CustomerEmail = "luke.skywalker@jedi.com",
						Subject = "Lightsaber training",
						Body = "Hi, Are you available for lightsaber training sessions?"
					}
				},
				new EmailRequest
				{
					EmailRequestId = 15,
					Email = new Email
					{
						CustomerEmail = "mary.poppins@umbrella.com",
						Subject = "Nanny services",
						Body = "Hello, Are you available for nanny services?"
					}
				},
				new EmailRequest
				{
					EmailRequestId = 16,
					Email = new Email
					{
						CustomerEmail = "neo.matrix@zion.com",
						Subject = "Virtual reality experience",
						Body = "Hi, Do you offer virtual reality experiences?"
					}
				},
				new EmailRequest
				{
					EmailRequestId = 17,
					Email = new Email
					{
						CustomerEmail = "oliver.twist@orphanage.com",
						Subject = "Charity donation request",
						Body = "Hello, Can you donate to our orphanage?"
					}
				},
				new EmailRequest
				{
					EmailRequestId = 18,
					Email = new Email
					{
						CustomerEmail = "peter.parker@dailybugle.com",
						Subject = "Photography job application",
						Body = "Hi, I'm interested in applying for a photography job."
					}
				},
				new EmailRequest
				{
					EmailRequestId = 19,
					Email = new Email
					{
						CustomerEmail = "quentin.quirrell@hogwarts.com",
						Subject = "Defense Against the Dark Arts",
						Body = "Hello, Do you offer lessons in Defense Against the Dark Arts?"
					}
				},
				new EmailRequest
				{
					EmailRequestId = 20,
					Email = new Email
					{
						CustomerEmail = "ron.weasley@hogwarts.com",
						Subject = "Wizard chess tournament",
						Body = "Hi, Are you hosting any wizard chess tournaments soon?"
					}
				},
				new EmailRequest
				{
					EmailRequestId = 21,
					Email = new Email
					{
						CustomerEmail = "sherlock.holmes@bakerstreet.com",
						Subject = "Detective services",
						Body = "Hello, Can you help solve a mystery?"
					}
				},
				new EmailRequest
				{
					EmailRequestId = 22,
					Email = new Email
					{
						CustomerEmail = "tony.stark@starkindustries.com",
						Subject = "Tech innovation partnership",
						Body = "Hi, Are you interested in a tech innovation partnership?"
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
