namespace SETUNA
{
	// Token: 0x02000037 RID: 55
	sealed partial class Mainform
	{
		// Token: 0x060001EC RID: 492 RVA: 0x0000A164 File Offset: 0x00008364
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000A184 File Offset: 0x00008384
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mainform));
            this.button1 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.timPool = new System.Windows.Forms.Timer(this.components);
            this.windowTimer = new System.Windows.Forms.Timer(this.components);
            this.setunaIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.setunaIconMenu = new SETUNA.Main.ContextStyleMenuStrip(this.components);
            this.subMenu = new SETUNA.Main.ContextStyleMenuStrip(this.components);
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.delayInitTimer = new System.Windows.Forms.Timer(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.WorkList = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button10 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.subMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.Font = new System.Drawing.Font("Microsoft YaHei", 14F);
            this.button1.ForeColor = System.Drawing.Color.Gray;
            this.button1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(71, 51);
            this.button1.TabIndex = 0;
            this.button1.Text = "截取";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.button4.ForeColor = System.Drawing.Color.Gray;
            this.button4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button4.Location = new System.Drawing.Point(89, 12);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(71, 51);
            this.button4.TabIndex = 4;
            this.button4.Text = "选项";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // timPool
            // 
            this.timPool.Tick += new System.EventHandler(this.timPool_Tick);
            // 
            // windowTimer
            // 
            this.windowTimer.Enabled = true;
            this.windowTimer.Interval = 500;
            this.windowTimer.Tick += new System.EventHandler(this.window_Tick);
            // 
            // setunaIcon
            // 
            this.setunaIcon.ContextMenuStrip = this.setunaIconMenu;
            this.setunaIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("setunaIcon.Icon")));
            this.setunaIcon.Text = "SETUNA2";
            this.setunaIcon.DoubleClick += new System.EventHandler(this.setunaIcon_MouseDoubleClick);
            this.setunaIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.setunaIcon_MouseClick);
            // 
            // setunaIconMenu
            // 
            this.setunaIconMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.setunaIconMenu.Name = "setunaIconMenu";
            this.setunaIconMenu.Scrap = null;
            this.setunaIconMenu.Size = new System.Drawing.Size(61, 4);
            this.setunaIconMenu.Opening += new System.ComponentModel.CancelEventHandler(this.setunaIconMenu_Opening);
            // 
            // subMenu
            // 
            this.subMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.subMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem});
            this.subMenu.Name = "subMenu";
            this.subMenu.Scrap = null;
            this.subMenu.Size = new System.Drawing.Size(98, 26);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(97, 22);
            this.testToolStripMenuItem.Text = "test";
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ShowAlways = true;
            this.toolTip1.StripAmpersands = true;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // delayInitTimer
            // 
            this.delayInitTimer.Interval = 1000;
            this.delayInitTimer.Tick += new System.EventHandler(this.delayInitTimer_Tick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(166, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(71, 51);
            this.button2.TabIndex = 5;
            this.button2.Text = "整合";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // WorkList
            // 
            this.WorkList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.WorkList.FormattingEnabled = true;
            this.WorkList.ItemHeight = 12;
            this.WorkList.Location = new System.Drawing.Point(0, 77);
            this.WorkList.Name = "WorkList";
            this.WorkList.Size = new System.Drawing.Size(682, 292);
            this.WorkList.TabIndex = 6;
            this.WorkList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.WorkList_MouseDoubleClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.checkBox2);
            this.panel1.Controls.Add(this.button10);
            this.panel1.Controls.Add(this.button9);
            this.panel1.Controls.Add(this.button8);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.button7);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.WorkList);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(682, 369);
            this.panel1.TabIndex = 7;
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(486, 12);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(75, 23);
            this.button10.TabIndex = 15;
            this.button10.Text = "重复检测";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(405, 40);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 14;
            this.button9.Text = "读取存档";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(405, 12);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 13;
            this.button8.Text = "重新扫描";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(486, 44);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 12;
            this.checkBox1.Text = "自动模式";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(324, 40);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 11;
            this.button7.Text = "删除";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click_1);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(324, 12);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 10;
            this.button6.Text = "添加";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click_1);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(243, 40);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 9;
            this.button5.Text = "静态识别";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(243, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "打开";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(564, 44);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(60, 16);
            this.checkBox2.TabIndex = 8;
            this.checkBox2.Text = "压缩包";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // Mainform
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(682, 369);
            this.ContextMenuStrip = this.setunaIconMenu;
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(100, 60);
            this.Name = "Mainform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SETUNA";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Mainform_FormClosing);
            this.Load += new System.EventHandler(this.Mainform_Load);
            this.Shown += new System.EventHandler(this.Mainform_Shown);
            this.subMenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

		}

		// Token: 0x040000D8 RID: 216
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040000D9 RID: 217
		private global::System.Windows.Forms.Button button1;

		// Token: 0x040000DA RID: 218
		private global::System.Windows.Forms.Button button4;

		// Token: 0x040000DB RID: 219
		private global::System.Windows.Forms.NotifyIcon setunaIcon;

		// Token: 0x040000DC RID: 220
		private global::SETUNA.Main.ContextStyleMenuStrip setunaIconMenu;

		// Token: 0x040000DD RID: 221
		private global::SETUNA.Main.ContextStyleMenuStrip subMenu;

		// Token: 0x040000DE RID: 222
		private global::System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;

		// Token: 0x040000DF RID: 223
		private global::System.Windows.Forms.Timer timPool;

		// Token: 0x040000E0 RID: 224
		private global::System.Windows.Forms.ToolTip toolTip1;

        private global::System.Windows.Forms.Timer windowTimer;
        private System.Windows.Forms.Timer delayInitTimer;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.ListBox WorkList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.CheckBox checkBox2;
    }
}
