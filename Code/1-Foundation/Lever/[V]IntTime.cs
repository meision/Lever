using System.Runtime.InteropServices;

namespace Meision
{
    [StructLayout(LayoutKind.Sequential)]
    public struct IntTime
    {
        #region Static
        private const char SegmentSeparator = ':';

        public static IntTime Create(string expression)
        {
            ThrowHelper.ArgumentNull((expression == null), nameof(expression));

            string[] segments = expression.Split(IntTime.SegmentSeparator);
            if (segments.Length == 3)
            {
                return new IntTime(int.Parse(segments[0]) * 3600 + int.Parse(segments[1]) * 60 + int.Parse(segments[2]));
            }
            else
            {
                return new IntTime(int.Parse(expression));
            }
        }

        public static bool operator ==(IntTime time1, IntTime time2)
        {
            return (time1._value == time2._value);
        }

        public static bool operator !=(IntTime time1, IntTime time2)
        {
            return !(time1 == time2);
        }

        public static bool operator <(IntTime time1, IntTime time2)
        {
            return time1._value < time2._value;
        }

        public static bool operator <=(IntTime time1, IntTime time2)
        {
            return time1._value <= time2._value;
        }

        public static bool operator >(IntTime time1, IntTime time2)
        {
            return time1._value > time2._value;
        }

        public static bool operator >=(IntTime time1, IntTime time2)
        {
            return time1._value >= time2._value;
        }

        public static explicit operator int(IntTime time)
        {
            return time._value;
        }

        public static implicit operator IntTime(int value)
        {
            IntTime time = new IntTime(value);
            return time;
        }
        #endregion Static

        #region Field & Property
        private int _value;
        public int Value
        {
            get
            {
                return this._value;
            }
        }

        public int Hour
        {
            get
            {
                return this._value / 3600;
            }
        }

        public int Minute
        {
            get
            {
                return (this._value % 3600) / 60;
            }
        }

        public int Second
        {
            get
            {
                return this._value % 60;
            }
        }
        #endregion Field & Property

        #region Constructor
        public IntTime(int value)
        {
            ThrowHelper.ArgumentMustNotNegative((value < 0), nameof(value));

            this._value = value;
        }
        #endregion Constructor

        #region Method
        public bool Equals(IntTime time)
        {
            return (this._value == time._value);
        }
        public override bool Equals(object obj)
        {
            if (!(obj is IntTime))
            {
                return false;
            }

            return (this._value == ((IntTime)obj)._value);
        }

        public override int GetHashCode()
        {
            return this._value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{this.Hour}{IntTime.SegmentSeparator}{this.Minute:d2}{IntTime.SegmentSeparator}{this.Second:d2}";
        }
        #endregion Method


    }
}
