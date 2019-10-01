using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.lib.Entities;
using MailSender.lib.Services.DataProviders.Interfaces;

namespace MailSender.lib.Services.DataProviders.InMemory
{
    public class InMemoryRecipientsDataProvider : InMemoryDataProvider<Recipient>, IRecipientsDataProvider
    {
        public override void Copy(ref Recipient destination, Recipient source)
        {
            destination.Name = source.Name;
            destination.Address = source.Address;
        }
    }
}
