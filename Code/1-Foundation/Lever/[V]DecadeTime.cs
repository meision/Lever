using System.Runtime.InteropServices;

namespace Meision
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DecadeTime
    {
        #region Static
        private const char SegmentSeparator = ':';

        public static DecadeTime Create(string expression)
        {
            ThrowHelper.ArgumentNull((expression == null), nameof(expression));

            string[] segments = expression.Split(DecadeTime.SegmentSeparator);
            if (segments.Length == 3)
            {
                return new DecadeTime(int.Parse(segments[0]) * 10000 + int.Parse(segments[1]) * 100 + int.Parse(segments[2]));
            }
            else
            {
                return new DecadeTime(int.Parse(expression));
            }
        }

        public static bool operator ==(DecadeTime time1, DecadeTime time2)
        {
            return (time1._value == time2._value);
        }

        public static bool operator !=(DecadeTime time1, DecadeTime time2)
        {
            return !(time1 == time2);
        }

        public static bool operator <(DecadeTime time1, DecadeTime time2)
        {
            return time1._value < time2._value;
        }

        public static bool operator <=(DecadeTime time1, DecadeTime time2)
        {
            return time1._value <= time2._value;
        }

        public static bool operator >(DecadeTime time1, DecadeTime time2)
        {
            return time1._value > time2._value;
        }

        public static bool operator >=(DecadeTime time1, DecadeTime time2)
        {
            return time1._value >= time2._value;
        }

        public static explicit operator int(DecadeTime time)
        {
            return time._value;
        }

        public static implicit operator DecadeTime(int value)
        {
            DecadeTime time = new DecadeTime(value);
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
                return this._value / 10000;
            }
        }

        public int Minute
        {
            get
            {
                return (this._value % 10000) / 100;
            }
        }

        public int Second
        {
            get
            {
                return this._value % 100;
            }
        }
        #endregion Field & Property

        #region Constructor
        public DecadeTime(int value)
        {
            ThrowHelper.ArgumentMustNotNegative((value < 0), nameof(value));

            this._value = value;
        }
        #endregion Constructor

        #region Method
        public bool Equals(DecadeTime time)
        {
            return (this._value == time._value);
        }
        public override bool Equals(object obj)
        {
            if (!(obj is DecadeTime))
            {
                return false;
            }

            return (this._value == ((DecadeTime)obj)._value);
        }

        public override int GetHashCode()
        {
            return this._value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{this.Hour}{DecadeTime.SegmentSeparator}{this.Minute:d2}{DecadeTime.SegmentSeparator}{this.Second:d2}";
        }
        #endregion Method


    }
}
