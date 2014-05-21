using FISCA.Data;
using FISCA.Presentation.Controls;
using FISCA.UDT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace IschoolMobileMessage
{
    public partial class MainForm : BaseForm
    {
        List<MessageRecord> data;
        List<MessageRecord> _InsertList, _UpdateList;
        BindingSource source;
        AccessHelper _A = new AccessHelper();
        QueryHelper _Q = new QueryHelper();

        public MainForm()
        {
            InitializeComponent();
            data = new List<MessageRecord>();
            source = new BindingSource();
            _InsertList = new List<MessageRecord>();
            _UpdateList = new List<MessageRecord>();

            dataGridViewX1.AutoGenerateColumns = false;
            dataGridViewX1.DataSource = source;
            source.DataSource = data;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            FormRefresh();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            _InsertList.Clear();

            DataTable dtable = _Q.Select("SELECT NOW()");
            //Parse資料
            DateTime time = DateTime.Now;
            DateTime.TryParse("" + dtable.Rows[0][0], out time);

            MessageRecord record = new MessageRecord();
            record.Datetime = time;
            record.Status = 1;
            record.OwnerType = 0;
            record.Owner = FISCA.Authentication.DSAServices.UserAccount;
            record.TargetType = 0;
            record.Target = "All";

            Editer edit = new Editer(record);
            edit.ShowDialog();

            if (edit.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                _InsertList.Add(record);
                _A.InsertValues(_InsertList);

                FormRefresh();
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            _UpdateList.Clear();

            if (dataGridViewX1.SelectedRows.Count > 0)
            {
                int index = dataGridViewX1.SelectedRows[0].Index;

                if (MessageBox.Show("確認刪除?", "ischool", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    data[index].Status = 0;

                    //data[index].Content = CombineContent(data[index].Content);

                    _UpdateList.Add(data[index]);
                    _A.UpdateValues(_UpdateList);

                    FormRefresh();
                }
            }
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewX1_DoubleClick(object sender, EventArgs e)
        {
            _UpdateList.Clear();

            if (dataGridViewX1.SelectedRows.Count > 0)
            {
                int index = dataGridViewX1.SelectedRows[0].Index;
                Editer edit = new Editer(data[index]);
                edit.ShowDialog();

                if (edit.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    _UpdateList.Add(data[index]);
                    _A.UpdateValues(_UpdateList);

                    FormRefresh();
                }
            }
        }

        private void FormRefresh()
        {
            data.Clear();

            data = _A.Select<MessageRecord>("status=1");

            foreach (MessageRecord record in data)
            {
                XmlDocument doc = new XmlDocument();
                record.Title = "";
                record.Org = "";
                record.Body = "";

                try 
                {
                    doc.LoadXml(record.Content);
                }
                catch
                {
                    //do nothing...
                }

                if (doc.SelectSingleNode("//Title") != null)
                    record.Title = doc.SelectSingleNode("//Title").InnerText;
                if (doc.SelectSingleNode("//Org") != null)
                    record.Org = doc.SelectSingleNode("//Org").InnerText;
                if (doc.SelectSingleNode("//Body") != null)
                    record.Body = doc.SelectSingleNode("//Body").InnerText;
                //record.Content = SelectContent(record.Content);
            }

            data.Sort(delegate(MessageRecord x, MessageRecord y)
            {
                return y.Datetime.CompareTo(x.Datetime);
            });

            source.DataSource = data;
            //source.ResetBindings(false);
        }

        //private string SelectContent(string str)
        //{
        //    string retVal = "";
        //    XmlDocument doc = new XmlDocument();
        //    try
        //    {
        //        doc.LoadXml(str);
        //        retVal = doc.SelectSingleNode("//Body").InnerText;
        //    }
        //    catch
        //    {
        //    }

        //    return retVal;
        //}

        //private string CombineContent(string str)
        //{
        //    string retVal = "";

        //    XmlElement root = new XmlDocument().CreateElement("Message");
        //    XmlElement title = root.OwnerDocument.CreateElement("Title");
        //    title.InnerText = "";
        //    XmlElement org = root.OwnerDocument.CreateElement("Org");
        //    org.InnerText = "";
        //    XmlElement body = root.OwnerDocument.CreateElement("Body");
        //    body.InnerText = str;

        //    root.AppendChild(title);
        //    root.AppendChild(org);
        //    root.AppendChild(body);

        //    retVal = root.OuterXml;

        //    return retVal;
        //}
    }
}
