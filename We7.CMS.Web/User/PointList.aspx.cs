﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using We7.CMS.Helpers;
using We7.CMS.Common.Enum;
using We7.CMS.Common.PF;
using We7.CMS.Accounts;

namespace We7.CMS.Web.User
{
    public partial class PointList : UserBasePage
    {
        PointHelper PointHelper
        {
            get { return HelperFactory.GetHelper<PointHelper>(); }
        }

        public Account ThisAccount
        {
            get
            {
                Account a = new Account();
                if (!We7Helper.IsEmptyID(Security.CurrentAccountID))
                    a = AccountHelper.GetAccount(Security.CurrentAccountID, null);
                return a;
            }
        }

        void DataBind()
        {
            gvList.DataSource = PointHelper.ListAllPointByAccount(Security.CurrentAccountID);
            gvList.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBind();
            }
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvList.PageIndex = e.NewPageIndex>=0?e.NewPageIndex:0;
            DataBind();
        }

        protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id=gvList.DataKeys[e.RowIndex]["ID"] as string;
            PointHelper.DelPoint(id);
            DataBind();
        }

    }
}
