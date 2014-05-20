using FISCA.Presentation.Controls;
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

namespace IschoolMobileMessage
{
    public partial class Editer : BaseForm
    {
        private MessageRecords _Record;
        public Editer(MessageRecords record)
        {
            InitializeComponent();

            _Record = record;

            textBoxX1.Text = _Record.Content;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            string content = textBoxX1.Text;

            XmlElement root = new XmlDocument().CreateElement("Message");
            XmlElement title = root.OwnerDocument.CreateElement("Title");
            title.InnerText = "";
            XmlElement org = root.OwnerDocument.CreateElement("Org");
            org.InnerText = "";
            XmlElement body = root.OwnerDocument.CreateElement("Body");
            body.InnerText = content;

            root.AppendChild(title);
            root.AppendChild(org);
            root.AppendChild(body);

            _Record.Content = root.OuterXml;
            MessageBox.Show("儲存完成!");
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
