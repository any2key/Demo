using Locker.Controllers;
using Locker.Models.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Locker
{
    public partial class StartForm : Form
    {
        Locker.Server.Server server;
        StartFormController controller;
        Settings settings;
        LockForm lf;

        public StartForm()
        {
            InitializeComponent();
            controller = new StartFormController();
            settings = controller.GetSettings();
        }


        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void StartForm_OnLoad(object sender, EventArgs e)
        {
            server = Server.Server.getServer();
            serverTb.Text = settings.Server;
            progressIndicator.Visible = false;

        }

        private void connectBtn_Click(object sender, EventArgs e)
        {
            connectBtn.Enabled = false;
            progressIndicator.Visible = true;
            settings.Server = serverTb.Text;
            controller.SaveSettings(settings);


            ThreadStart start = new ThreadStart(() =>
            {
                var res = controller.Register();
                if (!res.IsOk)
                    MessageBox.Show(res.Message);
                else
                {

                    this.Invoke(new MethodInvoker(() =>
                    {
                        lf = new LockForm();
                        lf.startForm = this;
                        lf.Show();

                    }));

                    //this.Invoke(new MethodInvoker(() =>
                    //{
                    //    this.Hide();
                    //}));
                }
                connectBtn.Invoke(new MethodInvoker(() =>
                {
                    connectBtn.Enabled = true;
                }));
                //connectBtn.Enabled = true;
                progressIndicator.Invoke(new MethodInvoker(() =>
                {
                    progressIndicator.Visible = false;
                }));
                this.Invoke(new MethodInvoker(() =>
                {
                    this.Hide();
                }));
                //  progressIndicator.Visible = false;
            });
            Thread th = new Thread(start);
            th.Start();
        }

        
    }
}
