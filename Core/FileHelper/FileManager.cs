using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.FileHelper
{
    public static class FileManager
    {
        public static void Write(int test)
        {
            string dosyaYolu = @"C:\Users\90542\source\repos\WarehouseProject\UId.txt";
            File.WriteAllText(dosyaYolu, string.Empty);
            FileStream fileStream = new FileStream(dosyaYolu, FileMode.Open, FileAccess.Write);
            StreamWriter writer = new StreamWriter(fileStream, Encoding.UTF8);
            writer.WriteLine(test);
            writer.Close();
        }

        public static int Read()
        {
            string dosyaYolu = @"C:\Users\90542\source\repos\WarehouseProject\UId.txt";
            FileStream fileStream = new FileStream(dosyaYolu, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamReader reader = new StreamReader(fileStream);


            string satir = reader.ReadLine();

            reader.Close();

            return int.Parse(satir);
        }
    }
}
