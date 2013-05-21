using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TJVISA
{
    public enum JobStatus
    {
        None=0,
        Start,
        Checking,
        Doing,
        HasCase,
        Valid,
        Invalid,
        Finish,
        End,
        Over
    }

    public static class JobStatusExt
    {
        public static string ToLabel(this JobStatus status)
        {
            switch (status)
            {
                case JobStatus.None:
                    return "待收件";
                case JobStatus.Start:
                    return "已收件";
                case JobStatus.Checking:
                    return "待审核";
                case JobStatus.Doing:
                    return "办理中";
                case JobStatus.HasCase:
                    return "问题案";
                case JobStatus.Invalid:
                    return "未通过";
                    case JobStatus.Valid:
                    return "通过";
                case JobStatus.Finish:
                    return "已完成";
                case JobStatus.End:
                    return "发件";
                case JobStatus.Over:
                    return "签收";
                default:
                    return "待收件";
            }
        }
    }

    public static partial class StringExt
    {
        public static JobStatus ToJobStatus(this string str)
        {
            switch (str)
            {
                case "待收件":
                    return JobStatus.None;
                case "已收件":
                    return JobStatus.Start;
                case "待审核":
                    return JobStatus.Checking;
                case "办理中":
                    return JobStatus.Doing;
                case "问题案":
                    return JobStatus.HasCase;
                case "未通过":
                    return JobStatus.Invalid;
                case "通过":
                    return JobStatus.Valid;
                case "已完成":
                    return JobStatus.Finish;
                case "发件":
                    return JobStatus.End;
                case "签收":
                   return JobStatus.Over;
                default:
                    return JobStatus.None;
            }
        }
    }
}
