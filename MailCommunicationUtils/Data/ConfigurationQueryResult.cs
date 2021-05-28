using MailCommunicationUtils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailCommunicationUtils.Data
{
    public class ConfigurationQueryResult<TQueryResult>
    {
        public Exception Error { get; set; }
        public TQueryResult QueryResult { get; set; }
        public QueryStatus Status { get; set; }
    }
}
