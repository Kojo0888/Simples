using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace IamGoingToSleep
{
    public static class Volume
    {
        private const int APPCOMMAND_VOLUME_MUTE = 0x80000;
        private const int APPCOMMAND_VOLUME_UP = 0xA0000;
        private const int APPCOMMAND_VOLUME_DOWN = 0x90000;
        private const int WM_APPCOMMAND = 0x319;

        private static IntPtr Handle = Process.GetCurrentProcess().MainWindowHandle;

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessageW(IntPtr hWnd, int Msg,
            IntPtr wParam, IntPtr lParam);

        public static void Mute()
        {
            SendMessageW(Handle, WM_APPCOMMAND, Handle,
                (IntPtr)APPCOMMAND_VOLUME_MUTE);
        }

        public static void VolDown()
        {
            SendMessageW(Handle, WM_APPCOMMAND, Handle,
                (IntPtr)APPCOMMAND_VOLUME_DOWN);
        }

        public static void VolUp()
        {
            SendMessageW(Handle, WM_APPCOMMAND, Handle,
                (IntPtr)APPCOMMAND_VOLUME_UP);
        }

    }
}
