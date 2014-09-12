using FISCA.Data;
using FISCA.Presentation.Controls;
using FISCA.UDT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace IschoolMobileMessage
{
    public partial class MainForm : BaseForm
    {
        BindingSource _source;
        ServiceCaller _caller;
        List<Response> _data;
        DateTime _dtStart;
        DateTime _dtEnd;

        public MainForm()
        {
            InitializeComponent();
            _source = new BindingSource();
            _caller = new ServiceCaller();
            _data = new List<Response>();

            dataGridViewX1.AutoGenerateColumns = false;
            dataGridViewX1.DataSource = _source;
            _source.DataSource = _data;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Editer edit = new Editer(_caller);
            edit.ShowDialog();

            if (edit.DialogResult == System.Windows.Forms.DialogResult.OK)
                FormRefresh();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewX1.SelectedRows.Count > 0)
            {
                int index = dataGridViewX1.SelectedRows[0].Index;

                if (MessageBox.Show("確認刪除?", "ischool", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    Response r = _data[index];
                    if(r.UID != null)
                        _caller.Delete(r);

                    FormRefresh();
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewX1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridViewX1.SelectedRows.Count > 0)
            {
                int index = dataGridViewX1.SelectedRows[0].Index;
                Editer edit = new Editer(_caller,_data[index]);
                edit.ShowDialog();

                if (edit.DialogResult == System.Windows.Forms.DialogResult.OK)
                    FormRefresh();
            }
        }

        private void FormRefresh()
        {
            _data.Clear();
            if (_dtStart != null && _dtEnd != null)
                _data = _caller.Select(_dtStart.ToString("yyyy/MM/dd"), _dtEnd.ToString("yyyy/MM/dd"));

            _data.Sort(delegate(Response x, Response y)
            {
                return x.LastUpdate.CompareTo(y.LastUpdate);
            });

            _source.DataSource = _data;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            _dtStart = dtInputerStart.Value;
            _dtEnd = dtInputerEnd.Value;
            FormRefresh();
        }
    }
}
