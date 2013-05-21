using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace TJVISA
{
    public class MessageBuilder:IDisposable
    {
        private StringBuilder _builder;
        public MessageBuilder()
        {
            _builder=new StringBuilder();
        }

        public MessageBuilder AppendLine(string msg)
        {
            if (msg.StartsWith("*"))
                _builder.Append(msg+"<br/>");
            else
                _builder.Append("* " + msg+"<br/>");
            return this;
        }

        public MessageBuilder AppendLine(Exception ex)
        {
            if (ex.Message.StartsWith("*"))
                _builder.Append(ex.Message + "<br/>");
            else
                _builder.Append("* " + ex.Message + "<br/>");
#if DEBUG
            if (ex.InnerException != null)
                AppendLine(ex.InnerException);
#endif

            return this;
        }

        public MessageBuilder Append(string msg)
        {
            _builder.Append(msg);
            return this;
        }

        public string Message
        {
            get { return _builder.ToString(); }
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            _builder = null;
        }

        #endregion
    }
}
