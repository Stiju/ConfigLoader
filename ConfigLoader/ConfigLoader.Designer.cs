namespace ConfigLoader
{
    partial class ConfigLoader
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.engineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.multiClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.dX9ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dX5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oGLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.SystemColors.Window;
            this.listBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(0, 24);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(163, 160);
            this.listBox1.TabIndex = 0;
            this.listBox1.UseTabStops = false;
            this.listBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox1_KeyDown);
            this.listBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseDoubleClick);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Image = global::ConfigLoader.Properties.Resources.Refresh;
            this.button2.Location = new System.Drawing.Point(1, 186);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 64);
            this.button2.TabIndex = 1;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Image = global::ConfigLoader.Properties.Resources.Start;
            this.button1.Location = new System.Drawing.Point(82, 186);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 64);
            this.button1.TabIndex = 2;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.engineToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(163, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.newToolStripMenuItem.Text = "&New...";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // engineToolStripMenuItem
            // 
            this.engineToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.multiClientToolStripMenuItem,
            this.toolStripMenuItem1,
            this.dX9ToolStripMenuItem,
            this.dX5ToolStripMenuItem,
            this.oGLToolStripMenuItem});
            this.engineToolStripMenuItem.Name = "engineToolStripMenuItem";
            this.engineToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.engineToolStripMenuItem.Text = "&Engine";
            // 
            // multiClientToolStripMenuItem
            // 
            this.multiClientToolStripMenuItem.CheckOnClick = true;
            this.multiClientToolStripMenuItem.Name = "multiClientToolStripMenuItem";
            this.multiClientToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.multiClientToolStripMenuItem.Text = "MultiClient";
            this.multiClientToolStripMenuItem.Click += new System.EventHandler(this.multiClientToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // dX9ToolStripMenuItem
            // 
            this.dX9ToolStripMenuItem.CheckOnClick = true;
            this.dX9ToolStripMenuItem.Name = "dX9ToolStripMenuItem";
            this.dX9ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.dX9ToolStripMenuItem.Text = "DX&9";
            this.dX9ToolStripMenuItem.Click += new System.EventHandler(this.engineToolStripMenuItem_Click);
            // 
            // dX5ToolStripMenuItem
            // 
            this.dX5ToolStripMenuItem.CheckOnClick = true;
            this.dX5ToolStripMenuItem.Name = "dX5ToolStripMenuItem";
            this.dX5ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.dX5ToolStripMenuItem.Text = "DX&5";
            this.dX5ToolStripMenuItem.Click += new System.EventHandler(this.engineToolStripMenuItem_Click);
            // 
            // oGLToolStripMenuItem
            // 
            this.oGLToolStripMenuItem.CheckOnClick = true;
            this.oGLToolStripMenuItem.Name = "oGLToolStripMenuItem";
            this.oGLToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.oGLToolStripMenuItem.Text = "&OGL";
            this.oGLToolStripMenuItem.Click += new System.EventHandler(this.engineToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // ConfigLoader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(163, 251);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ConfigLoader";
            this.Text = "ConfigLoader";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem engineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dX9ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dX5ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oGLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem multiClientToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}

