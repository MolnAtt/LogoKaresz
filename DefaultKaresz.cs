using System;
using System.Drawing;

namespace LogoKaresz
{
    partial class Form1
    {
        static Avatar defaultkaresz;
        # region konstansok
        const bool fel = false;
        const bool le = true;
        const int észak = 0;
        const int kelet = 90;
        const int dél = 180;
        const int nyugat = 270;
        Point közép { get => new Point(képkeret.Width / 2, képkeret.Height / 2); }
        #endregion

        #region definiált defaultkaresz-tulajdonságok
        double Irány { get => defaultkaresz.Irány; }
        double Előjeles_Irány { get => defaultkaresz.Előjeles_Irány; }
        double Matekos_Irány { get => defaultkaresz.Matekos_Irány; }
        double Matekos_Előjeles_Irány { get => defaultkaresz.Matekos_Előjeles_Irány; }
        int Várakozás { get => defaultkaresz.varakozas; set => defaultkaresz.varakozas = value; }
        #endregion
        
        #region Teleport
        bool teleportált_e_már = false;
        void Teleport(int x, int y, double ir = észak) => Teleport(new Point(x, y), ir);
        void Teleport(double x, double y, double ir = észak) => Teleport(new Pont(x, y), ir);
        void Teleport(Pont p, double ir = észak) => Teleport(p.ToPoint(), ir);
        void Teleport(Point p, double ir = észak)
        {
            if (teleportált_e_már)
                System.Windows.Forms.MessageBox.Show("Csak egyszer lehet teleportálni!");
            else
            {
                defaultkaresz.Irány = ir;
                defaultkaresz.Teleport(p);
                teleportált_e_már = true;
            }
        }
        #endregion

        #region statikus függvényváltozatok környezetekhez
        static bool Kilépek_e_a_pályáról(Avatar a, double d) => a.Kilépek_e_a_pályáról(d);
        static void Előre(Avatar a, double d) => a.Előre(d);
        static void Hátra(Avatar a, double d) => a.Hátra(d);
        static void Jobbra(Avatar a, double d) => a.Jobbra(d);
        static void Balra(Avatar a, double d) => a.Balra(d);
        static void Fordulj(Avatar a, double d) => a.Fordulj(d);
        static void Lépj(Avatar a, double d) => a.Lépj(d);
        static void Pihi(Avatar a, int i) => a.Pihi(i);
        static void Tollat(Avatar a, bool b) => a.Tollat(b);
        static void Tollszín(Avatar a, Color c) => a.Tollszín(c);
        static void Tollszín(Avatar a, int i) => a.Tollszín(i);
        static void Tollvastagság(Avatar a, float v) => a.Tollvastagság(v);
        static void Tölt(Avatar a, Color c, bool beszólós = true) => a.Tölt(c, beszólós);
        static void Ív(Avatar a, (double , double ) fr) => a.Ív(fr.Item1, fr.Item2);
        static void Ív(Avatar a, double f, double r) => a.Ív(f, r);
        #endregion

        #region deOOP-függvények (formmetódusváltozatok)
        
        bool Kilépek_e_a_pályáról(double d) => defaultkaresz.Kilépek_e_a_pályáról(d);
        void Előre(double d) => defaultkaresz.Előre(d);
        void Hátra(double d) => defaultkaresz.Hátra(d);
        void Jobbra(double d) => defaultkaresz.Jobbra(d);
        void Balra(double d) => defaultkaresz.Balra(d);
        void Fordulj(double d) => defaultkaresz.Fordulj(d);
        void Lépj(double d) => defaultkaresz.Lépj(d);
        void Pihi(int i) => defaultkaresz.Pihi(i);
        void Tollat(bool b) => defaultkaresz.Tollat(b);
        void Tölt(Color c, bool beszólós = true) => defaultkaresz.Tölt(c, beszólós);
        void Tollszín(Color c) => defaultkaresz.Tollszín(c);
        void Tollszín(int i) => defaultkaresz.Tollszín(i);
        void Tollvastagság(float v) => defaultkaresz.Tollvastagság(v);
        void Ív(double f, double r) => defaultkaresz.Ív(f, r);
        void Ív((double, double)fr) => defaultkaresz.Ív(fr.Item1, fr.Item2);
        #endregion

        #region "vezérlési szerkezetek" delegáltakkal
        static void Ismétlés(int db, Action a) { for (int i = 0; i < db; i++) a(); } // Iteráció ciklusváltozó nélkül. Logoba való fordítás végett.
        /* Például
         
            Ismétlés(4, delegate () 
			{
				Előre(mérték);
				Jobbra(90);
			});
         */
        #endregion

        #region "környezetek" classokkal

        public class Frissítés : IDisposable
        {
            Környezet<bool, bool> k;
            public Frissítés(Avatar a, bool rajzoljone) => k = new Környezet<bool, bool>(a, delegate (Avatar av, bool fr) { av.Állandó_frissítés = fr; }, rajzoljone, delegate (Avatar av, bool fr) { av.Állandó_frissítés = fr; }, a.Állandó_frissítés);
            public Frissítés(bool fr) : this(defaultkaresz, fr) { }
            public void Dispose() { k.Dispose(); GC.SuppressFinalize(this); }
        }
        /* Például

            using (new Frissítés(false))
            {
                Előre(100);
            }
            Előre(100);

         */
        public class Rajzol : IDisposable
        {
            Környezet<bool, bool> k;
            public Rajzol(Avatar a, bool rajzoljone) => k = new Környezet<bool, bool>(a, Tollat, rajzoljone, Tollat, a.rajzole);
            public Rajzol(bool r) : this(defaultkaresz, r) { }
            public void Dispose() { k.Dispose(); GC.SuppressFinalize(this); }
        }
        /* Például

            using (new Rajzol(false))
            {
				Előre(100);
            }
			Előre(100);

         */
        public class Szín : IDisposable
        {
            Környezet<Color, Color> k;
            public Szín(Avatar a, Color új_szín) => k = new Környezet<Color, Color>(a, Tollszín, új_szín, Tollszín, a.toll.Color);
            public Szín(Color új_szín) : this(defaultkaresz, új_szín) { }
            public void Dispose() { k.Dispose(); GC.SuppressFinalize(this); }
        }
        /* Például
            using (new Szín2(Color.Red))
            {
				Előre(100);
            }
			Előre(100);
         */
        public class Vastagság : IDisposable
        {
            Környezet<float, float> k;
            public Vastagság(Avatar a, float új_vastagság) => k = new Környezet<float, float>(a, Tollvastagság, új_vastagság, Tollvastagság, a.toll.Width);
            public Vastagság(float új_vastagság) : this(defaultkaresz, új_vastagság) { }
            public void Dispose() { k.Dispose(); GC.SuppressFinalize(this); }
        }
        /* Például
            using (new Vastagság(5))
            {
				Előre(100);
            }
			Előre(100);
         */
        public class Vékonyít : IDisposable
        {
            Környezet<float, float> k;
            public Vékonyít(Avatar a, float szorzó) => k = new Környezet<float, float>(a, delegate (Avatar av, float sz) { av.toll.Width *= sz; }, szorzó, delegate (Avatar av, float sz) { av.toll.Width /= sz; }, szorzó);
            public Vékonyít(Avatar a, double szorzó) : this(a, (float)szorzó) { }
            public Vékonyít(float szorzó) : this(defaultkaresz, szorzó) { }
            public Vékonyít(double szorzó) : this(defaultkaresz, szorzó) { }
            public void Dispose() { k.Dispose(); GC.SuppressFinalize(this); }
        }
        /* Például
         	Tollvastagság(5);
            using (new Vékonyít(.5))
            {
				Előre(100);
            }
			Előre(100);
         */
        public class Átmenetileg : IDisposable
        {
            Környezet<double, double> k;
            public Átmenetileg(Action<Avatar, double> eljárás, double előjeles_mérték) => k = new Környezet<double, double>(eljárás, előjeles_mérték, eljárás, -előjeles_mérték);
            public Átmenetileg(Avatar a, Action<Avatar, double> eljárás, double előjeles_mérték) => k = new Környezet<double, double>(a, eljárás, előjeles_mérték, eljárás, -előjeles_mérték);
            public void Dispose() { k.Dispose();  GC.SuppressFinalize(this); }
        }
        /* Például

            using (new Átmenetileg(Balra, 50))
            {
                Előre(100);
            }

            vagy 

            using (new Átmenetileg(Balra, 50, defaultkaresz))
            {
                Előre(100);
            }

         */
        public class Környezet<T,R> : IDisposable
        {
            Avatar a;
            Action<Avatar, R> post_a;
            R post_p;
            public Környezet(Avatar a, Action<Avatar, T> pre_a, T pre_p, Action<Avatar, R> post_a, R post_p) { (this.a, this.post_a, this.post_p) = (a, post_a, post_p); pre_a(a, pre_p); }
            public Környezet(Action<Avatar, T> pre_a, T pre_p, Action<Avatar, R> post_a, R post_p) : this(defaultkaresz, pre_a, pre_p, post_a, post_p) { }
            public void Dispose() { post_a(a, post_p); GC.SuppressFinalize(this); }
        }
        /* Például:
        
            using (new Környezet<double,double>(Balra, 50, Jobbra, 50))
            {
				Előre(100);
            }

            vagy 

            using (new Környezet<double,double>(Balra, 50, Jobbra, 50, defaultkaresz))
            {
				Előre(100);
            }

         */
        #endregion

        #region szövegkezelő függvények (funkcionális logo-fordításhoz)
        char első(string s) => s[0];
        string elsőnélküli(string s) => üres(s) ? "" : s.Substring(1);
        string utolsónélküli(string s) => s.Substring(0, s.Length - 1);
        bool üres(string s) => s.Length == 0;
        bool eleme(char e, string s) => s.Contains(e.ToString());
        string utolsónak(string u, string s) => s + u;
        string elsőnek(string u, string s) => u + s;
        #endregion
    }
}
