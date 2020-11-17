using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;
using System.IO;

namespace fogaskerekek
{
    class Dropdowngenerator
    {
        private ComboBox Box;
        public XmlDocument Doc;

        public Dropdowngenerator(XmlDocument doc,ComboBox box)
        {
            Doc = doc;
            Box = box;
        }
        
        public void GenerateBox()
        {
            Box.Items.Clear();
            foreach(XmlNode node in Doc.DocumentElement.ChildNodes)
            {
                Box.Items.Add(node.InnerText);
            }
        }        
    }
}
