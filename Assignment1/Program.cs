using CsvHelper;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;


namespace Assignment1
{
    class Program
    {

        public static void walk(String path)
        {

            string[] list = Directory.GetDirectories(path);


            if (list == null) return;

            foreach (string dirpath in list)
            // foreach (String f : list)
            {
                if (Directory.Exists(dirpath))
                {
                    walk(dirpath);
                }
            }
            string[] fileList = Directory.GetFiles(path);
            foreach (string filepath in fileList)
            {
                parse(filepath);
            }
        }

        //      
        public static void parse(String fileName)
        {
            int bad = 0;
            try
            {
                using (TextFieldParser parser = new TextFieldParser(fileName))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    while (!parser.EndOfData)
                    {
                        //Process row
                        string[] fields = parser.ReadFields();
                        bool noError = true;
                        String value = "";
                        for (int i = 0; i < fields.Length; i++)
                        {
                            if (fields[i] == null || fields[i].Length == 0)
                            {
                                noError = false;
                                bad++;
                                break;
                            }
                            value += fields[i] + ",";
                        }

                        if (noError)
                        {
                            value = value.Remove(value.Length - 1, 1);
                            File.AppendAllText(@"C:\Users\sigk\Desktop\New folder (2)\MCDA5510_Assignments\output.csv", value + "\n");

                        }
                    }
                    Console.WriteLine(bad);


                }
            }

            catch (IOException ioe)
            {
                Console.WriteLine(ioe.StackTrace);
            }

        }

        static void Main(string[] args)
        {
            var timer = new Stopwatch();
            timer.Start();
            using (StreamWriter sw = new StreamWriter(@"C:\Users\sigk\Desktop\New folder (2)\MCDA5510_Assignments\output.csv"))
                sw.WriteLine("Output");
            string root = @"C:\Users\sigk\Desktop\New folder (2)\MCDA5510_Assignments\Sample Data";
            walk(root);            
            timer.Stop();
            using (StreamWriter sw = new StreamWriter(@"C:\Users\sigk\Desktop\New folder (2)\MCDA5510_Assignments\log.txt"))
                sw.WriteLine(timer.Elapsed);
        }
    }
}