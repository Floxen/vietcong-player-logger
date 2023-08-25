using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VCLogger
{
    internal class TextBoxSender
    {
        private const int WM_SETTEXT = 0x000C;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private const int VK_RETURN = 0x0D;
        private const int BM_CLICK = 0x00F5;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, string lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetWindowText(IntPtr hWnd, IntPtr lpString, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, StringBuilder lParam);

        public void send(IntPtr hwnd, string message)
        {
            IntPtr editboxHwnd = FindWindowEx(hwnd, IntPtr.Zero, "Edit", null);

            if (message == "list")
            {
                if (hwnd != IntPtr.Zero)
                {
                    IntPtr hChild = FindWindowEx(hwnd, IntPtr.Zero, "Button", null);

                    if (hChild != IntPtr.Zero)
                    {
                        StringBuilder buttonText = new StringBuilder(256);

                        _ = GetWindowText(hChild, buttonText, buttonText.Capacity);

                        if (buttonText.ToString() == "Clear List")
                        {
                            _ = SendMessage(hChild, BM_CLICK, IntPtr.Zero, null);
                        }
                    }
                }
            }

            if (editboxHwnd != IntPtr.Zero)
            {
                SendMessage(editboxHwnd, WM_SETTEXT, IntPtr.Zero, message);
                SendMessage(editboxHwnd, 0x0201, IntPtr.Zero, null);
                SendMessage(editboxHwnd, 0x0202, IntPtr.Zero, null);
                PostMessage(editboxHwnd, WM_KEYDOWN, (IntPtr)VK_RETURN, IntPtr.Zero);
                PostMessage(editboxHwnd, WM_KEYUP, (IntPtr)VK_RETURN, IntPtr.Zero);
            }
        }
    }
}
