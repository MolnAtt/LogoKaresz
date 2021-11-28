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
		const bool fel = false;
		const bool le = true;
		private static Avatar defaultkaresz;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		public Form1()
		{
			InitializeComponent();
			rajzlap = new Bitmap(képkeret.Width, képkeret.Height);

			defaultkaresz = new Avatar(this, new Pont(400, 400), 90);

		}

		private bool Kilépek_e_a_pályáról(double d) => defaultkaresz.Kilépek_e_a_pályáról(d);
		private void Előre(double d) => defaultkaresz.Előre(d);
		private void Hátra(double d) => defaultkaresz.Hátra(d);
		private void Jobbra(double d) => defaultkaresz.Jobbra(d);
		private void Balra(double d) => defaultkaresz.Balra(d);
		private void Fordulj(double d) => defaultkaresz.Fordulj(d);
		private void Lépj(double d) => defaultkaresz.Lépj(d);
		private void Pihi(int i) => defaultkaresz.Pihi(i);
		private void Tollat(bool b) => defaultkaresz.Tollat(b);
		private void Tölt(Color c) => defaultkaresz.Tölt(c);

		private void Ív(double f, double r) => defaultkaresz.Ív(f, r);
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
            this.képkeret = new System.Windows.Forms.PictureBox();
            this.startgomb = new System.Windows.Forms.Button();
            this.dlx = new System.Windows.Forms.Label();
            this.dly = new System.Windows.Forms.Label();
            this.dli = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.képkeret)).BeginInit();
            this.SuspendLayout();
            // 
            // képkeret
            // 
            this.képkeret.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.képkeret.Location = new System.Drawing.Point(8, 7);
            this.képkeret.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.képkeret.Name = "képkeret";
            this.képkeret.Size = new System.Drawing.Size(742, 446);
            this.képkeret.TabIndex = 0;
            this.képkeret.TabStop = false;
            this.képkeret.Click += new System.EventHandler(this.képkeret_Click);
            // 
            // startgomb
            // 
            this.startgomb.Location = new System.Drawing.Point(754, 7);
            this.startgomb.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 459);
            this.Controls.Add(this.dli);
            this.Controls.Add(this.dly);
            this.Controls.Add(this.dlx);
            this.Controls.Add(this.startgomb);
            this.Controls.Add(this.képkeret);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
		public class Frissítés : IDisposable
		{
			bool fr_regi;
			Avatar a;
			public Frissítés(Avatar a, bool fr)
			{
				this.a = a;
				fr_regi = a.Állandó_frissítés;
				a.Állandó_frissítés = fr;
			}

			public Frissítés(bool fr) : this(defaultkaresz, fr) { }

			public void Dispose()
			{
				a.Állandó_frissítés = fr_regi;
				GC.SuppressFinalize(this);
			}
		}

		public class Rajzol : IDisposable
		{
			bool rajzole_regi;
			Avatar a;
			public Rajzol(Avatar a, bool rajzoljone)
			{
				this.a = a;
				rajzole_regi = a.rajzole;
				a.Tollat(rajzoljone);
			}

			public Rajzol(bool r) : this(defaultkaresz, r) { }

			public void Dispose()
			{
				a.Tollat(rajzole_regi);
				GC.SuppressFinalize(this);
			}
		}
	}
}

