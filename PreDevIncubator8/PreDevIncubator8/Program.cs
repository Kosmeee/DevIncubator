using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;

namespace PreDevIncubator8
{

    public class Dict
    {

    }
    class Program
    {
        static void Main(string[] args)
        {
            var arr = new string[] { "Масло", "Фланец", "Ось", "Свеча", "Фильтр", "Втулка", "Вал", "ШРУС", "ГРМ" };
            var dict = new Dictionary<string, int>();
            foreach(var a in arr)
            {
                dict.Add(a, 0);
            }
            using (StreamReader sr = new StreamReader($"../../../orders.csv"))
            {
                string line;
                string[] temp;
                while ((line = sr.ReadLine()) != null)
                {
                    line = line.Replace(" ","");
                    TextFieldParser parser = new TextFieldParser(new StringReader(line));
                    parser.HasFieldsEnclosedInQuotes = false;
                    parser.SetDelimiters(",",":");
                    temp = parser.ReadFields();
                    
                    for(int i = 1; i<temp.Length;i++)
                    {
                        dict[temp[i].Substring(0, 1).ToUpper() + (temp[i].Length > 1 ? temp[i].Substring(1) : "")] += 1;
                    }
                }
            }
            foreach(var a in dict)
            {
                Console.WriteLine($"{a.Key} - {a.Value} шт");
            }

        }
    }
}
