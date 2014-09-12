using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace IschoolMobileMessage
{
    public class Response
    {
        private int? _uid;
        public string School { get; set; }
        public string Unit { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string URL { get; set; }
        public DateTime ValidDate { get; set; }

        public DateTime LastUpdate { get; set; }

        public int? UID
        {
            get
            {
                return _uid;
            }
        }

        public Response()
        {

        }

        /// <summary>
        /// XML轉物件類別
        /// </summary>
        /// <param name="elem"></param>
        public Response(XElement elem)
        {
            //UID
            XElement uid = elem.XPathSelectElement("Uid");
            if(uid != null)
            {
                string uidStr = uid.Value;
                int i;
                if (int.TryParse(uidStr, out i))
                    this._uid = i;
            }

            //LastUpdateDate && ValidDate
            DateTime dt;
            XElement lastUpdate = elem.XPathSelectElement("LastUpdate");
            if (lastUpdate != null)
                this.LastUpdate = DateTime.TryParse(lastUpdate.Value, out dt) ? dt : new DateTime();
            else
                this.LastUpdate = new DateTime();

            XElement validDate = elem.XPathSelectElement("ValidDate");
            if (validDate != null)
                this.ValidDate = DateTime.TryParse(validDate.Value, out dt) ? dt : new DateTime();
            else
                this.ValidDate = new DateTime();

            //School
            XElement school = elem.XPathSelectElement("Content/Message/From/School");
            if (school != null)
                this.School = school.Value;
            else
                this.School = string.Empty;

            //Unit
            XElement unit = elem.XPathSelectElement("Content/Message/From/Unit");
            if (unit != null)
                this.Unit = unit.Value;
            else
                this.Unit = string.Empty;

            //Subject
            XElement subject = elem.XPathSelectElement("Content/Message/Subject");
            if (subject != null)
                this.Subject = subject.Value;
            else
                this.Subject = string.Empty;

            //Content
            XElement content = elem.XPathSelectElement("Content/Message/Content");
            if (content != null)
                this.Content = content.Value;
            else
                this.Content = string.Empty;

            //URL
            XElement url = elem.XPathSelectElement("Content/Message/Attachments/URL");
            if (url != null)
                this.URL = url.Value;
            else
                this.URL = string.Empty;
        }
    }
}
