using MailSender.ConsoleTest.HomeWork7.DBwork.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailSender.ConsoleTest.HomeWork7.DBwork
{
    class DBwork
    {
        public List<Person> GetAllPersons()
        {
            var persons = new List<Person>();
            using (var db = new HW7DBworkDB())
            {
                persons = db.Persons.ToList();
            }
            return persons;
        }

        public Person GetById(int Id)
        {
            Person person = null;
            using (var db = new HW7DBworkDB())
            {
                person = db.Persons.FirstOrDefault(p => p.Id == Id);
            }
            return person;
        }

        public Person Edit(int Id, Person newPerson)
        {
            Person person = null;

            using (var db = new HW7DBworkDB())
            {
                var prs = newPerson;
                person = db.Persons.Where(p => p.Id == Id).First();

                person.Name = prs.Name;
                person.Email = prs.Email;
                person.PhoneNumber = prs.PhoneNumber;

                db.SaveChanges();
            }
            return person;
        }

        List<Person> ImportFromCSV(string fileName)
        {
            var persons = new List<Person>();

            using (var sr = new StreamReader(fileName))
            {
                StringBuilder strB = new StringBuilder();
                string str;

                while (!((str = sr.ReadLine()) is null))
                {
                    var personStrings = str.Split(',');
                    var numTrim = personStrings[2].Trim();
                    strB.Append(numTrim.Split('+')[1].Split('(')[0]);
                    strB.Append(numTrim.Split('(')[1].Split(')')[0]);
                    var nums = numTrim.Split(')')[1].Trim().Split('-');
                    foreach (var part in nums)
                        strB.Append(part);

                    string number = strB.ToString();

                    persons.Add(new Person
                    {
                        Name = personStrings[0].Trim(),
                        Email = personStrings[1].Trim(),
                        PhoneNumber = number
                    });
                    strB.Clear();
                }
            }
            return persons;
        }

        public void ImportFromCSVToDataBase(string fileName)
        {
            var persons = ImportFromCSV(fileName);
            var rnd = new Random();
            int offset = rnd.Next(100, 500);


            for (int i = 0; i < persons.Count; i += offset)
            {
                using (var db = new HW7DBworkDB())
                {
                    for (int j = i; j < offset + i; j++)
                    {
                        foreach (var person in persons)
                        {
                            db.Persons.Add(person);
                        }
                    }
                    db.SaveChanges();
                }
            }

            
        }

        public string Help()
        {
            var strB = new StringBuilder();
            strB.Append("Работа с людьми.\n");
            strB.Append("1 или show - показать людей из базы данных\n");
            strB.Append("2 или edit - редактированть данные\n");
            strB.Append("3 - импорт из csv файла");
            strB.Append("help - помощь");
            strB.Append("exit - выход.\n");

            return strB.ToString();
        }
    }
}