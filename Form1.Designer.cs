using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace LogoKaresz
{
	partial class Form1
	{
		public Bitmap rajzlap;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		public Form1()
		{
			InitializeComponent();
			rajzlap = new Bitmap(képkeret.Width, képkeret.Height);

			defaultkaresz = new Avatar(this, new Pont(400, 400), 90);
            timer1.Interval = 5;
            timer2.Interval = 5;
		}

		private void startgomb_Click(object sender, EventArgs e)
		{
			FELADAT();
		}


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
            this.components = new System.ComponentModel.Container();
            this.képkeret = new System.Windows.Forms.PictureBox();
            this.startgomb = new System.Windows.Forms.Button();
            this.dlx = new System.Windows.Forms.Label();
            this.dly = new System.Windows.Forms.Label();
            this.dli = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.képkeret)).BeginInit();
            this.SuspendLayout();
            // 
            // képkeret
            // 
            this.képkeret.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.képkeret.Location = new System.Drawing.Point(8, 7);
            this.képkeret.Margin = new System.Windows.Forms.Padding(2);
            this.képkeret.Name = "képkeret";
            this.képkeret.Size = new System.Drawing.Size(742, 446);
            this.képkeret.TabIndex = 0;
            this.képkeret.TabStop = false;
            this.képkeret.Click += new System.EventHandler(this.képkeret_Click);
            // 
            // startgomb
            // 
            this.startgomb.Location = new System.Drawing.Point(754, 7);
            this.startgomb.Margin = new System.Windows.Forms.Padding(2);
            this.startgomb.Name = "startgomb";
            this.startgomb.Size = new System.Drawing.Size(86, 43);
            this.startgomb.TabIndex = 1;
            this.startgomb.Text = "START";
            this.startgomb.UseVisualStyleBackColor = true;
            this.startgomb.Click += new System.EventHandler(this.startgomb_Click);
            // 
            // dlx
            // 
            this.dlx.AutoSize = true;
            this.dlx.Location = new System.Drawing.Point(754, 68);
            this.dlx.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.dlx.Name = "dlx";
            this.dlx.Size = new System.Drawing.Size(35, 13);
            this.dlx.TabIndex = 2;
            this.dlx.Text = "label1";
            // 
            // dly
            // 
            this.dly.AutoSize = true;
            this.dly.Location = new System.Drawing.Point(754, 93);
            this.dly.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.dly.Name = "dly";
            this.dly.Size = new System.Drawing.Size(35, 13);
            this.dly.TabIndex = 3;
            this.dly.Text = "label1";
            // 
            // dli
            // 
            this.dli.AutoSize = true;
            this.dli.Location = new System.Drawing.Point(754, 120);
            this.dli.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.dli.Name = "dli";
            this.dli.Size = new System.Drawing.Size(35, 13);
            this.dli.TabIndex = 4;
            this.dli.Text = "label1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(763, 165);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(29, 32);
            this.button1.TabIndex = 5;
            this.button1.Text = "B";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button1_MouseDown);
            this.button1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button1_MouseUp);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(798, 165);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(29, 32);
            this.button2.TabIndex = 6;
            this.button2.Text = "J";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button2_MouseDown);
            this.button2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button2_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(777, 149);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "forgás";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 459);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dli);
            this.Controls.Add(this.dly);
            this.Controls.Add(this.dlx);
            this.Controls.Add(this.startgomb);
            this.Controls.Add(this.képkeret);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.képkeret)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		public System.Windows.Forms.PictureBox képkeret;
		private System.Windows.Forms.Button startgomb;
		public System.Windows.Forms.Label dlx;
		public System.Windows.Forms.Label dly;
		public System.Windows.Forms.Label dli;

		private void képkeret_Click(object sender, EventArgs e) => defaultkaresz.Teleport(((MouseEventArgs)e).Location);
        private void button1_MouseDown(object sender, MouseEventArgs e) => timer1.Start();
        private void button2_MouseDown(object sender, MouseEventArgs e) => timer2.Start();
        private void button1_MouseUp(object sender, MouseEventArgs e) => timer1.Stop();
        private void button2_MouseUp(object sender, MouseEventArgs e) => timer2.Stop();
        private void timer1_Tick(object sender, EventArgs e) => defaultkaresz.Balra(1);
        private void timer2_Tick(object sender, EventArgs e) => defaultkaresz.Jobbra(1);

        private Button button1;
        private Button button2;
        private Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
	}
}

