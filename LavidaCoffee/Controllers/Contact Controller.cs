using LavidaCoffee.Models;
using Microsoft.AspNetCore.Mvc;

namespace LavidaCoffee.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendRequest(EmailRequest emailRequest)
        {

            return RedirectToAction("RequestSent");
        }

        public IActionResult RequestSent()
        {
            ViewBag.RequestSentMessage = "Message ID:  \n \n Message sent successfully. \n \n If we haven't got back to you in 3 working days, please call us on +44 7552 215800.";
            return View();
        }
    }
}
