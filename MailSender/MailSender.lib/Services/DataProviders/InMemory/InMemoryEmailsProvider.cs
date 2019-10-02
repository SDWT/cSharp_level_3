using MailSender.lib.Entities;
using MailSender.lib.Services.DataProviders.Interfaces;

namespace MailSender.lib.Services.DataProviders.InMemory
{
    public class InMemoryEmailsProvider : InMemoryDataProvider<Email>, IEmailsDataProvider
    {
        public InMemoryEmailsProvider()
        {
            for (int i = 1; i < 21; i++)
            {
                Create(new Email { Subject = $"Сообщение {i}", Body = $"Тело письма {i}"});
            }
        }

        public override void Copy(ref Email destination, Email source)
        {
            destination.Subject = source.Subject;
            destination.Body = source.Body;
        }
    }


}
