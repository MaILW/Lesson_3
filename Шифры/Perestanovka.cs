using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace Шифры
{
    class Perestanovka
    {
        private readonly string path = @"D:\MaILW\Documents\Visual Studio 2019\Projects\Шифры\Рабочие Файлы\";
        private readonly string pathKey = @"D:\MaILW\Documents\Visual Studio 2019\Projects\Шифры\Рабочие Файлы\Ключи\";

        public void GenKeyP()
        {
            Console.Write("Введите размер блока перестановки: ");
            bool flErr;
            int sizeBlock = 0;
            do
            {
                try
                {
                    sizeBlock = Convert.ToInt32(Console.ReadLine());
                    flErr = false;

                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Неверный форнмат данных. Повторите попытку ввода.");
                    flErr = true;
                }
            } while (flErr == true);

            var rnd = new Random();
            int[] Key = new int[sizeBlock];
            for (int i = 0; i < sizeBlock; i++)
            {
                Key[i] = i;
            }
            for (int i = sizeBlock - 1; i >= 1; i--)
            {
                int j = rnd.Next(i + 1);
                var temp = Key[j];
                Key[j] = Key[i];
                Key[i] = temp;
            }
            Console.Write("Введите имя файла ключа: ");
            var writer = new StreamWriter(pathKey + Console.ReadLine() + ".key");
            writer.WriteLine("perest");
            Console.Write("Программа успешно сгенерировала новый ключ. Вывести полученный ключ на экран?(Д/Н)\n" +
                "Ваш выбор: ");
            if (Console.ReadLine() == "Д")
            {
                for (int i = 0; i < sizeBlock; i++)
                {
                    Console.Write(Key[i]);
                    writer.Write(Key[i]);
                }
                Console.WriteLine();
            }
            else
            {
                for (int i = 0; i < sizeBlock; i++)
                {
                    writer.Write(Key[i]);
                }
            }
            writer.Dispose(); writer.Close();

        }

        public void EncodeP()
        {
            string nameText;
            Console.Write("Введите название текса для шифрования: ");
            while (true)
            {
                nameText = Console.ReadLine();
                if (File.Exists(path + nameText + ".txt") == false)
                {
                    Console.WriteLine("Файл не существует. Повторите ввод.");
                    continue;
                }
                nameText = path + nameText + ".txt";
                break;
            }
            Console.Write("Введите имя ключа: ");
            string nameKey;
            while (true)
            {
                nameKey = Console.ReadLine();
                if (File.Exists(pathKey + nameKey + ".key") == false)
                {
                    Console.WriteLine("Файл не существует. Повторите ввод.");
                    continue;
                }
                nameKey = pathKey + nameKey + ".key";
                var reader = new StreamReader(nameKey);
                string type = reader.ReadLine();
                reader.Dispose();
                reader.Close();
                if (type == "perest")
                    break;
                else Console.WriteLine("Выбранный ключ не подходит для данного метода. Повторите попытку ввода.");
            }

            string[] key1 = File.ReadAllLines(nameKey);

            var key = key1[1].ToCharArray();
            int sizeKey = key.Length;
            

            string TextSt = File.ReadAllText(nameText);
            char[] Text = TextSt.ToCharArray();
            for (int i = 0; i < Text.Length; i = i + sizeKey)
            {
             
                int k = 0;
                char[] temp = new char[sizeKey];
                for(; k < sizeKey; k++)
                {
                    if ((i + k) >= Text.Length) break;
                    temp[(int)Char.GetNumericValue(key[k])] = Text[i + k];

                }
           
                k = 0;
                for (int p = 0; p < sizeKey; p++)
                {
                    
                    if(i + k >= Text.Length) break;
                    if (temp[p] == '\0') Array.Resize(ref Text, Text.Length + 1);
                    Text[i + k] = temp[p];
                    k++;

                }
                    
          

            }

            var writer = new StreamWriter(nameText + ".encode");
            writer.WriteLine("perest");
            for (int i = 0; i < Text.Length; i++)
                writer.Write(Text[i]);
            writer.Dispose();
            writer.Close();
        }

        public void DecodeP()
        {
            string nameText;
            Console.Write("Введите название текса для разшифрования: ");
            while (true)
            {
                nameText = Console.ReadLine();
                if (File.Exists(path + nameText + ".txt.encode") == false)
                {
                    Console.WriteLine("Файл не существует. Повторите ввод.");
                    continue;
                }
                nameText = path + nameText + ".txt.encode";
                var reader = new StreamReader(nameText);
                string type = reader.ReadLine();
                reader.Dispose();
                reader.Close();
                if (type != "perest")
                {
                    Console.WriteLine("Файл зашифрован другим методом. Используемый метод: " + type);
                    continue;
                }
                break;
            }
            Console.Write("Введите имя ключа: ");
            string nameKey;
            while (true)
            {
                nameKey = Console.ReadLine();
                if (File.Exists(pathKey + nameKey + ".key") == false)
                {
                    Console.WriteLine("Файл не существует. Повторите ввод.");
                    continue;
                }
                nameKey = pathKey + nameKey + ".key";
                var reader = new StreamReader(nameKey);
                string type = reader.ReadLine();
                reader.Dispose();
                reader.Close();
                if (type == "perest")
                    break;
                else Console.WriteLine("Выбранный ключ не подходит для данного метода. Повторите попытку ввода.");
            }

            string[] key1 = File.ReadAllLines(nameKey);

            var key = key1[1].ToCharArray();
            int sizeKey = key.Length;


            string TextSt = File.ReadAllText(nameText);
            TextSt = TextSt.Replace("perest\r\n", "");
            char[] Text = TextSt.ToCharArray();
            for (int i = 0; i < Text.Length; i = i + sizeKey)
            {
                char[] temp = new char[sizeKey];
                for (int k = 0; k < sizeKey; k++)
                {
                    if ((i + k) >= Text.Length) break;
                    if (i + (int)Char.GetNumericValue(key[k]) >= Text.Length) continue;
                    temp[k] = Text[i + (int)Char.GetNumericValue(key[k])];
                }
                for (int k = 0; k < sizeKey; k++)
                {
                    if (i + k >= Text.Length) break;
                    Text[i + k] = temp[k];
                }

            }

            var writer = new StreamWriter(path + Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(nameText)) + ".DECODE.txt", false, Encoding.UTF8);

            for (int i = 0; i < Text.Length; i++)
            {
                Console.WriteLine(Text[i]);
                writer.Write(Text[i]);
            }
            writer.Dispose();
            writer.Close();
        }

    }
}
