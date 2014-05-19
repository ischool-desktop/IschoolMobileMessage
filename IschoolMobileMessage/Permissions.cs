using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IschoolMobileMessage
{
    class Permissions
    {
        public static string 學校公告 { get { return "IschoolMobileMessage.44615D66-4BF5-41C1-B274-4A8773ECC148"; } }

        public static bool 學校公告權限
        {
            get { return FISCA.Permission.UserAcl.Current[學校公告].Executable; }
        }
    }
}
