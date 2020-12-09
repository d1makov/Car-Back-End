using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car.BLL.Dto.Email;
using Car.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Web;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailTestController : ControllerBase
    {
        private readonly IEmailSenderService _emailSender;

        public EmailTestController(IEmailSenderService emailSender)
        {
            _emailSender = emailSender;
        }


        [HttpGet]
        public async Task Get()
        {
            MailingMessage mailingMessage = new MailingMessage
            {
                PassangerAddress = new MimeKit.MailboxAddress(Encoding.UTF8, "Passanger2", "gavrylyakbogdan@gmail.com"),
                Passanger = new Car.DAL.Entities.User() { Name = "Passanger1" },
                CancelDate = DateTime.Now,
                Driver = new Car.DAL.Entities.User() { Name = "Driver1" },
                DriverAddress = new MimeKit.MailboxAddress(Encoding.UTF8, "Driver2", "driver1@gmail.com"),
            };

            

            await _emailSender.CancelJourneyAsync(mailingMessage);
        }


        // GET: api/<EmailTestController>
        /* [HttpGet]
         public IEnumerable<string> Get()
         {
             return new string[] { "value1", "value2" };
         }

         // GET api/<EmailTestController>/5
         [HttpGet("{id}")]
         public string Get(int id)
         {
             return "value";
         }

         // POST api/<EmailTestController>
         [HttpPost]
         public void Post([FromBody] string value)
         {
         }

         // PUT api/<EmailTestController>/5
         [HttpPut("{id}")]
         public void Put(int id, [FromBody] string value)
         {
         }

         // DELETE api/<EmailTestController>/5
         [HttpDelete("{id}")]
         public void Delete(int id)
         {
         }*/
    }
}



/*Car.BLL.Services.Implementation.EmailService emailService = new Car.BLL.Services.Implementation.EmailService();
await emailService.SendEmailAsync("gavrylyakbogdan@gmail.com", "subjectHTML",
    "<div align='center'>" +
        "<h1 style='color: red'>Message HTML</h1>" +
    "</div>");*/

// var message = new Message(new string[] { "gavrylyakbogdan@gmail.com" },
//    "Test mail async", "<div align = 'center' > " +
//        "<h1 style='color:DodgerBlue'>Message HTML</h1>" +
//    "</div>");

// await _emailSender.SendEmailAsync(message);