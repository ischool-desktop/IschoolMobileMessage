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
        private MessageRecord _Record;
        public Editer(MessageRecord record)
        {
            InitializeComponent();

            _Record = record;

            txtTitle.Text = string.IsNullOrWhiteSpace(_Record.Title) ? "" : _Record.Title;
            txtOrg.Text = string.IsNullOrWhiteSpace(_Record.Org) ? "" : _Record.Org;
            txtBody.Text = string.IsNullOrWhiteSpace(_Record.Body) ? "" : _Record.Body.Replace("<br>","\r\n");
            //txtBody.Text = _Record.Content;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            string content = txtBody.Text;

            XmlElement root = new XmlDocument().CreateElement("Message");
            XmlElement title = root.OwnerDocument.CreateElement("Title");
            title.InnerText = txtTitle.Text;
            XmlElement org = root.OwnerDocument.CreateElement("Org");
            org.InnerText = txtOrg.Text;
            XmlElement body = root.OwnerDocument.CreateElement("Body");
            body.InnerText = txtBody.Text.Replace("\r\n", "<br>");

            root.AppendChild(title);
            root.AppendChild(org);
            root.AppendChild(body);

            _Record.Content = root.OuterXml;
            MessageBox.Show("儲存完成!");
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void txtBody_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                ((TextBox)sender).SelectAll();
            } 
        }
    }
}
