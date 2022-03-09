﻿using System;
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
		double fokpoz(double i) => i < 0 ? i + 360 : i;
		double fokelőjeles(double i) => i > 180 ? i-360 : i;
		public double Irány { get => fokpoz(irány - 90); set => irány = value + 90; }
		public double Előjeles_Irány { get => fokelőjeles(Irány); set => Irány = value; }
		public double Matekos_Irány { get => fokpoz(180-irány); set => irány = 180-value; }
		public double Matekos_Előjeles_Irány { get => fokelőjeles(Matekos_Irány); set => Matekos_Irány = value; }
		Form1 szülőform;
		private PictureBox avatarpb;
		public Pen toll;
		Graphics gr;
		int w;
		int h;
		public bool rajzole;
		public int varakozas;
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



		static Color[] színek = ((KnownColor[])Enum.GetValues(typeof(KnownColor))).Select(x => Color.FromKnownColor((KnownColor)x)).ToArray();
		static Color[] erős_színek_fehérre = new Color[]
			{ Color.Aqua, Color.Aquamarine, Color.Black, Color.Blue, Color.BlueViolet, Color.Brown,
				Color.BurlyWood, Color.CadetBlue, Color.Chartreuse, Color.Chocolate, Color.Coral,
				Color.CornflowerBlue, Color.Crimson, Color.Cyan, Color.DarkBlue, Color.DarkCyan,
				Color.DarkGoldenrod, Color.DarkGray, Color.DarkGreen, Color.DarkKhaki, Color.DarkMagenta,
				Color.DarkOliveGreen, Color.DarkOrange, Color.DarkOrchid, Color.DarkRed, Color.DarkSalmon,
				Color.DarkSeaGreen, Color.DarkSlateBlue, Color.DarkSlateGray, Color.DarkTurquoise, Color.DarkViolet,
				Color.DeepPink, Color.DeepSkyBlue, Color.DimGray, Color.DodgerBlue, Color.ForestGreen, Color.Gainsboro,
				Color.Gold, Color.Goldenrod, Color.Gray, Color.Green, Color.GreenYellow, Color.HotPink, Color.IndianRed,
				Color.Indigo, Color.Khaki, Color.LawnGreen, Color.LightBlue, Color.LightCoral, Color.LightGreen,
				Color.LightGray, Color.LightPink, Color.LightSalmon, Color.LightSeaGreen, Color.LightSkyBlue, Color.LightSlateGray,
				Color.LightSteelBlue, Color.Lime, Color.LimeGreen, Color.Magenta, Color.Maroon, Color.MediumAquamarine, Color.MediumBlue,
				Color.MediumOrchid, Color.MediumPurple, Color.MediumSeaGreen, Color.MediumSlateBlue, Color.MediumSpringGreen,
				Color.MediumTurquoise, Color.MediumVioletRed, Color.MidnightBlue, Color.MistyRose, Color.Moccasin,
				Color.NavajoWhite, Color.Navy, Color.Olive, Color.OliveDrab, Color.Orange, Color.OrangeRed, Color.Orchid,
				Color.PaleGoldenrod, Color.PaleGreen, Color.PaleTurquoise, Color.PaleVioletRed, Color.PeachPuff,
				Color.Peru, Color.Pink, Color.Plum, Color.PowderBlue, Color.Purple, Color.Red, Color.RosyBrown,
				Color.RoyalBlue, Color.SaddleBrown, Color.Salmon, Color.SandyBrown, Color.SeaGreen, Color.Sienna,
				Color.Silver, Color.SkyBlue, Color.SlateBlue, Color.SlateGray, Color.SpringGreen, Color.SteelBlue,
				Color.Tan, Color.Teal, Color.Thistle, Color.Tomato, Color.Turquoise, Color.Violet, Color.Wheat, Color.Yellow,
				Color.YellowGreen
			};
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
		// setter függvények, bár a toll is public. Az esetleges logóba való fordítóprogramok miatt lenne jó.
		public void Tollszín(Color szín) => toll.Color = szín;
		public void Tollszín(int i) => toll.Color = erős_színek_fehérre[i];
		public void Tollvastagság(float v) => toll.Width = v;

		public void Lépj(double t)
		{
			Pont hollesz = hely - new Pont(irány, t, "polár"); // ez most polárkoordinátás kellene legyen!
			if (hollesz.DescartesBenneVan(szülőform.rajzlap))
			{
				if (rajzole)
					gr.DrawLine(toll, hely.ToPoint(), hollesz.ToPoint());
				hely = hollesz;
				Thread.Sleep(varakozas);
				Frissít();
			}
			else
				MessageBox.Show("Au. Ez a pálya vége.");
		}
		public void Teleport(Point loc) { hely = new Pont(loc); Frissít(); }
		public void Előre(double t) { Lépj(t); }
		public void Hátra(double t) { Lépj(-t); }

		public void Fordulj(double f) { irány -= f; irány %= 360; Frissít(); }
		public void Jobbra(double f) { Fordulj(-f); }
		public void Balra(double f) { Fordulj(f); }
		public bool Kilépek_e_a_pályáról(double t) => !(hely - new Pont(irány, t, "polár")).DescartesBenneVan(szülőform.rajzlap);
		public void Pihi(int idő) { Thread.Sleep(idő); }

		// Tollat(fel)
		// Tollat(le)
		// fel és le igazából logikai konstansok lesznek -- ebből a felhasználó semmit nem fog látni (mint Karesznél a jobbra és a balra!)
		public void Tollat(bool le) { rajzole = le; }

		public void Tölt(Color mire, bool beszólós = true)
		{
			Point h = this.hely.ToPoint();
			Color mit = szülőform.rajzlap.GetPixel(h.X, h.Y);
			if (mit.ToArgb() == mire.ToArgb())
			{
				if (beszólós)
					MessageBox.Show($"Mit színezzek ezen? Ez már {mire} színű.");
				return;
			}
			//Rekurzív_kitöltés(h.X, h.Y, szülőform.rajzlap.GetPixel(h.X, h.Y), mire);
			Kitöltés_szélességi_bejárással(h.X, h.Y, szülőform.rajzlap.GetPixel(h.X, h.Y), mire);
			//Kitöltés_mélységi_bejárással(h.X, h.Y, mit, mire);
			Frissít();
		}

		private void Kitöltés_szélességi_bejárással(int x, int y, Color mit, Color mire)
		{
			Queue<Point> tennivalók = new Queue<Point>(); // azon pixelek listája, akiknek majd még meg kell nézni a szomszédait!
			tennivalók.Enqueue(new Point(x, y));
			szülőform.rajzlap.SetPixel(x, y, mire);

			Point p;
			Point[] szomszédok;
			try
			{
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
							0 <= sz.Y && sz.Y < szülőform.rajzlap.Height)
						{
							szülőform.rajzlap.SetPixel(sz.X, sz.Y, mire);
							tennivalók.Enqueue(sz);
						}
					}
				}
			}
			catch (System.ArgumentOutOfRangeException)
			{
				MessageBox.Show("Jaj... Ez kifolyt a pályáról...");
			}
		}
		private void Kitöltés_mélységi_bejárással(int x, int y, Color mit, Color mire)
		{
			Stack<Point> tennivalók = new Stack<Point>(); // azon pixelek listája, akiknek majd még meg kell nézni a szomszédait!
			tennivalók.Push(new Point(x, y));
			szülőform.rajzlap.SetPixel(x, y, mire);

			Point p;
			Point[] szomszédok;
			try
			{
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
			catch (System.ArgumentOutOfRangeException)
			{
				MessageBox.Show("Jaj... Ez kifolyt a pályáról...");
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

		void Kontrolpont(Pont p, Color szin, float w = 10, float h = 10)
		{
			gr.FillEllipse(new SolidBrush(szin), (float)p.X - w / 2, (float)p.Y - h / 2, w, h);
		}

		void Kontrolszakasz(Pont p, Pont q, Color szin)
		{
			gr.DrawLine(new Pen(szin), p.ToPoint(), q.ToPoint());
		}


		/// <summary>
		/// Karesz egy másodrendű Bezier-görbét követve mozog. 
		/// A négy kontrolpont:
		/// 1. Ahol Karesz van
		/// 2. Polárkoordinátával: Amerre karesz néz + 1. paraméter
		/// 3. A 4. kontrolpontból kivonva a 2-3. paraméterekkel megadott polárkoordinátás vektor.
		/// 4. 4-5. paraméterek által megadott pont polárkoordinátás felírással.
		/// </summary>
		/// <param name="ilyen_erővel_indul"></param>
		/// <param name="erre_néz_érkezéskor"></param>
		/// <param name="ilyen_erővel_érkezik"></param>
		/// <param name="az_érkezési_pont_jelenleg_ilyen_irányban_van"></param>
		/// <param name="az_érkezési_pont_ilyen_messze_van"></param>
		public void Bezier(double ilyen_erővel_indul,
						double erre_néz_érkezéskor,
						double ilyen_erővel_érkezik,
						double az_érkezési_pont_jelenleg_ilyen_irányban_van,
						double az_érkezési_pont_ilyen_messze_van,
						bool kontrolpont = false,
						bool kontrolszakasz = false
						)
		{
			Pont hollesz = hely - new Pont(irány + az_érkezési_pont_jelenleg_ilyen_irányban_van, az_érkezési_pont_ilyen_messze_van, "polár");

			if (hollesz.DescartesBenneVan(szülőform.rajzlap))
			{
				Pont cp1 = hely - new Pont(irány, ilyen_erővel_indul, "polár");
				Pont cp2 = hollesz - new Pont(270+erre_néz_érkezéskor, ilyen_erővel_érkezik, "polár");
				if (rajzole)
				{
					gr.DrawBezier(toll, hely.ToPoint(), cp1.ToPoint(), cp2.ToPoint(), hollesz.ToPoint());
				}
				if (kontrolszakasz)
				{
					Kontrolszakasz(hely, cp1, Color.FromArgb(64, 0, 0, 255));
					Kontrolszakasz(cp2, hollesz, Color.FromArgb(64, 0, 0, 255));
				}
				if (kontrolpont)
				{
					Kontrolpont(cp1, Color.LightGreen);
					Kontrolpont(cp2, Color.Red);
				}
				hely = hollesz;
				irány += erre_néz_érkezéskor;
				Thread.Sleep(varakozas);
				Frissít();
			}
			else
				MessageBox.Show("Az érkezési pont a pályán kívül helyezkedne el!");
		}

		private Pont Vászon_koordinátarendszerébe(Pont cél) => hely - cél.Forgatása_az_origó_körül(Előjeles_Irány).Tükrözése_az_y_tengelyre();

		/// <summary>
		/// Egy másodrendű Bezier-görbét 4 pont határoz meg. A kezdőpont, az 1-2. kontrolpontok és a végpont.
		/// A kezdőpont itt Karesz, a kontrolpontok és a végpont pedig a megfelelő pontok -- Karesz koordinátarendszerében felírva.
		/// Karesz koordinátarendszere az a jobbsodrású koordinátarendszer, amelyben Karesz az origó
		/// </summary>
		/// <param name="erre_indul"></param>
		/// <param name="erről_érkezik"></param>
		/// <param name="cél"></param>
		/// <param name="kontrolpont"></param>
		/// <param name="kontrolszakasz"></param>
		public void Bezier_3_pontos(
						(double, double) erre_indul,
						(double, double) erről_érkezik,
						(double, double) cél,
						bool kontrolpont = false,
						bool kontrolszakasz = false
						) => Bezier_3_pontos(new Pont(erre_indul), new Pont(erről_érkezik), new Pont(cél), kontrolpont, kontrolszakasz);
		public void Bezier_3_pontos(
						Pont erre_indul, Pont erről_érkezik, Pont cél,
						bool kontrolpont = false,
						bool kontrolszakasz = false
						)
		{
			Pont hollesz = Vászon_koordinátarendszerébe(cél);

			if (hollesz.DescartesBenneVan(szülőform.rajzlap))
			{
				Pont cp1 = Vászon_koordinátarendszerébe(erre_indul);
				Pont cp2 = Vászon_koordinátarendszerébe(erről_érkezik);
				if (rajzole)
				{
					gr.DrawBezier(toll, hely.ToPoint(), cp1.ToPoint(), cp2.ToPoint(), hollesz.ToPoint());
				}
				if (kontrolszakasz)
				{
					Kontrolszakasz(hely, cp1, Color.FromArgb(64, 0, 0, 255));
					Kontrolszakasz(cp2, hollesz, Color.FromArgb(64, 0, 0, 255));
				}
				if (kontrolpont)
				{
					Kontrolpont(cp1, Color.LightGreen);
					Kontrolpont(cp2, Color.Red);
				}
				hely = hollesz;
				Thread.Sleep(varakozas);
				Frissít();
			}
			else
				MessageBox.Show("Az érkezési pont a pályán kívül helyezkedne el!");
		}


        private void Frissít()
		{
			if (állandó_frissítés)
			{
				avatarpb.Image = rotateImage(Properties.Resources.Karesz0, (float)irány-90);

				szülőform.dlx.Text = hely.X.ToString();
				szülőform.dly.Text = hely.Y.ToString();
				szülőform.dli.Text = Előjeles_Irány.ToString();

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
