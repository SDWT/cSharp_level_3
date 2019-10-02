using System;
using System.ComponentModel;
using MailSender.lib.Entities.Base;

namespace MailSender.lib.Entities
{
    public class Recipient : HumanEntity, IDataErrorInfo
    {
        public override string Name
        {
            get => base.Name;
            set
            {
                if (value is null) throw new ArgumentNullException(nameof(value));
                if (value.Length <= 3)
                    throw new ArgumentOutOfRangeException(nameof(value), "Длина строки должна быть больше 3");
                base.Name = value;
            }
        }

        string IDataErrorInfo.Error => "";

        string IDataErrorInfo.this[string PropertyName]
        {
            get
            {
                switch (PropertyName)
                {
                    default:
                        return string.Empty;
                    case nameof(PropertyName):
                        var name = Name;
                        if (name.Length <= 3) return "Имя должно быть длинее 3 символов";
                        if (!char.IsLetter(name[0])) return "Имя должно начинаться с буквы";
                        return string.Empty;
                }
            }
        }

    }
}
