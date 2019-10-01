using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.lib.Services.Interfaces
{
    public interface IDataProvider<T>
    {
        IEnumerable<T> GetAll();

        T GetById(int id);

        int Create(T item);

        void Edit(int id, T item);

        bool Remove(int id);

        void SaveChanges();
    }
}
