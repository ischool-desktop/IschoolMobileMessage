﻿using FISCA;
using FISCA.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IschoolMobileMessage
{
    public class Program
    {

        [FISCA.MainMethod]
        public static void main()
        {
            //診斷模式不要執行 UDM 更新。(此段code尚未發佈)
            if (!RTContext.IsDiagMode)
                ServerModule.AutoManaged("http://module.ischool.com.tw/module/63816/ischool.parent.app/udm.xml");

            FISCA.Presentation.RibbonBarItem item1 = FISCA.Presentation.MotherForm.RibbonBarItems["學務作業", "校務通訊"];
            item1["學校公告"].Image = Properties.Resources.script_fav_128;
            item1["學校公告"].Size = FISCA.Presentation.RibbonBarButton.MenuButtonSize.Large;
            item1["學校公告"].Enable = Permissions.學校公告權限;

            item1["學校公告"].Click += delegate
            {
                MainForm form = new MainForm();
                form.ShowDialog();
            };

            //權限設定
            Catalog permission = RoleAclSource.Instance["學務作業"]["功能按鈕"];
            permission.Add(new RibbonFeature(Permissions.學校公告, "學校公告"));
        }
    }
}
