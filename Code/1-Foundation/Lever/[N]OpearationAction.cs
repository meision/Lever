using System;

namespace Meision
{
    [Flags]
    public enum OperationAction
    {
        None,
        Add = 0x0001,
        Edit = 0x0002,
        Delete = 0x0004,
        View = 0x0008,
        Refresh = 0x0010,
        Load = 0x0020,
        Save = 0x0040,
        Close = 0x0080
    }
}
