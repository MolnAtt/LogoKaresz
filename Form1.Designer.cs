namespace LogoKaresz
{
	partial class Form1
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
			this.képkeret = new System.Windows.Forms.PictureBox();
			this.startgomb = new System.Windows.Forms.Button();
			this.dlx = new System.Windows.Forms.Label();
			this.dly = new System.Windows.Forms.Label();
			this.dli = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.képkeret)).BeginInit();
			this.SuspendLayout();
			// 
			// rajzlap
			// 
			this.képkeret.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.képkeret.Location = new System.Drawing.Point(10, 9);
			this.képkeret.Name = "rajzlap";
			this.képkeret.Size = new System.Drawing.Size(989, 549);
			this.képkeret.TabIndex = 0;
			this.képkeret.TabStop = false;
			this.képkeret.Paint += new System.Windows.Forms.PaintEventHandler(this.rajzlap_Paint);
			// 
			// startgomb
			// 
			this.startgomb.Location = new System.Drawing.Point(1005, 9);
			this.startgomb.Name = "startgomb";
			this.startgomb.Size = new System.Drawing.Size(114, 53);
			this.startgomb.TabIndex = 1;
			this.startgomb.Text = "START";
			this.startgomb.UseVisualStyleBackColor = true;
			this.startgomb.Click += new System.EventHandler(this.startgomb_Click);
			// 
			// dlx
			// 
			this.dlx.AutoSize = true;
			this.dlx.Location = new System.Drawing.Point(1005, 84);
			this.dlx.Name = "dlx";
			this.dlx.Size = new System.Drawing.Size(46, 17);
			this.dlx.TabIndex = 2;
			this.dlx.Text = "label1";
			// 
			// dly
			// 
			this.dly.AutoSize = true;
			this.dly.Location = new System.Drawing.Point(1005, 115);
			this.dly.Name = "dly";
			this.dly.Size = new System.Drawing.Size(46, 17);
			this.dly.TabIndex = 3;
			this.dly.Text = "label1";
			// 
			// dli
			// 
			this.dli.AutoSize = true;
			this.dli.Location = new System.Drawing.Point(1005, 148);
			this.dli.Name = "dli";
			this.dli.Size = new System.Drawing.Size(46, 17);
			this.dli.TabIndex = 4;
			this.dli.Text = "label1";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1131, 565);
			this.Controls.Add(this.dli);
			this.Controls.Add(this.dly);
			this.Controls.Add(this.dlx);
			this.Controls.Add(this.startgomb);
			this.Controls.Add(this.képkeret);
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
	}
}

