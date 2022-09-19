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

                smtp.Credentials = new NetworkCredential("apikey", "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");

                smtp.Send("m01555232728@gmail.com", mail.ToMail, mail.Subject, mail.Body);

                return "Succeed";

            }
            catch (Exception ex)
            {
                return "Faild";
            }
        }

    }
}
