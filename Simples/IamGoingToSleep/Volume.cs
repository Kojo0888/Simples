using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Runtime.InteropServices;

namespace VolumeControl
{
    public class Volume
    {
        [DllImport("winmm.dll")]
        public static extern int waveOutGetVolume(IntPtr hwo, out uint dwVolume);

        [DllImport("winmm.dll")]
        public static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);

        public float GetVolumeNAudio()
        {
            try
            {
                NAudio.CoreAudioApi.MMDeviceEnumerator MMDE = new NAudio.CoreAudioApi.MMDeviceEnumerator();
                NAudio.CoreAudioApi.MMDeviceCollection DevCol = MMDE.EnumerateAudioEndPoints(NAudio.CoreAudioApi.DataFlow.All, NAudio.CoreAudioApi.DeviceState.Active);
                foreach (NAudio.CoreAudioApi.MMDevice dev in DevCol)
                {
                    Console.WriteLine("Name of device: " + dev.FriendlyName);
                    return dev.AudioEndpointVolume.MasterVolumeLevelScalar;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("Excepion: " + ex.Message);
            }
            return 1.0f;
        }

        public void SetVolumeNAudio(float volumeScalar)
        {
            try
            {
                NAudio.CoreAudioApi.MMDeviceEnumerator MMDE = new NAudio.CoreAudioApi.MMDeviceEnumerator();
                NAudio.CoreAudioApi.MMDeviceCollection DevCol = MMDE.EnumerateAudioEndPoints(NAudio.CoreAudioApi.DataFlow.All, NAudio.CoreAudioApi.DeviceState.Active);
                foreach (NAudio.CoreAudioApi.MMDevice dev in DevCol)
                {
                    dev.AudioEndpointVolume.MasterVolumeLevelScalar = volumeScalar;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("Excepion: " + ex.Message);
            }
        }
    }
}