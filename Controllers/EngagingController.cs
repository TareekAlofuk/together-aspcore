using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using together_aspcore.App.Member.Models;

namespace together_aspcore.Controllers
{
    [ApiController]
    [Route("api/engaging")]
    public class EngagingController : ControllerBase
    {
        [HttpPost]
        [Route("sms")]
        public ActionResult SendSms([FromForm] EmailEngagingRequestModel emailEngagingRequestModel)
        {
            var members = JsonConvert.DeserializeObject<Member[]>(emailEngagingRequestModel.Members);
            Console.WriteLine(members);
            return Ok();
        }

        [HttpPost]
        [Route("email")]
        public async Task<ActionResult> SendEmail([FromForm] EmailEngagingRequestModel emailEngagingRequestModel)
        {
            var members = JsonConvert.DeserializeObject<Member[]>(emailEngagingRequestModel.Members);
            await SendEmail(members, emailEngagingRequestModel.Message, emailEngagingRequestModel.Subject);
            return Ok();
        }

        private async Task SendEmail(IEnumerable<Member> members, string message, string subject)
        {
            var client = new SmtpClient("smtp.hostinger.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("test@tareekofuk.tech", "123456")
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("test@tareekofuk.tech")
            };
            foreach (var member in members)
            {
                if (member.Email != null)
                {
                    mailMessage.To.Add(member.Email);
                }
            }

            mailMessage.Body = message;
            mailMessage.Subject = subject;
            await client.SendMailAsync(mailMessage);
        }
    }

    public class EmailEngagingRequestModel
    {
        public string Message { get; set; }
        public string Subject { get; set; }
        public string Members { get; set; }
    }
}