﻿using System;
using System.Runtime.InteropServices;

namespace MyLab.SmevClient.Crypt
{
    [StructLayout(LayoutKind.Sequential)]
    public struct CRYPT_DATA_BLOB
    {
        public int cbData;
        public IntPtr pbData;
    }
}
