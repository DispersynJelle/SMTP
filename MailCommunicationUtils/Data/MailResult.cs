using MailCommunicationUtils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailCommunicationUtils.Data
{
    public class MailResult
    {
        public MailSendingStatus Status { get; set; }
        public string Message { get; set; }
    }
}
