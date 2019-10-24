using System;

namespace Шифры
{
    class Program
    {
        static void Main(string[] args)
        {
            var gamma = new Gamir();
            var zamena = new Zamena();
            var perest = new Perestanovka();
           // bool flag = true;
            do
            {
                Console.Write("Главное меню:\n" +
                    "\t1) Зашифровать/Расшифровать\n" +
                    "\t2) Сгенерировать ключ\n" +
                    "Ваш выбор: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        {
                            Console.Write("Зашифровать/Расшифровать:\n" +
                                "\t1) Зашифровать\n" +
                                "\t2) Расшифровать\n" +
                                "Ваш выбор: ");
                            switch (Console.ReadLine())
                            {
                                case "1":
                                    {
                                        Console.Write("Выберите метод шифровки:\n" +
                                            "\t1) Применить шифр замены\n" +
                                            "\t2) Применить шифр перестановки\n" +
                                            "\t3) Применить метод гамирования\n" +
                                            "Ваш выбор: ");
                                        switch (Console.ReadLine())
                                        {
                                            case "1":
                                                {
                                                    zamena.EncodeZ();
                                                    break;
                                                }
                                            case "2":
                                                {
                                                    perest.EncodeP();
                                                    break;
                                                }
                                            case "3":
                                                {
                                                    gamma.EncodeGBi();
                                                    break;
                                                }
                                            default:
                                                {
                                                    Console.WriteLine("Неверно сделан выбор.");
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                                case "2":
                                    {
                                        Console.Write("Выберите метод разшифровки:\n" +
                                           "\t1) Применить метод замены\n" +
                                           "\t2) Применить метод перестановки\n" +
                                           "\t3) Применить метод гамирования\n" +
                                           "Ваш выбор: ");
                                        switch (Console.ReadLine())
                                        {
                                            case "1":
                                                {
                                                    zamena.DecodeZ();
                                                    break;
                                                }
                                            case "2":
                                                {
                                                    perest.DecodeP();
                                                    break;
                                                }
                                            case "3":
                                                {
                                                    gamma.DecodeGBi();
                                                    break;
                                                }
                                            default:
                                                {
                                                    Console.WriteLine("Неверно сделан выбор.");
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                                default:
                                    {
                                        Console.WriteLine("Неверно сделан выбор.");
                                        break;
                                    }
                            }

                            break;
                        }
                    case "2":
                        {
                            Console.Write("Выполняется процедура генерации ключа...\n" +
                            "\t1) Шифр замены\n" +
                            "\t2) Шифр перестановки\n" +
                            "\t3) Шифр гамирования\n" +
                            "Ваш выбор: ");
                            switch (Console.ReadLine())
                            {
                                case "1":
                                    {
                                        zamena.GenKluchZ();
                                        break;
                                    }
                                case "2":
                                    {
                                        perest.GenKeyP();
                                        break;
                                    }
                                case "3":
                                    {
                                        gamma.GenKluchG();
                                        break;
                                    }
                                default:
                                    {
                                        Console.WriteLine("Неверно сделан выбор.");
                                        break;
                                    }
                            }
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Неверно сделан выбор.");
                            break;
                        }
                }
                Console.Write("Для продолжения наберите \"next\":\t");
            } while (Console.ReadLine() == "next");
        }
    }
}
