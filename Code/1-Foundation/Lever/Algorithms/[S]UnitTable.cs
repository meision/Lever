namespace Meision.Algorithms
{
    public static class UnitTable
    {
        public const int GuidBytes = 16;
        public const int GuidBits = 128;
        public const int GuidNStringLength = 32;

        public const int ByteBits = 8;

        public const int HexTextCountPerByte = 2;

        public const int SmallImageWidth = 16;
        public const int SmallImageHeight = 16;
        public const int LargeImageWidth = 32;
        public const int LargeImageHeight = 32;

        public const int IPv4AddressBytes = 4;
        public const int IPv6AddressBytes = 16;
        public const int MacAddressBytes = 6;

        public const int CharLength = 1;

        // Time
        public const int DayToHour = 24;
        public const int HourToMinute = 60;
        public const int MinuteToSecond = 60;
        public const int SecondToMillisecond = 1000;
        public const int MillisecondToMicrosecond = 1000;
        public const int MicrosecondToNanosecond = 1000;
        public const int SecondToMicrosecond = SecondToMillisecond * MillisecondToMicrosecond;
        public const int SecondToNanosecond = SecondToMicrosecond * MicrosecondToNanosecond;
        public const int SecondToTick = SecondToMillisecond * MillisecondToTick;
        public const int MillisecondToNanosecond = MillisecondToMicrosecond * MicrosecondToNanosecond;
        public const int TickToNanosecond = 100;
        public const int MillisecondToTick = MillisecondToNanosecond / TickToNanosecond;
        public const int MicrosecondToTick = MicrosecondToNanosecond / TickToNanosecond;

        // Size
        public const int B = 1;
        public const int KB = 1024;
        public const int MB = 1024 * 1024;
        public const int GB = 1024 * 1024 * 1024;

        public const int ByteToBit = 8;
        public const int KiloByteToByte = 1024;
        public const int MegaByteToKiloByte = 1024;
        public const int GigaByteToMegaByte = 1024;
        public const int TeraByteToGigaByte = 1024;
        public const int PetaByteToTeraByte = 1024;
        public const int ExaByteToPetaByte = 1024;
        public const int PetaByteToExaByte = 1024;
        public const int YottaByteToPetaByte = 1024;
        public const int NonaByteToYottaByte = 1024;
        public const int DoggaByteToNonaByte = 1024;
    }
}
