using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Locker.Client;
using Locker.Controllers;
using Microsoft.Win32;

namespace Locker
{
    public partial class LockForm : Form
    {
        enum CloseType
        {
            Exit,
            Work
        }


        public StartForm startForm;
        CloseType type;
        Server.Server server;
        LockFormController controller;

        [StructLayout(LayoutKind.Sequential)]
        private struct KBDLLHOOKSTRUCT
        {
            public Keys key;
            public int scanCode;
            public int flags;
            public int time;
            public IntPtr extra;
        }
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int id, LowLevelKeyboardProc callback, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hook);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hook, int nCode, IntPtr wp, IntPtr lp);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string name);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern short GetAsyncKeyState(Keys key);
        private IntPtr ptrHook;
        private LowLevelKeyboardProc objKeyboardProcess;

        private IntPtr captureKey(int nCode, IntPtr wp, IntPtr lp)
        {
            if (nCode >= 0)
            {
                KBDLLHOOKSTRUCT objKeyInfo = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lp, typeof(KBDLLHOOKSTRUCT));


                if (objKeyInfo.key == Keys.RWin
                    || objKeyInfo.key == Keys.LWin
                    || objKeyInfo.key == Keys.Tab && HasAltModifier(objKeyInfo.flags)
                    || objKeyInfo.key == Keys.Escape && (ModifierKeys & Keys.Control) == Keys.Control)
                {
                    return (IntPtr)1; // if 0 is returned then All the above keys will be enabled
                }
            }
            return CallNextHookEx(ptrHook, nCode, wp, lp);
        }

        bool _closed = true;


        bool HasAltModifier(int flags)
        {
            return (flags & 0x20) == 0x20;
        }

        public void DisableCtrlAltDelButtons()
        {
            string sk = @"Software\Microsoft\Windows\CurrentVersion\Policies\System";
            string _sk = @"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer";


            var rk = Registry.CurrentUser.CreateSubKey(sk);
            var _rk = Registry.CurrentUser.CreateSubKey(_sk);
            rk.SetValue("DisableChangePassword", 1, RegistryValueKind.DWord);
            rk.SetValue("DisableLockWorkstation", 1, RegistryValueKind.DWord);
            rk.SetValue("DisableTaskMgr", 1, RegistryValueKind.DWord);
            _rk.SetValue("NoLogoff", 1, RegistryValueKind.DWord);
            rk.Close();
            _rk.Close();
        }

        public LockForm()
        {
            InitializeComponent();
            server = Server.Server.getServer();
            server.Unlocked += Server_Unlocked;
            DisableCtrlAltDelButtons();
            controller = new LockFormController();
        }

        private void Server_Unlocked(int seconds)
        {
            WorkForm wf = new WorkForm();
            wf.lf = this;
            wf.Show();
            _closed = false;
            type = CloseType.Work;
            HttpClient.Get($"{controller.GetSettings().Server}/api/workstation/ChangeState?Id={Helpers.Helper.GetHwid()}&State={1}");
            //this.Invoke(new MethodInvoker(() =>
            //{
            // //  this.Close();
            //}));
        }

        private void Server_Locked()
        {
            throw new NotImplementedException();
        }

        private void LockForm_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
             this.WindowState = FormWindowState.Maximized;
            ProcessModule objCurrentModule = Process.GetCurrentProcess().MainModule;
            objKeyboardProcess = new LowLevelKeyboardProc(captureKey);
            ptrHook = SetWindowsHookEx(13, objKeyboardProcess, GetModuleHandle(objCurrentModule.ModuleName), 0);
        }

        private void LockForm_Closing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = _closed;
            if (startForm != null && !_closed)
            {
                if (type == CloseType.Exit)
                    startForm.Show();
                server.Unlocked -= Server_Unlocked;
            }
        }

        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LockPanel_DoubleClick(object sender, EventArgs e)
        {
            _closed = false;
        }

        private void LockForm_OnLeave(object sender, EventArgs e)
        {
            this.Activate();
            this.Focus();
        }

        private void LockForm_Activated(object sender, EventArgs e)
        {
            type = CloseType.Exit;
        }

        private void LockForm_Close(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }
}
