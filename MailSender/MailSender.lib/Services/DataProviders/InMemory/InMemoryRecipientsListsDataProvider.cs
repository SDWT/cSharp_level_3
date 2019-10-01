using MailSender.lib.Entities;
using MailSender.lib.Services.DataProviders.Interfaces;

namespace MailSender.lib.Services.DataProviders.InMemory
{
    public class InMemoryRecipientsListsDataProvider : InMemoryDataProvider<RecipientsList>, IRecipientsListsDataProvider
    {
        public override void Copy(ref RecipientsList destination, RecipientsList source)
        {
            destination.Name = source.Name;
            destination.Recipients = source.Recipients;
        }
    }


}
