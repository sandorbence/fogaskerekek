using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace fogaskerekek
{
    public partial class Foprogram : Form
    {
        public Foprogram()
        {
            InitializeComponent();
        }

        private Kerekek form, formOld;
        private Szamolasok sz;
        private bool Clicked;

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Visible = label2.Visible = label3.Visible = label4.Visible = label5.Visible = label6.Visible
                = label7.Visible = label8.Visible = label9.Visible = label10.Visible = label11.Visible = label12.Visible =
                label13.Visible = label14.Visible = label15.Visible = label16.Visible = label17.Visible = label18.Visible
                = label19.Visible = label20.Visible = label21.Visible = label22.Visible = label23.Visible = label24.Visible =
                label25.Visible = label26.Visible = label27.Visible = label28.Visible = label29.Visible = label30.Visible = 
                label31.Visible = label32.Visible = label33.Visible = false;
            button2.Visible = false;
            Clicked = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label7.Visible = label8.Visible = label9.Visible = label10.Visible = label11.Visible = label12.Visible =
                label13.Visible = label14.Visible = label15.Visible = label16.Visible = label17.Visible = label18.Visible =
                label19.Visible = label20.Visible = label21.Visible = label22.Visible = label23.Visible = label24.Visible =
                label25.Visible = label26.Visible = label27.Visible = label28.Visible = label29.Visible = label31.Visible = label32.Visible = !label7.Visible;
            if (Clicked) button2.Text = "Kevesebb";
            else button2.Text = "Részletek";
            Clicked = !Clicked;
        }

        private void dIN399041ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formOld == null) form = new Kerekek();
            else form = formOld;
            form.ShowDialog();
            if (form.Success)
            {
                this.BackgroundImage = null;
                formOld = form;
                sz = new Szamolasok(form);
                sz.AltalanosSzamitasok();
                sz.KTenyezok();
                sz.Hertz();
                sz.FormaTenyezo();
                sz.FeszKorrTenyezo();
                sz.Fogto();
                label1.Visible = label2.Visible = label3.Visible = label4.Visible = label5.Visible = label6.Visible = label30.Visible = label33.Visible = true;
                label1.Text = "σHe1 = " + Math.Round(sz.SzigmaHe1, 4).ToString() + " [MPa]";
                label33.Text = "σHe2 = " + Math.Round(sz.SzigmaHe2, 4).ToString() + " [MPa]";
                label2.Text = "σHp1 = " + Math.Round(sz.SzigmaHp1, 4).ToString() + " [MPa]";
                label30.Text = "σHp2 = " + Math.Round(sz.SzigmaHp2, 4).ToString() + " [MPa]";
                label3.Text = "σFe1 = " + Math.Round(sz.SzigmaFe1, 4).ToString() + " [MPa]";
                label4.Text = "σFp1 = " + Math.Round(sz.SzigmaFp1, 4).ToString() + " [MPa]";
                label5.Text = "σFe2 = " + Math.Round(sz.SzigmaFe2, 4).ToString() + " [MPa]";
                label6.Text = "σFp2 = " + Math.Round(sz.SzigmaFp2, 4).ToString() + " [MPa]";
                if (sz.SH1 < form.SH) label7.ForeColor = Color.Red;
                label7.Text = "SH1 = " + Math.Round(sz.SH1, 4).ToString();
                if (sz.SH2 < form.SH) label8.ForeColor = Color.Red;
                label8.Text = "SH2 = " + Math.Round(sz.SH2, 4).ToString();
                if (sz.SF1 < form.SF) label9.ForeColor = Color.Red;
                label9.Text = "SF1 = " + Math.Round(sz.SF1, 4).ToString();
                if (sz.SF2 < form.SF) label10.ForeColor = Color.Red;
                label10.Text = "SF2 = " + Math.Round(sz.SF2, 4).ToString();
                label11.Text = "KA = " + Math.Round(sz.KA, 4).ToString();
                label12.Text = "Kv = " + Math.Round(sz.KV, 4).ToString();
                label13.Text = "KHα = " + Math.Round(sz.KHalfa, 4).ToString();
                label14.Text = "KHβ = " + Math.Round(sz.KHbeta, 4).ToString();
                label15.Text = "ZE = " + Math.Round(sz.ZE, 4).ToString();
                label16.Text = "ZH = " + Math.Round(sz.ZH, 4).ToString();
                label17.Text = "Zβ = " + Math.Round(sz.ZBeta, 4).ToString();
                label18.Text = "Zε = " + Math.Round(sz.ZEpsz, 4).ToString();
                label19.Text = "ZL = " + Math.Round(sz.ZL, 4).ToString();
                label20.Text = "ZV = " + Math.Round(sz.ZV, 4).ToString();
                label21.Text = "ZR = " + Math.Round(sz.ZR, 4).ToString();
                label22.Text = "YS1 = " + Math.Round(sz.YS1, 4).ToString();
                label23.Text = "YS2 = " + Math.Round(sz.YS2, 4).ToString();
                label24.Text = "Yβ = " + Math.Round(sz.YBeta, 4).ToString();
                label25.Text = "YF1 = " + Math.Round(sz.YF1, 4).ToString();
                label26.Text = "YF2 = " + Math.Round(sz.YF2, 4).ToString();
                label27.Text = "KFα = " + Math.Round(sz.KFalfa, 4).ToString();
                label28.Text = "KFβ = " + Math.Round(sz.KFbeta, 4).ToString();
                label29.Text = "Yε = " + Math.Round(sz.YEpsz, 4).ToString();
                label31.Text = "YδrelT = " + Math.Round(sz.YDeltarelT1, 4).ToString();
                label32.Text = "YRrelT = " + Math.Round(sz.YRrelT, 4).ToString();
                button2.Visible = true;
            }

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string fileName1 = @"../../anyagok.xml";
            string fileName2 = @"../../kenoanyagok.xml";
            Anyagok form = new Anyagok(fileName1, fileName2);
            form.ShowDialog();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Használati útmutató" +
                "\n\nÚj számolás: Számolás indítása, minden mező kitöltése után elérhetővé válik az OK gomb." +
                " A program csak számokat enged bevinni, magyar nyelvű beállítás esetén a tizedeselválasztó vessző, angol nyelvű beállításnál pont." +
                " Ha már indítottunk számolást, és nem kiléptünk hanem OK-val mentünk tovább, akkor újabb számolás indításakor a korábban megadott adatok töltődnek vissza." +
                "\n\nAnyagok: Megnyitja az anyagok és kenőanyagok adatbázisát, lehet szerkeszteni az egyes anyagok kifáradási határait, illetve lehet új anyagokat hozzáadni." +
                " Ha új anyagot akarunk hozzáadni, de már kiválasztottunk egy másik anyagot szerkesztésre, válasszuk ki az új anyag lehetőséget a legördülő menüből." +
                "\n\nMentés: Pdf formátumba menthető ki a számolás eredménye. Ha még nem indítottunk számolást nem tudunk mit kimenteni, így ez csak legalább egy számolás elvégzése után lesz elérhető.");
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

            SavePDF S = new SavePDF(saveFileDialog1, form, sz);
            if (form != null && form.Success)
            {
                S.Save();
            }
            else MessageBox.Show("Még nem készült számolás!");
        }
    }
}
