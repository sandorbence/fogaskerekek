using System;

namespace fogaskerekek
{
    class Szamolasok
    {
        private Kerekek K;
        private Pontossag P;
        const double Cgamma_egyenes = 20;
        const double Cgamma_ferde = 18.7;
        const double Cvesszo_egyenes = 14;
        const double Cvesszo_ferde = 13.1;
        public double SzigmaHp1, SzigmaHp2, SzigmaHe1, SzigmaHe2, epszilonAlfa, epszilonBeta, epszilonGamma, KA, KV, KHalfa, KHbeta, Ft, SzigmaFp1, SzigmaFp2, SzigmaFe1, SzigmaFe2;
        public double ZE, ZH, ZBeta, ZEpsz, B, Alfawt, ZL, ZV, ZX, ZR, ZW, D1, D2, DA1, DA2, DB1, DB2, DF1, DF2, Cgamma, Cvesszo, ZN1, ZN2, p, pt, pet;
        public double YEpsz, YBeta, YFS1, YFS2, KFalfa, KFbeta, YDeltarelT1, YDeltarelT2, YRrelT, v, N, SF1, SF2, SH1, SH2, Galfa, BetaB, ad, k;
        public Szamolasok(Kerekek kerekek)
        {
            K = kerekek;
        }

        public void AltalanosSzamitasok()
        {
            ad = K.Mt * (K.Z1 + K.Z2) / 2;
            k = (K.A - ad) / K.Mn - (K.X1 + K.X2);
            D1 = K.D1;
            D2 = K.D2;
            KA = K.KA;
            if (K.DA1 == 0 || K.DA2 == 0) //fejkörök ha nem adjuk meg
            {

                DA1 = D1 + 2 * K.Mn * (K.X1 + K.H + k);
                DA2 = D2 + 2 * K.Mn * (K.X2 + K.H + k);
            }
            else
            {
                DA1 = K.DA1;
                DA2 = K.DA2;
            }
            if (K.Beta == 0)
            {
                Cgamma = Cgamma_egyenes;
                Cvesszo = Cvesszo_egyenes;
            }
            else
            {
                Cgamma = Cgamma_ferde;
                Cvesszo = Cvesszo_ferde;
            }
            DF1 = D1 - 2 * K.Mn * (K.H + K.C - K.X1); //lábkörök
            DF2 = D2 - 2 * K.Mn * (K.H + K.C - K.X2);
            DB1 = K.Z1 * K.Mt * Math.Cos(K.Alfat);
            DB2 = K.Z2 * K.Mt * Math.Cos(K.Alfat);
            v = Math.PI * D1 * K.N1 / 60000; //érintő irányú sebesség
            Ft = 60000000 / Math.PI * K.P / (D1 * K.N1); //tangenciális terhelés
            p = Math.PI * K.Mn; //osztás
            pt = Math.PI * K.Mt; //homlokosztás            
            double db1 = D1 * Math.Cos(K.Alfat);
            double db2 = D2 * Math.Cos(K.Alfat);
            Alfawt = Math.Acos(K.Mt / (2 * K.A) * (K.Z1 + K.Z2) * Math.Cos(K.Alfat));
            Galfa = 0.5 * (Math.Sqrt(Math.Pow(DA1, 2) - Math.Pow(db1, 2))            //érintkezési hossz
                   + K.U / Math.Abs(K.U) * Math.Sqrt(Math.Pow(DA2, 2) - Math.Pow(db2, 2))) - K.A * Math.Sin(Alfawt);
            pet = pt * Math.Cos(K.Alfat);
            BetaB = Math.Asin(Math.Cos(K.Alfa) * Math.Sin(K.Beta));
            epszilonAlfa = Galfa / pet;
            B = K.B;
            epszilonBeta = B * Math.Tan(BetaB) / pet;
            epszilonGamma = epszilonAlfa + epszilonBeta;
            if (epszilonBeta < 1) ZEpsz = Math.Sqrt((4 - epszilonAlfa) / 3 * (1 - epszilonBeta) + epszilonBeta / epszilonAlfa);
            else ZEpsz = Math.Sqrt(1 / epszilonAlfa); //érintkezési tényező
            N = 1.19 * K.Z1 / 10 * v / 100 * Math.Sqrt(10 / Cgamma * Math.Pow(K.U, 2) / (Math.Pow(K.U, 2) + 1));
        }

        public double Bp, Bf, Bk, yp, yf, Cay1, Cay2, h1, h2;
        public double fpe, fAlfa, fpeEff, fAlfaEff, fBeta, FBetaX, FBetaY, ybeta, yalfa;

        public void KTenyezok()
        {
            Cay1 = 1 / 18 * Math.Pow((K.K1.Anyag.SzigmaH / 97 - 18.45), 2) + 1.5;
            Cay2 = 1 / 18 * Math.Pow((K.K2.Anyag.SzigmaH / 97 - 18.45), 2) + 1.5;
            P = new Pontossag(K.K1, K.K2, K.I);
            P.Turesek();
            fpe = P.Fp;
            fAlfa = P.FAlfa;
            fBeta = P.FBeta;
            yp = 0.075 * fpe;
            if (yp > 3) yp = 3;
            yf = 0.075 * fAlfa;
            if (yf > 3) yf = 3;
            fpeEff = fpe - yp;
            fAlfaEff = fAlfa - yf;
            Bp = Cvesszo * fpeEff / (Ft / K.B * KA); //gyártási pontosságot figyelembe vevő paraméterek
            Bf = Cvesszo * fAlfaEff / (Ft / K.B * KA);
            if (K.Ca1 != 0) Bk = Math.Abs((1 - Cvesszo * K.Ca1) / (Ft / B * KA));
            else Bk = Math.Abs((1 - Cvesszo * (Cay1 + Cay2) / 2) / (Ft / B * KA));
            if (K.I >= 7) Bk = 1;
           
            double Cv1, Cv2, Cv3, Cv4, Cv5, Cv6, Cv7;
            if (epszilonGamma <= 2)
            {
                Cv1 = 0.32;
                Cv2 = 0.34;
                Cv3 = 0.23;
                /*Cv4 = 0.9;
                Cv5 = 0.47;
                Cv6 = 0.47;*/
            }
            else
            {
                Cv1 = 0.32;
                Cv2 = 0.57 / (epszilonGamma - 0.3);
                Cv3 = 0.096 / (epszilonGamma - 1.56);
                /*Cv4 = (0.57 - 0.05 * epszilonGamma) / (epszilonGamma - 1.44);
                Cv5 = 0.47;
                Cv6 = 0.12 / (epszilonGamma - 1.74);*/
            }
            /*if (epszilonGamma <= 1.5) Cv7 = 0.75;
            if (epszilonGamma > 1.5 && epszilonGamma <= 2.5) Cv7 = 0.125 * Math.Sin(Math.PI * (epszilonGamma - 2)) + 0.875;
            if (epszilonGamma > 2.5) Cv7 = 1;*/
            KV = N * (Cv1 * Bp + Cv2 * Bf + Cv3 * Bk) + 1; //dinamikus tényező
            FBetaX = fBeta / 2;
            if (FBetaX < 0.005 * Ft / B * KA * KV) FBetaX = 0.005 * Ft / B * KA * KV;
            ybeta = FBetaX * 0.15;
            if (ybeta > 6) ybeta = 6;
            FBetaY = FBetaX - ybeta;
            if (K.Beta == 0) KHbeta = 1;
            else KHbeta = 1 + (Cgamma + FBetaY) / (2 * Ft / B * KA * KV); //felületi terhelési tényező Hertzre
            h1 = (DA1 - DF1) / 2;
            h2 = (DA2 - DF2) / 2;
            double h;
            if (h1 > h2) h = h1;
            else h = h2;
            if (h / B > 1 / 3) h = B / 3; //h/b=1/3 behelyettesítése
            KFbeta = Math.Pow(KHbeta, 1 / (1 + h / B + Math.Pow(h / B, 2))); //felületi terhelés tényező fogtő feszültségre
            yalfa = 0.075 * fpe;
            if (yalfa > 3) yalfa = 3;
            if (epszilonGamma <= 2)
            {
                KFalfa = KHalfa = epszilonGamma / 2 * (0.9 + 0.4 * Cgamma * (fpe - yalfa) / (Ft / B * KA * KV * KHbeta));
            }
            else
            {
                KHalfa = KFalfa = 0.9 + 0.4 * Math.Sqrt(2 * (epszilonGamma - 1) / epszilonGamma) * Cgamma * (fpe - yalfa) / (Ft / B * KA * KV * KHbeta);
            }
            if (KHalfa > epszilonGamma / (epszilonGamma * Math.Pow(ZEpsz, 2))) KHalfa = epszilonGamma / (epszilonGamma * Math.Pow(ZEpsz, 2));
            if (KHalfa < 1) KHalfa = 1;
            double epszilonAlfan = epszilonAlfa / Math.Pow(Math.Cos(K.Betab), 2);
            YEpsz = 0.25 + 0.75 / epszilonAlfan;
            if (KFalfa > epszilonGamma / (epszilonAlfa * YEpsz)) KFalfa = epszilonGamma / (epszilonAlfa * YEpsz);
            if (KFalfa < 1) KFalfa = 1;
        }

        public double M1, M2, ZB, ZD, Rz;

        public void Hertz()
        {
            M1 = Math.Tan(Alfawt) / Math.Sqrt((Math.Sqrt(Math.Pow(DA1, 2) / Math.Pow(DB1, 2) - 1) - 2 * Math.PI / K.Z1) *
                (Math.Sqrt(Math.Pow(DA2, 2) / Math.Pow(DB2, 2) - 1) - (epszilonAlfa - 1) * 2 * Math.PI / K.Z2));
            M2 = Math.Tan(Alfawt) / Math.Sqrt((Math.Sqrt(Math.Pow(DA2, 2) / Math.Pow(DB2, 2) - 1) - 2 * Math.PI / K.Z2) *
               (Math.Sqrt(Math.Pow(DA1, 2) / Math.Pow(DB1, 2) - 1) - (epszilonAlfa - 1) * 2 * Math.PI / K.Z1));
            if (K.Beta == 0)
            {
                ZB = M1;
                if (ZB < 1) ZB = 1;
                ZD = M2;
                if (ZD < 1) ZD = 1;
            }
            else
            {
                if (epszilonBeta >= 1) ZB = ZD = 1;
                else
                {
                    ZB = M1 - epszilonBeta * (M1 - 1);
                    if (ZB < 1) ZB = 1;
                    ZD = M2 - epszilonBeta * (M2 - 1);
                    if (ZD < 1) ZD = 1;
                }
            }
            ZL = 0.91 + 0.36 / Math.Pow((1.2 + 134 / K.V40), 2);
            ZV = 0.93 + 0.14 / Math.Sqrt(0.8 + 32 / v); //sebesség tényező
            Rz = (K.Ra1 + K.Ra2) * 6; //mivel átlagos érdességet adtunk meg
            ZR = 1.02 * Math.Pow(Math.Pow(K.A, 1 / 3) / Rz, 0.008); //érdességi tényező
            SzigmaHp1 = ZL * ZV * ZR * K.K1.Anyag.SzigmaH;
            SzigmaHp2 = ZL * ZV * ZR * K.K2.Anyag.SzigmaH;
            //megengedett Hertz feszültség

            ZBeta = Math.Sqrt(Math.Cos(K.Beta)); //fogferdeségi tényező           
            ZE = 189.8; //rugalmassági tényező
            ZH = 1 / Math.Cos(K.Alfat) * Math.Sqrt(2 * Math.Cos(K.Betab) / Math.Tan(Alfawt));
            SzigmaHe1 = ZB * ZE * ZH * ZBeta * ZEpsz * Math.Sqrt(KA * KV * KHalfa * KHbeta * (K.U + 1) / K.U * Ft / (D1 * B));
            SzigmaHe2 = ZD * ZE * ZH * ZBeta * ZEpsz * Math.Sqrt(KA * KV * KHalfa * KHbeta * (K.U + 1) / K.U * Ft / (D1 * B));
            //Effektív Hertz feszültség

            SH1 = SzigmaHp1 / SzigmaHe1;
            SH2 = SzigmaHp2 / SzigmaHe2;
        }

        public double sFn1, sFn2, hFe1, hFe2, roF1, roF2, YF1, YF2;

        public void FormaTenyezo()
        {
            double alfaFen1, alfaFen2, alfaen1, alfaen2, ye1, ye2, dbn1, dbn2, den1, den2, dn1, dn2, dan1, dan2,
                epszilonAlfan, theta1, theta2, E1, E2, G1, G2, H1, H2, rofP1, rofP2, hfP, spr1, spr2;
            rofP1 = K.RoaP * K.Mn;
            rofP2 = K.RoaP * K.Mn;
            spr1 = K.Spr1;
            spr2 = K.Spr2;
            dn1 = D1 / Math.Pow(Math.Cos(K.Betab), 2);
            dn2 = D2 / Math.Pow(Math.Cos(K.Betab), 2);
            dbn1 = dn1 * Math.Cos(K.Alfa);
            dbn2 = dn2 * Math.Cos(K.Alfa);
            dan1 = dn1 + DA1 - D1;
            dan2 = dn2 + DA2 - D2;
            ZN1 = K.Z1 / (Math.Pow(Math.Cos(K.Betab), 2) * Math.Cos(K.Beta));
            ZN2 = K.Z2 / (Math.Pow(Math.Cos(K.Betab), 2) * Math.Cos(K.Beta));
            hfP = K.H * K.Mn;
            E1 = Math.PI / 4 * K.Mn - hfP * Math.Tan(K.Alfa) + spr1 / Math.Cos(K.Alfa) - (1 - Math.Sin(K.Alfa)) * rofP1 / Math.Cos(K.Alfa);
            E2 = Math.PI / 4 * K.Mn - hfP * Math.Tan(K.Alfa) + spr2 / Math.Cos(K.Alfa) - (1 - Math.Sin(K.Alfa)) * rofP2 / Math.Cos(K.Alfa);
            G1 = rofP1 / K.Mn - hfP / K.Mn + K.X1;
            G2 = rofP2 / K.Mn - hfP / K.Mn + K.X2;
            H1 = 2 / ZN1 * (Math.PI / 2 - E1 / K.Mn) - Math.PI / 3;
            H2 = 2 / ZN2 * (Math.PI / 2 - E2 / K.Mn) - Math.PI / 3;
            epszilonAlfan = epszilonAlfa / Math.Pow(Math.Cos(K.Betab), 2);
            den1 = 2 * K.Z1 / Math.Abs(K.Z1) * Math.Sqrt(Math.Pow(Math.Sqrt(Math.Pow(dan1 / 2, 2) - Math.Pow(dbn1 / 2, 2))
                - Math.PI * D1 * Math.Cos(K.Beta) * Math.Cos(K.Alfa) / Math.Abs(K.Z1) * (epszilonAlfan - 1), 2) + Math.Pow(dbn1 / 2, 2));
            den2 = 2 * K.Z2 / Math.Abs(K.Z2) * Math.Sqrt(Math.Pow(Math.Sqrt(Math.Pow(dan2 / 2, 2) - Math.Pow(dbn2 / 2, 2))
                - Math.PI * D2 * Math.Cos(K.Beta) * Math.Cos(K.Alfa) / Math.Abs(K.Z2) * (epszilonAlfan - 1), 2) + Math.Pow(dbn2 / 2, 2));
            alfaen1 = Math.Acos(dbn1 / den1);
            alfaen2 = Math.Acos(dbn2 / den2);
            ye1 = (Math.PI / 2 + 2 * K.X1 * Math.Tan(K.Alfa)) / ZN1 + (Math.Tan(K.Alfa) - K.Alfa) - (Math.Tan(alfaen1) - alfaen1);
            ye2 = (Math.PI / 2 + 2 * K.X2 * Math.Tan(K.Alfa)) / ZN2 + (Math.Tan(K.Alfa) - K.Alfa) - (Math.Tan(alfaen2) - alfaen2);
            alfaFen1 = alfaen1 - ye1;
            alfaFen2 = alfaen2 - ye2;
            double tmp1 = Math.PI / 6; //az iteráció kezdeti értékei
            double tmp2 = Math.PI / 6;
            double diff1, diff2;
            do
            {
                theta1 = 2 * G1 / ZN1 * Math.Tan(tmp1) - H1;
                diff1 = Math.Abs(theta1 - tmp1);
                tmp1 = theta1;
            } while (diff1 > 0.001);
            do
            {
                theta2 = 2 * G2 / ZN2 * Math.Tan(tmp2) - H2;
                diff2 = Math.Abs(theta2 - tmp2);
                tmp2 = theta2;
            } while (diff2 > 0.001);
            //theta1 = tmp1;
            //theta2 = tmp2;
            hFe1 = K.Mn / 2 * ((Math.Cos(ye1) - Math.Sin(ye1) * Math.Tan(alfaFen1)) * den1 / K.Mn - ZN1 * Math.Cos(Math.PI / 3 - theta1) - G1 / Math.Cos(theta1) + rofP1 / K.Mn);
            hFe2 = K.Mn / 2 * ((Math.Cos(ye2) - Math.Sin(ye2) * Math.Tan(alfaFen2)) * den2 / K.Mn - ZN2 * Math.Cos(Math.PI / 3 - theta2) - G2 / Math.Cos(theta2) + rofP2 / K.Mn);
            sFn1 = K.Mn * ZN1 * Math.Sin(Math.PI / 3 - theta1) + K.Mn * Math.Sqrt(3) * (G1 / Math.Cos(theta1) - rofP1 / K.Mn);
            sFn2 = K.Mn * ZN2 * Math.Sin(Math.PI / 3 - theta2) + K.Mn * Math.Sqrt(3) * (G2 / Math.Cos(theta2) - rofP2 / K.Mn);
            YF1 = 6 * hFe1 / K.Mn * Math.Cos(alfaFen1) / (Math.Pow(sFn1 / K.Mn, 2) * Math.Cos(K.Alfa)); //külső fogazat esetén a forma tényezők
            YF2 = 6 * hFe2 / K.Mn * Math.Cos(alfaFen2) / (Math.Pow(sFn2 / K.Mn, 2) * Math.Cos(K.Alfa));
            roF1 = rofP1 + 2 * Math.Pow(G1, 2) * K.Mn / (Math.Cos(theta1) * (ZN1 * Math.Pow(Math.Cos(theta1), 2) - 2 * G1));
            roF2 = rofP2 + 2 * Math.Pow(G2, 2) * K.Mn / (Math.Cos(theta2) * (ZN2 * Math.Pow(Math.Cos(theta2), 2) - 2 * G2));
        }

        public double qs1, qs2, YS1, YS2;

        public void FeszKorrTenyezo()
        {
            double L1, L2;
            L1 = sFn1 / hFe1;
            L2 = sFn2 / hFe2;
            qs1 = sFn1 / (2 * roF1);
            qs2 = sFn2 / (2 * roF2);
            YS1 = (1.2 + 0.13 * L1) * Math.Pow(qs1, 1 / (1.21 + 2.3 / L1));
            YS2 = (1.2 + 0.13 * L2) * Math.Pow(qs2, 1 / (1.21 + 2.3 / L2));
        }

        public void Fogto()
        {                     
            YDeltarelT1 = 0.9434 + 0.0231 * Math.Sqrt(1 + 2 * qs1);
            YDeltarelT2 = 0.9434 + 0.0231 * Math.Sqrt(1 + 2 * qs2);
            YRrelT = 1.674 - 0.529 * Math.Pow((Rz + 1), 0.1);
            SzigmaFp1 = 2 * YDeltarelT1 * YRrelT * K.Anyag1.SzigmaF;
            SzigmaFp2 = 2 * YDeltarelT2 * YRrelT * K.Anyag2.SzigmaF;
            //Megengedett fogtő feszültség

            double ebtmp = epszilonBeta;

            if (ebtmp > 1) ebtmp = 1;
            if (K.Beta > Math.PI / 6) K.Beta = Math.PI / 6;
            YBeta = 1 - ebtmp * K.Beta / (2 * Math.PI / 3); //ferdeségi tényező          
            SzigmaFe1 = KA * KV * KFalfa * KFbeta * Ft / (B * K.Mn) * YF1 * YS1 * YBeta;
            SzigmaFe2 = KA * KV * KFalfa * KFbeta * Ft / (B * K.Mn) * YF2 * YS2 * YBeta;
            //Effektív fogtő feszültség

            SF1 = SzigmaFp1 / SzigmaFe1;
            SF2 = SzigmaFp2 / SzigmaFe2;
        }

    }
}
