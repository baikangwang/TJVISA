using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TJVISA
{
    public delegate void PagingEvent(object sender, EventArgs e);

    public class PagingSetting:ISerializable
    {
        public event PagingEvent OnPageNumChanged;
        private int _pageNum = 1;
        private int _pageSize = 10;
        private int _pageCount = 1;
        private int _itemCount = 10;

        public PagingSetting(int pageNum, int pageSize, int pageCount,int itemCount)
        {
            _pageSize = pageSize;
            _pageNum = pageNum;
            _pageCount = pageCount;
            _itemCount = itemCount;
        }

        public int PageNum
        {
            get { return _pageNum; }
            set
            {
                _pageNum = value;
                if (OnPageNumChanged != null)
                    OnPageNumChanged(this, new EventArgs());
            }
        }

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }

        public int PageCount
        {
            get { return _pageCount; }
            set { _pageCount = value; }
        }

        public int ItemCount
        {
            get { return _itemCount; }
            set { _itemCount = value; }
        }

        #region Implementation of ISerializable

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("PageNum",PageNum);
            info.AddValue("PageSize",PageSize);
            info.AddValue("PageCount",PageCount);
            info.AddValue("ItemCount",ItemCount);
        }

        protected PagingSetting(SerializationInfo info)
        {
            _pageSize = info.GetInt32("PageSize");
            _pageNum = info.GetInt32("PageNum");
            _pageCount = info.GetInt32("PageCount");
            _itemCount = info.GetInt32("ItemCount");
        }

        #endregion
    }
}
