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
			this.w = 5;
			this.h = 5;
			this.rajzole = true;
			this.varakozas = 0;
			this.állandó_frissítés = true;



			avatarpb = new PictureBox();
			avatarpb.BackColor = Color.Blue;
			avatarpb.Size = new Size(w, h);
			szülőform.Controls.Add(avatarpb);
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

		public void Kör(double r) // Karesz van a középpontban
		{
			Előre(r);
			Jobbra(90.5);
			double a = 2 * r * Math.Sin(0.5 * Math.PI / 180);
			for (int i = 0; i < 360; i++)
			{
				Előre(a);
				Jobbra(1);
			}
			Balra(90.5);
			Hátra(r);
		}
		public void Ív(int fok, double r, bool frissít_e = false)
		{
			using (new Form1.Frissítés(this, frissít_e))
			{
				double a = 2 * r * Math.Sin(0.5 * Math.PI / 180);
				for (int i = 0; i < fok; i++)
				{
					Előre(a);
					Jobbra(1);
				}
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
				szülőform.dlx.Text = hely.X.ToString();
				szülőform.dly.Text = hely.Y.ToString();
				szülőform.dli.Text = irány.ToString();
				avatarpb.Location = hely.ToPoint(szülőform.képkeret.Location, w, h);

				szülőform.képkeret.Image = szülőform.rajzlap; // innen töltődik be a legújabb változat

				szülőform.Refresh();
			}
		}
	}

	
}
