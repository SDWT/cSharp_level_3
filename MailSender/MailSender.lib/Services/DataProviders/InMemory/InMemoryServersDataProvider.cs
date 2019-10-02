using MailSender.lib.Entities;
using MailSender.lib.Services.DataProviders.Interfaces;

namespace MailSender.lib.Services.DataProviders.InMemory
{
    public class InMemoryServersDataProvider : InMemoryDataProvider<Server>, IServersDataProvider
    {
        public override void Copy(ref Server destination, Server source)
        {
            destination.Host = source.Host;
            destination.Port = source.Port;
            destination.UserName = source.UserName;
            destination.Password = source.Password;
            destination.UseSSL = source.UseSSL;

            destination.Name = source.Name;
            destination.Description = source.Description;
        }
    }


}
