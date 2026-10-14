using Microsoft.AspNetCore.Mvc;
using SparxNexora.Models;
using System.Net;
using System.Net.Mail;

namespace SparxNexora.Controllers;

public class HomeController : Controller
{
    private readonly IConfiguration _config;
    public HomeController(IConfiguration config) => _config = config;

    [HttpGet] public IActionResult Index() => View();
    [HttpGet] public IActionResult Services() => View();
    [HttpGet] public IActionResult Technologies() => View();
    [HttpGet] public IActionResult Industries() => View();
    [HttpGet] public IActionResult Process() => View();
    [HttpGet] public IActionResult Portfolio() => View();
    [HttpGet] public IActionResult About() => View();
    [HttpGet] public IActionResult Faq() => View();
    [HttpGet] public IActionResult Privacy() => View();
    [HttpGet] public IActionResult Terms() => View();
    [HttpGet] public IActionResult Cookies() => View();
    [HttpGet] public IActionResult Contact() => View(new ContactRequest());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Contact(ContactRequest model)
    {
        if (!ModelState.IsValid) return View(model);
        var email = _config.GetSection("Email");
        try
        {
            using var message = new MailMessage(email["From"]!, email["AdminEmail"]!);
            message.Subject = $"New Sparx Nexora enquiry — {model.Name}";
            message.Body = $"Name: {model.Name}\nEmail: {model.Email}\nPhone: {model.Phone}\nCompany: {model.Company}\nService: {model.Service}\nProject Type: {model.ProjectType}\nBudget: {model.Budget}\nTimeline: {model.Timeline}\n\nRequirement:\n{model.Message}";
            message.ReplyToList.Add(new MailAddress(model.Email));
            using var smtp = new SmtpClient(email["SmtpHost"]!, int.Parse(email["SmtpPort"] ?? "587"))
            { EnableSsl = true, Credentials = new NetworkCredential(email["Username"], email["Password"]) };
            await smtp.SendMailAsync(message);
            TempData["Success"] = "Thanks! Your requirement has been sent. Our team will contact you shortly.";
        }
        catch
        {
            TempData["Success"] = "Your requirement was received by the website. For an instant response, please use WhatsApp.";
        }
        return RedirectToAction(nameof(Contact));
    }
}
