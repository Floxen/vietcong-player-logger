using System;
using System.Diagnostics;
using System.Threading;

namespace VCLogger
{
    internal class EventListener
    {
        private string workingDirectory;
        private string format;
        private string type;
        private int refreshTime = 500;

        private TextBoxSender sender;
        private IntPtr handler;

        public EventListener(string workingDirectory, string port, string format, string type, string refreshTime)
        {
            this.workingDirectory = workingDirectory;
            this.format = format;
            this.type = type;
            if (int.TryParse(refreshTime, out int time)) this.refreshTime = time;

            handler = getHandleOfWindow(port);
            sender = new TextBoxSender();
        }

        public IntPtr getHandleOfWindow(string port)
        {
            foreach (var proc in Process.GetProcesses())
            {
                if (proc.MainWindowTitle.EndsWith(port))
                {
                    return proc.MainWindowHandle;
                }
            }

            return IntPtr.Zero;
        }

        public void connectionTask()
        {
            string file = workingDirectory + "/logs/connections.txt";

            long lastFileSize = Files.getFilesize(file);

            if (refreshTime < 5)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("WARN: refresh time are at critical value!");
                Console.ForegroundColor = ConsoleColor.White;
            }

            while (true)
            {
                long now = Files.getFilesize(file);

                if (lastFileSize != now)
                {
                    string lastLine = Files.getLastFileLine(file);

                    if (lastLine != null && lastLine != String.Empty && lastLine.Length >= 26)
                    {
                        lastLine = lastLine.Substring(26);

                        string[] playerArgs = lastLine.Split('/');

                        if (type == "1") sender.send(handler, "adminsay \"" + format.Replace("{name}", playerArgs[0].Replace("\"", "''").Trim()).Replace("{country}", playerArgs[2].Trim()) + "\"");
                        if (type == "2") sender.send(handler, "say \"" + format.Replace("{name}", playerArgs[0].Replace("\"", "''").Trim()).Replace("{country}", playerArgs[2].Trim()) + "\"");
                        if (type == "3") sender.send(handler, ".redsay " + format.Replace("{name}", playerArgs[0].Replace("\"", "''").Trim()).Replace("{country}", playerArgs[2].Trim()));
                        if (type == "4") sender.send(handler, ".serversay " + format.Replace("{name}", playerArgs[0].Replace("\"", "''").Trim()).Replace("{country}", playerArgs[2].Trim()));

                        Console.WriteLine("[" + DateTime.Now.TimeOfDay.ToString().Split('.')[0].Trim() + "] " + format.Replace("{name}", playerArgs[0].Replace("\"", "''").Trim()).Replace("{country}", playerArgs[2].Trim()));
                    }
                }

                lastFileSize = now;

                Thread.Sleep(refreshTime);
            }
        }

        public void chatTask()
        {
            string file = workingDirectory + "/dev/chatlog.txt";

            long lastFileSize = Files.getFilesize(file);

            while (true)
            {
                long now = Files.getFilesize(file);

                if (lastFileSize != now)
                {
                    string lastLine = Files.getLastFileLine(file);

                    if (lastLine != null && lastLine != String.Empty && lastLine.Length >= 12 && lastLine.Contains(": /"))
                    {
                        string command = lastLine.Split(new[] { ": /" }, StringSplitOptions.None)[1].Trim();
                        string id = lastLine.Substring(26).Split('[')[1].Split(']')[0].Trim();
                        string name = lastLine.Split(new[] { "[" + id + "]" }, StringSplitOptions.None)[1].Split(new[] { ": /" }, StringSplitOptions.None)[0].Trim();

                        if (command != "")
                        {
                            if (command.Contains("swap"))
                            {
                                sender.send(handler, "say \"Hrac '" + name + "' swapped!\"");
                                sender.send(handler, "swapplayer " + id);
                            }
                            else if (command.Contains("kick"))
                            {
                                sender.send(handler, "say \"Player '" + name + "' kicked for own request!\"");
                                sender.send(handler, ".kick " + id);
                            }
                            else if (command.Contains("info"))
                            {
                                sender.send(handler, "adminsay \"VCCTF v0.4.8 by @floxiceeq\"");
                            }
                        }
                    }
                }

                lastFileSize = now;

                Thread.Sleep(refreshTime);
            }
        }
    }
}
