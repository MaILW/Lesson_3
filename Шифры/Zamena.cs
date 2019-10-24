using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace Шифры
{
    class Zamena
    {
        private readonly string path = @"D:\MaILW\Documents\Visual Studio 2019\Projects\Шифры\Рабочие Файлы\";
        private readonly string pathKey = @"D:\MaILW\Documents\Visual Studio 2019\Projects\Шифры\Рабочие Файлы\Ключи\";
        public void GenKluchZ()
        {
            Console.Write("Введите название алфавита: ");

            string alp;
            while (true)
            {
                alp = Console.ReadLine();
                if (File.Exists(path+ @"Алфавиты\" + alp + ".alph") == false)
                {
                    Console.WriteLine("Файл не существует. Повторите ввод.");
                    continue;
                }
                alp = path+ @"Алфавиты\" + alp + ".alph";
                break;
            }

           
            string a = File.ReadAllText(alp);
            string[] alph = a.Split(new char[] { ':' });
            int size = alph.Length;
            string[] Key = new string[size]; alph.CopyTo(Key, 0);
            var rnd = new Random();

            for (int i = size - 1; i >= 1; i--)
            {
                int j = rnd.Next(i + 1);
                var temp = Key[j];
                Key[j] = Key[i];
                Key[i] = temp;
            }
            Console.Write("Введите имя файла ключа: ");
            var writer = new StreamWriter(pathKey + Console.ReadLine() + ".key");
            Console.Write("Программа успешно сгенерировала новый ключ. Вывести полученный ключ на экран?(Д/Н)\n" +
                "Ваш выбор: ");
            if (Console.ReadLine() == "Д")
            {
                writer.WriteLine("zamena");
                for (int i = 0; i < size; i++)
                {
                    Console.WriteLine(alph[i] + " " + Key[i]);
                    writer.WriteLine(alph[i] + " " + Key[i]);
                }
                writer.Dispose(); writer.Close();
            }
            else
            {
                writer.WriteLine("zamena");
                for (int i = 0; i < size; i++)
                {
                    writer.WriteLine(alph[i] + ":" + Key[i]);
                }
                writer.Dispose(); writer.Close();
            }
        }

        public void EncodeZ()
        {
            string nameText;
            Console.Write("Введите название текса для шифрования: ");
            while (true)
            {
                nameText = Console.ReadLine();
                if (File.Exists(path + nameText+ ".txt") == false)
                {
                    Console.WriteLine("Файл не существует. Повторите ввод.");
                    continue;
                }
                nameText = path + nameText + ".txt";
                break;
            }
            Console.Write("Введите имя ключа: ");
            string nameKey;
            while(true)
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
                if (type == "zamena") 
                break;
                else Console.WriteLine("Выбранный ключ не подходит для данного метода. Повторите попытку ввода.");
            }

            string[] key1 = File.ReadAllLines(nameKey);
            string[][] key = new string[key1.Length - 1][];
            for (int i = 1;i < key1.Length; i++)
            {
                key[i-1] = key1[i].Split(new char[] { ':' });
            }

            string TextSt = File.ReadAllText(nameText);
            char[] Text = TextSt.ToCharArray();
            for (int i = 0; i < Text.Length; i++)
            {
                //if (Text[i] == ' ') continue;
                for (int k = 0; k < key.Length; k++)
                    if (Text[i] == Convert.ToChar(key[k][0]))
                    {
                        Text[i] = Convert.ToChar(key[k][1]);
                        break;
                    }

            }
            
            var writer = new StreamWriter(nameText + ".encode");
            writer.WriteLine("zamena");
            for (int i = 0; i < Text.Length; i++)
                writer.Write(Text[i]);
            writer.Dispose();
            writer.Close();
        }

        public void DecodeZ()
        {
            string nameText;
            Console.Write("Введите название текса для разшифрования: ");
            while (true)
            {
                nameText = Console.ReadLine();
                if (File.Exists(path + nameText + ".txt.encode") == false)
                {
                    Console.WriteLine("Файл не существует. Повторите ввод.\a");
                    continue;
                }
               
                nameText = path + nameText + ".txt.encode";
                var reader = new StreamReader(nameText);
                string type = reader.ReadLine();
                reader.Dispose();
                reader.Close();
                if (type != "zamena")
                {
                    Console.WriteLine("Файл зашифрован другим методом. Используемый метод: " +  type);
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
                if (type == "zamena")
                    break;
                else Console.WriteLine("Выбранный ключ не подходит для данного метода. Повторите попытку ввода.");
            }

            string[] key1 = File.ReadAllLines(nameKey);
            

            string[][] key = new string[key1.Length - 1][];
            for (int i = 1; i < key1.Length; i++)
            {
                key[i - 1] = key1[i].Split(new char[] { ':' });
            }

            string TextSt = File.ReadAllText(nameText);
            TextSt = TextSt.Replace("zamena\r\n","");
            char[] Text = TextSt.ToCharArray();
            for (int i = 0; i < Text.Length; i++)
            {
                //if (Text[i] == ' ') continue;
                for (int k = 0; k < key.Length; k++)
                    if (Text[i] == Convert.ToChar(key[k][1]))
                    {
                        Text[i] = Convert.ToChar(key[k][0]);
                        break;
                    }

            }

            var writer = new StreamWriter(path + Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(nameText))+ ".DECODE.txt");
            
            for (int i = 0; i < Text.Length; i++)
                writer.Write(Text[i]);
            writer.Dispose();
            writer.Close();
        }

    }
}