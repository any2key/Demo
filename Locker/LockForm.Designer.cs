
namespace Locker
{
    partial class LockForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Guna2GradientPanel lockPanel;
            lockPanel = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.SuspendLayout();
            // 
            // lockPanel
            // 
            lockPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            lockPanel.AutoSize = true;
            lockPanel.BackColor = System.Drawing.Color.Transparent;
            lockPanel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            lockPanel.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            lockPanel.Location = new System.Drawing.Point(-65, -34);
            lockPanel.Name = "lockPanel";
            lockPanel.ShadowDecoration.Parent = lockPanel;
            lockPanel.Size = new System.Drawing.Size(885, 489);
            lockPanel.TabIndex = 0;
            lockPanel.UseTransparentBackground = true;
            lockPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.guna2GradientPanel1_Paint);
            lockPanel.DoubleClick += new System.EventHandler(this.LockPanel_DoubleClick);
            // 
            // LockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(lockPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LockForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LockForm";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.LockForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LockForm_Closing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LockForm_Close);
            this.Load += new System.EventHandler(this.LockForm_Load);
            this.Leave += new System.EventHandler(this.LockForm_OnLeave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}