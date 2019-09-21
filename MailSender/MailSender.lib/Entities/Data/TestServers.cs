using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.lib.Entities.Data
{
    public static class TestData
    {
        public static List<Server> Servers { get; } = new List<Server>
        {
            new Server{ Id = 1, Name = "Yandex", Address = "smtp.yandex.ru", UserName = "UserName", Password = "Pass"},
            new Server{ Id = 2, Name = "Mail.ru", Address = "smtp.yandex.ru", UserName = "UserName", Password = "Pass"},
            new Server{ Id = 3, Name = "Gmail", Address = "smtp.yandex.ru", UserName = "UserName", Password = "Pass"}
        };

        public static List<Sender> Senders { get; } = new List<Sender>
        {
            new Sender{Id = 1, Name = "", Address=""},
            new Sender{Id = 2, Name = "", Address=""},
            new Sender{Id = 3, Name = "", Address=""}
        };



    }
}
