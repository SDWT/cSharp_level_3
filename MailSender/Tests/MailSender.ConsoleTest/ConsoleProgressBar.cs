using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.ConsoleTest
{
    class ConsoleProgressBar
    {
        private int _Top;
        private int _Left;
        private int _Value = 0;
        private int _MaxValue = 0;
        public int Value 
        {
            get => _Value; 
            set
            {
                var newValue = value;
                if (newValue == _Value)
                    return;
                _Value = value < 0 ? 0 : value > _MaxValue ? _MaxValue : value;
                ChangeValue();
            }
        }

        public ConsoleProgressBar(int MaxValue, int Value = 0)
        {
            _MaxValue = MaxValue;
            this.Value = Value;

            _Left = Console.CursorLeft;
            _Top = Console.CursorTop;


            char chr = ' ';
            Console.WriteLine($"[{chr,10}]");
        }

        private void ChangeValue()
        {
            var cursorLeft = Console.CursorLeft;
            var cursorTop = Console.CursorTop;

            var strB = new StringBuilder();
            var cnt = 10 * _Value / _MaxValue;

            for (int i = 0; i < cnt; i++)
            {
                strB.Append("█");
            }

            string progress = strB.ToString();
            Console.SetCursorPosition(_Left + 1, _Top);
            
            Console.Write(progress);

            Console.SetCursorPosition(cursorLeft, cursorTop);
            //var cnt = 10 * _Value / _MaxValue;

            //for (int i = 0; i < cnt; i++)
            //{
            //    Console.Write("█");
            //}
        }

    }
}
