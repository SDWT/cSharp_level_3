using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MailSender.lib.Services
{
    public static class XmlSerializerPool
    {
        private static readonly ConcurrentDictionary<Type, XmlSerializer> _SerializerPool = new ConcurrentDictionary<Type, XmlSerializer>();

        public static XmlSerializer GetXmlSerializer<T>() => GetXmlSerializer(typeof(T));

        public static XmlSerializer GetXmlSerializer(Type ObjectType)
        {
            return _SerializerPool.GetOrAdd(ObjectType, type => new XmlSerializer(type));

        }
    }
}
