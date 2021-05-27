using MailCommunicationUtils.Data;
using MailCommunicationUtils.Enums;
using MailCommunicationUtils.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailCommunicationUtils.Classes
{
    public class Deserialize : IDeserializer
    {
        public ConverterResult<T> DeserializeObjectFromFile<T>(string absoluteFolderPath, string fileName, string fileType)
        {
            var result = new ConverterResult<T>() { Status = ConverterStatus.Ok };
            var fullFilePath = Path.Combine(absoluteFolderPath, fileName);
            string JSONString = File.ReadAllText(fullFilePath);
            result.ReturnValue = System.Text.Json.JsonSerializer.Deserialize<List<PersonList>>(JSONString)  ;

            return result2;
        }
    }
}

