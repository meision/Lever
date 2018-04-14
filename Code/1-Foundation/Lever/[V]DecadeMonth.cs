using System.Runtime.InteropServices;

namespace Meision
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DecadeMonth
    {
        #region Static
        private const char SegmentSeparator = '-';

        public static DecadeMonth Create(string expression)
        {
            ThrowHelper.ArgumentNull((expression == null), nameof(expression));

            string[] segments = expression.Split(DecadeMonth.SegmentSeparator);
            if (segments.Length == 2)
            {
                return new DecadeMonth(int.Parse(segments[0]) * 100 + int.Parse(segments[1]));
            }
            else
            {
                return new DecadeMonth(int.Parse(expression));
            }
        }

        public static bool operator ==(DecadeMonth month1, DecadeMonth month2)
        {
            return (month1._value == month2._value);
        }

        public static bool operator !=(DecadeMonth month1, DecadeMonth month2)
        {
            return !(month1 == month2);
        }

        public static bool operator <(DecadeMonth month1, DecadeMonth month2)
        {
            return month1._value < month2._value;
        }

        public static bool operator <=(DecadeMonth month1, DecadeMonth month2)
        {
            return month1._value <= month2._value;
        }

        public static bool operator >(DecadeMonth month1, DecadeMonth month2)
        {
            return month1._value > month2._value;
        }

        public static bool operator >=(DecadeMonth month1, DecadeMonth month2)
        {
            return month1._value >= month2._value;
        }


        public static explicit operator int(DecadeMonth month)
        {
            return month._value;
        }

        public static implicit operator DecadeMonth(int value)
        {
            DecadeMonth month = new DecadeMonth(value);
            return month;
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
                return this._value / 100;
            }
        }

        public int Month
        {
            get
            {
                return this._value % 100;
            }
        }
        #endregion Field & Property

        #region Constructor
        public DecadeMonth(int value)
        {
            ThrowHelper.ArgumentMustNotNegative((value < 0), nameof(value));

            this._value = value;
        }
        #endregion Constructor

        #region Method
        public bool Equals(DecadeMonth month)
        {
            return (this._value == month._value);
        }
        public override bool Equals(object obj)
        {
            if (!(obj is DecadeMonth))
            {
                return false;
            }

            return (this._value == ((DecadeMonth)obj)._value);
        }

        public override int GetHashCode()
        {
            return this._value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{this.Year:d4}{DecadeMonth.SegmentSeparator}{this.Month:d2}";
        }
        #endregion Method


    }
}
