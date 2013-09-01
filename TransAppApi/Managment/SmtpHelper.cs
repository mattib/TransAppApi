using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace TransAppApi.Managment
{
    public class SmtpHelper
    {
        public void SendEmail(string reciverEmail, string reciverName, string subject, string body)
        {
            var senderEmail = "transappapp@gmail.com";
            var senderName = "TransApp - Acovado";

            var fromAddress = new MailAddress(senderEmail, senderName);
            var toAddress = new MailAddress(reciverEmail, reciverName);

            SendEmail(subject, body, fromAddress, toAddress);
        }

        private static void SendEmail(string subject, string body, MailAddress fromAddress, MailAddress toAddress)
        {
            var senderPassword = "TransApp!";

            var smtpClient = GetSmtpClient(senderPassword, fromAddress);

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            })
            {
                try
                {
                    smtpClient.Send(message);
                }
                catch (Exception anyException)
                {
                    var log = anyException;
                    // write to log the failer
                }
            }
        }

        private static SmtpClient GetSmtpClient(string senderPassword, MailAddress fromAddress)
        {
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,

                Credentials = new NetworkCredential(fromAddress.Address, senderPassword)
            };
            return smtp;
        }
    }
}