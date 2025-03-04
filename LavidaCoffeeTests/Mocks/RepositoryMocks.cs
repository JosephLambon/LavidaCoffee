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
using Org.BouncyCastle.Bcpg;

namespace LavidaCoffeeTests.Mocks
{
	internal class RepositoryMocks
	{
		public static Mock<IEmailRepository> GetEmailRepository()
		{
			var emails = new List<Email>
			{
				new Email
				{
					EmailId = 0,
					CustomerEmail = "john.appleseed@gmail.com",
					Subject = "Event booking request",
					Body = "Hi there, How much does it cost to book you for an 8hr event?"
				},
				new Email
				{
					EmailId = 1,
					CustomerEmail = "tina.turner@hotmail.com",
					Subject = "I love the house blend!",
					Body = "Hi there, What is the house blend you use called? I'd love to buy the same beans if possible?"
				},
				new Email
				{
					EmailId = 2,
					CustomerEmail = "mac.miller@icloud.com",
					Subject = "Sponsorship request",
					Body = "Hey, Do you guys sponsor charity events?"
				},
				new Email
				{
					EmailId = 3,
					CustomerEmail = "alice.wonderland@yahoo.com",
					Subject = "Catering services inquiry",
					Body = "Hello, Can you provide catering services for a wedding reception?"
				},
				new Email
				{
					EmailId = 4,
					CustomerEmail = "bob.builder@gmail.com",
					Subject = "Construction project proposal",
					Body = "Hi, I would like to discuss a potential construction project with your team."
				},
				new Email
				{
					EmailId = 5,
					CustomerEmail = "charlie.brown@outlook.com",
					Subject = "Event photography rates",
					Body = "Hello, What are your rates for event photography?"
				},
				new Email
				{
					EmailId = 6,
					CustomerEmail = "diana.prince@icloud.com",
					Subject = "Fitness training sessions",
					Body = "Hi, Do you offer personal fitness training sessions?"
				},
				new Email
				{
					EmailId = 7,
					CustomerEmail = "edward.scissorhands@hotmail.com",
					Subject = "Haircut appointment",
					Body = "Hello, Can I book a haircut appointment for next week?"
				},
				new Email
				{
					EmailId = 8,
					CustomerEmail = "fiona.shrek@gmail.com",
					Subject = "Makeup artist availability",
					Body = "Hi, Are you available for a makeup session on the 15th?"
				},
				new Email
				{
					EmailId = 9,
					CustomerEmail = "george.jetson@yahoo.com",
					Subject = "Tech support needed",
					Body = "Hello, I'm having issues with my computer. Can you help?"
				},
				new Email
				{
					EmailId = 10,
					CustomerEmail = "harry.potter@hogwarts.com",
					Subject = "Magic show booking",
					Body = "Hi, How much do you charge for a magic show performance?"
				},
				new Email
				{
					EmailId = 11,
					CustomerEmail = "isabella.swan@vampire.com",
					Subject = "Vampire-themed party",
					Body = "Hello, Do you offer services for themed parties?"
				},
				new Email
				{
					EmailId = 12,
					CustomerEmail = "jack.sparrow@pirates.com",
					Subject = "Boat rental inquiry",
					Body = "Hi, How much does it cost to rent a boat for a day?"
				},
				new Email
				{
					EmailId = 13,
					CustomerEmail = "katniss.everdeen@district12.com",
					Subject = "Archery lessons",
					Body = "Hello, Do you offer archery lessons for beginners?"
				},
				new Email
				{
					EmailId = 14,
					CustomerEmail = "luke.skywalker@jedi.com",
					Subject = "Lightsaber training",
					Body = "Hi, Are you available for lightsaber training sessions?"
				},
				new Email
				{
					EmailId = 15,
					CustomerEmail = "mary.poppins@umbrella.com",
					Subject = "Nanny services",
					Body = "Hello, Are you available for nanny services?"
				},
				new Email
				{
					EmailId = 16,
					CustomerEmail = "neo.matrix@zion.com",
					Subject = "Virtual reality experience",
					Body = "Hi, Do you offer virtual reality experiences?"
				},
				new Email
				{
					EmailId = 17,
					CustomerEmail = "oliver.twist@orphanage.com",
					Subject = "Charity donation request",
					Body = "Hello, Can you donate to our orphanage?"
				},
				new Email
				{
					EmailId = 18,
					CustomerEmail = "peter.parker@dailybugle.com",
					Subject = "Photography job application",
					Body = "Hi, I'm interested in applying for a photography job."
				},
				new Email
				{
					EmailId = 19,
					CustomerEmail = "quentin.quirrell@hogwarts.com",
					Subject = "Defense Against the Dark Arts",
					Body = "Hello, Do you offer lessons in Defense Against the Dark Arts?"
				},
				new Email
				{
					EmailId = 20,
					CustomerEmail = "ron.weasley@hogwarts.com",
					Subject = "Wizard chess tournament",
					Body = "Hi, Are you hosting any wizard chess tournaments soon?"
				},
				new Email
				{
					EmailId = 21,
					CustomerEmail = "sherlock.holmes@bakerstreet.com",
					Subject = "Detective services", 
					Body = "Hello, Can you help solve a mystery?"
				},
				new Email
				{
					EmailId = 22,
					CustomerEmail = "tony.stark@starkindustries.com",
					Subject = "Tech innovation partnership",
					Body = "Hi, Are you interested in a tech innovation partnership?"
				}
			};

			var mockEmailRepository = new Mock<IEmailRepository>();
			mockEmailRepository.Setup(repo => repo.GetAllEmailsAsync()).ReturnsAsync(emails.ToList());
			mockEmailRepository.Setup(repo => repo.GetEmailByIdAsync(It.IsAny<int>())).ReturnsAsync((int id) => emails.FirstOrDefault(e => e.EmailId == id));
			mockEmailRepository.Setup(repo => repo.CreateEmailAsync(It.IsAny<Email>())).Callback<Email>(e=> emails.Add(e));
			mockEmailRepository.Setup(repo => repo.GetAllEmailsCountAsync()).ReturnsAsync(emails.Count);
			mockEmailRepository.Setup(repo => repo.GetEmailsPagedAsync(It.IsAny<int>(), It.IsAny<int>()))
				.ReturnsAsync((int? pageNumber, int pageSize) =>
				{
					pageNumber ??= 1;
					return emails.Skip((pageNumber.Value - 1) * pageSize).Take(pageSize);
				});
			return mockEmailRepository;
		}

		public static Mock<IEventRepository> GetEventRepository()
		{
			var events = new List<Event>
			{
				new Event
				{
					EventId = 1,
					Title = "Cookielicious",
					Date = DateTime.Now.AddDays(-7),
					Address = "1 Cookie Lane, Sugar County",
					ShortDescription = "Join us in trying delicious fresh cookies!",
					LongDescription = "We'll be joining Cookielicious for their bake sale day, taste testing all the cookies in their new range. Come with us!",
					ImageUrl = "https://images.pexels.com/photos/29587987/pexels-photo-29587987/free-photo-of-vibrant-nightlife-in-tokyo-s-shinjuku-district.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
					ThumbnailUrl = "https://images.pexels.com/photos/29587987/pexels-photo-29587987/free-photo-of-vibrant-nightlife-in-tokyo-s-shinjuku-district.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"

				},
				new Event
				{
					EventId = 2,
					Title = "Art in the Park",
					Date = DateTime.Now.AddDays(7),
					Address = "Central Park, Art District",
					ShortDescription = "An outdoor art exhibition.",
					LongDescription = "Join us for a day of art in the park, featuring local artists and their latest creations.",
					ImageUrl = "https://images.pexels.com/photos/102127/pexels-photo-102127.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
					ThumbnailUrl = "https://images.pexels.com/photos/102127/pexels-photo-102127.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
				},
				new Event
				{
					EventId = 3,
					Title = "Tech Innovators Meetup",
					Date = DateTime.Now.AddDays(14),
					Address = "Tech Hub, Silicon Valley",
					ShortDescription = "A meetup for tech enthusiasts.",
					LongDescription = "Network with fellow tech innovators and learn about the latest trends in technology.",
					ImageUrl = "https://images.pexels.com/photos/3183197/pexels-photo-3183197.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
					ThumbnailUrl = "https://images.pexels.com/photos/3183197/pexels-photo-3183197.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
				},
				new Event
				{
					EventId = 4,
					Title = "Yoga in the Park",
					Date = DateTime.Now.AddDays(21),
					Address = "Green Meadows Park",
					ShortDescription = "A relaxing yoga session.",
					LongDescription = "Join us for a morning of yoga in the park, suitable for all levels.",
					ImageUrl = "https://images.pexels.com/photos/3822622/pexels-photo-3822622.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
					ThumbnailUrl = "https://images.pexels.com/photos/3822622/pexels-photo-3822622.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
				},
				new Event
				{
					EventId = 5,
					Title = "Food Truck Fiesta",
					Date = DateTime.Now.AddDays(28),
					Address = "Downtown Plaza",
					ShortDescription = "A gathering of food trucks.",
					LongDescription = "Sample a variety of cuisines from the best food trucks in the city.",
					ImageUrl = "https://images.pexels.com/photos/1231265/pexels-photo-1231265.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
					ThumbnailUrl = "https://images.pexels.com/photos/1231265/pexels-photo-1231265.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
				},
				new Event
				{
					EventId = 6,
					Title = "Book Club Meeting",
					Date = DateTime.Now.AddDays(35),
					Address = "City Library, Room 101",
					ShortDescription = "A monthly book club meeting.",
					LongDescription = "Discuss the book of the month with fellow book enthusiasts.",
					ImageUrl = "https://images.pexels.com/photos/256541/pexels-photo-256541.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
					ThumbnailUrl = "https://images.pexels.com/photos/256541/pexels-photo-256541.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
				},
				new Event
				{
					EventId = 7,
					Title = "Charity Fun Run",
					Date = DateTime.Now.AddDays(42),
					Address = "Riverside Park",
					ShortDescription = "A 5K charity run.",
					LongDescription = "Participate in a 5K run to raise funds for a local charity.",
					ImageUrl = "https://images.pexels.com/photos/1199590/pexels-photo-1199590.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
					ThumbnailUrl = "https://images.pexels.com/photos/1199590/pexels-photo-1199590.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
				},
				new Event
				{
					EventId = 8,
					Title = "Music Festival",
					Date = DateTime.Now.AddDays(49),
					Address = "Open Air Theater",
					ShortDescription = "A weekend of live music.",
					LongDescription = "Enjoy performances from various bands and solo artists over the weekend.",
					ImageUrl = "https://images.pexels.com/photos/167636/pexels-photo-167636.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
					ThumbnailUrl = "https://images.pexels.com/photos/167636/pexels-photo-167636.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
				},
				new Event
				{
					EventId = 9,
					Title = "Farmers Market",
					Date = DateTime.Now.AddDays(56),
					Address = "Market Square",
					ShortDescription = "A weekly farmers market.",
					LongDescription = "Shop for fresh produce and handmade goods from local farmers and artisans.",
					ImageUrl = "https://images.pexels.com/photos/1508666/pexels-photo-1508666.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
					ThumbnailUrl = "https://images.pexels.com/photos/1508666/pexels-photo-1508666.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
				},
				new Event
				{
					EventId = 10,
					Title = "Science Fair",
					Date = DateTime.Now.AddDays(63),
					Address = "Community Center",
					ShortDescription = "A local science fair.",
					LongDescription = "Explore science projects and experiments presented by students and enthusiasts.",
					ImageUrl = "https://images.pexels.com/photos/256369/pexels-photo-256369.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
					ThumbnailUrl = "https://images.pexels.com/photos/256369/pexels-photo-256369.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
				},
				new Event
				{
					EventId = 11,
					Title = "Craft Workshop",
					Date = DateTime.Now.AddDays(70),
					Address = "Art Studio, Main Street",
					ShortDescription = "A hands-on craft workshop.",
					LongDescription = "Learn new crafting techniques and create your own handmade items.",
					ImageUrl = "https://images.pexels.com/photos/1109541/pexels-photo-1109541.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
					ThumbnailUrl = "https://images.pexels.com/photos/1109541/pexels-photo-1109541.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
				}
			};

			var mockEventRepository = new Mock<IEventRepository>();
			mockEventRepository.Setup(repo => repo.GetAllEventsAsync()).ReturnsAsync(events.OrderBy(e=>e.EventId).ToList());
			mockEventRepository.Setup(repo => repo.GetUpcomingEventsAsync()).ReturnsAsync(() =>
			{
				return events.Where(e => e.Date > DateTime.Today).OrderBy(e=> e.Date);
			}				
				);
			mockEventRepository.Setup(repo => repo.GetEventByIdAsync(It.IsAny<int>())).ReturnsAsync((int id) => events.FirstOrDefault(e => e.EventId == id));

			mockEventRepository.Setup(repo => repo.DeleteEventAsync(It.IsAny<Event>())).Callback((Event selectedEvent) => events.Remove(selectedEvent)).Returns(Task.CompletedTask);

			mockEventRepository.Setup(repo => repo.CreateEventAsync(It.IsAny<Event>())).Callback((Event newEvent) => events.Add(newEvent)).Returns(Task.CompletedTask);
			mockEventRepository.Setup(repo => repo.UpdateEventAsync(It.IsAny<Event>())).Callback<Event>(e =>
			{
				var existingEventIndex = events.FindIndex(ev => ev.EventId == e.EventId);
				events[existingEventIndex] = e;
			});
			mockEventRepository.Setup(repo => repo.GetAllEventsCountAsync()).ReturnsAsync(() => events.Count);
			mockEventRepository.Setup(repo => repo.GetEventsPagedAsync(It.IsAny<int>(), It.IsAny<int>()))
				.ReturnsAsync((int? pageNumber, int pageSize) =>
				{
					pageNumber ??= 1;
					return events.Skip((pageNumber.Value - 1) * pageSize).Take(pageSize);
				});
			mockEventRepository.Setup(repo => repo.GetEventsPagedAndSortedAsync(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int>()))
				.ReturnsAsync((string sortBy, int? pageNumber, int pageSize) =>
				{
					IQueryable<Event> sortedEvents = events.AsQueryable();

					switch (sortBy)
					{
						case "eventid_descending":
							sortedEvents = sortedEvents.OrderByDescending(e => e.EventId);
							break;
						case "eventid":
							sortedEvents = sortedEvents.OrderBy(e => e.EventId);
							break;
						case "title_descending":
							sortedEvents = sortedEvents.OrderByDescending(e => e.Title);
							break;
						case "title":
							sortedEvents = sortedEvents.OrderBy(e => e.Title);
							break;
						case "date_descending":
							sortedEvents = sortedEvents.OrderByDescending(e => e.Date);
							break;
						case "date":
							sortedEvents = sortedEvents.OrderBy(e => e.Date);
							break;
						default:
							sortedEvents = sortedEvents.OrderBy(e => e.EventId);
							break;
					}

					pageNumber ??= 1;
					var pagedEvents = sortedEvents.Skip((pageNumber.Value - 1) * pageSize).Take(pageSize);

					return pagedEvents.ToList();
				});

		return mockEventRepository;
		}
	}
}
