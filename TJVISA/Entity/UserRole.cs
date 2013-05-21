using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TJVISA
{
    public enum UserRole
    {
        None = 0,
        Business = 1,
        Client = 2,
        Administrator = 3
    }

    public static class UserRoleExt
    {
        public static string ToLabel(this UserRole role)
        {
            switch (role)
            {
                case UserRole.Business:
                    return "业务员";
                case UserRole.Client:
                    return "服务员";
                case UserRole.Administrator:
                    return "管理员";
                case UserRole.None:
                default:
                    return GlobalConstant.None;
            }
        }

        public static IList<JobStatus> GetVisibleStatus(this UserRole role)
        {
            switch (role)
            {
                case UserRole.Business:
                    return new List<JobStatus>()
                               {
                                   JobStatus.Checking,
                                   JobStatus.Doing,
                                   JobStatus.Valid
                               };
                case UserRole.Client:
                    return new List<JobStatus>()
                               {
                                   JobStatus.None,
                                   JobStatus.Invalid,
                                   JobStatus.End,
                               };
                case UserRole.Administrator:
                    return new List<JobStatus>()
                               {
                                   JobStatus.Start,
                                   JobStatus.HasCase,
                                   JobStatus.Finish
                               };
                case UserRole.None:
                default:
                    return new List<JobStatus>();
            }
        }

        public static IList<JobStatus> GetProcessStatus(this UserRole role, JobStatus status)
        {
            switch (role)
            {
                case UserRole.Business:
                    switch (status)
                    {
                        case JobStatus.Checking:
                            return new List<JobStatus>()
                                       {
                                           JobStatus.Doing
                                       };

                        case JobStatus.Doing:
                            return new List<JobStatus>()
                                       {
                                           JobStatus.HasCase,
                                           JobStatus.Finish
                                       };

                        default:
                            return new List<JobStatus>();
                    }

                case UserRole.Client:
                    switch (status)
                    {
                        case JobStatus.None:
                        case JobStatus.Invalid:
                            return new List<JobStatus>()
                                       {
                                           JobStatus.Start
                                       };
                        case JobStatus.End:
                            return new List<JobStatus>(){JobStatus.Over};
                        default:
                            return new List<JobStatus>();
                    }

                case UserRole.Administrator:

                    switch (status)
                    {
                        case JobStatus.Start:
                            return new List<JobStatus>()
                                       {
                                           JobStatus.Checking
                                       };
                        case JobStatus.HasCase:
                            return new List<JobStatus>()
                                       {
                                           JobStatus.Invalid,
                                           JobStatus.Valid
                                       };
                        case JobStatus.Finish:
                            return new List<JobStatus>() {JobStatus.End};
                        default:
                            return new List<JobStatus>();
                    }
                case UserRole.None:
                default:
                    return new List<JobStatus>();
            }
        }
    }

    public static partial class StringExt
    {
        public static UserRole ToUserRole(this string str)
        {
            switch (str)
            {
                case "业务员":
                    return UserRole.Business;
                case "前台人员":
                    return UserRole.Client;
                case "管理员":
                    return UserRole.Administrator;
                default:
                    return UserRole.None;
            }
        }
    }
}
