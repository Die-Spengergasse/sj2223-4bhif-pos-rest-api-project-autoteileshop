using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Application
{
    public class SendMail
    {
        //"mailtestdavid01@gmail.com" Test Mail Addrese zum versenden von Mails
        //davidMailEmpfangTestSPG@web.de -- Empfänger
        public string Send(string Acc, string emailFrom, string emailTo, string emailSubject, string emailBody)
        {
            string guidString = Guid.NewGuid().ToString().Substring(0, 8);

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(emailFrom, "bppkrfqjnqxvthxl"),
                EnableSsl = true
            };
            if (emailSubject.Count() == 0)
            {
                emailSubject = "Anmelde Bestätigung SpengerCars";
            }
            if (emailBody.Count() == 0)
            { emailBody = "Dies ist die Bestätigung für die Anmeldung des Accounts " + Acc + " ,\r\nBitte geben sie diesen Code: " + guidString + " zur Bestätigung ein. \r\n Dieser Code läuft nach 15 Minuten ab"; }
            //client.Send(emailFrom, emailTo, emailSubject, emailBody);
            return guidString;
        }
    }
}
