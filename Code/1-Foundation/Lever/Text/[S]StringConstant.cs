namespace Meision.Text
{
    public static class StringConstant
    {
        public const string Infinite = "\u221e";
        public const string Indicator = ":";
        public const string Separator = ",";
        public const string Conjunction = "|";
        public const string Blank = " ";
        public const string EmptyChar = "\0";
        public const string Space = " ";
        public const string Sub = ".";
        public const string Quotation = "\"";
        public const string Domain = "@";
        public const string Equal = "=";
        public const string Comment = "//";
        public const string LF = "\n";
        public const string CRLF = "\r\n";
        public const string DoubleCRLF = "\r\n\r\n";
        public const string PasswordMaskChar = "*";
        public const string NA = "N/A";
                
        // Network
        public const string IPv4Separator = ".";
        public const string IPv6Separator = ":";
        public const string PortIndicator = ":";
        public const string NetworkSeparatorChar = "/";
        public const string NetworkRootLeading = "//";

        // File system.
        public const string FileExtension = ".";
        public const string ParentFolder = "..";
        public const string CurrentFolder = ".";
        public const string Wildcard_Single = "?";
        public const string Wildcard_Multiple = "*";
        public static readonly char[] InvalidFileNameChars = { '\t', '\\', '/', ':', '*', '?', '"', '<', '>', '|' };

        // Xml
        public const string XmlElementRootLeading = "//";
        public const string XmlElementSeparatorChar = "/";
    }
}
