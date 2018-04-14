using System;
using System.Runtime.InteropServices;

namespace Meision
{
    [StructLayout(LayoutKind.Sequential)]
    public struct IntMinute
    {
        #region Static
        private const char SegmentSeparator = ':';

        public static IntMinute Create(string expression)
        {
            ThrowHelper.ArgumentNull((expression == null), nameof(expression));

            string[] segments = expression.Split(IntMinute.SegmentSeparator);
            if (segments.Length == 2)
            {
                return new IntMinute(int.Parse(segments[0]) * 60 + int.Parse(segments[1]));
            }
            else
            {
                int value = int.Parse(expression);
                return new IntMinute(value);
            }
        }

        public static bool operator ==(IntMinute time1, IntMinute time2)
        {
            return (time1._value == time2._value);
        }

        public static bool operator !=(IntMinute time1, IntMinute time2)
        {
            return !(time1 == time2);
        }

        public static bool operator <(IntMinute time1, IntMinute time2)
        {
            return time1._value < time2._value;
        }

        public static bool operator <=(IntMinute time1, IntMinute time2)
        {
            return time1._value <= time2._value;
        }

        public static bool operator >(IntMinute time1, IntMinute time2)
        {
            return time1._value > time2._value;
        }

        public static bool operator >=(IntMinute time1, IntMinute time2)
        {
            return time1._value >= time2._value;
        }

        public static explicit operator int(IntMinute time)
        {
            return time._value;
        }

        public static implicit operator IntMinute(int value)
        {
            IntMinute time = new IntMinute(value);
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
                return this._value / 60;
            }
        }

        public int Minute
        {
            get
            {
                return this._value % 60;
            }
        }
        #endregion Field & Property

        #region Constructor
        public IntMinute(int value)
        {
            ThrowHelper.ArgumentMustNotNegative((value < 0), nameof(value));

            this._value = value;
        }
        #endregion Constructor

        #region Method
        public bool Equals(IntMinute time)
        {
            return (this._value == time._value);
        }
        public override bool Equals(object obj)
        {
            if (!(obj is IntMinute))
            {
                return false;
            }

            return (this._value == ((IntMinute)obj)._value);
        }

        public override int GetHashCode()
        {
            return this._value.GetHashCode();
        }

        public TimeSpan ToTimeSpan()
        {
            return new TimeSpan(this.Hour, this.Minute, 0);
        }

        public override string ToString()
        {
            return $"{this.Hour}{IntMinute.SegmentSeparator}{this.Minute:d2}";
        }
        #endregion Method
    }
}
