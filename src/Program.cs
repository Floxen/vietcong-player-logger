using System;
using System.IO;
using System.Threading;

namespace VCLogger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("+ VCLogger by @floxiceeq");

            string configPathFile = Environment.CurrentDirectory + "/vclogger.ini";

            if (!File.Exists(configPathFile))
            {
                File.WriteAllText(configPathFile, "// VCLogger configuration\n// by @floxiceeq\n// https://floxen.xyz\n\n[port] = 5425\n[join_msg_format] = Welcome {name} ({country})\n[join_msg_type] = 1          // 1 - adminsay, 2 - say, 3 - redsay, 4 - serversay\n\n[refresh_interval] = 500     // in MS!  1000ms equals to 1 second!");
            }

            string port = "";
            string format = "";
            string type = "";
            string refeshTime = "500";

            foreach (var conf in File.ReadLines(configPathFile))
            {
                if (!conf.Contains("=")) continue;

                string name = conf.Split('=')[0].Split('[')[1].Split(']')[0].Trim();
                string value = conf.Split('=')[1];

                if (value.Contains("//"))
                {
                    value = value.Split(new[] { "/" }, StringSplitOptions.None)[0].Trim();
                }

                value = value.Trim();

                if (name.Contains("port"))
                {
                    port = value;
                }
                else if (name.Contains("join_msg_format"))
                {
                    format = value;
                }
                else if (name.Contains("join_msg_type"))
                {
                    type = value;
                }
                else if (name.Contains("refresh_interval"))
                {
                    refeshTime = value;
                }
            }

            if (port != String.Empty && format != String.Empty && type != String.Empty)
            {
                Console.WriteLine("\n> Listening on port " + port + "\n\n");

                EventListener jm = new EventListener(Environment.CurrentDirectory, port, format, type, refeshTime);
                new Thread(jm.connectionTask).Start();
            }
        }
    }
}
