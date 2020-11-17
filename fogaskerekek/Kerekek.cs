using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Globalization;

namespace fogaskerekek
{
    public partial class Kerekek : Form
    {
        public Anyag Anyag1, Anyag2;
        public Kerek K1, K2;
        public bool Success, first;
        public double H, C, Mn, Beta, Alfa, A, Ra1, Ra2, X1, X2, Alfat, Alfawt, Mt, B,
            V40, N1, P, KA, SF, SH, Betab, DA1, DA2, U, D1, D2, Ca1, Ca2, RoaP, Spr1, Spr2, Pt1, Pt2, Q, SzummaX;

        public string d = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
        public char dec;

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) && e.KeyChar != dec && e.KeyChar != '-';
        }

        public int c1, c2, c3, c4, c5, c6;
        
        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            if (textBox7.Text != "" && textBox8.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox2.Text != "" && textBox12.Text != "" && textBox13.Text != "")
            {
                try
                {
                    textBox12.Text = (Math.Round(SzummaX - Convert.ToDouble(textBox13.Text), 4)).ToString();
                }
                catch (FormatException) { }
            }
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

            if (textBox7.Text != "" && textBox8.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox2.Text != "" && textBox12.Text != "" && textBox13.Text != "")
            {
                try
                {
                    textBox13.Text = (Math.Round(SzummaX - Convert.ToDouble(textBox12.Text), 4)).ToString();
                }
                catch (FormatException) { }
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            textBox15.Visible = textBox19.Visible = !textBox15.Visible;
            label19.Visible = label26.Visible = !label19.Visible;
            textBox15.Text = textBox19.Text = "";
        }

        private void label35_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Visible = false;
        }

        private void label35_MouseHover(object sender, EventArgs e)
        {
            pictureBox2.Visible = true;
        }

        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {
            label31.Visible = label39.Visible = !label31.Visible;
            textBox22.Visible = textBox26.Visible = !textBox22.Visible;
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox6.SelectedIndex == 0)
            {
                textBox27.Text = "0";
                textBox11.Text = "1,25";
            }
            if (comboBox6.SelectedIndex == 1) textBox11.Text = "3";
            if (comboBox6.SelectedIndex == 2) textBox11.Text = "5";
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox5.SelectedIndex == 0)
            {
                textBox24.Text = "0";
                textBox10.Text = "1,25";
            }
            if (comboBox5.SelectedIndex == 1) textBox10.Text = "3";
            if (comboBox5.SelectedIndex == 2) textBox10.Text = "5";
        }

        public int Z1, Z2, I;

        public Kerekek()
        {
            InitializeComponent();
            Success = false;
            first = false;
        }

        private void label25_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
        }

        private void label25_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox6.Visible = !textBox6.Visible;
            comboBox4.Visible = !comboBox4.Visible;
        }

        private void timer_Tick(object sender, EventArgs e) // a bevitt adatok helyességének ellenőrzése
        {
            try
            {
                if (textBox1.Text != "" && Convert.ToDouble(textBox1.Text) < 0) textBox1.Text = "0";
                if (textBox2.Text != "" && Convert.ToDouble(textBox2.Text) < 0) textBox2.Text = "0";
                if (textBox3.Text != "" && Convert.ToDouble(textBox3.Text) < 0) textBox3.Text = "0";
                if (textBox4.Text != "" && Convert.ToDouble(textBox4.Text) < 0) textBox4.Text = "0";
                if (textBox5.Text != "" && Convert.ToDouble(textBox5.Text) < 0) textBox5.Text = "0";
                if (textBox6.Text != "" && Convert.ToDouble(textBox6.Text) < 0) textBox6.Text = "0";
                if (textBox7.Text != "" && Convert.ToDouble(textBox7.Text) < 0) textBox7.Text = "0";
                if (textBox8.Text != "" && Convert.ToDouble(textBox8.Text) < 0) textBox8.Text = "0";
                if (textBox10.Text != "" && Convert.ToDouble(textBox10.Text) < 0) textBox10.Text = "0";
                if (textBox11.Text != "" && Convert.ToDouble(textBox11.Text) < 0) textBox11.Text = "0";
                if (textBox14.Text != "" && Convert.ToDouble(textBox14.Text) < 0) textBox14.Text = "0";
                if (textBox15.Text != "" && Convert.ToDouble(textBox15.Text) < 0) textBox15.Text = "0";
                if (textBox16.Text != "" && Convert.ToDouble(textBox16.Text) < 0) textBox16.Text = "0";
                if (textBox17.Text != "" && Convert.ToDouble(textBox17.Text) < 0) textBox17.Text = "0";
                if (textBox18.Text != "" && Convert.ToDouble(textBox18.Text) < 1) textBox18.Text = "1";
                if (textBox19.Text != "" && Convert.ToDouble(textBox19.Text) < 0) textBox19.Text = "0";
                if (textBox20.Text != "" && Convert.ToDouble(textBox20.Text) < 0) textBox20.Text = "0";
                if (textBox23.Text != "" && Convert.ToDouble(textBox23.Text) < 0) textBox20.Text = "0";
                if (textBox7.Text != "" && textBox8.Text != "") textBox9.Text =
                        (Convert.ToDouble(textBox8.Text) / Convert.ToDouble(textBox7.Text)).ToString();
                if (textBox7.Text != "" && textBox8.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox2.Text != "")
                {
                    double alfa, beta, mn, mt, alfat, alfawt, a, ad, k, h, d1, d2, da1, da2;
                    int z1, z2;
                    if (checkBox1.Checked == false) mn = Convert.ToDouble(textBox6.Text);
                    else
                    {
                        XmlNodeList list = docModul.GetElementsByTagName("modul");
                        XmlNode node = list[comboBox4.SelectedIndex];
                        mn = Convert.ToDouble(node.InnerText);
                    }
                    alfa = Convert.ToDouble(textBox4.Text) / 180 * Math.PI;
                    beta = Convert.ToDouble(textBox3.Text) / 180 * Math.PI;
                    z1 = Convert.ToInt32(textBox7.Text);
                    z2 = Convert.ToInt32(textBox8.Text);
                    a = Convert.ToDouble(textBox2.Text);
                    h = Convert.ToDouble(textBox1.Text);
                    mt = mn / Math.Cos(beta);
                    d1 = mt * z1;
                    d2 = mt * z2;
                    alfat = Math.Atan(Math.Tan(alfa) / Math.Cos(beta));
                    alfawt = Math.Acos(mt / (2 * a) * (z1 + z2) * Math.Cos(alfat));
                    SzummaX = (z1 + z2) / (2 * Math.Tan(alfa)) * (Math.Tan(alfawt) - alfawt - (Math.Tan(alfat) - alfat));
                    ad = mt * (z1 + z2) / 2;
                    k = (a - ad) / mn - SzummaX;
                    label38.Visible = true;
                    label38.Text = "Σx = " + Math.Round(SzummaX, 4).ToString();
                    if (textBox12.Text != "" && textBox13.Text == "") textBox13.Text = (Math.Round(SzummaX - Convert.ToDouble(textBox12.Text), 4)).ToString();
                    if (textBox13.Text != "" && textBox12.Text == "") textBox12.Text = (Math.Round(SzummaX - Convert.ToDouble(textBox13.Text), 4)).ToString();
                    if (textBox12.Text != "" && textBox13.Text != "")
                    {
                        if (Math.Abs((Convert.ToDouble(textBox12.Text) + Convert.ToDouble(textBox13.Text)) - SzummaX) > 0.01) label38.ForeColor = Color.Red;
                        else label38.ForeColor = Color.Black;
                        da1 = Math.Round(d1 + 2 * mn * (Convert.ToDouble(textBox12.Text) + h + k), 4);
                        da2 = Math.Round(d2 + 2 * mn * (Convert.ToDouble(textBox13.Text) + h + k), 4);
                        if (!first)
                        {
                            if (textBox15.Text == "") textBox15.Text = da1.ToString();
                            if (textBox19.Text == "") textBox19.Text = da2.ToString();
                            first = true;
                        }
                    }
                    if (Math.Abs(SzummaX-Convert.ToDouble(textBox12.Text)-Convert.ToDouble(textBox13.Text))>0.01)
                    {
                        textBox12.Clear();
                        textBox13.Clear();
                        textBox15.Clear();
                        textBox19.Clear();
                        first = false;
                    }
                }
            }
            catch (FormatException) { }
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" &&
                textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "" && textBox8.Text != "" &&
                textBox9.Text != "" && textBox10.Text != "" && textBox11.Text != "" && textBox12.Text != "" &&
                textBox13.Text != "" && textBox14.Text != "" && textBox16.Text != "" &&
                textBox17.Text != "" && textBox18.Text != "" && textBox20.Text != "" &&
                textBox21.Text != "" && textBox23.Text != "" && textBox24.Text != "" &&
                textBox25.Text != "" && textBox27.Text != "")
            {
                if (checkBox2.Checked)
                {
                    if (textBox22.Text != "" && textBox26.Text != "") button1.Enabled = true;
                    else button1.Enabled = false;
                }
                else button1.Enabled = true;
            }
            else button1.Enabled = false;  
        }

        XmlDocument docAnyagok, docKenoanyagok, docModul;
        string fullPathAnyagok, fullPathKenoanyagok, fullPathModul;

        private void Kerekek_Load(object sender, EventArgs e)
        {
            dec = d[0];
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" &&
                textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "" && textBox8.Text != "" &&
                textBox9.Text != "" && textBox10.Text != "" && textBox11.Text != "" && textBox12.Text != "" &&
                textBox13.Text != "" && textBox14.Text != "" && textBox16.Text != "" &&
                textBox17.Text != "" && textBox18.Text != "" && textBox20.Text != "" &&
                textBox21.Text != "" && textBox23.Text != "" && textBox24.Text != "" &&
                textBox25.Text != "" && textBox27.Text != "")
            {
                if (checkBox2.Checked)
                {
                    if (textBox22.Text != "" && textBox26.Text != "") button1.Enabled = true;
                    else button1.Enabled = false;
                }
                else button1.Enabled = true;
            }
            else button1.Enabled = false;
            checkBox2.Checked = false;
            label31.Visible = false;
            label38.Visible = false;
            label39.Visible = false;
            textBox22.Visible = false;
            textBox26.Visible = false;
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            comboBox4.Visible = false;
            string fnAnyagok = @"../../anyagok.xml";
            string fnKanyagok = @"../../kenoanyagok.xml";
            string fnKA = @"../../dynamic factor.png";
            string fnPt = @"../../yf.png";
            string fnModul = @"../../modul_DIN780.xml";
            string fnAP = @"../../din867.png";
            Bitmap df = new Bitmap(fnKA);
            Bitmap df2 = new Bitmap(fnPt);
            Bitmap df3 = new Bitmap(fnAP);
            Picture ka = new Picture(pictureBox1, df);
            Picture pt = new Picture(pictureBox2, df2);
            Picture ap = new Picture(pictureBox3, df3);
            Bitmap dfnew = ka.ResizeImage(342, 171); //kép újraméretezése, hogy beleférjen a tabba
            Bitmap dfnew2 = pt.ResizeImage(342, 171);
            Bitmap dfnew3 = ap.ResizeImage(322, 161);
            pictureBox1.ClientSize = new Size(dfnew.Width, dfnew.Height);
            pictureBox1.Image = dfnew;
            pictureBox2.ClientSize = new Size(dfnew2.Width, dfnew2.Height);
            pictureBox2.Image = dfnew2;
            pictureBox3.ClientSize = new Size(dfnew3.Width, dfnew2.Height);
            pictureBox3.Image = dfnew3;
            fullPathAnyagok = Path.GetFullPath(fnAnyagok);
            fullPathKenoanyagok = Path.GetFullPath(fnKanyagok);
            fullPathModul = Path.GetFullPath(fnModul);
            docAnyagok = new XmlDocument();
            docKenoanyagok = new XmlDocument();
            docModul = new XmlDocument();
            docAnyagok.Load(fullPathAnyagok);
            docKenoanyagok.Load(fullPathKenoanyagok);
            docModul.Load(fullPathModul);
            Dropdowngenerator gen1, gen2, gen3, gen4;
            gen1 = new Dropdowngenerator(docAnyagok, comboBox1);
            gen1.GenerateBox();
            gen2 = new Dropdowngenerator(docAnyagok, comboBox2);
            gen2.GenerateBox();
            gen3 = new Dropdowngenerator(docKenoanyagok, comboBox3);
            gen3.GenerateBox();
            gen4 = new Dropdowngenerator(docModul, comboBox4);
            gen4.GenerateBox();
            comboBox5.Items.Add("Köszörülés");
            comboBox5.Items.Add("Lefejtő marás");
            comboBox5.Items.Add("Fogvésés");
            comboBox6.Items.Add("Köszörülés");
            comboBox6.Items.Add("Lefejtő marás");
            comboBox6.Items.Add("Fogvésés");
            for (int i = 0; i < 12; i++)
            {
                comboBox7.Items.Add((i + 1).ToString());
            }
            if (button1.Enabled == false)
            {
                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 0;
                comboBox3.SelectedIndex = 8;
                comboBox4.SelectedIndex = 0;
                comboBox5.SelectedIndex = 0;
                comboBox6.SelectedIndex = 0;
                comboBox7.SelectedIndex = 6;
            }
            else
            {
                comboBox1.SelectedIndex = c1;
                comboBox2.SelectedIndex = c2;
                comboBox3.SelectedIndex = c3;
                comboBox4.SelectedIndex = 0;
                comboBox5.SelectedIndex = c4;
                comboBox6.SelectedIndex = c5;
                comboBox7.SelectedIndex = c6;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool ex = false;
            try
            {
                H = Convert.ToDouble(textBox1.Text);
                C = Convert.ToDouble(textBox5.Text);
                A = Convert.ToDouble(textBox2.Text);
                Beta = Convert.ToDouble(textBox3.Text);
                Alfa = Convert.ToDouble(textBox4.Text);
                if (textBox6.Visible) Mn = Convert.ToDouble(textBox6.Text);
                Z1 = Convert.ToInt32(textBox7.Text);
                Z2 = Convert.ToInt32(textBox8.Text);
                Ra1 = Convert.ToDouble(textBox10.Text);
                Ra2 = Convert.ToDouble(textBox11.Text);
                X1 = Convert.ToDouble(textBox12.Text);
                X2 = Convert.ToDouble(textBox13.Text);
                B = Convert.ToDouble(textBox14.Text);
                N1 = Convert.ToDouble(textBox16.Text);
                P = Convert.ToDouble(textBox17.Text);
                KA = Convert.ToDouble(textBox18.Text);
                SH = Convert.ToDouble(textBox20.Text);
                SF = Convert.ToDouble(textBox23.Text);
                U = Convert.ToDouble(textBox9.Text);
                RoaP = Convert.ToDouble(textBox21.Text);
                Ca1 = Convert.ToDouble(textBox22.Text);
                Ca2 = Convert.ToDouble(textBox26.Text);
                Pt1 = Convert.ToDouble(textBox24.Text);
                Pt2 = Convert.ToDouble(textBox27.Text);
                Q = Convert.ToDouble(textBox25.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Nem megfelelő a beviteli formátum!");
                ex = true;
            }
            if (!ex)
            {
                tabControl1.SelectedIndex = 0;
                H = Convert.ToDouble(textBox1.Text);
                C = Convert.ToDouble(textBox5.Text);
                A = Convert.ToDouble(textBox2.Text);
                Beta = Convert.ToDouble(textBox3.Text) / 180 * Math.PI;
                Alfa = Convert.ToDouble(textBox4.Text) / 180 * Math.PI;
                if (checkBox1.Checked == false) Mn = Convert.ToDouble(textBox6.Text);
                else
                {
                    XmlNodeList list = docModul.GetElementsByTagName("modul");
                    XmlNode node = list[comboBox4.SelectedIndex];
                    Mn = Convert.ToDouble(node.InnerText);
                }
                Alfat = Math.Atan(Math.Tan(Alfa) / Math.Cos(Beta));
                Betab = Math.Asin(Math.Cos(Alfa) * Math.Sin(Beta));
                Mt = Mn / Math.Cos(Beta);
                Z1 = Convert.ToInt32(textBox7.Text);
                Z2 = Convert.ToInt32(textBox8.Text);               
                Ra1 = Convert.ToDouble(textBox10.Text);
                Ra2 = Convert.ToDouble(textBox11.Text);
                X1 = Convert.ToDouble(textBox12.Text);
                X2 = Convert.ToDouble(textBox13.Text);
                B = Convert.ToDouble(textBox14.Text);
                N1 = Convert.ToDouble(textBox16.Text);
                P = Convert.ToDouble(textBox17.Text);
                KA = Convert.ToDouble(textBox18.Text);
                SH = Convert.ToDouble(textBox20.Text);
                SF = Convert.ToDouble(textBox23.Text);
                RoaP = Convert.ToDouble(textBox21.Text);
                Ca1 = Convert.ToDouble(textBox22.Text);
                Ca2 = Convert.ToDouble(textBox26.Text);
                Pt1 = Convert.ToDouble(textBox24.Text);
                Pt2 = Convert.ToDouble(textBox27.Text);
                Q = Convert.ToDouble(textBox25.Text);
                Spr1 = Pt1 - Q;
                Spr2 = Pt2 - Q;
                if (Spr1 < 0) Spr1 = 0;
                if (textBox15.Text == "" || textBox19.Text == "")
                {
                    DA1 = 0;
                    DA2 = 0;
                }
                else
                {
                    DA1 = Convert.ToDouble(textBox15.Text);
                    DA2 = Convert.ToDouble(textBox19.Text);
                }
                U = Convert.ToDouble(textBox9.Text);
                XmlNodeList nodeList1 = docAnyagok.GetElementsByTagName("anyag");
                XmlNodeList nodeList2 = docKenoanyagok.GetElementsByTagName("kenoanyag");
                XmlNode node1, node2, node3;
                node1 = nodeList1[comboBox1.SelectedIndex];
                node2 = nodeList1[comboBox2.SelectedIndex];
                node3 = nodeList2[comboBox3.SelectedIndex];
                string name1 = node1.InnerText;
                string szigmaH1 = node1.Attributes["SzigmaH"].Value;
                string szigmaF1 = node1.Attributes["SzigmaF"].Value;
                string name2 = node2.InnerText;
                string szigmaH2 = node2.Attributes["SzigmaH"].Value;
                string szigmaF2 = node2.Attributes["SzigmaF"].Value;
                Anyag1 = new Anyag(name1, szigmaH1, szigmaF1);
                Anyag2 = new Anyag(name2, szigmaH2, szigmaF2);
                V40 = Convert.ToDouble(node3.Attributes["viszkozitas"].Value);
                I = comboBox7.SelectedIndex + 1;
                D1 = Z1 * Mt;
                D2 = Z2 * Mt;
                K1 = new Kerek(Anyag1, Z1, Mn, Beta, X1, Ra1, B, D1); //kiskerék
                K2 = new Kerek(Anyag2, Z2, Mn, Beta, X2, Ra2, B, D2); //nagykerék
                c1 = comboBox1.SelectedIndex;
                c2 = comboBox2.SelectedIndex;
                c3 = comboBox3.SelectedIndex;
                c4 = comboBox5.SelectedIndex;
                c5 = comboBox6.SelectedIndex;
                c6 = comboBox7.SelectedIndex;
                Success = true;
                this.Close();
            }
        }
    }
}
