using MailSender.lib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.lib.Services.Interfaces
{
    public interface IRecipientsDataProvider
    {
        IEnumerable<Recipient> GetAll();

        Recipient GetById(int id);

        int Create(Recipient item);

        void Edit(int id, Recipient item);

        bool Remove(int id);

        void SaveChanges();
    }
}
