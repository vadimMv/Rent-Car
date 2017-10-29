using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;
using vadim_final_326960382_91338._16.Models;

namespace vadim_final_326960382_91338._16.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// index page
        /// </summary>
        /// <returns></returns>
        // GET: Home
        public ActionResult Index()
        {
            if (Request.IsAuthenticated && User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "ManageRent", new { area = "Admin" });
            }
            else if (Request.IsAuthenticated && User.IsInRole("Staff"))
            {
                return RedirectToAction("Index", "Worker", new { area = "Staff" });
            }
            else
            {
                using (CarM c = new CarM())
                {
                    return View(c.GetAll().ToList());
                }
            }
        }
        /// <summary>
        /// about page
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        /// <summary>
        /// contact page
        /// </summary>
        /// <returns></returns>
        public ActionResult Contact()
        {
            ViewBag.Message = "If you have any question, feel free to ask. <br> We will answer as soon as possible.";

            return View();
        }
        /// <summary>
        /// access denied page
        /// </summary>
        /// <returns></returns>
        public ActionResult AccessDenied() {

            return View();
        }
        /// <summary>
        /// send contact form message to mail
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Contact(Contact model)
        {
            model.Message = Request.Form["message"];

            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                MailMessage mail = new MailMessage();

 
                // Message details
                mail.From = new MailAddress(model.Email);
                mail.Subject = model.Subject;
                mail.IsBodyHtml = true;
                mail.To.Add("vadimmukovozov@gmail.com");

                string sender = "Sender: " + model.Name + " (" + model.Email + ")" + "\n\n";
                string body = model.Message + "\n\n";
                string sendFrom = "Sent from : RentCar Israel";
                mail.Body = sender + body + sendFrom;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("vadimmukovozov@gmail.com", "mukovozov1986");
                smtp.EnableSsl = true;
                smtp.Send(mail);

                ViewBag.Message = "<br> Your message has been sent successfuly.";
                return View();
            }
            catch
            {
                ViewBag.Message = "<br> Your message has not been sent. Please try again. <br>";
                return View();
            }
        }
    }
}