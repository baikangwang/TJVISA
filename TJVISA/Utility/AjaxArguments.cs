using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Web.UI;

namespace TJVISA
{
    public class AjaxArguments
    {
        private readonly string _commadName;
        private readonly IList<string> _args; 
        
        public string CommandName { get { return _commadName; } }

        public bool HasArgs
        {
            get { return _args.Count != 0; }
        }

        protected AjaxArguments():base()
        {
            _commadName = "";
            _args=new List<string>();
        }

        public AjaxArguments(AjaxRequestEventArgs e):this()
        {
            string[] args = e.Argument.Split('|');
            
            for (int i = 0; i < args.Length; i++)
            {
                if (i == 0)
                    _commadName = args[i];
                else
                    _args.Add(args[i]);
            }
        }

        public string this[int index]
        {
            get
            {
                return _args[index];
            }
        }
     }
}
