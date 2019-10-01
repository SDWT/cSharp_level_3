using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.lib.Entities.Base;
using MailSender.lib.Services.DataProviders.Interfaces;

namespace MailSender.lib.Services.DataProviders.InMemory
{
    public abstract class InMemoryDataProvider<T> : IDataProvider<T> where T : BaseEntity
    {
        private readonly List<T> _Items = new List<T>();
        public IEnumerable<T> GetAll() => _Items;

        public T GetById(int id) => _Items.FirstOrDefault(item => item.Id == id);

        public void SaveChanges() { }

        public int Create(T item)
        {
            if (_Items.Contains(item)) return item.Id;
            item.Id = _Items.Count == 0 ? 1 : _Items.Max(r => r.Id) + 1;
            _Items.Add(item);
            return item.Id;
        }

        public abstract void Copy(ref T destination, T source);

        public void Edit(int id, T item)
        {
            var db_item = GetById(id);
            if (db_item is null) return;

            Copy(ref db_item, item);
        }

        public bool Remove(int id)
        {
            var db_item = GetById(id);
            return _Items.Remove(db_item);
        }

    }
}
