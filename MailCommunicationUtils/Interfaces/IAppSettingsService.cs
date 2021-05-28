using MailCommunicationUtils.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailCommunicationUtils.Interfaces
{
    public interface IAppSettingsService<TAppSettings>
    {
        public ConfigurationQueryResult<TSection> GetConfigurationSection<TSection>(string sectionName);
    }
}
