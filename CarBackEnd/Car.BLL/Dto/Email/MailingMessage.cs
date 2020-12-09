using MimeKit;
using System;
using Car.DAL.Entities;
using System.Collections.Generic;
using System.Text;

namespace Car.BLL.Dto.Email
{
    public class MailingMessage
    {
        //public string OwnerName { get; set; }
        public MailboxAddress DriverAddress { get; set; }
        public User Driver { get; set; }
        //public string UserName { get; set; }
        public MailboxAddress PassangerAddress { get; set; }
        public User Passanger { get; set; }
        //public int RequestId { get; set; }
        public DateTime CancelDate { get; set; }
        //public string BookName { get; set; }
        //public int BookId { get; set; }
    }
}
