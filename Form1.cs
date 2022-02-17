using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace LogoKaresz
{
	public partial class Form1 : Form
	{
		void fodros_fraktál(int év, double méret)
		{
			if (év == 1)
			{
				Előre(méret);
				Jobbra(60);
				Előre(méret);
				Balra(120);
				Előre(2 * méret);
				Jobbra(120);
				Előre(méret);
				Balra(60);
			}
			else if (év > 1)
			{
				fodros_fraktál(év - 1, méret / 3);
				Jobbra(60);
				fodros_fraktál(év - 1, méret / 3);
				Balra(120);
				fodros_fraktál(év - 1, méret / 3);
				fodros_fraktál(év - 1, méret / 3);
				Jobbra(120);
				fodros_fraktál(év - 1, méret / 3);
				Balra(60);
			}
		}

		
		void szigetj(int n, int év, double méret)
		{
            for (int i = 0; i < n; i++)
            {
				fodros_fraktál(év, méret);
				Jobbra(360/(double)n);
            }
		}
		void szigete(int n, int év, double méret)
		{
			for (int i = 0; i < n; i++)
			{
				fodros_fraktál(év, méret);
				Balra(360 / (double)n);
			}
		}

		Action[] feladatok;
		void Feladateloszto() 
		{
			feladatok = new Action[] 
			{	
				FELADAT0,
				FELADAT1,
			};
		}

		void FELADAT0()
		{
            using (new Frissítés(false))
            {
				szigetj(6, 5, 20);
            }
		}

		void FELADAT1()
		{
			using (new Frissítés(false))
			{
				szigete(6, 5, 20);
			}
		}
	}
}
