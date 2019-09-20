using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Model
{
    public class Smtp
    {
        public int Port { get; }
        public string SmptUrl { get; }

        public Smtp(string smptUrl, int port)
        {
            SmptUrl = smptUrl;
            Port = port;
        }
    }
}
