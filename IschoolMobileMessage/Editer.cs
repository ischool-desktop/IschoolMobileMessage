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
        private ServiceCaller _caller;
        private Response _resp;
        public Editer(ServiceCaller caller)
        {
            InitializeComponent();
            this._caller = caller;
            this._resp = new Response();
        }

        public Editer(ServiceCaller caller, Response resp)
        {
            InitializeComponent();
            this._caller = caller;
            this._resp = resp;

            txtUnit.Text = _resp.Unit;
            txtSubject.Text = _resp.Subject;
            txtContent.Text = _resp.Content;
            txtUrl.Text = _resp.URL;
            dtInputer.Value = _resp.ValidDate;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            _resp.School = K12.Data.School.ChineseName;
            _resp.Unit = txtUnit.Text;
            _resp.Subject = txtSubject.Text;
            _resp.Content = txtContent.Text;
            _resp.URL = txtUrl.Text;
            _resp.ValidDate = dtInputer.Value;

            if (string.IsNullOrWhiteSpace(_resp.School))
                MessageBox.Show("未設定學校名稱,請確認學校資訊已設定完成後再繼續...");
            else
                _caller.Save(_resp);
               
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
