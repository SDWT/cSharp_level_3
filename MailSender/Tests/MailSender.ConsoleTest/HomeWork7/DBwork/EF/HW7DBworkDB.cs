using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.ConsoleTest.HomeWork7.DBwork.EF
{
    class HW7DBworkDB : DbContext
    {

        public DbSet<Person> Persons { get; set; }

        public HW7DBworkDB() : this("name=HW7DBworkDB") { }

        public HW7DBworkDB(string Connection) : base(Connection) { }
    }
}
