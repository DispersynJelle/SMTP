using MailCommunicationUtils.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailCommunicationUtils.Interfaces
{
    interface IDeserializer
    {
        public ConverterResult<T> DeserializeObjectFromFile<T>(string absoluteFolderPath, string fileName, string fileType);
    }
}
