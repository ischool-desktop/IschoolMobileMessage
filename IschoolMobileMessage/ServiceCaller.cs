using FISCA.DSAClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace IschoolMobileMessage
{
    public class ServiceCaller
    {
        private Connection _conn;
        private bool _success;

        public ServiceCaller()
        {
            _success = true;
            _conn = new Connection();
            if (FISCA.Authentication.DSAServices.PassportToken != null)
                _conn.Connect(FISCA.Authentication.DSAServices.AccessPoint, "ischool.parent.desktop", FISCA.Authentication.DSAServices.PassportToken);
            else
                _success = false;
        }

        /// <summary>
        /// 發布公告
        /// </summary>
        /// <param name="school"></param>
        /// <param name="unit"></param>
        /// <param name="subject"></param>
        /// <param name="content"></param>
        /// <param name="url"></param>
        /// <param name="validDate"></param>
        public void Insert(string school, string unit, string subject, string content, string url, string validDate)
        {
            if (_success)
            {
                XElement xml = new XElement("Request",
                    new XElement("Ischool.im.message",
                        new XElement("Content",
                            new XElement("Message", new XElement("From", new XElement("School", school), new XElement("Unit", unit)), new XElement("Subject", subject), new XElement("Content", new XCData(content)), new XElement("Attachments", new XElement("URL", url)))),
                            new XElement("ValidDate", validDate)));

                Envelope rsp = _conn.SendRequest("im.PostBulletin", new Envelope(new XmlStringHolder(xml)));
            }
            else
                MessageBox.Show("此登入帳號無法通過驗證,請嘗試重啟功能或切換帳號","呼叫新增服務失敗");
        }

        /// <summary>
        /// 取得公告
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public List<Response> Select(string dateStart, string dateEnd)
        {
            List<Response> retVal = new List<Response>();

            if (_success)
            {
                XElement xml = new XElement("Request", new XElement("ValidDateStart", dateStart), new XElement("ValidDateEnd", dateEnd));

                Envelope rsp = _conn.SendRequest("im.GetBulletin", new Envelope(new XmlStringHolder(xml)));
                XElement xrsp = XElement.Parse(rsp.Body.XmlString);
                foreach (XElement x1 in xrsp.Elements("Ischool.im.message"))
                {
                    retVal.Add(new Response(x1));
                }
            }
            else
                MessageBox.Show("此登入帳號無法通過驗證,請嘗試重啟功能或切換帳號", "呼叫查詢服務失敗");

            return retVal;
        }

        /// <summary>
        /// 更新公告
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="school"></param>
        /// <param name="unit"></param>
        /// <param name="subject"></param>
        /// <param name="content"></param>
        /// <param name="url"></param>
        /// <param name="validDate"></param>
        public void Update(string UID,string school, string unit, string subject, string content, string url, string validDate)
        {
            if (_success)
            {
                XElement xml = new XElement("Request",
                    new XElement("Ischool.im.message",
                        new XElement("Content",
                            new XElement("Message", new XElement("From", new XElement("School", school), new XElement("Unit", unit)), new XElement("Subject", subject), new XElement("Content", new XCData(content)), new XElement("Attachments", new XElement("URL", url)))),
                            new XElement("ValidDate", validDate),
                            new XElement("Uid",UID)));

                Envelope rsp = _conn.SendRequest("im.UpdateBulletin", new Envelope(new XmlStringHolder(xml)));
            }
            else
                MessageBox.Show("此登入帳號無法通過驗證,請嘗試重啟功能或切換帳號", "呼叫更新服務失敗");
        }

        /// <summary>
        /// 儲存或更新公告
        /// </summary>
        /// <param name="resp"></param>
        public void Save(Response resp)
        {
            if (resp.UID == null)
                Insert(resp.School, resp.Unit, resp.Subject, resp.Content, resp.URL, resp.ValidDate.ToString("yyyy/MM/dd HH:mm:ss"));
            else
                Update(resp.UID + "",resp.School, resp.Unit, resp.Subject, resp.Content, resp.URL, resp.ValidDate.ToString("yyyy/MM/dd HH:mm:ss"));
        }

        /// <summary>
        /// 封存公告
        /// </summary>
        /// <param name="UID"></param>
        public void Delete(string UID)
        {
            if(_success)
            {
                XElement xml = new XElement("Request", new XElement("Ischool.im.message", new XElement("Uid", UID)));

                Envelope rsp = _conn.SendRequest("im.ArchiveBulletin", new Envelope(new XmlStringHolder(xml)));
            }
            else
                MessageBox.Show("此登入帳號無法通過驗證,請嘗試重啟功能或切換帳號", "呼叫刪除服務失敗");
        }

        public void Delete(Response resp)
        {
            Delete(resp.UID + "");
        }
    }
}
