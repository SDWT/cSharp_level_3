using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MailSender.lib.Data.Linq2SQL;

namespace MailSender.lib.Services
{
    public class RecipientsDataProvider
    {
        private readonly MailSenderDBDataContext _db;

        public RecipientsDataProvider(MailSenderDBDataContext db) { _db = db; }

        public IEnumerable<Recipient> GetAll()
        {
            _db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues);
            return _db.Recipient.ToArray();
        }

        public int Create(Recipient recipient)
        {
            if (recipient is null) throw new ArgumentNullException(nameof(recipient));

            _db.Recipient.InsertOnSubmit(recipient);
            SaveChanges();
            return recipient.Id;
        }

        public void SaveChanges()
        {
            _db.SubmitChanges();
        }
    }
}
