using System.Text;
using System.Text.RegularExpressions;

namespace Meision.Text
{
    public static class RegexConstant
    {
        #region Static
        // Generic
        public static readonly string NotEmpty = @".+";
        public static readonly string ContainsNonSpace = @"^\s*\S+\s*$";
        public static readonly string Numeric = @"\d+";
        public static readonly string Amount = @"^\d{1,6}(\.\d{1,2})?$";
        public static readonly string Base64Characters = @"[a-zA-Z0-9\+\/\=]";
        public static readonly string Email = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
        public static readonly string Username = @"[a-zA-Z]([a-zA-Z0-9\-]*[a-zA-Z0-9])?";
        public static readonly string Password = @"\S+";
        public static readonly string Integer = @"[0-9]{1,9}";
        public static readonly string ShortInteger = @"((\d{1,4})|([0-5]\d{4})|(6[0-4]\d{3})|(65[0-4]\d{2})|(655[0-2]\d)|(6553[0-5]))";

        public static readonly string Name = @"[a-zA-Z_][a-zA-Z0-9_\-]*";
        public static readonly string Token = @"[^\x00-\x1F\x28\x29\x3C\x3E\x40\x2C\x3B\x3A\x5C\x22\x2F\x5B\x5D\x3F\x3D\x7B\x7D\x20\x7F]";
        public static readonly string LWS = @"\r\n(\ |\t)";

        public static readonly string MD5Code = "[a-zA-Z0-9]{32}";

        // File
        public static readonly string FileSystemNameCharacter = @"[^\\\/\:\*\?\""\<\>\|\t]";
        
        // Network
        public static readonly string DomainNameOnly = @"[a-zA-Z0-9](([a-zA-Z0-9])*|([\-a-zA-Z0-9]*[a-zA-Z0-9]))";
        public static readonly string IPv4 = @"((\d{1,2})|([0-1]\d{2})|(2[0-4]\d)|(25[0-5]))(\.((\d{1,2})|([0-1]\d{2})|(2[0-4]\d)|(25[0-5]))){3}";
        public static readonly string IPv6 = @"(^(([0-9A-F]{1,4}(((:[0-9A-F]{1,4}){5}::[0-9A-F]{1,4})|((:[0-9A-F]{1,4}){4}::[0-9A-F]{1,4}(:[0-9A-F]{1,4}){0,1})|((:[0-9A-F]{1,4}){3}::[0-9A-F]{1,4}(:[0-9A-F]{1,4}){0,2})|((:[0-9A-F]{1,4}){2}::[0-9A-F]{1,4}(:[0-9A-F]{1,4}){0,3})|(:[0-9A-F]{1,4}::[0-9A-F]{1,4}(:[0-9A-F]{1,4}){0,4})|(::[0-9A-F]{1,4}(:[0-9A-F]{1,4}){0,5})|(:[0-9A-F]{1,4}){7}))$|^(::[0-9A-F]{1,4}(:[0-9A-F]{1,4}){0,6})$)|^::$)|^((([0-9A-F]{1,4}(((:[0-9A-F]{1,4}){3}::([0-9A-F]{1,4}){1})|((:[0-9A-F]{1,4}){2}::[0-9A-F]{1,4}(:[0-9A-F]{1,4}){0,1})|((:[0-9A-F]{1,4}){1}::[0-9A-F]{1,4}(:[0-9A-F]{1,4}){0,2})|(::[0-9A-F]{1,4}(:[0-9A-F]{1,4}){0,3})|((:[0-9A-F]{1,4}){0,5})))|([:]{2}[0-9A-F]{1,4}(:[0-9A-F]{1,4}){0,4})):|::)((25[0-5]|2[0-4][0-9]|[0-1]?[0-9]{0,2})\.){3}(25[0-5]|2[0-4][0-9]|[0-1]?[0-9]{0,2})";
        public static readonly string Host = @"[A-Za-z0-9\.-]+";
        public static readonly string Port = ShortInteger;

        public static readonly string UrlEncodedCharacter = string.Format(
            System.Globalization.CultureInfo.InvariantCulture,
            @"[a-zA-Z0-9{0}]",
            Regex.Escape(@"\()*-._!+%/"));
        #endregion Static

        #region Method
        public static string GetFullRegularExpression(string expression)
        {
            ThrowHelper.ArgumentNull((expression == null), nameof(expression));

            StringBuilder builder = new StringBuilder();
            if (!expression.StartsWith("^"))
            {
                builder.Append("^");
            }
            builder.Append(expression);
            if (!expression.EndsWith("$"))
            {
                builder.Append("$");
            }

            return builder.ToString();
        }
        #endregion Method
    }
}
