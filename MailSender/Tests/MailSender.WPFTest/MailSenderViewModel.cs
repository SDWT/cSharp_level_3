using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MailSender.lib.MVVM;

namespace MailSender.WPFTest 
{
    class MailSenderViewModel : MailSender.lib.MVVM.ViewModel
    {
        private string _Title = "Заголовок";
    }
}
