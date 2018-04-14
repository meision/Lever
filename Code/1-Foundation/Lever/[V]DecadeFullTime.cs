using System.Runtime.InteropServices;

namespace Meision
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DecadeFullTime
    {
        #region Static
        private const char SegmentSeparator = ':';
        private const char MillisecondDelimiter = '.';

        public static DecadeFullTime Create(string expression)
        {
            ThrowHelper.ArgumentNull((expression == null), nameof(expression));

            string[] segments = expression.Split(DecadeFullTime.SegmentSeparator);
            if (segments.Length == 3)
            {
                int pos = segments[2].IndexOf(DecadeFullTime.MillisecondDelimiter);
                if (pos >= 0)
                {
                    return new DecadeFullTime(int.Parse(segments[0]) * 10000000 + int.Parse(segments[1]) * 100000 + int.Parse(segments[2].Substring(0, pos)) * 1000 + int.Parse(segments[2].Substring(pos + 1)));
                }
                else
                {
                    return new DecadeFullTime(int.Parse(segments[0]) * 10000000 + int.Parse(segments[1]) * 100000 + int.Parse(segments[2]) * 1000);
                }
            }
            else
            {
                return new DecadeFullTime(int.Parse(expression));
            }
        }

        public static bool operator ==(DecadeFullTime time1, DecadeFullTime time2)
        {
            return (time1._value == time2._value);
        }

        public static bool operator !=(DecadeFullTime time1, DecadeFullTime time2)
        {
            return !(time1 == time2);
        }

        public static bool operator <(DecadeFullTime time1, DecadeFullTime time2)
        {
            return time1._value < time2._value;
        }

        public static bool operator <=(DecadeFullTime time1, DecadeFullTime time2)
        {
            return time1._value <= time2._value;
        }

        public static bool operator >(DecadeFullTime time1, DecadeFullTime time2)
        {
            return time1._value > time2._value;
        }

        public static bool operator >=(DecadeFullTime time1, DecadeFullTime time2)
        {
            return time1._value >= time2._value;
        }

        public static explicit operator int(DecadeFullTime time)
        {
            return time._value;
        }

        public static implicit operator DecadeFullTime(int value)
        {
            DecadeFullTime time = new DecadeFullTime(value);
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
                return this._value / 10000000;
            }
        }

        public int Minute
        {
            get
            {
                return (this._value % 10000000) / 100000;
            }
        }

        public int Second
        {
            get
            {
                return (this._value % 100000) / 1000;
            }
        }

        public int Millisecond
        {
            get
            {
                return (this._value % 1000);
            }
        }
        #endregion Field & Property

        #region Constructor
        public DecadeFullTime(int value)
        {
            ThrowHelper.ArgumentMustNotNegative((value < 0), nameof(value));

            this._value = value;
        }
        #endregion Constructor

        #region Method
        public bool Equals(DecadeFullTime time)
        {
            return (this._value == time._value);
        }
        public override bool Equals(object obj)
        {
            if (!(obj is DecadeFullTime))
            {
                return false;
            }

            return (this._value == ((DecadeFullTime)obj)._value);
        }

        public override int GetHashCode()
        {
            return this._value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{this.Hour}{DecadeFullTime.SegmentSeparator}{this.Minute:d2}{DecadeFullTime.SegmentSeparator}{this.Second:d2}{DecadeFullTime.MillisecondDelimiter}{this.Millisecond:d3}";
        }
        #endregion Method


    }
}
