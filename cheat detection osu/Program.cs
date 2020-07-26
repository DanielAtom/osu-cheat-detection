using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Renci.SshNet;

namespace cheat_detection_osu
{
    class Program
    {
        public static int Send(string fileName)
        {
            string host = "ip";
            string username = "username";
            string password = "pw";
            
            using (var sftp = new SftpClient(host, 22, username, password))
            {
                sftp.Connect();
                sftp.ChangeDirectory("replays");
                using (var uplfileStream = System.IO.File.OpenRead(fileName))
                {
                    sftp.UploadFile(uplfileStream, fileName, true);
                }
                sftp.Disconnect();
            }
            return 0;
        }


            static void Main(string[] args)
            {
            Console.WriteLine("osu!katakuna analyzer | by DanielAtom | v1.0");

            Console.WriteLine("Looking for osu!...");

            Process[] processes = Process.GetProcesses();

            Process theone = Process.GetCurrentProcess();

            foreach (Process p in processes)
            {
                if (p.ProcessName == "osu!")
                {
                    theone = p;
                    break;
                }
            }

            if (theone.ProcessName != "osu!")
            {
                Console.WriteLine("osu! not found.");
                Console.Read();
            }
            else
            {
                Console.WriteLine("osu! found. Initializing tester...");
                Console.WriteLine("PID:  " + theone.Id);
                Console.WriteLine("Name: " + theone.ProcessName);

                for (int i = 0; i < 100; ++i)
                {
                    string logname = "log" + i + "-" + Environment.MachineName + ".txt";

                    using (StreamWriter sw = File.CreateText(logname))
                    {
                        sw.WriteLine("PROCESSES: ");

                        foreach (Process p in processes)
                        {
                            try
                            {
                                sw.WriteLine("\n");
                                sw.WriteLine("----------------------------------------");
                                sw.WriteLine(p.ProcessName);
                                sw.WriteLine(p.Id);
                                sw.WriteLine(p.MainWindowTitle);
                                sw.WriteLine(p.MainModule.FileName);
                                sw.WriteLine("\n");
                                sw.WriteLine("----------------------------------------");
                            }
                            catch (Exception ex)
                            {

                            }
                            
                        }

                        sw.WriteLine("\n \n \n OSU! MODULES: ");

                        foreach (ProcessModule module in theone.Modules)
                        {
                            try
                            {
                                sw.WriteLine("\n");
                                sw.WriteLine("----------------------------------------");
                                sw.WriteLine(module.ModuleName);
                                sw.WriteLine(module.FileName);
                                sw.WriteLine(File.GetLastWriteTime(module.FileName).ToString());
                                sw.WriteLine(File.GetCreationTime(module.FileName).ToString());
                                sw.WriteLine("\n");
                                sw.WriteLine("----------------------------------------");
                            }
                            catch (Exception ex)
                            {

                            }
                            
                        }

                        sw.WriteLine("\n \n \n END OF ANALYSIS");

                        sw.Close();
                    }

                    Send(logname);

                    Thread.Sleep(30000);
                }
                Console.Read();
            }
        }
    }
}
