using Demo.BL.Models;
using Microsoft.AspNetCore.Mvc;
namespace Demo.PL.Controllers
{
    public class MailController : Controller
    {


        public IActionResult SendMail()
        {
            return View();
        }


        [HttpPost]
        public IActionResult SendMail(MailVM mail)
        {

            try
            {

                if (ModelState.IsValid)
                {
                    
                    //TempData["Msg"] = MailSender.SendEmail(mail);
                    return RedirectToAction("SendMail");

                }

                //ModelState.Clear();
                return View();
            }
            catch (Exception ex)
            {
                TempData["Msg"] = "Faild";
                //ModelState.Clear();
                return View();
            }
        }
    }
}
