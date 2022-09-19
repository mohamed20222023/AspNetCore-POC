using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Demo.BL.Models;


namespace Demo.BL.Helper
{
    public class MailSender
    {

        public static string SendEmail(MailVM mail)
        {
            try
            {
                var smtp = new SmtpClient("smtp.sendgrid.net", 587);

                smtp.EnableSsl = true;

                smtp.Credentials = new NetworkCredential(" // Your Key", " // Your Code");

                smtp.Send(" // Your Email ", mail.ToMail, mail.Subject, mail.Body);

                return "Succeed";

            }
            catch (Exception ex)
            {
                return "Faild";
            }
        }

    }
}
