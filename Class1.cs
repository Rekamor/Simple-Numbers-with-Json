using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Dynamic;
using Newtonsoft.Json;

namespace SimpleNums
{
    public class Program
    {
        static string filename = "Simple_Numbers.json";
        static string folder = Path.GetTempPath();
        static string DBFilePath = folder + filename;
        
        static void Main()
        {
            if (File.Exists(DBFilePath) == false)
            {
                var file = File.Create(DBFilePath);
                file.Close();
                File.WriteAllText(DBFilePath, "[2]");
            }

            AddSimples(int.Parse(Console.ReadLine()));
            
            Console.WriteLine("Готово");
            Console.ReadLine();
        }

        static void AddSimples(int maxvalue,bool isusedlastdata = true)
        {
            List<int> simples = new List<int>();
            if (isusedlastdata) simples = JsonConvert.DeserializeObject<List<int>>(File.ReadAllText(DBFilePath));
            
            int lastsim = 1;
            if (isusedlastdata && simples.Count > 0) lastsim = simples.Last();
            
            for (int i = lastsim + 1; i <= maxvalue; i++)
            {
                bool issimple = true;

                if (isusedlastdata)
                {
                    for (int j = 0; simples[j] <= Math.Sqrt(i); j++)
                    {
                        if (i % simples[j] == 0) issimple = false;
                    }
                }
                if (issimple) simples.Add(i);
            }
            Console.WriteLine(simples.Count);
            File.WriteAllText(DBFilePath, JsonConvert.SerializeObject(simples));
        }
    }
}
