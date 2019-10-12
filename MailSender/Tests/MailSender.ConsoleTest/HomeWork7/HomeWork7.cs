using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using MailSender.ConsoleTest.HomeWork7.DBwork;

namespace MailSender.ConsoleTest.HomeWork7
{
    static class HomeWork7
    {
        public static void Start()
        {
            DBworkStart();

        }

        private static void DBworkStart()
        {
            var dbwork = new DBwork.DBwork();
            //dbwork.ImportFromCSVToDataBase(Path.Combine("TestData", "hw7persons.csv"));
            bool isWork = true;
            bool Conditions = false;

            Console.WriteLine(dbwork.Help());
            while (isWork)
            {

                var command = Console.ReadLine();

                switch (command.ToLower())
                {
                    case "1":
                    case "show":
                        var persons = dbwork.GetAllPersons();
                        foreach (var person in persons)
                        {
                            Console.WriteLine($"{person.Id} {person.Name} {person.Email} {person.PhoneNumber}");
                        }
                        break;
                    case "2":
                    case "edit":
                        bool isEdit = true;
                        string nName, nEmail, nPhone;
                        int oldId = 0, max = dbwork.GetAllPersons().Count;

                        Console.WriteLine("Выберите пользователя введя его Id");

                        do
                        {
                            Conditions = !(int.TryParse(Console.ReadLine(), out oldId));
                            Conditions = false;
                            if (oldId < 1)
                            {
                                Conditions = true;
                                Console.WriteLine("Number less 1");
                            }
                            else if (oldId > max)
                            {
                                Conditions = true;
                                Console.WriteLine("Number higher maximum of personId");
                            }
                            else if (Conditions)
                                Console.WriteLine("It's not a number");
                        } while (Conditions);

                        Person old = dbwork.GetById(oldId);
                        nName = old.Name;
                        nEmail = old.Email;
                        nPhone = old.PhoneNumber;


                        while (isEdit)
                        {
                            Console.WriteLine("Введите, что вы хотите заменить: name, email, phone");
                            Console.WriteLine("save - сохранить изменения");
                            Console.WriteLine("exit - выйти без сохранения");
                            var change = Console.ReadLine();

                            //dbwork.Edit(oldId, old);

                            Func<string> readValueFunc = () =>
                                {
                                    Console.WriteLine("Введите, на что вы хотите заменить");
                                    return Console.ReadLine();
                                };

                            switch (change.ToLower())
                            {
                                case "name":
                                    nName = readValueFunc();
                                    break;
                                case "email":
                                    nEmail = readValueFunc();
                                    break;
                                case "phone":
                                    nPhone = readValueFunc();
                                    break;
                                case "save":
                                    dbwork.Edit(oldId, new Person
                                    {
                                        Name = nName,
                                        Email = nEmail,
                                        PhoneNumber = nPhone
                                    });
                                    break;
                                case "exit":
                                    isEdit = false;
                                    break;
                                default:
                                    Console.WriteLine("This command not aviable, please check.");
                                    break;
                            }


                        }
                        break;
                    case "3":
                        string sourceFilePath;
                        Console.WriteLine("Enter source file name");
                        do
                        {
                            Conditions = false;
                            sourceFilePath = Console.ReadLine();
                            if (!File.Exists(sourceFilePath))
                            {
                                Console.WriteLine("File does not exisit");
                                Conditions = true;
                            }
                        } while (Conditions);

                        dbwork.ImportFromCSVToDataBase(sourceFilePath);
                        break;
                    case "help":
                        dbwork.Help();
                        break;
                    case "exit":
                        isWork = false;
                        break;
                    default:
                        Console.WriteLine("This command not aviable, please check.");
                        break;
                }
            }

        }

    }
}
