using MailCommunicationUtils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailCommunicationUtils.Data
{
    public class ConverterResult<TResultObject>
    {
        public ConverterStatus Status { get; set; }
        public Exception Error { get; set; }
        public TResultObject ReturnValue { get; set; }
    }
}
