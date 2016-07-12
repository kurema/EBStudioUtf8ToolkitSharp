using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Runtime.InteropServices;

namespace FontDumpK
{
    public static class Conversion
    {
        [DllImport("Kernel32", CharSet = CharSet.Unicode, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        private static extern int LCMapStringEx(string lpLocaleName, uint dwMapFlags, string lpSrcStr, int cchSrc, char[] lpDestStr, int cchDest, LPNLSVERSIONINFO lpVersionInformation, object lpReserved, IntPtr sortHandle);

        [DllImport("Kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int LCMapString(int Locale, int dwMapFlags, string lpSrcStr, int cchSrc, string lpDestStr, int cchDest);

        [StructLayout(LayoutKind.Sequential)]
        private struct LPNLSVERSIONINFO
        {
            public uint dwNLSVersionInfoSize;
            public uint dwNLSVersion;
            public uint dwDefinedVersion;
        }

        public static string SimplifiedToTraditional(string str)
        {
            string result = new string(' ', str.Length);
            LCMapString(0x0800, 0x04000000, str, str.Length, result, result.Length);
            return result;
        }

    }
}
