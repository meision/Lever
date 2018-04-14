using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Meision.Resources;
using Meision.Text;

namespace Meision.Net
{
    public class NetworkEndPoint
    {
        #region Static
        public const int MaxPort = 0xffff;
        public const int MinPort = 0;

        public static readonly string HostRegularExpression = RegexConstant.Host;
        public static readonly string PortRegularExpression = RegexConstant.Port;

        public const string PortDelimiter = ":";
        public const string RegularExpressionGroupName_Host = "Host";
        public const string RegularExpressionGroupName_Port = "Port";
        public static readonly string RegularExpression = string.Format(
            System.Globalization.CultureInfo.InvariantCulture,
            @"(?<{0}>{1})({2}(?<{3}>{4}))?",
            RegularExpressionGroupName_Host,
            HostRegularExpression,
            Regex.Escape(PortDelimiter),
            RegularExpressionGroupName_Port,
            PortRegularExpression);

        public static NetworkEndPoint Create(string expression)
        {
            ThrowHelper.ArgumentNull((expression == null), nameof(expression));

            Match match = Regex.Match(expression, RegexConstant.GetFullRegularExpression(RegularExpression));
            if (!match.Success)
            {
                return null;
            }

            Group hostGroup = match.Groups[RegularExpressionGroupName_Host];
            Group portGroup = match.Groups[RegularExpressionGroupName_Port];

            string host = hostGroup.Value;
            int port = 0;
            if (portGroup.Success)
            {
                port = Convert.ToInt32(portGroup.Value);
            }

            NetworkEndPoint point = new NetworkEndPoint(host, port);
            return point;
        }
        #endregion Static

        #region Field & Property
        private string _host;
        public string Host
        {
            get
            {
                return this._host;
            }
        }

        private int _port;
        public int Port
        {
            get
            {
                return this._port;
            }
        }
        #endregion Field & Property

        #region Constructor
        //private NetworkEndPoint()
        //{
        //}

        public NetworkEndPoint(string host, int port)
        {
            ThrowHelper.ArgumentNull((host == null), nameof(host));
            ThrowHelper.ArgumentOutOfRange(((port < MinPort) || (port > MaxPort)), nameof(port), SR.NetworkEndPoint_Exception_InvalidPort);

            this._host = host;
            this._port = port;
        }

        public NetworkEndPoint(IPEndPoint point)
        {
            this._host = point.Address.ToString();
            this._port = point.Port;
        }
        #endregion Constructor

        #region Method
        public bool Equals(NetworkEndPoint point)
        {
            if (point == null)
            {
                return false;
            }

            return (this._host.Equals(point._host, StringComparison.OrdinalIgnoreCase) && this._port.Equals(point._port));
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as NetworkEndPoint);
        }

        public override int GetHashCode()
        {
            return this._host.GetHashCode() ^ this._port.GetHashCode();
        }

        public IPEndPoint ToIPEndPoint()
        {
            return new IPEndPoint(Dns.GetHostAddresses(this.Host)[0], this.Port);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(this._host);
            if (this._port > 0)
            {
                builder.Append(PortDelimiter);
                builder.Append(this._port);
            }

            return builder.ToString();
        }
        #endregion Method
    }
}
