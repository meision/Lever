using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

public static partial class Extensions
{
    /// <summary>
    ///     A string extension method that return the left part of the string.
    /// </summary>
    /// <param name="instance">The instance to act on.</param>
    /// <param name="length">The length.</param>
    /// <returns>The left part.</returns>
    public static string Left(this string instance, int length)
    {
        ThrowHelper.ArgumentNull((instance == null), nameof(instance));
        ThrowHelper.ArgumentMustNotNegative((length < 0), nameof(length));

        return instance.Substring(0, length);
    }

    /// <summary>
    ///     A string extension method that left safe.
    /// </summary>
    /// <param name="instance">The instance to act on.</param>
    /// <param name="length">The length.</param>
    /// <returns>A string.</returns>
    public static string LeftSafe(this string instance, int length)
    {
        ThrowHelper.ArgumentNull((instance == null), nameof(instance));
        ThrowHelper.ArgumentMustNotNegative((length < 0), nameof(length));

        return instance.Substring(0, Math.Min(length, instance.Length));
    }
    
    /// <summary>
    ///     A string extension method that return the right part of the string.
    /// </summary>
    /// <param name="instance">The instance to act on.</param>
    /// <param name="length">The length.</param>
    /// <returns>The right part.</returns>
    public static string Right(this string instance, int length)
    {
        ThrowHelper.ArgumentNull((instance == null), nameof(instance));
        ThrowHelper.ArgumentMustNotNegative((length < 0), nameof(length));

        return instance.Substring(instance.Length - length);
    }

    /// <summary>
    ///     A string extension method that right safe.
    /// </summary>
    /// <param name="instance">The instance to act on.</param>
    /// <param name="length">The length.</param>
    /// <returns>A string.</returns>
    public static string RightSafe(this string instance, int length)
    {
        ThrowHelper.ArgumentNull((instance == null), nameof(instance));
        ThrowHelper.ArgumentMustNotNegative((length < 0), nameof(length));

        return instance.Substring(Math.Max(0, instance.Length - length));
    }
    
    public static string GetBefore(this string instance, string searchvalue)
    {
        ThrowHelper.ArgumentNull((instance == null), nameof(instance));
        ThrowHelper.ArgumentNull((searchvalue == null), nameof(searchvalue));

        int index = instance.IndexOf(searchvalue);
        if (index < 0)
        {
            return null;
        }

        return instance.Substring(0, index);
    }
    public static string GetAfter(this string instance, string searchvalue)
    {
        ThrowHelper.ArgumentNull((instance == null), nameof(instance));
        ThrowHelper.ArgumentNull((searchvalue == null), nameof(searchvalue));

        int index = instance.IndexOf(searchvalue);
        if (index < 0)
        {
            return null;
        }

        return instance.Substring(index + searchvalue.Length);
    }
    public static string GetBetween(this string instance, string leftText, string rightText)
    {
        return GetBetween(instance, 0, leftText, rightText);
    }
    public static string GetBetween(this string instance, int startIndex, string leftText, string rightText)
    {
        ThrowHelper.ArgumentNull((instance == null), nameof(instance));
        ThrowHelper.ArgumentOutOfRange(((startIndex < 0) || (startIndex > instance.Length)), nameof(startIndex));

        int pos;
        if (string.IsNullOrEmpty(leftText))
        {
            pos = startIndex;
        }
        else
        {
            pos = instance.IndexOf(leftText, startIndex) + leftText.Length;
            if (pos < leftText.Length)
            {
                return null;
            }
        }

        if (string.IsNullOrEmpty(rightText))
        {
            return instance.Substring(pos);
        }
        else
        {
            int end = instance.IndexOf(rightText, pos, StringComparison.Ordinal);
            if (end < pos)
            {
                return null;
            }

            return instance.Substring(pos, end - pos);
        }
    }
    public static string Extract(this string instance, string pattern, RegexOptions options = RegexOptions.None)
    {
        if (instance == null)
        {
            throw new ArgumentNullException(nameof(instance));
        }

        StringBuilder builder = new StringBuilder();
        foreach (Match match in Regex.Matches(instance, pattern, options))
        {
            builder.Append(match.Value);
        }
        return builder.ToString();
    }
    public static string NullIfEmpty(this string instance)
    {
        return instance == string.Empty ? null : instance;
    }
    public static string EmptyIfNull(this string instance)
    {
        return instance == null ? string.Empty : instance;
    }


    public static string[] Split(this string instance, string separator, StringSplitOptions options = StringSplitOptions.None)
    {
        ThrowHelper.ArgumentNull((instance == null), nameof(instance));

        return instance.Split(new string[] { separator }, options);
    }
    public static string[] GetLines(this string instance, StringSplitOptions options = StringSplitOptions.None)
    {
        ThrowHelper.ArgumentNull((instance == null), nameof(instance));

        return instance.Split(new string[] { Environment.NewLine }, options);
    }

    public static string Quote(this string instance, string left = "\"", string right = "\"")
    {
        ThrowHelper.ArgumentNull((instance == null), nameof(instance));

        return $"{left}{instance}{right}";
    }

    public static string FormatWith(this string instance, params object[] args)
    {
        ThrowHelper.ArgumentNull((instance == null), nameof(instance));

        return string.Format(
            System.Globalization.CultureInfo.InvariantCulture,
            instance,
            args);
    }

    /// <summary>
    ///     A string extension method that repeats the string a specified number of times.
    /// </summary>
    /// <param name="instance">The instance to act on.</param>
    /// <param name="repeatCount">Number of repeats.</param>
    /// <returns>The repeated string.</returns>
    public static string Repeat(this string instance, int repeatCount)
    {
        ThrowHelper.ArgumentNull((instance == null), nameof(instance));
        ThrowHelper.ArgumentMustNotNegative((repeatCount < 0), nameof(repeatCount));

        StringBuilder builder = new StringBuilder(repeatCount * instance.Length);
        for (int i = 0; i < repeatCount; i++)
        {
            builder.Append(instance);
        }

        return builder.ToString();
    }



    public static void WriteToConsole(this string instance)
    {
        ThrowHelper.ArgumentNull((instance == null), nameof(instance));

        Console.Write(instance);
    }
    public static void WriteLineToConsole(this string instance)
    {
        ThrowHelper.ArgumentNull((instance == null), nameof(instance));

        Console.WriteLine(instance);
    }
    public static void WriteToDebug(this string instance)
    {
        ThrowHelper.ArgumentNull((instance == null), nameof(instance));

        Debug.Write(instance);
    }
    public static void WriteLineToDebug(this string instance)
    {
        ThrowHelper.ArgumentNull((instance == null), nameof(instance));

        Debug.WriteLine(instance);
    }
}
