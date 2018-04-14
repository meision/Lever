using System;
using Meision.Resources;

public static class ThrowHelper
{
    public static void ArgumentNull(bool expression, string paramName)
    {
        if (expression) { throw new ArgumentNullException(paramName); }
    }

    public static void ArgumentOutOfRange(bool expression, string paramName, string message = null)
    {
        if (expression) { throw new ArgumentOutOfRangeException(paramName, message); }
    }

    public static void ArgumentException(bool expression, string message, string paramName = null)
    {
        if (expression) { throw new ArgumentException(message, paramName); }
    }

    public static void ArgumentIndexOutOfRange(bool expression, string paramName)
    {
        if (expression) { throw new ArgumentOutOfRangeException(paramName, SR._Exception_IndexOutOfRange); }
    }

    public static void ArgumentLengthOutOfRange(bool expression, string paramName)
    {
        if (expression) { throw new ArgumentOutOfRangeException(paramName, SR._Exception_LengthOutOfRange); }
    }

    public static void ArgumentIndexLengthOutOfArray(bool expression)
    {
        if (expression) { throw new ArgumentException(SR._Exception_IndexLengthOutOfArray); }
    }

    public static void ArgumentEnumNotDefine(bool expression, string paramName)
    {
        if (expression) { throw new ArgumentException(SR._Exception_EnumNotDefine, paramName); }
    }

    public static void ArgumentMustNotNegative(bool expression, string paramName)
    {
        if (expression) { throw new ArgumentException(SR._Exception_ValueMustNotNegative, paramName); }
    }

    public static void ArgumentMustPositive(bool expression, string paramName)
    {
        if (expression) { throw new ArgumentException(SR._Exception_ValueMustPositive, paramName); }
    }

    public static void ArgumentArrayMustNotEmpty(bool expression, string paramName)
    {
        if (expression) { throw new ArgumentException(SR._Exception_ArrayMustNotEmpty, paramName); }
    }

    public static void ArgumentStringMustNotEmpty(bool expression, string paramName)
    {
        if (expression) { throw new ArgumentException(SR._Exception_StringMustNotEmpty, paramName); }
    }
}
