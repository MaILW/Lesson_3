using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace Шифры
{
    class Gamir
    {
        private readonly string alfavitPath = @"D:\MaILW\Documents\Visual Studio 2019\Projects\Шифры\Рабочие Файлы\Алфавиты\alfavitGamir.alph";
        private readonly string pathKey = @"D:\MaILW\Documents\Visual Studio 2019\Projects\Шифры\Рабочие Файлы\Ключи\";
        private readonly string path = @"D:\MaILW\Documents\Visual Studio 2019\Projects\Шифры\Рабочие Файлы\";
        public void GenKluchG()
        {
            
            Console.Write("Введите длину гаммы: ");
            int gammasize = 0;
            bool flErr;
            do
            {
                try
                {
                    gammasize = Convert.ToInt32(Console.ReadLine());
                    flErr = false;

                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Неверный форнмат данных. Повторите попытку ввода.");
                    flErr = true;
                }
            } while (flErr == true);


            
            string a = File.ReadAllText(alfavitPath);
            string[] alph = a.Split(new char[] { ':' });
            var rnd = new Random();
            string gamma = "";
            
            for (int i = 0; i < gammasize; i++)
            {
                gamma += alph[rnd.Next(0, alph.Length)];

            }

            Console.Write("Введите название ключа: ");

            File.WriteAllText(pathKey + Console.ReadLine() + ".key", "gamir\n" + gamma);

        }

        public void EncodeG()
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
                if (type == "gamir")
                    break;
                else Console.WriteLine("Выбранный ключ не подходит для данного метода. Повторите попытку ввода.");
            }

            string[] key1 = File.ReadAllLines(nameKey);

            var gamma = key1[1].ToCharArray();
            int sizeGamma = gamma.Length;


            string TextSt = File.ReadAllText(nameText);
            char[] Text = TextSt.ToCharArray();
            TextSt = "";

            string a = File.ReadAllText(alfavitPath);
            string[] alphStr = a.Split(new char[] { ':' });
            char[] alph = new char[alphStr.Length];
         
            for (int i = 0; i < alphStr.Length; i++)
            {
                alph[i] = Convert.ToChar(alphStr[i]);
            }
            for (int i = 0; i < Text.Length; i++)
            {
                if (Text[i] == '\n' || Text[i] == '\r' || Text[i] == ' ') continue;
               
                int temp;
               temp = (Array.IndexOf(alph,  Text[i]) + Array.IndexOf(alph, gamma[i % sizeGamma])) % alph.Length;
             
                Text[i] = Convert.ToChar(alph[temp] );
             
            }
            for (int i = 0; i < Text.Length; i++)
            {
                TextSt += Convert.ToString(Text[i]);
            }
            File.WriteAllText(path + "CodeGamma.encode", "gamir\n" + TextSt);

        
        
        }

        public void DecodeG()
        {
            string nameText;
            Console.Write("Введите название текса для расшифрования: ");
            while (true)
            {
                nameText = Console.ReadLine();
                if (File.Exists(path + nameText + ".encode") == false)
                {
                    Console.WriteLine("Файл не существует. Повторите ввод.");
                    continue;
                }
                nameText = path + nameText + ".encode";
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
                if (type == "gamir")
                    break;
                else Console.WriteLine("Выбранный ключ не подходит для данного метода. Повторите попытку ввода.");
            }

            string[] key1 = File.ReadAllLines(nameKey);

            var gamma = key1[1].ToCharArray();
            int sizeGamma = gamma.Length;


            string TextSt = File.ReadAllText(nameText);
            TextSt = TextSt.Replace("gamir\n", "");
            char[] Text = TextSt.ToCharArray();
            TextSt = "";

            string a = File.ReadAllText(alfavitPath);
            string[] alphStr = a.Split(new char[] { ':' });
            char[] alph = new char[alphStr.Length];

            for (int i = 0; i < alphStr.Length; i++)
            {
                alph[i] = Convert.ToChar(alphStr[i]);
            }
            for (int i = 0; i < Text.Length; i++)
            {
                if (Text[i] == '\n' || Text[i] == '\r' || Text[i] == ' ') continue;
               
                int temp;
                temp = (Array.IndexOf(alph, Text[i]) - Array.IndexOf(alph, gamma[i % sizeGamma])) % alph.Length;
                if (temp < 0) 
                    temp = temp + alph.Length;
                
                Text[i] = Convert.ToChar(alph[temp]);

            }
            for (int i = 0; i < Text.Length; i++)
            {
                TextSt += Convert.ToString(Text[i]);
            }
            File.WriteAllText(path + "CodeGamma.txt", TextSt);
        }

        public void EncodeGBi()
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
                if (type == "gamir")
                    break;
                else Console.WriteLine("Выбранный ключ не подходит для данного метода. Повторите попытку ввода.");
            }

            string[] key1 = File.ReadAllLines(nameKey);

            var gamma = key1[1].ToCharArray();
            int sizeGamma = gamma.Length;


            string TextSt = File.ReadAllText(nameText);
            char[] Text = TextSt.ToCharArray();
            TextSt = "";

            string a = File.ReadAllText(alfavitPath);
            string[] alphStr = a.Split(new char[] { ':' });
            char[] alph = new char[alphStr.Length];

            for (int i = 0; i < alphStr.Length; i++)
            {
                alph[i] = Convert.ToChar(alphStr[i]);
            }
            for (int i = 0; i < Text.Length; i++)
            {
                if (Text[i] == '\n' || Text[i] == '\r' || Text[i] == ' ') continue;
                byte x, g, y;
                x = (byte)Array.IndexOf(alph, Text[i]);

                g = (byte)Array.IndexOf(alph, gamma[i % sizeGamma]);
                y = (byte)(x ^ g);
                Text[i] = Convert.ToChar(alph[y % alph.Length]);

            }
            for (int i = 0; i < Text.Length; i++)
            {
                TextSt += Convert.ToString(Text[i]);
            }
            File.WriteAllText(nameText + ".encode", "gamir\n" + TextSt);



        }

        public void DecodeGBi()
        {
            string nameText;
            Console.Write("Введите название текса для расшифрования: ");
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
                if (type != "gamir")
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
                if (type == "gamir")
                    break;
                else Console.WriteLine("Выбранный ключ не подходит для данного метода. Повторите попытку ввода.");
            }

            string[] key1 = File.ReadAllLines(nameKey);

            var gamma = key1[1].ToCharArray();
            int sizeGamma = gamma.Length;


            string TextSt = File.ReadAllText(nameText);
            TextSt = TextSt.Replace("gamir\n", "");
            char[] Text = TextSt.ToCharArray();
            TextSt = "";

            string a = File.ReadAllText(alfavitPath);
            string[] alphStr = a.Split(new char[] { ':' });
            char[] alph = new char[alphStr.Length];

            for (int i = 0; i < alphStr.Length; i++)
            {
                alph[i] = Convert.ToChar(alphStr[i]);
            }
            for (int i = 0; i < Text.Length; i++)
            {
                if (Text[i] == '\n' || Text[i] == '\r' || Text[i] == ' ') continue;
                byte x, g, y;
                x = (byte)Array.IndexOf(alph, Text[i]);

                g = (byte)Array.IndexOf(alph, gamma[i % sizeGamma]);
                y = (byte)(x ^ g);
                Text[i] = Convert.ToChar(alph[y % alph.Length]);

            }
            for (int i = 0; i < Text.Length; i++)
            {
                TextSt += Convert.ToString(Text[i]);
            }

           
            File.WriteAllText(path + Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(nameText)) + ".DECODE.txt", TextSt);
        }

    }

}
