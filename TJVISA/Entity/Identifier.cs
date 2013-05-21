using System;

namespace TJVISA.Entity
{
    public class Identifier
    {
        public AppType AppType { get { return _appType; }  }

        public AppSubType AppSubType { get { return _appSubType; } }

        private readonly AppType _appType;
        private readonly AppSubType _appSubType;

        public Identifier(AppType appType, AppSubType orien)
        {
            _appType = appType;
            _appSubType = orien;
            _value = Generate(appType, orien);
        }

        public Identifier(string value)
        {
            _value = value;
            _appType = GetAppType(value);
            _appSubType = GetAppSubType(value);
        }

        private string _value;

        public string Value
        {
            get
            {
                return _value;
            }
        }

        public override string ToString()
        {
            return Value;
        }

        protected string Generate(AppType style, AppSubType orientation)
        {
            string header = orientation == AppSubType.NonSingle ? "G" : "S";
            string date = DateTime.Now.ToString("yyyyMMdd");
            string s = "0" + (int) style;
            string o = "0" + (int) orientation;
            string random4 = DateTime.UtcNow.Ticks.ToString().Substring(13, 4);

            return header + date + s + o+random4;
        }

        protected AppType GetAppType(string value)
        {
            string s = value.Substring(10, 1);

            return (AppType) Enum.ToObject(typeof (AppType), int.Parse(s));
        }

        protected  AppSubType GetAppSubType(string value)
        {
            string o = value.Substring(12, 1);
            return (AppSubType)Enum.ToObject(typeof(AppSubType), int.Parse(o));
        }

        public void UpdateAppType(string name)
        {
            AppType t = AppType.ToObject(name);
            char[] temp = _value.ToCharArray();
            temp.SetValue((int)t,10);
            _value = temp.ToString();
        }

        public void UpdateAppType(AppType type)
        {
            char[] temp = _value.ToCharArray();
            temp.SetValue((int)type, 10);
            _value = temp.ToString();
        }

        public void UpdateAppSubType(string name)
        {
            AppSubType t = AppSubType.ToObject(name);
            char[] temp = _value.ToCharArray();
            temp.SetValue((int)t, 12);
            _value = temp.ToString();
        }

        public void UpdateAppSubType(AppSubType subType)
        {
            char[] temp = _value.ToCharArray();
            temp.SetValue((int)subType, 12);
            _value = temp.ToString();
        }

        public static Identifier New(AppType style, AppSubType orien)
        {
            return new Identifier(style,orien);
        }

        public static Identifier Load(string value)
        {
            return new Identifier(value);
        }
    }
}
