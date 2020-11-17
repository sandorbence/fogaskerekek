using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;

namespace fogaskerekek
{
    class SavePDF
    {
        SaveFileDialog S;
        StreamWriter Sw;
        Kerekek Form;
        Szamolasok Sz;

        public SavePDF(SaveFileDialog s, Kerekek form, Szamolasok sz)
        {
            S = s;
            Form = form;
            Sz = sz;
        }

        public void Save()
        {
            DateTime date = DateTime.Now;
            date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);
            S.Filter = "Pdf files(*.pdf)| *.pdf | All files(*.*) | *.*";
            S.Title = "Adatok mentése pdf fájlba";
            S.ShowDialog();
            if (S.FileName != "")
            {
                Document document = new Document();
                MigraDoc.DocumentObjectModel.Style style = document.Styles["Normal"];
                style.Font.Name = "Arial";
                style.Font.Size = 12;
                Section section = document.AddSection();
                section.PageSetup.DifferentFirstPageHeaderFooter = true;
                Paragraph footer = section.Footers.FirstPage.AddParagraph();
                footer.AddFormattedText(date.ToString(), TextFormat.Bold);
                Paragraph p1 = section.AddParagraph();
                p1.Format.Alignment = ParagraphAlignment.Center;
                FormattedText title = p1.AddFormattedText("Fogaskerék szilárdsági számítási eredmények", TextFormat.Bold);
                title.Font.Size = 20;
                Paragraph p2 = section.AddParagraph();
                p2.Format.Alignment = ParagraphAlignment.Left;
                FormattedText eredmenyek = p2.AddFormattedText("\nSzámolt feszültségek és biztonsági tényezők", TextFormat.Bold);
                eredmenyek.Font.Size = 14;
                p2.AddText("\n\nHertz feszültség a kiskeréken: σHe1 = " + Math.Round(Sz.SzigmaHe1, 4).ToString() + " [MPa]");
                p2.AddText("\nMegengedett Hertz feszültség a kiskeréken: σHp1 = " + Math.Round(Sz.SzigmaHp1, 4).ToString() + " [MPa]");
                p2.AddText("\nBiztonság pitting ellen a kiskeréken: SH1 = " + Math.Round(Sz.SH1, 4).ToString());
                p2.AddText("\nHertz feszültség a nagykeréken: σHe2 = " + Math.Round(Sz.SzigmaHe2, 4).ToString() + " [MPa]");
                p2.AddText("\nMegengedett Hertz feszültség a nagykeréken: σHp2 = " + Math.Round(Sz.SzigmaHp2, 4).ToString() + " [MPa]");
                p2.AddText("\nBiztonság pitting ellen a nagykeréken: SH2 = " + Math.Round(Sz.SH2, 4).ToString());
                p2.AddText("\nFogtő feszültség a kiskeréken: σFe1 = " + Math.Round(Sz.SzigmaFe1, 4).ToString() + " [MPa]");
                p2.AddText("\nMegengedett fogtő feszültség a kiskeréken: σFp1 = " + Math.Round(Sz.SzigmaFp1, 4).ToString() + " [MPa]");
                p2.AddText("\nBiztonság fogtörés ellen a kiskeréken: SF1 = " + Math.Round(Sz.SF1, 4).ToString());
                p2.AddText("\nFogtő feszültség a nagykeréken: σFe2 = " + Math.Round(Sz.SzigmaFe2, 4).ToString() + " [MPa]");
                p2.AddText("\nMegengedett fogtő feszültség a nagykeréken: σFp2 = " + Math.Round(Sz.SzigmaFp2, 4).ToString() + " [MPa]");
                p2.AddText("\nBiztonság fogtörés ellen a nagykeréken:SF2 = " + Math.Round(Sz.SF2, 4).ToString());
                FormattedText tenyezok = p2.AddFormattedText("\n\nTovábbi számolt tényezők", TextFormat.Bold);
                tenyezok.Font.Size = 14;
                p2.AddText("\n\nÜzemtényező: KA = " + Math.Round(Sz.KA, 4).ToString());
                p2.AddText("\nDinamikus tényező: KV = " + Math.Round(Sz.KV, 4).ToString());
                p2.AddText("\nHomloktényező Hertz feszültségre: KHα = " + Math.Round(Sz.KHalfa, 4).ToString());
                p2.AddText("\nTerhelési tényező Hertz feszültségre: KHβ = " + Math.Round(Sz.KHbeta, 4).ToString());
                p2.AddText("\nHomloktényező fogtő feszültségre: KFα = " + Math.Round(Sz.KFalfa, 4).ToString());
                p2.AddText("\nTerhelési tényező fogtő feszültségre: KFβ = " + Math.Round(Sz.KFbeta, 4).ToString());
                p2.AddText("\nRugalmassági tényező: ZE = " + Math.Round(Sz.ZE, 4).ToString());
                p2.AddText("\nZónatényező: ZH = " + Math.Round(Sz.ZH, 4).ToString());
                p2.AddText("\nFogferdeségi tényező (pitting): Zβ = " + Math.Round(Sz.ZBeta, 4).ToString());
                p2.AddText("\nKapcsolószám tényező (pitting): Zε = " + Math.Round(Sz.ZEpsz, 4).ToString());
                p2.AddText("\nÉrdességi tényező: ZR = " + Math.Round(Sz.ZR, 4).ToString());
                p2.AddText("\nKenési tényező: ZL = " + Math.Round(Sz.ZL, 4).ToString());
                p2.AddText("\nSebesség tényező: ZV = " + Math.Round(Sz.ZV, 4).ToString());
                p2.AddText("\nFogferdeségi tényező (fogtő): Yβ = " + Math.Round(Sz.YBeta, 4).ToString());
                p2.AddText("\nKapcsolószám tényező (fogtő): Yε = " + Math.Round(Sz.YEpsz, 4).ToString());
                p2.AddText("\nKiskerék fogalaktényező: YF1 = " + Math.Round(Sz.YF1, 4).ToString());
                p2.AddText("\nNagykerék fogalaktényező: YF2 = " + Math.Round(Sz.YF2, 4).ToString());
                p2.AddText("\nKiskerék feszültségkorrekciós tényező: YS1 = " + Math.Round(Sz.YS1, 4).ToString());
                p2.AddText("\nNagykerék feszültségkorrekciós tényező: YS2 = " + Math.Round(Sz.YS2, 4).ToString());
                p2.AddText("\nRelatív bemetszés érzékenység tényező kiskerék: YδrelT1 = " + Math.Round(Sz.YDeltarelT1, 4).ToString());
                p2.AddText("\nRelatív bemetszés érzékenység tényező nagykerék: YδrelT2 = " + Math.Round(Sz.YDeltarelT2, 4).ToString());
                p2.AddText("\nRelatív felületi tényező: YRrelT = " + Math.Round(Sz.YRrelT, 4).ToString());
                Section section2 = document.AddSection();
                Paragraph kozos = section2.AddParagraph();
                kozos.Format.Alignment = ParagraphAlignment.Left;
                FormattedText kozoscim = kozos.AddFormattedText("Megadott közös adatok", TextFormat.Bold);
                kozoscim.Font.Size = 14;
                kozos.AddText("\n\nTengelytáv: a = " + Form.A.ToString() + " [mm]");
                kozos.AddText("\nNormálmodul: mn = " + Form.Mn.ToString() + " [mm]");
                kozos.AddText("\nFogferdeség: β = " + (Form.Beta / Math.PI * 180).ToString() + " [°]");
                kozos.AddText("\nKapcsolószög: α = " + (Form.Alfa / Math.PI * 180).ToString() + " [°]");
                kozos.AddText("\nFejmagasság tényező: h* = " + Form.H.ToString());
                kozos.AddText("\nLábhézag tényező: c* = " + Form.C.ToString());
                kozos.AddText("\nLábrádiusz tényező: ρ* = " + Form.RoaP.ToString());
                kozos.AddText("\nEffektív fogszélesség: b = " + Form.B.ToString() + " [mm]");
                kozos.AddText("\nTeljesítmény: P = " + Form.P.ToString() + " [kW]");
                kozos.AddText("\nKiskerék fordulatszám: n1 = " + Form.N1.ToString() + " [1/min]");
                kozos.AddText("\nÜzemtényező: KA = " + Form.KA.ToString());
                kozos.AddText("\nKenőanyag viszkozitása 40 °C-on: V40 = " + Form.V40.ToString() + " [cSt]");
                kozos.AddText("\nMinimális biztonsági tényező pittingre: SHmin = " + Form.SH.ToString());
                kozos.AddText("\nMinimális biztonsági tényező fogtörésre: SFmin = " + Form.SF.ToString());
                kozos.AddText("\nMegmunkálási ráhagyás: q = " + Form.Q.ToString() + " [mm]");
                kozos.AddText("\nMegmunkálás pontossága: I = " + Form.I.ToString());
                Paragraph kiskerek = section2.AddParagraph();
                kiskerek.Format.Alignment = ParagraphAlignment.Left;
                FormattedText kcim = kiskerek.AddFormattedText("\nA kiskerék megadott és számított adatai", TextFormat.Bold);
                kcim.Font.Size = 14;
                kiskerek.AddFormattedText("\n\nMegadott mennyiségek", TextFormat.Italic);
                kiskerek.AddText("\nAnyag:" + Form.K1.Anyag.ToString());
                kiskerek.AddText("\nFogszám: z1 = " + Form.K1.Z.ToString());
                kiskerek.AddText("\nProfileltolás: x1 = " + Form.K1.X.ToString());
                kiskerek.AddText("\nFelületi érdesség: Ra1 = " + Form.K1.Ra.ToString() + " [μm]");
                kiskerek.AddText("\nProtuberancia: p1 = " + Form.Pt1.ToString() + " [mm]");
                kiskerek.AddText("\nFoglenyesés: Ca1 = " + Form.Ca1.ToString() + " [mm]");
                kiskerek.AddText("\nFordulatszám: n1 = " + Form.N1.ToString() + " [1/min]");
                FormattedText kszamitott = kiskerek.AddFormattedText("\nSzámított mennyiségek", TextFormat.Italic);
                kiskerek.AddText("\nGördülőkör: d1 = " + Math.Round(Form.K1.D, 4).ToString() + " [mm]");
                kiskerek.AddText("\nFejkör: da1 = " + Math.Round(Sz.DA1, 4).ToString() + " [mm]");
                kiskerek.AddText("\nLábkör: df1 = " + Math.Round(Sz.DF1, 4).ToString() + " [mm]");
                kiskerek.AddText("\nAlapkör: db1 = " + Math.Round(Sz.DB1, 4).ToString() + " [mm]");
                kiskerek.AddText("\nVirtuális fogszám: zn1 = " + Math.Round(Sz.ZN1, 4).ToString());
                Paragraph nagykerek = section2.AddParagraph();
                nagykerek.Format.Alignment = ParagraphAlignment.Left;
                FormattedText ncim = nagykerek.AddFormattedText("\nA nagykerék megadott és számított adatai", TextFormat.Bold);
                ncim.Font.Size = 14;
                nagykerek.AddFormattedText("\n\nMegadott mennyiségek", TextFormat.Italic);
                nagykerek.AddText("\nAnyag:" + Form.K2.Anyag.ToString());
                nagykerek.AddText("\nFogszám: z2 = " + Form.K2.Z.ToString());
                nagykerek.AddText("\nProfileltolás: x2 = " + Form.K2.X.ToString());
                nagykerek.AddText("\nFelületi érdesség: Ra2 = " + Form.K2.Ra.ToString() + " [μm]");
                nagykerek.AddText("\nProtuberancia: p2 = " + Form.Pt2.ToString() + " [mm]");
                nagykerek.AddText("\nFoglenyesés: Ca2 = " + Form.Ca2.ToString() + " [mm]");
                nagykerek.AddFormattedText("\nSzámított mennyiségek", TextFormat.Italic);
                nagykerek.AddText("\nGördülőkör: d2 = " + Math.Round(Form.K2.D, 4).ToString() + " [mm]");
                nagykerek.AddText("\nFejkör: da2 = " + Math.Round(Sz.DA2, 4).ToString() + " [mm]");
                nagykerek.AddText("\nLábkör: df2 = " + Math.Round(Sz.DF2, 4).ToString() + " [mm]");
                nagykerek.AddText("\nAlapkör: db2 = " + Math.Round(Sz.DB2, 4).ToString() + " [mm]");
                nagykerek.AddText("\nVirtuális fogszám: zn2 = " + Math.Round(Sz.ZN2, 4).ToString());
                Paragraph mszamolt = section2.AddParagraph();
                mszamolt.Format.Alignment = ParagraphAlignment.Left;
                FormattedText mcim = mszamolt.AddFormattedText("\nMinden további számított érték", TextFormat.Bold);
                mcim.Font.Size = 14;
                mszamolt.AddText("\n\nHomlok kapcsolószög: αt = " + Math.Round(Form.Alfat * 180 / Math.PI, 4) + " [°]");
                mszamolt.AddText("\nTényleges homlok kapcsolószög: αwt = " + Math.Round(Sz.Alfawt * 180 / Math.PI, 4) + " [°]");
                mszamolt.AddText("\nHomlokmodul: mt = " + Math.Round(Form.Mt, 4) + " [mm]");
                mszamolt.AddText("\nAlap fogferdeség: βb = " + Math.Round(Form.Betab * 180 / Math.PI, 4) + " [°]");
                mszamolt.AddText("\nAlap homlokosztás: pet = " + Math.Round(Sz.pet, 4) + " [mm]");
                mszamolt.AddText("\nTangenciális erő komponens: Ft = " + Math.Round(Sz.Ft, 4) + " [N]");
                mszamolt.AddText("\nÉrintő irányú sebesség: v = " + Math.Round(Sz.v, 4) + " [m/s]");
                mszamolt.AddText("\nRezonancia hányados: N = " + Math.Round(Sz.N, 4));
                mszamolt.AddText("\nNormálosztás: p = " + Math.Round(Sz.p, 4) + " [mm]");
                mszamolt.AddText("\nAlaposztás: pt = " + Math.Round(Sz.pt, 4) + " [mm]");
                mszamolt.AddText("\nHomlokosztás a kapcsolódási hosszon: pet = " + Math.Round(Sz.pet, 4) + " [mm]");
                mszamolt.AddText("\nKapcsolódási hossz: gα = " + Math.Round(Sz.Galfa, 4) + " [mm]");
                mszamolt.AddText("\nNormál kapcsolószám: εα = " + Math.Round(Sz.epszilonAlfa, 4));
                mszamolt.AddText("\nÁtfedés: εβ = " + Math.Round(Sz.epszilonBeta, 4));
                mszamolt.AddText("\nÖsszkapcsolószám: εγ = " + Math.Round(Sz.epszilonGamma, 4));
                mszamolt.AddText("\nTényleges tengelytáv: ad = " + Math.Round(Sz.ad, 4));
                mszamolt.AddText("\nFejkör módosítási tényező: k = " + Math.Round(Sz.k, 4) + " [mm]");
                mszamolt.AddFormattedText("\nAlkalmazott gyártási tűrések", TextFormat.Italic);
                mszamolt.AddText("\nA gyártási pontossági osztály: I = " + Form.I.ToString());
                mszamolt.AddText("\nAz alaposztás tűrése: fpe = " + Math.Round(Sz.fpe, 4) + " [μm]");
                mszamolt.AddText("\nAz alaposztás effektív eltérése: fpeeff = " + Math.Round(Sz.fpeEff, 4) + " [μm]");
                mszamolt.AddText("\nA kapcsolószög tűrése: ffα = " + Math.Round(Sz.fAlfa, 4) + " [μm]");
                mszamolt.AddText("\nA kapcsolószög effektív eltérése: ffαeeff = " + Math.Round(Sz.fAlfaEff, 4) + " [μm]");
                mszamolt.AddText("\nSzögeltérés tűrése: fHβ= " + Math.Round(Sz.fBeta, 4) + " [μm]");
                mszamolt.AddText("\nBejáratás előtti egyenértékű tengelytávhiba: FβX= " + Math.Round(Sz.FBetaX, 4) + " [μm]");
                mszamolt.AddText("\nBejáratás utáni effektív egyenértékű tengelytávhiba: FβY= " + Math.Round(Sz.FBetaY, 4) + " [μm]");
                PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true);
                pdfRenderer.Document = document;
                pdfRenderer.RenderDocument();
                pdfRenderer.PdfDocument.Save(S.FileName);
            }
        }
    }
   
}
