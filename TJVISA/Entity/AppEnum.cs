namespace TJVISA
{
    public enum AppType
    {
        Visa=1,
        Certification=2
    }

    public enum AppSubType
    {
        Single=1,
        NonSingle=2
    }

    public static class AppEnumExt
    {
        public static string ToLabel(this AppType type)
        {
            switch (type)
            {
                    case AppType.Certification:
                    return "认证";
                    case AppType.Visa:
                default:
                    return "签证";
            }
        }
        
        public static string ToLabel(this AppSubType subType, AppType type)
        {
            switch (subType)
            {
                case AppSubType.Single:
                    return type == AppType.Visa ? "单人" : "单认证";
                case AppSubType.NonSingle:
                default:
                    return type == AppType.Visa ? "团体" : "双认证";
            }
        }

        public static AppType ToObject(this AppType type, string name)
        {
            switch (name)
            {
                    case "认证":
                    return AppType.Certification;
                    case "签证":
                default:
                    return AppType.Visa;
            }
        }

        public static AppSubType ToObject(this AppSubType subType,string name)
        {
            switch (name)
            {
                case "单人":
                case "单认证":
                    return AppSubType.Single;
                case "团体":
                case "双认证":
                default:
                    return AppSubType.NonSingle;
            }
        }
    }
}
