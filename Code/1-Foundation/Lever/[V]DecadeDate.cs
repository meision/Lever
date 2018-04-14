using System;
using System.Runtime.InteropServices;

namespace Meision
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DecadeDate
    {
        #region Static
        private const char SegmentSeparator = '-';

        public static DecadeDate Create(string expression)
        {
            ThrowHelper.ArgumentNull((expression == null), nameof(expression));

            string[] segments = expression.Split(DecadeDate.SegmentSeparator);
            if (segments.Length == 3)
            {
                return new DecadeDate(int.Parse(segments[0]) * 10000 + int.Parse(segments[1]) * 100 + int.Parse(segments[2]));
            }
            else
            {
                return new DecadeDate(int.Parse(expression));
            }
        }

        public static DecadeDate Create(DateTime date)
        {
            return new DecadeDate((int)(date.Year * 10000 + date.Month * 100 + date.Day));
        }

        public static bool operator ==(DecadeDate date1, DecadeDate date2)
        {
            return (date1._value == date2._value);
        }

        public static bool operator !=(DecadeDate date1, DecadeDate date2)
        {
            return !(date1 == date2);
        }

        public static bool operator <(DecadeDate date1, DecadeDate date2)
        {
            return date1._value < date2._value;
        }

        public static bool operator <=(DecadeDate date1, DecadeDate date2)
        {
            return date1._value <= date2._value;
        }

        public static bool operator >(DecadeDate date1, DecadeDate date2)
        {
            return date1._value > date2._value;
        }

        public static bool operator >=(DecadeDate date1, DecadeDate date2)
        {
            return date1._value >= date2._value;
        }

        public static explicit operator int(DecadeDate date)
        {
            return date._value;
        }

        public static implicit operator DecadeDate(int value)
        {
            DecadeDate date = new DecadeDate(value);
            return date;
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

        public int Year
        {
            get
            {
                return this._value / 10000;
            }
        }

        public int Month
        {
            get
            {
                return (this._value % 10000) / 100;
            }
        }

        public int Day
        {
            get
            {
                return this._value % 100;
            }
        }
        #endregion Field & Property

        #region Constructor
        public DecadeDate(int value)
        {
            ThrowHelper.ArgumentMustNotNegative((value < 0), nameof(value));

            this._value = value;
        }
        #endregion Constructor

        #region Method
        public bool Equals(DecadeDate date)
        {
            return (this._value == date._value);
        }
        public override bool Equals(object obj)
        {
            if (!(obj is DecadeDate))
            {
                return false;
            }

            return (this._value == ((DecadeDate)obj)._value);
        }

        public override int GetHashCode()
        {
            return this._value.GetHashCode();
        }

        public DateTime ToDateTime()
        {
            return new DateTime(this.Year, this.Month, this.Day);
        }

        public override string ToString()
        {
            return $"{this.Year:d4}{DecadeDate.SegmentSeparator}{this.Month:d2}{DecadeDate.SegmentSeparator}{this.Day:d2}";
        }
        #endregion Method


    }
}
