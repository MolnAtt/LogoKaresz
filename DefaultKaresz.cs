using System;
using System.Drawing;

namespace LogoKaresz
{
    partial class Form1
    {
        const bool fel = false;
        const bool le = true;
        private static Avatar defaultkaresz;

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

        void Tollszín(Color szín) => defaultkaresz.Tollszín(szín);
        void Tollszín(int i) => defaultkaresz.Tollszín(i);
        void Tollvastagság(float v) => defaultkaresz.Tollvastagság(v);
        private void Ív(double f, double r) => defaultkaresz.Ív(f, r);
        public class Frissítés : IDisposable
        {
            Avatar a;
            bool fr;
            public Frissítés(Avatar a, bool fr) { (this.a, this.fr) = (a, a.Állandó_frissítés); a.Állandó_frissítés = fr; }
            public Frissítés(bool fr) : this(defaultkaresz, fr) { }
            public void Dispose() { a.Állandó_frissítés = fr; GC.SuppressFinalize(this); }
        }
        public class Rajzol : IDisposable
        {
            Avatar a;
            bool rajzole_regi;
            public Rajzol(Avatar a, bool rajzoljone) { (this.a, this.rajzole_regi) = (a, a.rajzole); a.Tollat(rajzoljone); }
            public Rajzol(bool r) : this(defaultkaresz, r) { }
            public void Dispose() { a.Tollat(rajzole_regi); GC.SuppressFinalize(this); }
        }
    }
}
