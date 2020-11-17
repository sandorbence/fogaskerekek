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
    public partial class Anyagok : Form
    {
        public string NevFA, NevKA;
        public double SzigmaH, SzigmaF, V40;
        public Anyag A;
        public XmlDocument docAnyagok, docKenoanyagok;
        string fullPathAnyagok, fullPathKenoanyagok;
        private bool B1 = false;
        private bool B2 = false;

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != comboBox1.Items.IndexOf("Új anyag"))
            {
                XmlNodeList list = docAnyagok.GetElementsByTagName("anyag");
                XmlNode node = list[comboBox1.SelectedIndex];
                button1.Text = "Szerkeszt";
                textBox1.Text = node.InnerText;
                textBox2.Text = node.Attributes["SzigmaH"].Value;
                textBox3.Text = node.Attributes["SzigmaF"].Value;
                B1 = true;
            }
            else
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                B1 = false;
                button1.Text = "Hozzáad";
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex != comboBox2.Items.IndexOf("Új anyag"))
            {
                XmlNodeList list = docKenoanyagok.GetElementsByTagName("kenoanyag");
                XmlNode node = list[comboBox2.SelectedIndex];
                textBox4.Text = node.InnerText;
                textBox5.Text = node.Attributes["viszkozitas"].Value;
                button2.Text = "Szerkeszt";
                B2 = true;
            }
            else
            {
                textBox4.Text = "";
                textBox5.Text = "";
                B2 = false;
                button2.Text = "Hozzáad";
            }
        }

        public Anyagok(string fa,string ka)
        {
            InitializeComponent();
            docAnyagok = new XmlDocument();
            docKenoanyagok = new XmlDocument();
            fullPathAnyagok = Path.GetFullPath(fa);
            fullPathKenoanyagok = Path.GetFullPath(ka);
            docAnyagok.Load(fullPathAnyagok);
            docKenoanyagok.Load(fullPathKenoanyagok);
        }

        private void Ujanyag_Load(object sender, EventArgs e)
        {
            Dropdowngenerator d1, d2;
            d1 = new Dropdowngenerator(docAnyagok, comboBox1);
            d2 = new Dropdowngenerator(docKenoanyagok, comboBox2);
            d1.GenerateBox();
            d2.GenerateBox();
            comboBox1.Items.Add("Új anyag");
            comboBox2.Items.Add("Új anyag");
        }

        private void button1_Click(object sender, EventArgs e)
        {         
            bool ex = false;
            try
            {
                NevFA = textBox1.Text;
                SzigmaH = Convert.ToDouble(textBox2.Text);
                SzigmaF = Convert.ToDouble(textBox3.Text);
            }
            catch(System.FormatException)
            {
                MessageBox.Show("Nem megfelelő a beviteli formátum!");
                ex = true;
            }
            if (!ex)
            {
                NevFA = textBox1.Text;
                SzigmaH = Convert.ToDouble(textBox2.Text);
                SzigmaF = Convert.ToDouble(textBox3.Text);
                A = new Anyag(NevFA, SzigmaH, SzigmaF);
                if(B1)
                {
                    A.EditNode(docAnyagok, comboBox1.SelectedIndex);
                }
                else A.ToXml(docAnyagok);
                docAnyagok.Save(fullPathAnyagok);
                Dropdowngenerator d1 = new Dropdowngenerator(docAnyagok, comboBox1);
                d1.GenerateBox();
                comboBox1.Items.Add("Új anyag");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool ex = false;
            try
            {
                NevKA = textBox4.Text;
                V40 = Convert.ToDouble(textBox5.Text);
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Nem megfelelő a beviteli formátum!");
                ex = true;
            }
            if (!ex)
            {
                NevKA = textBox4.Text;
                V40 = Convert.ToDouble(textBox5.Text);
                if (B2)
                {
                    XmlNodeList list = docKenoanyagok.GetElementsByTagName("kenoanyag");
                    XmlNode node = list[comboBox2.SelectedIndex];
                    node.InnerText = NevKA;
                    node.Attributes[0].Value = V40.ToString();
                }
                else
                {
                    XmlElement xmlel = docKenoanyagok.CreateElement("kenoanyag");
                    xmlel.SetAttribute("viszkozitas", V40.ToString());
                    xmlel.InnerText = NevKA;
                    docKenoanyagok.DocumentElement.AppendChild(xmlel);
                }
                docKenoanyagok.Save(fullPathKenoanyagok);
                Dropdowngenerator d2 = new Dropdowngenerator(docKenoanyagok, comboBox2);
                d2.GenerateBox();
                comboBox2.Items.Add("Új anyag");
            }
        }
    }
}
