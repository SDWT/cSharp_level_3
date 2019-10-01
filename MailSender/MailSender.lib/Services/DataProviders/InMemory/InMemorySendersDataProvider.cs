using MailSender.lib.Entities;
using MailSender.lib.Services.DataProviders.Interfaces;

namespace MailSender.lib.Services.DataProviders.InMemory
{
    public class InMemorySendersDataProvider : InMemoryDataProvider<Sender>, ISendersDataProvider
    {
        public InMemorySendersDataProvider()
        {
            for (int i = 1; i < 11; i++)
            {
                Create(new Sender
                {
                    Name = $"Отправитель {i}",
                    Address = $"sender{i}@server.com"
                });
            }
        }

        public override void Copy(ref Sender destination, Sender source)
        {
            destination.Address = source.Address;
            destination.Name = source.Name;
        }
    }


}
