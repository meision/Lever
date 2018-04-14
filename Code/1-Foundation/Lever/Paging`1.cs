using System.Collections.Generic;

namespace Meision
{
    public class Paging<T>
    {
        private int _pageIndex;
        public int PageIndex
        {
            get
            {
                return this._pageIndex;
            }
            set
            {
                this._pageIndex = value;
            }
        }

        private int _pageSize;
        public int PageSize
        {
            get
            {
                return this._pageSize;
            }
            set
            {
                this._pageSize = value;
            }
        }

        private int _itemTotal;
        public int ItemTotal
        {
            get
            {
                return this._itemTotal;
            }
            set
            {
                this._itemTotal = value;
            }
        }

        public int PageTotal
        {
            get
            {
                return Meision.Algorithms.Calculator.CeilingDivision(this.ItemTotal, this.PageSize);
            }
        }

        private List<T> _items;
        public List<T> Items
        {
            get
            {
                return this._items;
            }
            set
            {
                this._items = value;
            }
        }

        public Paging()
        {
        }

        public Paging(int pageIndex, int pageSize)
        {
            this._pageIndex = pageIndex;
            this._pageSize = pageSize;
        }

        public Paging(int pageIndex, int pageSize, int itemTotal, List<T> items)
        {
            this._pageIndex = pageIndex;
            this._pageSize = pageSize;
            this._itemTotal = itemTotal;
            this._items = items;
        }
    }
}
