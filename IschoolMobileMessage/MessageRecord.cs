using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IschoolMobileMessage
{
    [FISCA.UDT.TableName("ischool.mobile.message")]
    public class MessageRecord : FISCA.UDT.ActiveRecord
    {
        [FISCA.UDT.Field(Field = "datetime")]
        public DateTime Datetime { get; set; }

        [FISCA.UDT.Field(Field = "ref_message_id", Indexed = true)]
        public int RefMsgId { get; set; }

        [FISCA.UDT.Field(Field = "content")]
        public string Content { get; set; }

        [FISCA.UDT.Field(Field = "owner_type")]
        public int OwnerType { get; set; }

        [FISCA.UDT.Field(Field = "owner", Indexed = true)]
        public string Owner { get; set; }

        [FISCA.UDT.Field(Field = "target_type")]
        public int TargetType { get; set; }

        [FISCA.UDT.Field(Field = "target", Indexed = true)]
        public string Target { get; set; }

        [FISCA.UDT.Field(Field = "status")]
        public int Status { get; set; }

        public string Title { get; set; }

        public string Org { get; set; }

        public string Body { get; set; }
    }
}
