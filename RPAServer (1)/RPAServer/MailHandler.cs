using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Text;
using MailKit.Security;
using MimeKit;
using System.IO;

namespace CoreServer
{
    class MailHandler
    {

        public int SendMail(string fromAddress, string toAddress, string subject, string messageBody, string username, string password)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(fromAddress));
            message.To.Add(new MailboxAddress(toAddress));
            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = messageBody
            };

            var client = new SmtpClient();

            client.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);

            client.Authenticate(username, password);

            client.Send(message);
            

            client.Disconnect(true);

            return Result.OK;
        }
        public int SendMailWithAttachements(string fromAddress, string toAddress, string subject, string messageBody, string username, string password)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Joey", "joey@friends.com"));
            message.To.Add(new MailboxAddress("Alice", "alice@wonderland.com"));
            message.Subject = "How you doin?";

            var builder = new BodyBuilder();

            // Set the plain-text version of the message text
            builder.TextBody = @"Hey Alice,

What are you up to this weekend? Monica is throwing one of her parties on
Saturday and I was hoping you could make it.

Will you be my +1?

-- Joey
";

            // We may also want to attach a calendar event for Monica's party...
            builder.Attachments.Add(@"C:\Users\Joey\Documents\party.ics");

            // Now we just need to set the message body and we're done
            message.Body = builder.ToMessageBody();
            var client = new SmtpClient();

            client.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);

            client.Authenticate(username, password);

          

            client.Send(message);


            client.Disconnect(true);

            return Result.OK;
        }
    
}
}
