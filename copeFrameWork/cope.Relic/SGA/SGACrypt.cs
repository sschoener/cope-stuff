#region

using System;
using System.Runtime.InteropServices;

#endregion

namespace cope.Relic.SGA
{
    public static class SGACrypt
    {
        public static uint CRYPT_NEWKEYSET = 0x8;
        public static uint CRYPT_DELETEKEYSET = 0x10;
        public static uint CRYPT_MACHINE_KEYSET = 0x20;
        public static uint CRYPT_SILENT = 0x40;
        public static uint CRYPT_DEFAULT_CONTAINER_OPTIONAL = 0x80;
        public static uint CRYPT_VERIFYCONTEXT = 0xF0000000;
        public static uint PROV_RSA_FULL = 1;

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CryptAcquireContext(ref IntPtr hProv, string pszContainer,
                                                      string pszProvider, uint dwProvType, uint dwFlags);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CryptImportKey(IntPtr hProv, byte[] pbKeyData, UInt32 dwDataLen, IntPtr hPubKey,
                                                 UInt32 dwFlags, ref IntPtr hKey);

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CryptDecrypt(IntPtr hKey, IntPtr hHash, int Final, uint dwFlags, byte[] pbData,
                                               ref uint pdwDataLen);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool CryptDestroyKey(IntPtr phKey);

        [DllImport("advapi32.dll")]
        public static extern bool CryptReleaseContext(IntPtr hProv, uint dwFlags);
    }
}