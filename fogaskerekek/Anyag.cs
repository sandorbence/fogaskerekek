using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace fogaskerekek
{
    public class Anyag
    {
        public string Name;
        public double SzigmaH, SzigmaF;

        public Anyag(string name, double szh, double szf)
        {
            Name = name;
            SzigmaF = szf;
            SzigmaH = szh;
        }
        public Anyag(string name, string szh, string szf)
        {
            Name = name;
            SzigmaH = Convert.ToDouble(szh);
            SzigmaF = Convert.ToDouble(szf);
        }
        public void ToXml(XmlDocument doc)
        {
            XmlElement el = doc.CreateElement("anyag");
            el.SetAttribute("SzigmaH", SzigmaH.ToString());
            el.SetAttribute("SzigmaF", SzigmaF.ToString());
            el.InnerText = Name;
            doc.DocumentElement.AppendChild(el);
        }
        
        public void EditNode(XmlDocument doc,int index)
        {
            XmlNodeList list = doc.GetElementsByTagName("anyag");
            XmlNode node = list[index];
            node.InnerText = Name;
            node.Attributes[0].Value = SzigmaH.ToString();
            node.Attributes[1].Value = SzigmaF.ToString();
        }

        public override string ToString()
        {
            return this.Name + " (σHlim = " + Convert.ToString(SzigmaH) + " [Mpa], σFlim = " + Convert.ToString(SzigmaF) + " [Mpa])";
        }
    }
}
