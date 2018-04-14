using System;
using System.Text;

namespace Meision.IO
{
    public interface IDataHandler
    {
        byte[] Buffer { get; }
        int Start { get; }
        int End { get; }
        int Length { get; }
        int Index { get; }
        int Position { get; set; }
        int Remaining { get; }
        byte Current { get; set; }

        int PositionToIndex(int position);
        int IndexToPosition(int index);
        IDataPin CreateDataPin();
        byte[] ExportAllBytes();
        byte[] ExportUsedBytes();

        byte[] ReadBytes(int size);
        void ReadBytes(byte[] buffer);
        void ReadBytes(byte[] buffer, int index, int count);
        T ReadObject<T>() where T : struct;
        object ReadObject(Type type);
        Byte ReadByte();
        SByte ReadSByte();
        Boolean ReadBoolean();
        Char ReadChar();
        Int16 ReadInt16();
        UInt16 ReadUInt16();
        Int32 ReadInt32();
        UInt32 ReadUInt32();
        Int64 ReadInt64();
        UInt64 ReadUInt64();
        Single ReadSingle();
        Double ReadDouble();
        String ReadStringWithTerminator();
        String ReadStringWithTerminator(Encoding encoding);
        String ReadString(int length);
        String ReadString(int length, Encoding encoding);

        void WriteBytes(byte[] values);
        void WriteBytes(byte[] values, int index, int count);
        void WriteZeroBytes(int count);
        void WriteObject<T>(T value);
        void WriteObject(object value);
        void WriteByte(Byte value);
        void WriteSByte(SByte value);
        void WriteBoolean(Boolean value);
        void WriteChar(Char value);
        void WriteInt16(Int16 value);
        void WriteUInt16(UInt16 value);
        void WriteInt32(Int32 value);
        void WriteUInt32(UInt32 value);
        void WriteInt64(Int64 value);
        void WriteUInt64(UInt64 value);
        void WriteSingle(Single value);
        void WriteDouble(Double value);
        void WriteStringWithTerminator(String value);
        void WriteStringWithTerminator(String value, Encoding encoding);
        void WriteString(String value);
        void WriteString(String value, Encoding encoding);
    }

    public interface IDataPin : IDisposable
    {

    }


}
