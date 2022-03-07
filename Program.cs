using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Drawing;
using Console = Colorful.Console;


namespace ARPatcherCMD
{
    internal class Program
    {
        static List<string> rLines = new List<string>();
        static List<string> tLines = new List<string>();
        static void Main(string[] args)
        {
            //rLines = File.ReadAllLines("D:/Users/kitka/Desktop/linesToRemove.txt").ToList();

            if(args.Length == 0)
            {
                Console.WriteLine("Make sure to do backup of AppraiserRes.dll");
                Console.WriteLine("Specify path to AppraiserRes.dll (or drop the AppraiserRes.dll if supported):", Color.White);
                mainFunc(Console.ReadLine());
            }
            else
            {
                mainFunc(args[0]);
            }
        }

        static void mainFunc(string target)
        {
            if (target.Contains("AppraiserRes.dll"))
            {
                tLines = File.ReadAllLines(target).ToList();
                rLines = GetEmbeddedResource("ARPatcherCMD", "linesToRemove.txt");

                removeLines(target);
                Console.WriteLine("Done!", Color.Green);
                Console.WriteLine("Press any key to exit...", Color.White);
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Specify path to AppraiserRes.dll", Color.Red);
                mainFunc(Console.ReadLine());
            }
        }

        static List<string> GetEmbeddedResource(string namespacename, string filename)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = namespacename + "." + filename;

            List<string> list = new List<string>();

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                //string result = reader.ReadToEnd();
                //return result;


                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    list.Add(line); // Add to list.
                }
                return list;
            }
        }

        static void removeLines(string target)
        {
            //Iterate through rLines
            //for (int i = 0; i < rLines.Count; i++)
            //{
            //    for (int j = 0; j < tLines.Count; j++)
            //    {
            //        if (tLines[j] == rLines[i])
            //        {
            //            tLines.RemoveAt(j);
            //        }
            //    }
            //}

            for (int i = 0; i < rLines.Count; i++)
            {
                tLines.Remove(rLines[i]);
            }

            //Write to target
            File.WriteAllLines(target, tLines);
        }

    }
}
