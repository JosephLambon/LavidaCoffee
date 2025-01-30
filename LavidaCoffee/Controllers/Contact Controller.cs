using LavidaCoffee.Models;
using LavidaCoffee.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LavidaCoffee.Controllers
{
    public class ContactController : Controller
    {
        private readonly IEmailRequestRepository _emailRequestRepository;

        public ContactController(IEmailRequestRepository emailRequestRepository)
        {
            _emailRequestRepository = emailRequestRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult EmailRequest(int id)
        {
            var emailRequest = _emailRequestRepository.GetEmailRequestById(id);

            if (emailRequest == null)
            {
                return NotFound();
            }

            var emailRequestViewModel = new EmailRequestViewModel
            {
                EmailRequest = emailRequest
            };

            return View(emailRequestViewModel);
        }

        [HttpPost]
        public IActionResult SendRequest(Email email)
        {            
            if (email == null || email.Subject == "")
            {
                ModelState.AddModelError("", "Your email has no subject. Please assign one first.");
            }

            // Valid form 
            if (ModelState.IsValid)
            {
                var emailRequest = new EmailRequest { Email = email };
                _emailRequestRepository.CreateEmailRequest(emailRequest);
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
