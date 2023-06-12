using GemBox.Email;
using System.Net;
using System.Net.Mail;


namespace Spg.AutoTeileShop.Application
{
    public class SendMail
    {
        //"mailtestdavid01@gmail.com" Test Mail Adresse zum versenden von Mails
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
            //client.Send(emailFrom, emailTo, emailSubject, emailBody);     //not active in test phase
            return guidString;
        }

        public bool ValidateMail(string email)
        {
            // If you have a Professional version, put your license key below.
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");


            // The email address has an invalid format.
            MailAddressValidationResult result = MailAddressValidator.Validate(email);
            if (result.Status != MailAddressValidationStatus.Ok)
                return false;
            return true;


            //Wenn du genau wissn willst was das problem ist:
            //throw new EigeneValidException("Mail not Valid", result.Status)
        }
    }
}
