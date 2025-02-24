using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Collections;

namespace OfflineRetailV2.Data
{
    public class RawPrinterHelper
    {

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        struct ByteStruct
        {
            public Int32 val;
        }
        // Structure and API declarions:

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]

        public class DOCINFOA
        {

            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;

            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;

            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;

        }

        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true,
        CharSet = CharSet.Ansi, ExactSpelling = true,
        CallingConvention = CallingConvention.StdCall)]

        public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)]
string szPrinter, out IntPtr hPrinter, long pd);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true,
        ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]

        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true,
        CharSet = CharSet.Ansi, ExactSpelling = true,
        CallingConvention = CallingConvention.StdCall)]

        public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level,
        [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true,
        ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]

        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true,
        ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]

        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true,
        ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]

        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true,
        ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]

        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32
        dwCount, out Int32 dwWritten);

        [DllImport("kernel32.dll", EntryPoint = "GetLastError", SetLastError = false,
        ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]

        public static extern Int32 GetLastError();

        public static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes,
        Int32 dwCount)
        {

            Int32 dwError = 0, dwWritten = 0;

            IntPtr hPrinter = new IntPtr(0);

            DOCINFOA di = new DOCINFOA();

            bool bSuccess = false; // Assume failure unless you specifically succeed.

            di.pDocName = "My C#.NET RAW Document";

            di.pDataType = "RAW";

            if (OpenPrinter(szPrinterName, out hPrinter, 0))
            {

                if (StartDocPrinter(hPrinter, 1, di))
                {

                    if (StartPagePrinter(hPrinter))
                    {

                        bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);

                        EndPagePrinter(hPrinter);

                    }

                    EndDocPrinter(hPrinter);

                }

                ClosePrinter(hPrinter);

            }

            if (bSuccess == false)
            {

                dwError = GetLastError();

            }

            return bSuccess;

        }

        public static bool SendFileToPrinter(string szPrinterName, string
        szFileName)
        {

            FileStream fs = new FileStream(szFileName, FileMode.Open);

            BinaryReader br = new BinaryReader(fs);

            Byte[] bytes = new Byte[fs.Length];

            bool bSuccess = false;

            IntPtr pUnmanagedBytes = new IntPtr(0);

            int nLength;

            nLength = Convert.ToInt32(fs.Length);

            bytes = br.ReadBytes(nLength);

            pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength);

            Marshal.Copy(bytes, 0, pUnmanagedBytes, nLength);

            bSuccess = SendBytesToPrinter(szPrinterName, pUnmanagedBytes, nLength);

            Marshal.FreeCoTaskMem(pUnmanagedBytes);

            return bSuccess;

        }

        public static bool SendStringToPrinter(string szPrinterName, string
        szString)
        {
            IntPtr pBytes;

            Int32 dwCount;

            dwCount = szString.Length;

            pBytes = Marshal.StringToCoTaskMemAnsi(szString);

            SendBytesToPrinter(szPrinterName, pBytes, dwCount);

            Marshal.FreeCoTaskMem(pBytes);

            return true;
        }

        public static bool OpenCashDrawer1(string szPrinterName, string commandcode)
        {
            Int32 dwError = 0, dwWritten = 0;

            IntPtr hPrinter = new IntPtr(0);

            DOCINFOA di = new DOCINFOA();

            bool bSuccess = false;

            di.pDocName = "OpenDrawer";

            di.pDataType = "RAW";

            if (OpenPrinter(szPrinterName, out hPrinter, 0))
            {
                if (StartDocPrinter(hPrinter, 1, di))
                {
                    if (StartPagePrinter(hPrinter))
                    {
                        int nLength;
                        String[] sbuffer = commandcode.Split(',');
                        byte[] DrawerOpen = new byte[5];

                        StrToByteArray(commandcode, ref DrawerOpen);

                        nLength = DrawerOpen.Length;

                        IntPtr p = Marshal.AllocCoTaskMem(nLength);

                        Marshal.Copy(DrawerOpen, 0, p, nLength);

                        bSuccess = WritePrinter(hPrinter, p, DrawerOpen.Length, out dwWritten);

                        EndPagePrinter(hPrinter);

                        Marshal.FreeCoTaskMem(p);
                    }
                    EndDocPrinter(hPrinter);
                }
                ClosePrinter(hPrinter);
            }

            if (bSuccess == false)
            {
                dwError = GetLastError();
            }
            return bSuccess;
        }

        public static void StrToByteArray(string str, ref byte[] refbyte)
        {

            ArrayList arrList = new ArrayList();
            ByteStruct bst = new ByteStruct();
            String[] sbuffer = str.Split(',');
            int[] ints = new int[sbuffer.Length];
            List<int> list = new List<int>();
            int i = 0;
            foreach (string s in sbuffer)
            {
                bst.val = Convert.ToInt32(s);
                refbyte[i] = (byte)bst.val;
                ints[i] = Convert.ToInt32(s);
                list.Add(Convert.ToInt32(s));
                i++;
            }
            arrList.AddRange(ints);

        }

        public static byte[] RawSerialize(object anything)
        {
            int rawsize = Marshal.SizeOf(anything);
            IntPtr buffer = Marshal.AllocHGlobal(rawsize);
            Marshal.StructureToPtr(anything, buffer, false);
            byte[] rawdatas = new byte[rawsize];
            Marshal.Copy(buffer, rawdatas, 0, rawsize);
            Marshal.FreeHGlobal(buffer);
            return rawdatas;
        }

        public static bool FullCut(string szPrinterName)
        {

            Int32 dwError = 0, dwWritten = 0;

            IntPtr hPrinter = new IntPtr(0);

            DOCINFOA di = new DOCINFOA();

            bool bSuccess = false;

            di.pDocName = "FullCut";

            di.pDataType = "RAW";

            if (OpenPrinter(szPrinterName, out hPrinter, 0))
            {

                if (StartDocPrinter(hPrinter, 1, di))
                {

                    if (StartPagePrinter(hPrinter))
                    {

                        int nLength;

                        byte[] DrawerOpen = new byte[] { 27, 100, 51 };

                        nLength = DrawerOpen.Length;

                        IntPtr p = Marshal.AllocCoTaskMem(nLength);

                        Marshal.Copy(DrawerOpen, 0, p, nLength);

                        bSuccess = WritePrinter(hPrinter, p, DrawerOpen.Length, out dwWritten);

                        EndPagePrinter(hPrinter);

                        Marshal.FreeCoTaskMem(p);

                    }

                    EndDocPrinter(hPrinter);

                }

                ClosePrinter(hPrinter);

            }

            if (bSuccess == false)
            {

                dwError = GetLastError();

            }

            return bSuccess;

        }

    }
}
