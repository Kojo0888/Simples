using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IamGoingToSleep
{
    class Program
    {
        public static int Minutes { get; set; } = int.Parse(ConfigurationManager.AppSettings["Minutes"]);

        public static int VolumeSkip { get; set; } = Minutes;

        public static int CurrentVolumeSkip { get; set; }

        public static int CurrentMinutes { get; set; }

        public static int CurrentSeconds { get; set; }

        static void Main(string[] args)
        {
            try
            {

                CurrentMinutes = Minutes;
                CurrentVolumeSkip = VolumeSkip / 2 + CurrentMinutes;
                Console.WriteLine("Go to sleep :) relax...");
                while (CurrentMinutes > 0 || CurrentSeconds > 0)
                {
                    if (CurrentSeconds == 0 && CurrentMinutes > 0)
                    {
                        CurrentSeconds = 60;
                        CurrentMinutes--;
                    }

                    CurrentSeconds--;

                    Console.Write(string.Format("{0}:{1}", CurrentMinutes.ToString("D2"), CurrentSeconds.ToString("D2")));
                    Thread.Sleep(1000);


                    if (--CurrentVolumeSkip == 0)
                    {
                        CurrentVolumeSkip = VolumeSkip / 2 + CurrentMinutes;
                        Volume.VolDown();
                    }
                    Console.CursorLeft = 0;
                }
                Process.Start("CMD.exe", "/C shutdown /h");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadKey();
            }

        }
    }
}
