using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.InteropServices;

namespace ConfigLoader
{
    public class Client
    {
        public string Version;
        [XmlArrayItem("Address")]
        public string[] Configs;
        //public string MultiClient = string.Empty;
    }

    public class Settings
    {
        public int Engine;
        public bool MultiClient;

        public Settings()
        {
            Engine = -1;
        }
    }

    public class ConfigFile
    {
        public Settings Settings = new Settings();
        public List<Client> Clients = new List<Client>();

        public Client LoadConfig(string fileVersion)
        {
            var client = Clients.Find(c => c.Version.Equals(fileVersion));
            if (client != null)
                return client;
            return UpdateConfigFile(fileVersion);
        }

        public Client UpdateConfigFile(string fileVersion)
        {
            List<string> list = new List<string>();
            Win32.STARTUPINFO si = new Win32.STARTUPINFO();
            Win32.PROCESS_INFORMATION pi = new Win32.PROCESS_INFORMATION();
            Win32.CreateProcess("Tibia.exe", null, IntPtr.Zero, IntPtr.Zero, true,
                Win32.CREATE_SUSPENDED, IntPtr.Zero, null, ref si, out pi);

            uint baseAddress = 0;
            IntPtr threadId = new IntPtr();
            IntPtr hThread = Win32.CreateRemoteThread(pi.hProcess, IntPtr.Zero, 0, Win32.GetProcAddress(Win32.GetModuleHandle("Kernel32"), "GetModuleHandleA"), IntPtr.Zero, 0, out threadId);
            Win32.WaitForSingleObject(hThread, Win32.INFINITE);
            Win32.GetExitCodeThread(hThread, out baseAddress);
            Win32.CloseHandle(hThread);

            IntPtr read;

            Win32.IMAGE_DOS_HEADER image_dos_header = Win32.ReadStruct<Win32.IMAGE_DOS_HEADER>(pi.hProcess, baseAddress, 64, out read);
            Win32.IMAGE_NT_HEADERS32 image_nt_headers = Win32.ReadStruct<Win32.IMAGE_NT_HEADERS32>(pi.hProcess, (uint)(baseAddress + image_dos_header.e_lfanew), 248, out read);

            uint end = baseAddress + image_nt_headers.OptionalHeader.SizeOfImage, addrToCfg = 0;

            int index = 0;
            byte[] buffer = new byte[1024];
            string tibiacfg = "Tibia.cfg";
            for (uint current = baseAddress; current < end; )
            {
                uint bytesToRead = end - current;
                if (bytesToRead > 1024)
                    bytesToRead = 1024;
                Win32.ReadProcessMemory(pi.hProcess, current, buffer, bytesToRead, out read);

                int loop = read.ToInt32();
                for (int i = 0; i < loop; i++)
                {
                    if (buffer[i] == tibiacfg[index])
                    {
                        index++;
                        if (index == tibiacfg.Length)
                        {
                            addrToCfg = (uint)(current + i - index + 1);
                            goto gotcha;
                        }
                    }
                    else
                    {
                        index = 0;
                    }
                }

                current += bytesToRead;
            }
        gotcha:
            if (addrToCfg != 0)
            {
                index = 0;
                byte[] baddr = BitConverter.GetBytes(addrToCfg);
                for (uint current = baseAddress; current < end; )
                {
                    uint bytesToRead = end - current;
                    if (bytesToRead > 1024)
                        bytesToRead = 1024;
                    Win32.ReadProcessMemory(pi.hProcess, current, buffer, bytesToRead, out read);

                    int loop = read.ToInt32();
                    for (int i = 0; i < loop; i++)
                    {
                        if (buffer[i] == baddr[index])
                        {
                            index++;
                            if (index == 4)
                            {
                                list.Add("0x" + (current + i - index - baseAddress).ToString("X8"));
                                index = 0;
                            }
                        }
                        else
                        {
                            index = 0;
                        }
                    }

                    current += bytesToRead;
                }
            }

            Win32.TerminateProcess(pi.hProcess, 0);
            Win32.CloseHandle(pi.hProcess);
            Win32.CloseHandle(pi.hThread);
            if (list.Count > 0)
            {
                var client = new Client { Version = fileVersion, Configs = list.ToArray() };
                Clients.Add(client);
                Clients.Sort(delegate(Client c1, Client c2) { return c2.Version.CompareTo(c1.Version); });
                Serialize();
                return client;
            }
            return null;
        }

        public void Serialize()
        {
            var xs = new XmlSerializer(typeof(ConfigFile));
            using(var tw = new FileStream("Config.xml", FileMode.Create, FileAccess.Write))
                xs.Serialize(tw, this);
        }

        public static ConfigFile Deserialize()
        {
			try
			{
				ConfigFile ret = null;
				var xs = new XmlSerializer(typeof(ConfigFile));
				using (var tr = new FileStream("Config.xml", FileMode.Open, FileAccess.ReadWrite))
					ret = xs.Deserialize(tr) as ConfigFile;
                if (ret == null)
                    ret = new ConfigFile();
				return ret;
			}
			catch (FileNotFoundException)
			{
			}
			catch (InvalidOperationException ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message);
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message);
			}
            return new ConfigFile();
        }
    }
}
