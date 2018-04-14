using System;

namespace Meision
{
    [Flags]
    public enum Directions
    {
        None = 0x00,
        Up = 0x01,
        Down = 0x02,
        Left = 0x04,
        Right = 0x08,
        All = Up & Down & Left & Right,
    }
}
