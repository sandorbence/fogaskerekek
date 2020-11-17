using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace fogaskerekek
{
    class Pontossag
    {
        public double Mn, D, Beta, B;
        public double FAlfa, Fp, FBeta;
        public int I;

        public Pontossag(Kerek k1, Kerek k2, int i)
        {
            Mn = k1.Mn;
            Beta = k1.Beta;
            B = k1.B;
            if (k1.D > k2.D) D = k1.D;
            else D = k2.D; //mivel a nagyobbik kerékre nagyobb lesz a hiba, és a számolásokban azt használjuk
            I = i - 1;
        }

        public int GetTable()
        {
            if (D < 10) return 1;
            if (D >= 10 && D < 50) return 2;
            if (D >= 50 && D < 125) return 3;
            if (D >= 125 && D < 280) return 4;
            if (D >= 280 && D < 560) return 5;
            if (D >= 560 && D < 1000) return 6;
            if (D >= 1000 && D < 1600) return 7;
            if (D >= 1600 && D < 2500) return 8;
            if (D >= 2500 && D < 4000) return 9;
            if (D >= 4000 && D < 6300) return 10;
            if (D >= 6300) return 11;
            else return -1;
        }

        public double[] GetNumbers(string line)
        {
            string[] numbers = line.Split(';');
            double[] list = new double[numbers.Length];
            for (int i = 0; i < numbers.Length; i++)
            {
                list[i] = Convert.ToDouble(numbers[i]);
            }
            return list;
        }

        string profil=Path.GetFullPath(@"../../pontossag_profil.csv");
        string osztas = Path.GetFullPath(@"../../pontossag_osztas.csv");
        string foghajlas = Path.GetFullPath(@"../../pontossag_foghajlas.csv");

        public void Turesek()
        {
            string[] fAlfasorok = File.ReadAllLines(profil);
            double[] fAlfaszamok;
            string[] fBetasorok = File.ReadAllLines(foghajlas);
            double[] fBetaszamok;
            string[] fpsorok = File.ReadAllLines(osztas);
            double[] fpszamok;
            int index = GetTable();
            if (Mn < 2)
            {
                fAlfaszamok = GetNumbers(fAlfasorok[1]);
                FAlfa = fAlfaszamok[I];
                fpszamok = GetNumbers(fpsorok[index]);
                Fp = fpszamok[I];
            }
            if (Mn >= 2 && Mn < 3.55)
            {
                fAlfaszamok = GetNumbers(fAlfasorok[2]);
                FAlfa = fAlfaszamok[I];
                fpszamok = GetNumbers(fpsorok[index + 12]);
                Fp = fpszamok[I];
            }
            if (Mn >= 3.55 && Mn < 6)
            {
                fAlfaszamok = GetNumbers(fAlfasorok[3]);
                FAlfa = fAlfaszamok[I];
                fpszamok = GetNumbers(fpsorok[index + 24]);
                Fp = fpszamok[I];
            }
            if (Mn >= 6 && Mn < 10)
            {
                fAlfaszamok = GetNumbers(fAlfasorok[4]);
                FAlfa = fAlfaszamok[I];
                fpszamok = GetNumbers(fpsorok[index + 36]);
                Fp = fpszamok[I];
            }
            if (Mn >= 10 && Mn < 16)
            {
                fAlfaszamok = GetNumbers(fAlfasorok[5]);
                FAlfa = fAlfaszamok[I];
                fpszamok = GetNumbers(fpsorok[index + 48]);
                Fp = fpszamok[I];
            }
            if (Mn >= 16 && Mn < 25)
            {
                fAlfaszamok = GetNumbers(fAlfasorok[6]);
                FAlfa = fAlfaszamok[I];
                fpszamok = GetNumbers(fpsorok[index + 60]);
                Fp = fpszamok[I];
            }
            if (Mn >= 25 && Mn < 40)
            {
                fAlfaszamok = GetNumbers(fAlfasorok[7]);
                FAlfa = fAlfaszamok[I];
                fpszamok = GetNumbers(fpsorok[index + 72]);
                Fp = fpszamok[I];
            }
            if (Mn >= 40)
            {
                fAlfaszamok = GetNumbers(fAlfasorok[8]);
                FAlfa = fAlfaszamok[I];
                fpszamok = GetNumbers(fpsorok[index + 84]);
                Fp = fpszamok[I];
            }


            if (B < 20)
            {
                fBetaszamok = GetNumbers(fBetasorok[1]);
                FBeta = fBetaszamok[I];
            }
            if (B >= 20 && B < 40)
            {
                fBetaszamok = GetNumbers(fBetasorok[2]);
                FBeta = fBetaszamok[I];
            }
            if (B >= 40 && B < 100)
            {
                fBetaszamok = GetNumbers(fBetasorok[3]);
                FBeta = fBetaszamok[I];
            }
            if (B >= 100 && B < 160)
            {
                fBetaszamok = GetNumbers(fBetasorok[4]);
                FBeta = fBetaszamok[I];
            }
            if (B >= 160)
            {
                fBetaszamok = GetNumbers(fBetasorok[5]);
                FBeta = fBetaszamok[I];
            }
        }

    }
}
