using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ConfigLoader
{
    public partial class ConfigLoader : Form
    {
        private ConfigFile configFile;
        private Client config;
        private bool modified = false;
        public bool Loaded = false;

        private string dir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Tibia";

        public ConfigLoader()
        {
            InitializeComponent();
            try
            {
                Loaded = InitAddresses();
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("Error: Could not locate " + ex.Message, "ConfigLoader",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            if (Loaded)
                RefreshList();
        }

        private bool InitAddresses()
        {
            FileVersionInfo verInfo = FileVersionInfo.GetVersionInfo("Tibia.exe");

            configFile = ConfigFile.Deserialize();
            switch (configFile.Settings.Engine)
            {
                case 0: dX5ToolStripMenuItem.Checked = true; break;
                case 1: oGLToolStripMenuItem.Checked = true; break;
                case 2: dX9ToolStripMenuItem.Checked = true; break;
            }
            multiClientToolStripMenuItem.Checked = configFile.Settings.MultiClient;
            config = configFile.LoadConfig(verInfo.FileVersion);
			if (config == null)
			{
				MessageBox.Show("Tibia: " + verInfo.FileVersion + "\nUnsupported Tibia version!",
					"ConfigLoader", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
            this.Text = "CL - Tibia " + verInfo.FileVersion;
            return true;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (modified)
                configFile.Serialize();
            base.OnClosing(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartClient();
        }
        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            StartClient();
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                StartClient();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void RefreshList()
        {
            DirectoryInfo di = new DirectoryInfo(dir);
            FileInfo[] files = di.GetFiles("*.cfg");
            listBox1.Items.Clear();
            for (int i = 0; i < files.Length; i++)
                listBox1.Items.Add(files[i].Name);
            if (files.Length > 0)
                listBox1.SelectedIndex = 0;
        }

        private void StartClient()
        {
            if (listBox1.SelectedIndex == -1)
                return;
            string fileName = listBox1.Items[listBox1.SelectedIndex].ToString();
            string cmdLine = "";
            if (configFile.Settings.Engine >= 0)
            {
                cmdLine += " engine " + configFile.Settings.Engine;
            }

            Win32.STARTUPINFO si = new Win32.STARTUPINFO();
            Win32.PROCESS_INFORMATION pi = new Win32.PROCESS_INFORMATION();
            Win32.CreateProcess("Tibia.exe", cmdLine, IntPtr.Zero, IntPtr.Zero, true,
                Win32.CREATE_SUSPENDED, IntPtr.Zero, null, ref si, out pi);

			uint baseAddress = 0;
			IntPtr threadId = new IntPtr();
			IntPtr hThread = Win32.CreateRemoteThread(pi.hProcess, IntPtr.Zero, 0, Win32.GetProcAddress(Win32.GetModuleHandle("Kernel32"), "GetModuleHandleA"), IntPtr.Zero, 0, out threadId);
			Win32.WaitForSingleObject(hThread, Win32.INFINITE);
			Win32.GetExitCodeThread(hThread, out baseAddress);
			Win32.CloseHandle(hThread);

            uint temp = Win32.VirtualAllocEx(pi.hProcess, 0, (uint)fileName.Length, Win32.MEM_COMMIT | Win32.MEM_RESERVE, Win32.PAGE_READWRITE);
            Win32.WriteProcessMemory(pi.hProcess, temp, ASCIIEncoding.ASCII.GetBytes(fileName), (uint)fileName.Length, IntPtr.Zero);

            foreach (string address in config.Configs)
				Win32.WriteProcessMemory(pi.hProcess, Convert.ToUInt32(address, 16) + 1 + baseAddress, BitConverter.GetBytes(temp), 4, IntPtr.Zero);

            if (configFile.Settings.MultiClient)
            {
                IntPtr read;

                Win32.IMAGE_DOS_HEADER image_dos_header = Win32.ReadStruct<Win32.IMAGE_DOS_HEADER>(pi.hProcess, baseAddress, 64, out read);
                Win32.IMAGE_NT_HEADERS32 image_nt_headers = Win32.ReadStruct<Win32.IMAGE_NT_HEADERS32>(pi.hProcess, (uint)(baseAddress + image_dos_header.e_lfanew), 248, out read);
                Win32.IMAGE_IMPORT_DESCRIPTOR image_import_descriptor = Win32.ReadStruct<Win32.IMAGE_IMPORT_DESCRIPTOR>(pi.hProcess, (baseAddress + image_nt_headers.OptionalHeader.DataDirectory[1].VirtualAddress), 20, out read);

                uint iterator1 = 1, iterator2 = 1;
                while (image_import_descriptor.FirstThunk != 0)
                {

                    string name = Win32.ReadString(pi.hProcess, (baseAddress + image_import_descriptor.Name), 32, out read);
                    if (name.Equals("KERNEL32.DLL", StringComparison.CurrentCultureIgnoreCase))
                    {
                        Win32.IMAGE_THUNK_DATA image_thunk_data = Win32.ReadStruct<Win32.IMAGE_THUNK_DATA>(pi.hProcess, (baseAddress + image_import_descriptor.OriginalFirstThunk), 4, out read);

                        while (image_thunk_data.AddressOfData != 0)
                        {
                            name = Win32.ReadString(pi.hProcess, (baseAddress + image_thunk_data.AddressOfData + 2), 32, out read);
                            if (name.Equals("CreateMutexA", StringComparison.CurrentCultureIgnoreCase))
                            {
                                byte[] retn = { 0xC2, 0x0C, 0x00 };
                                uint replaceFunction = Win32.VirtualAllocEx(pi.hProcess, 0, 3, Win32.MEM_COMMIT | Win32.MEM_RESERVE, Win32.PAGE_EXECUTE_READWRITE);
                                Win32.WriteProcessMemory(pi.hProcess, replaceFunction, retn, 3, IntPtr.Zero);

                                uint createMutexA = (baseAddress + image_import_descriptor.FirstThunk + ((iterator2 - 1) * 4));
                                uint oldProtect;
                                Win32.VirtualProtectEx(pi.hProcess, createMutexA, 4, Win32.PAGE_READWRITE, out oldProtect);
                                Win32.WriteProcessMemory(pi.hProcess, createMutexA, BitConverter.GetBytes(replaceFunction), 4, IntPtr.Zero);
                                Win32.VirtualProtectEx(pi.hProcess, createMutexA, 4, oldProtect, out oldProtect);
                                break;
                            }
                            image_thunk_data = Win32.ReadStruct<Win32.IMAGE_THUNK_DATA>(pi.hProcess, (baseAddress + image_import_descriptor.OriginalFirstThunk + (4 * iterator2++)), 4, out read);
                        }
                        break;
                    }
                    image_import_descriptor = Win32.ReadStruct<Win32.IMAGE_IMPORT_DESCRIPTOR>(pi.hProcess, (baseAddress + image_nt_headers.OptionalHeader.DataDirectory[1].VirtualAddress + (20 * iterator1++)), 20, out read);
                }
            }

            Win32.ResumeThread(pi.hThread);
        }

        private void engineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tmp = sender as ToolStripMenuItem;
            bool check = tmp.Checked;
            dX9ToolStripMenuItem.Checked = false;
            dX5ToolStripMenuItem.Checked = false;
            oGLToolStripMenuItem.Checked = false;
            tmp.Checked = check;

            modified = true;
            if (dX9ToolStripMenuItem.Checked)
                configFile.Settings.Engine = 2;
            else if (oGLToolStripMenuItem.Checked)
                configFile.Settings.Engine = 1;
            else if (dX5ToolStripMenuItem.Checked)
                configFile.Settings.Engine = 0;
            else
                configFile.Settings.Engine = -1;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream stream;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Config files (*.cfg)|*.cfg";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.InitialDirectory = dir;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if ((stream = saveFileDialog.OpenFile()) != null)
                    stream.Close();
            }
            RefreshList();
        }

        private void multiClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tmp = sender as ToolStripMenuItem;
            configFile.Settings.MultiClient = tmp.Checked;
            modified = true;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ConfigLoader Version 1.7\nWebsite: Stiju.com\n© Stiju 2008-2013", "About");
        }
    }
}