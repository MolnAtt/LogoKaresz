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
	public class Avatar
	{
		private Pont hely;
		private double irány;
		Form1 szülőform;
		private PictureBox avatarpb;
		Pen toll;
		Graphics gr;
		int w;
		int h;
		public bool rajzole;
		int varakozas;
		private bool állandó_frissítés;
		public bool Állandó_frissítés
		{ 
			get => állandó_frissítés;
			set 
			{
				állandó_frissítés = value;
				Frissít();
			}
		}


		public Avatar(Form1 szülőform, Pont hely, double irány)
		{
			this.szülőform = szülőform;
			this.hely = hely;
			this.irány = irány;
			this.toll = new Pen(Color.Black);
			this.gr = Graphics.FromImage(szülőform.rajzlap); // formnak itt már lennie kell!
			this.w = 30;
			this.h = 30;
			this.rajzole = true;
			this.varakozas = 0;
			this.állandó_frissítés = true;



			avatarpb = new PictureBox();
			avatarpb.BackColor = Color.Transparent;
			avatarpb.Size = new Size(w, h);
			avatarpb.Image = Properties.Resources.Karesz0;
			szülőform.Controls.Add(avatarpb);
			avatarpb.Parent = szülőform.képkeret;
			/* https://stackoverflow.com/questions/68916508/c-sharp-bitmap-rotate-with-transparent-image
			 * Hosting controls by a PictureBox does not mean it becomes by default their parent because it is not a container control like a Panel or GroupBox. 
			 * The parent of the hosted controls remains the parent of the hosting PictureBox itself which is the Form in your case. 
			 * That's why the inner PictureBox fills the transparent area with the color of its Parent, the Form. 
			 * Meaning, you need to explicit set the Parent property of the inner PictureBox to the outer one.*/
			avatarpb.BringToFront();

			Frissít();
		}

		public void Lépj(double t)
		{
			Pont hollesz = hely - new Pont(irány, t, "polár"); // ez most polárkoordinátás kellene legyen!
			if (rajzole)
			{
				gr.DrawLine(toll, hely.ToPoint(), hollesz.ToPoint());
			}
			hely = hollesz;
			Thread.Sleep(varakozas);
			Frissít();
		}
		public void Előre(double t) { Lépj(t); }
		public void Hátra(double t) { Lépj(-t); }

		public void Fordulj(double f) { irány -= f; irány %= 360; Frissít(); }
		public void Jobbra(double f) { Fordulj(-f); }
		public void Balra(double f) { Fordulj(f); }

		public void Pihi(int idő) { Thread.Sleep(idő); }

		// Tollat(fel)
		// Tollat(le)
		// fel és le igazából logikai konstansok lesznek -- ebből a felhasználó semmit nem fog látni (mint Karesznél a jobbra és a balra!)
		public void Tollat(bool le) { rajzole = le; }

		public void Tölt(Color mire)
		{
			Point h = this.hely.ToPoint();
			//Rekurzív_kitöltés(h.X, h.Y, szülőform.rajzlap.GetPixel(h.X, h.Y), mire);
			//Kitöltés_szélességi_bejárással(h.X, h.Y, szülőform.rajzlap.GetPixel(h.X, h.Y), mire);
			Kitöltés_mélységi_bejárással(h.X, h.Y, szülőform.rajzlap.GetPixel(h.X, h.Y), mire);
			Frissít();
		}

		private void Kitöltés_szélességi_bejárással(int x, int y, Color mit, Color mire)
		{
			Queue<Point> tennivalók = new Queue<Point>(); // azon pixelek listája, akiknek majd még meg kell nézni a szomszédait!
			tennivalók.Enqueue(new Point(x, y));
			szülőform.rajzlap.SetPixel(x, y, mire);

			Point p;
			Point[] szomszédok;
			while (tennivalók.Count != 0)
			{
				p = tennivalók.Dequeue();
				szomszédok = new Point[4] 
					{ 
						new Point(p.X, p.Y + 1), 
						new Point(p.X - 1, p.Y), 
						new Point(p.X, p.Y - 1), 
						new Point(p.X + 1, p.Y) 
					};

				// szomszédvizsgálat
				foreach (Point sz in szomszédok)
				{
					if (szülőform.rajzlap.GetPixel(sz.X, sz.Y) == mit &&
						0 <= sz.X && sz.Y < szülőform.rajzlap.Width &&
						0 <= sz.Y && sz.Y < szülőform.rajzlap.Height )
					{
						szülőform.rajzlap.SetPixel(sz.X, sz.Y, mire);
						tennivalók.Enqueue(sz);
					}
				}
			}
		}
		private void Kitöltés_mélységi_bejárással(int x, int y, Color mit, Color mire)
		{
			Stack<Point> tennivalók = new Stack<Point>(); // azon pixelek listája, akiknek majd még meg kell nézni a szomszédait!
			tennivalók.Push(new Point(x, y));
			szülőform.rajzlap.SetPixel(x, y, mire);

			Point p;
			Point[] szomszédok;
			while (tennivalók.Count != 0)
			{
				p = tennivalók.Pop();
				szomszédok = new Point[4]
					{
						new Point(p.X, p.Y + 1),
						new Point(p.X - 1, p.Y),
						new Point(p.X, p.Y - 1),
						new Point(p.X + 1, p.Y)
					};

				// szomszédvizsgálat
				foreach (Point sz in szomszédok)
				{
					if (szülőform.rajzlap.GetPixel(sz.X, sz.Y) == mit &&
						0 <= sz.X && sz.Y < szülőform.rajzlap.Width &&
						0 <= sz.Y && sz.Y < szülőform.rajzlap.Height)
					{
						szülőform.rajzlap.SetPixel(sz.X, sz.Y, mire);
						tennivalók.Push(sz);
					}
				}
			}
		}
		private void Rekurzív_kitöltés(int x, int y, Color mit, Color mire) // Nem jó sajnos, stack overflow :(
		{
			if (szülőform.rajzlap.GetPixel(x, y) == mit &&
				0 <= x && x < szülőform.rajzlap.Width &&
				0 <= y && y < szülőform.rajzlap.Height )
			{
				szülőform.rajzlap.SetPixel(x, y, mire);
				Rekurzív_kitöltés(x, y + 1, mit, mire);
				Rekurzív_kitöltés(x - 1, y, mit, mire);
				Rekurzív_kitöltés(x, y - 1, mit, mire);
				Rekurzív_kitöltés(x + 1, y, mit, mire);
			}
		}

		public void Kör_kerületből(double r) => Ív(360, r);

		public void Kör_középpontból(double r) // Karesz van a középpontban
		{
			bool rajzolt_e_előtte = rajzole;

			rajzole = false;
			Előre(r);
			rajzole = rajzolt_e_előtte;

			Jobbra(90);

			Kör_kerületből(r);

			Balra(90);

			rajzole = false;
			Hátra(r);
			rajzole = rajzolt_e_előtte;
		}

		public void Ív(double fok, double r, bool frissít_e = false)
		{
			using (new Form1.Frissítés(this, frissít_e))
			{
				double a = 2 * r * Math.Tan(Math.PI / 360);
				Előre(a / 2);
				for (int i = 0; i < fok - 1; i++)
				{
					Jobbra(1);
					Előre(a);
				}
				Jobbra(1);
				Előre(a / 2);
			}
		}
		public void Ív(float fok, double r)
		{
			/*
				double x;
				double y;
				float d = 2 * (float)r;
				float startangle;
				float sweepangle;
				gr.DrawArc(toll, x, y, d, d , startangle, sweepangle );
			*/
			throw new NotImplementedException();
		}

		//		private void Bezier()

		private void Frissít()
		{
			if (állandó_frissítés)
			{
				avatarpb.Image = rotateImage(Properties.Resources.Karesz0, (float)irány-90);

				szülőform.dlx.Text = hely.X.ToString();
				szülőform.dly.Text = hely.Y.ToString();
				szülőform.dli.Text = irány.ToString();

				/* Ez volt jó akkor, amikor az avatarpb Parent-je még a form volt, nem a képkeret * /
				avatarpb.Location = hely.ToPoint(szülőform.képkeret.Location, w, h);
				/**/

				/* most, hogy az avatarpb parentje a képkeret, nem kell offset */
				avatarpb.Location = hely.ToPoint(new Point(0,0), w, h);
				/**/

				szülőform.képkeret.Image = szülőform.rajzlap; // innen töltődik be a legújabb változat

				szülőform.Refresh();

			}
		}

		private Bitmap rotateImage(Bitmap b, float angle)
		{
			/*
			int maxside = (int)(Math.Sqrt(b.Width * b.Width + b.Height * b.Height));			
			Bitmap returnBitmap = new Bitmap(maxside,maxside);
			*/
			Bitmap returnBitmap = new Bitmap(42,42); // azért feleslegesen ne szívassuk a felsővel. A fenti kód arra kell, ha valaki custom képpel akar rajzolni és lusta kiszámolni...
			using (Graphics g = Graphics.FromImage(returnBitmap))
			{
				g.TranslateTransform((float)b.Width / 2, (float)b.Height / 2);
				g.RotateTransform(angle);
				g.TranslateTransform(-(float)b.Width / 2, -(float)b.Height / 2);
				g.DrawImage(b, new Point(0, 0));
				//b.MakeTransparent();
			}
			/*
			*/
			return returnBitmap;
		}
	}
}
