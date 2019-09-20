using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Model
{
    public static class Smtps
    {
        static public Smtp MailRu = new Smtp("smtp.mail.ru", 25);
        static public Smtp Yandex = new Smtp("smtp.yandex.ru", 25);

    }
}
