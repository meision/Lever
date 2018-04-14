namespace Meision
{
    public struct Range
    {
        private int _start;
        public int Start
        {
            get
            {
                return this._start;
            }
            set
            {
                this._start = value;
            }
        }

        private int _end;
        public int End
        {
            get
            {
                return this._end;
            }
            set
            {
                this._end = value;
            }
        }
        
        public Range(int start, int end)
        {
            this._start = start;
            this._end = end;
        }
    }
}
