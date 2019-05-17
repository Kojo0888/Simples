using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VolumeControl;

namespace IamGoingToSleep
{
    class Program
    {
        public static int Minutes { get; set; } = int.Parse(ConfigurationManager.AppSettings["Minutes"]);

        public static int AlarmInMinutes { get; set; } = int.Parse(ConfigurationManager.AppSettings["AlarmInMinutes"]);

        public static int CurrentMinutes { get; set; }

        public static int CurrentSeconds { get; set; }

        static void Main(string[] args)
        {
            Volume s = new Volume();
            //s.SetVolumeNAudio(10);
            float initialVolumeValue = s.GetVolumeNAudio();

            try
            {
                CurrentMinutes = Minutes;
                Console.WriteLine("Go to sleep :) relax...");
                while (CurrentMinutes > 0 || CurrentSeconds > 0)
                {
                    if (CurrentSeconds == 0 && CurrentMinutes > 0)
                    {
                        CurrentSeconds = 60;
                        CurrentMinutes--;
                    }
                    //12
                    CurrentSeconds--;
                    //123
                    Console.Write(string.Format("{0}:{1}", CurrentMinutes.ToString("D2"), CurrentSeconds.ToString("D2")));
                    Thread.Sleep(1000);

                    var scallar = (float)(CurrentMinutes * 60 + CurrentSeconds) / (Minutes * 60);
                    var volumeScallar = initialVolumeValue * scallar;
                    s.SetVolumeNAudio(volumeScallar);
                    Console.CursorLeft = 0;
                }

                if (AlarmInMinutes > 0)
                {
                    while (true)
                    {
                        if (CurrentMinutes > AlarmInMinutes)
                        {
                            s.SetVolumeNAudio(initialVolumeValue);
                            SoundPlayer player = new SoundPlayer();
                            player.SoundLocation = "Alarm.wav";
                            player.Load();
                            player.Play();
                            Console.WriteLine("Alarm!!!");
                            Thread.Sleep(10000);
                            break;
                        }

                        Thread.Sleep(60000);
                        CurrentMinutes++;
                    }
                }
                else
                {
                    Process.Start("CMD.exe", "/C shutdown /h");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadKey();
            }

        }
    }
}
