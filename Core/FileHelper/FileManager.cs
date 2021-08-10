using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.FileHelper
{
    public static class FileManager
    {
        public static void WriteUID(int test)
        {
            string dosyaYolu = @"C:\Users\90542\source\repos\WarehouseProject\UId.txt";
            File.WriteAllText(dosyaYolu, string.Empty);
            FileStream fileStream = new FileStream(dosyaYolu, FileMode.Open, FileAccess.Write);
            StreamWriter writer = new StreamWriter(fileStream, Encoding.UTF8);
            writer.WriteLine(test);
            writer.Close();
        }

        public static int ReadUID()
        {
            string dosyaYolu = @"C:\Users\90542\source\repos\WarehouseProject\UId.txt";
            FileStream fileStream = new FileStream(dosyaYolu, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamReader reader = new StreamReader(fileStream);


            string satir = reader.ReadLine();

            reader.Close();

            return int.Parse(satir);
        }
        public static void Write(string FilePath,string line)
        {
            StreamWriter writer = File.AppendText(FilePath);
            writer.WriteLine(line);
            writer.Close();
        }

        public static List<string> Read(string FilePath)
        {
            FileStream fileStream = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamReader reader = new StreamReader(fileStream);
            List<string> line = new List<string>();
            string fileLine ="";
            while (fileLine!=null)
            {
                fileLine = (reader.ReadLine());
                if (fileLine != null)
                {
                    if (fileLine.Trim() != null)
                    {
                        line.Add(fileLine);
                    }
                }
                
            }
            reader.Close();

            return line;
        }
        public static void Update(string FilePath,int LineNumber, string data)
        {
            FileStream fileStream = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamReader reader = new StreamReader(fileStream);
            List<string> line = new List<string>();
            string fileLine = "";
            while (fileLine != null)
            {
                fileLine = (reader.ReadLine());
                line.Add(fileLine);
            }
            reader.Close();
            line[LineNumber] = data;

            File.WriteAllText(FilePath, string.Empty);
            FileStream file = new FileStream(FilePath, FileMode.Open, FileAccess.Write);
            StreamWriter writer = new StreamWriter(file, Encoding.UTF8);
            int i = 0;
            while (line.Count!=i)
            {
                writer.WriteLine(line[i]);
                i++;
            }
            writer.Close();
        }

        public static void Delete(string FilePath,int LineNumber)
        {
            FileStream fileStream = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamReader reader = new StreamReader(fileStream);
            List<string> line = new List<string>();
            string fileLine = "";
            while (fileLine != null)
            {
                fileLine = (reader.ReadLine());
                line.Add(fileLine);
            }
            reader.Close();
            line.RemoveAt(LineNumber);

            File.WriteAllText(FilePath, string.Empty);
            FileStream file = new FileStream(FilePath, FileMode.Open, FileAccess.Write);
            StreamWriter writer = new StreamWriter(file, Encoding.UTF8);
            int i = 0;
            while (line.Count != i)
            {
                writer.WriteLine(line[i]);
                i++;
            }
            writer.Close();
        }
    }
}
