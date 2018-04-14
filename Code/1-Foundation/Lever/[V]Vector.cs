using System;

namespace Meision
{
    public struct Vector
    {
        #region Static
        public static Vector None
        {
            get
            {
                return new Vector(0, 0);
            }
        }

        public static bool operator ==(Vector vector1, Vector vector2)
        {
            return vector1.Equals(vector2);
        }

        public static bool operator !=(Vector vector1, Vector vector2)
        {
            return !vector1.Equals(vector2);
        }
        #endregion Static

        #region Field & Property
        private int _offset;
        public int Offset
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

        private int _length;
        public int Length
        {
            get
            {
                return this._length;
            }
            set
            {
                this._length = value;
            }
        }
        #endregion Field & Property

        #region Constructor
        public Vector(int offset, int length)
        {
            this._offset = offset;
            this._length = length;
        }
        #endregion Constructor

        #region Method
        public Vector Intersect(Vector value)
        {
            Vector first, second;
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
                return default(Vector);
            }

            Vector range;
            range._offset = second._offset;
            range._length = Math.Min(first._offset + first._length, second._offset + second._length) - range._offset;
            return range;
        }

        public Vector Union(Vector value)
        {
            Vector first, second;
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
                return default(Vector);
            }

            Vector range;
            range._offset = first._offset;
            range._length = Math.Max(first._offset + first._length, second._offset + second._length) - range._offset;
            return range;
        }

        public bool Contains(int x)
        {
            return ((this._offset <= x) && (x < (this._offset + this._length)));
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Vector))
            {
                return false;
            }

            Vector vector = (Vector)obj;
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
        #endregion Method
    }
}
