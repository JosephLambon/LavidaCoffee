using LavidaCoffee.Models;
using LavidaCoffee.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LavidaCoffee.Controllers
{
    public class ContactController : Controller
    {
        private readonly IEmailRepository _emailRepository;

        public ContactController(IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EmailRequest(int id)
        {
            var emailRequest = await _emailRepository.GetEmailRequestByIdAsync(id);

            if (emailRequest == null)
            {
                return NotFound();
            }

            var emailRequestViewModel = new EmailViewModel
            {
                EmailRequest = emailRequest
            };

            return View(emailRequestViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(Email email)
        {            
            if (email == null || email.Subject == "")
            {
                ModelState.AddModelError("", "Your email has no subject. Please assign one first.");
            }

            // Valid form 
            if(ModelState.IsValid)
            {
                var emailRequest = new Email
                {
                    CustomerEmail = email.CustomerEmail,
                    ToEmail =   email.ToEmail,
                    Subject = email.Subject,
                    Body = email.Body,
                };
                await _emailRepository.CreateEmailRequestAsync(emailRequest);
                return RedirectToAction("RequestSent");
            }

            return RedirectToAction("Index");

        }

        public IActionResult RequestSent()
        {
            return View();
        }
    }
}
