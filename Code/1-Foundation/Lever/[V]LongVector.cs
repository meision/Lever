using System;

namespace Meision
{
    public struct LongVector
    {
        #region Static
        public static bool operator ==(LongVector vector1, LongVector vector2)
        {
            return vector1.Equals(vector2);
        }

        public static bool operator !=(LongVector vector1, LongVector vector2)
        {
            return !vector1.Equals(vector2);
        }
        #endregion Static

        private long _offset;
        public long Offset
        {
            get
            {
                return this._offset;
            }
            set
            {
                this._offset = value;
            }
        }

        private long _length;
        public long Length
        {
            get
            {
                return this._length;
            }
            set
            {
                this._offset = value;
            }
        }

        public LongVector(long offset, long length)
        {
            this._offset = offset;
            this._length = length;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is LongVector))
            {
                return false;
            }

            LongVector vector = (LongVector)obj;
            return (this._offset == vector._offset) && (this._length == vector._length);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format(
                System.Globalization.CultureInfo.InvariantCulture,
                "{0}, {1}",
                this._offset,
                this._length);
        }

        public LongVector Intersect(LongVector value)
        {
            LongVector first, second;
            if (this._offset < value._offset)
            {
                first = this;
                second = value;
            }
            else
            {
                first = value;
                second = this;
            }

            // Not intersect.
            if ((second._offset + second._length - first._offset) >= (first._length + second._length))
            {
                return default(LongVector);
            }

            LongVector range;
            range._offset = second._offset;
            range._length = Math.Min(first._offset + first._length, second._offset + second._length) - range._offset;
            return range;
        }

        public LongVector Union(LongVector value)
        {
            LongVector first, second;
            if (this._offset < value._offset)
            {
                first = this;
                second = value;
            }
            else
            {
                first = value;
                second = this;
            }

            // Not intersect.
            if ((second._offset + second._length - first._offset) >= (first._length + second._length))
            {
                return default(LongVector);
            }

            LongVector range;
            range._offset = first._offset;
            range._length = Math.Max(first._offset + first._length, second._offset + second._length) - range._offset;
            return range;
        }
    }
}
