using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;

namespace TJVISA
{
    public static class Dialog
    {
        public static void Error(Page page,MessageBuilder msg)
        {
            string script =
                string.Format(
                    "function RunOnce() {{ radalert('{0}', 400, 100,'{1}');Sys.Application.remove_load(RunOnce); return false; }}  Sys.Application.add_load(RunOnce);", msg.Message,
                    GlobalConstant.ProductName);
            ScriptManager.RegisterStartupScript(page,typeof(Page),"ErrorDialog",script,true);
        }

        public static void Error(Page page, Exception ex)
        {
            using (var builder=new MessageBuilder())
            {
                builder.AppendLine(ex);
                Error(page, builder);
            }
        }

        public static void Error(Page page, string err)
        {
            using (var builder = new MessageBuilder())
            {
                builder.AppendLine(err);
                Error(page, builder);
            }
        }

        public static void Info(Page page, string info)
        {
            using (var builder = new MessageBuilder())
            {
                builder.AppendLine(info);
                Info(page, builder);
            }
        }

        public static void Info(Page page, Exception ex)
        {
            using (var builder = new MessageBuilder())
            {
                builder.AppendLine(ex);
                Info(page, builder);
            }
        }

        public static void Info(Page page, MessageBuilder msg)
        {
            string script =
                " function RunOnce() { radalert(\"" + msg.Message + "\", 400, 100,\"" +
                GlobalConstant.ProductName +
                "\");Sys.Application.remove_load(RunOnce);}  Sys.Application.add_load(RunOnce);";
            ScriptManager.RegisterStartupScript(page, typeof(Page), "InfoDialog", script, true);
        }
    }
}
