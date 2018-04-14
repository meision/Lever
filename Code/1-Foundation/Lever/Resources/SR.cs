// Generate by Lever visual studio extension - ExcelLanguagesGenerator. Please do not modify this file manully.
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading;

namespace Meision.Resources
{
    static partial class SR
    {
        private static readonly ReadOnlyCollection<CultureInfo> __locales = new ReadOnlyCollection<CultureInfo>(new CultureInfo[]
        {
            new CultureInfo(""),
        });

        /// <summary>
        /// Get all support locales.
        /// </summary>
        /// <returns>A ReadOnlyCollection instance.</returns>
        public static ReadOnlyCollection<CultureInfo> GetSupportLocales()
        {
            return SR.__locales;
        }

        private static readonly Dictionary<int, int> __columnMappings = GetColumnMappings();
        private static Dictionary<int, int> GetColumnMappings()
        {
            Dictionary<int, int> columnMappings = new Dictionary<int, int>();
            columnMappings.Add(127, 0);
            return columnMappings;
        }

        private static readonly Dictionary<string, string[]> __languages = GetLanguages();
        private static Dictionary<string, string[]> GetLanguages()
        {
            string key;
            string[] value;

            Dictionary<string, string[]> languages = new Dictionary<string, string[]>(296);
            key = @"_Exception_IndexOutOfRange";
            value = new string[1];
            value[0] = @"Index(Offset) must not be negative or not larger than array size.";
            languages.Add(key, value);
            key = @"_Exception_ValueOutOfRange";
            value = new string[1];
            value[0] = @"Value '{0}' is out of range.";
            languages.Add(key, value);
            key = @"_Exception_StringMustNotEmpty";
            value = new string[1];
            value[0] = @"String must no be empty.";
            languages.Add(key, value);
            key = @"_Exception_ArrayMustNotEmpty";
            value = new string[1];
            value[0] = @"Array must no be empty.";
            languages.Add(key, value);
            key = @"_Exception_IndexLengthOutOfArray";
            value = new string[1];
            value[0] = @"Index(Offset) and length(Count) must refer to a location within the array.";
            languages.Add(key, value);
            key = @"_Exception_LengthOutOfRange";
            value = new string[1];
            value[0] = @"Length(Count) must not be negative or not larger than array size.";
            languages.Add(key, value);
            key = @"_Exception_ValueMustPositive";
            value = new string[1];
            value[0] = @"Value must larger than 0.";
            languages.Add(key, value);
            key = @"_Exception_ValueMustNotNegative";
            value = new string[1];
            value[0] = @"Value must not be negative";
            languages.Add(key, value);
            key = @"_Exception_EnumNotDefine";
            value = new string[1];
            value[0] = @"The enum value does not define.";
            languages.Add(key, value);
            key = @"Socket_Exception_ReceiveEmtpy";
            value = new string[1];
            value[0] = @"Not any data has been received.";
            languages.Add(key, value);
            key = @"API_Shell_InvalidInterface";
            value = new string[1];
            value[0] = @"Type provided must be an Interface.";
            languages.Add(key, value);
            key = @"ArrayCollection_CouldNotInsertSelf";
            value = new string[1];
            value[0] = @"Could not insert itself.";
            languages.Add(key, value);
            key = @"ASN1Helper_InvalidData";
            value = new string[1];
            value[0] = @"Invalid key data.";
            languages.Add(key, value);
            key = @"ASN1Object_ClassificationNotSupport";
            value = new string[1];
            value[0] = @"Only support data of universal classification.";
            languages.Add(key, value);
            key = @"ASN1Object_InvalidData";
            value = new string[1];
            value[0] = @"Invalid ASN1 data.";
            languages.Add(key, value);
            key = @"ASN1Object_InvalidSize";
            value = new string[1];
            value[0] = @"Invalid size.";
            languages.Add(key, value);
            key = @"ASN1Object_PrimitiveTypeNotSupport";
            value = new string[1];
            value[0] = @"Primitive type does not support.";
            languages.Add(key, value);
            key = @"BitManager_Exception_DataExist";
            value = new string[1];
            value[0] = @"Data already exist.";
            languages.Add(key, value);
            key = @"BitManager_Exception_LengthOutOfRange";
            value = new string[1];
            value[0] = @"Length is out of range.";
            languages.Add(key, value);
            key = @"BPListSerializer_Exception_InvalidDataType";
            value = new string[1];
            value[0] = @"Invalid data type.";
            languages.Add(key, value);
            key = @"BPListSerializer_Exception_InvalidIntegerSize";
            value = new string[1];
            value[0] = @"Invalid integer size.";
            languages.Add(key, value);
            key = @"BPListSerializer_Exception_InvalidLengthSize";
            value = new string[1];
            value[0] = @"Invalid length size.";
            languages.Add(key, value);
            key = @"BPListSerializer_Exception_InvalidRealSize";
            value = new string[1];
            value[0] = @"Invalid real size.";
            languages.Add(key, value);
            key = @"BPListSerializer_Exception_InvalidSizeData";
            value = new string[1];
            value[0] = @"Invalid size data.";
            languages.Add(key, value);
            key = @"BPListSerializer_Exception_InvalidUIDData";
            value = new string[1];
            value[0] = @"Invalid UID data.";
            languages.Add(key, value);
            key = @"BPListSerializer_Exception_NotBinaryPropertyListData";
            value = new string[1];
            value[0] = @"Not binary property list data.";
            languages.Add(key, value);
            key = @"BrowsableCollection_OutOfRange";
            value = new string[1];
            value[0] = @"Browse index is out of range of collection.";
            languages.Add(key, value);
            key = @"BrowseFileButton_Description_FileBrowsing";
            value = new string[1];
            value[0] = @"Occurs when file browsing";
            languages.Add(key, value);
            key = @"BrowseFileButton_Description_SelectedFileChanged";
            value = new string[1];
            value[0] = @"Occurs when SelectedFile changed.";
            languages.Add(key, value);
            key = @"BrowseFileButton_Description_Target";
            value = new string[1];
            value[0] = @"Set the text for the target control when selected file changed.";
            languages.Add(key, value);
            key = @"BrowseFolderButton_Description_FolderBrowsing";
            value = new string[1];
            value[0] = @"Occurs when folder browsing.";
            languages.Add(key, value);
            key = @"BrowseFolderButton_Description_SelectedPathChanged";
            value = new string[1];
            value[0] = @"Occurs when SelectedPath changed.";
            languages.Add(key, value);
            key = @"BrowseFolderButton_Description_Target";
            value = new string[1];
            value[0] = @"Set the text for the target control when selected path changed.";
            languages.Add(key, value);
            key = @"Bytes_ConvertToStringArray_Exception_InvalidByteArray";
            value = new string[1];
            value[0] = @"Byte array is invalid. A valid byte array must be divided by 2 and end with '\0'(0x0000)";
            languages.Add(key, value);
            key = @"Bytes_Exception_BytesContainsNull";
            value = new string[1];
            value[0] = @"Parameters lists contains null at index of {0}.";
            languages.Add(key, value);
            key = @"Bytes_Exception_InvalidHexText";
            value = new string[1];
            value[0] = @"Invalid hex text.";
            languages.Add(key, value);
            key = @"Bytes_Exception_InvalidHexChar";
            value = new string[1];
            value[0] = @"Fail to parse hex string for '{0}'.";
            languages.Add(key, value);
            key = @"Stream_Exception_StreamLength";
            value = new string[1];
            value[0] = @"Stream length must be non-negative and less than 2^31 - 1 - origin.";
            languages.Add(key, value);
            key = @"Stream_Exception_SeekBeforeBegin";
            value = new string[1];
            value[0] = @"An attempt was made to move the position before the beginning of the stream.";
            languages.Add(key, value);
            key = @"Stream_Exception_SmallCapacity";
            value = new string[1];
            value[0] = @"Capacity was less than the current size.";
            languages.Add(key, value);
            key = @"Stream_Exception_StreamTooLong";
            value = new string[1];
            value[0] = @"Stream was too long.";
            languages.Add(key, value);
            key = @"Stream_Exception_WriteNotSupport";
            value = new string[1];
            value[0] = @"Stream does not support writing.";
            languages.Add(key, value);
            key = @"Stream_Exception_NotExpandable";
            value = new string[1];
            value[0] = @"Stream is not expandable.";
            languages.Add(key, value);
            key = @"Stream_Exception_ReadBeyondEOF";
            value = new string[1];
            value[0] = @"Unable to read beyond the end of the stream.";
            languages.Add(key, value);
            key = @"CacheTcpClient_Exception_Pattern";
            value = new string[1];
            value[0] = @"The pattern of the bytes must at least one byte.";
            languages.Add(key, value);
            key = @"Calculator_Allocate_PercentagesContainsNegative";
            value = new string[1];
            value[0] = @"Percentages contains negative values.";
            languages.Add(key, value);
            key = @"Calculator_Allocate_PercentagesTotalNotEqualsOne";
            value = new string[1];
            value[0] = @"Sum of percentages must equals 1.";
            languages.Add(key, value);
            key = @"Calculator_EnumContainsFlags_FlagsIsEmpty";
            value = new string[1];
            value[0] = @"Must specifies one or more checking flag(s).";
            languages.Add(key, value);
            key = @"ColumnModel_Exception_NameEmpty";
            value = new string[1];
            value[0] = @"Column name must not be empty.";
            languages.Add(key, value);
            key = @"ColumnModelCollection_Exception_NameExists";
            value = new string[1];
            value[0] = @"Column name already exists.";
            languages.Add(key, value);
            key = @"CompletionPort_Group_CouldNotSetPropertyDueToNoReady";
            value = new string[1];
            value[0] = @"Could not set property due to EngineGroup is not in ready status.";
            languages.Add(key, value);
            key = @"CompletionPort_Group_Disposed";
            value = new string[1];
            value[0] = @"EngineGroup already disposed.";
            languages.Add(key, value);
            key = @"CompletionPort_Group_IsNotReady";
            value = new string[1];
            value[0] = @"EngineGroup is not ready.";
            languages.Add(key, value);
            key = @"CompletionPort_Group_IsNotRunning";
            value = new string[1];
            value[0] = @"EngineGroup  is not running.";
            languages.Add(key, value);
            key = @"ConfirmButton_Description_SuppressDialogResult";
            value = new string[1];
            value[0] = @"Specifies whether suppress the system  function after click the button.";
            languages.Add(key, value);
            key = @"ConfirmButton_Description_Type";
            value = new string[1];
            value[0] = @"Set the confirm type in order to show system button text.";
            languages.Add(key, value);
            key = @"ContentType_Exception_InvalidSubType";
            value = new string[1];
            value[0] = @"Invalid sub type.";
            languages.Add(key, value);
            key = @"ContentType_Exception_InvalidType";
            value = new string[1];
            value[0] = @"Invalid type.";
            languages.Add(key, value);
            key = @"ControlManager_Exception_TypeIsNotEnum";
            value = new string[1];
            value[0] = @"Type is not a enumeration type.";
            languages.Add(key, value);
            key = @"CollectionManager_Concatenate_ContainsNullArray";
            value = new string[1];
            value[0] = @"Contains null array.";
            languages.Add(key, value);
            key = @"CRC16_Exception_InvalidString";
            value = new string[1];
            value[0] = @"Invalid String.";
            languages.Add(key, value);
            key = @"CRC16List_Exception_InvalidString";
            value = new string[1];
            value[0] = @"Invalid String.";
            languages.Add(key, value);
            key = @"CRC32_Exception_InvalidString";
            value = new string[1];
            value[0] = @"Invalid String.";
            languages.Add(key, value);
            key = @"CRC32List_Exception_InvalidString";
            value = new string[1];
            value[0] = @"Invalid String.";
            languages.Add(key, value);
            key = @"Cryptography_Exceiption_InvalidIV";
            value = new string[1];
            value[0] = @"Invalid IV.";
            languages.Add(key, value);
            key = @"Cryptography_Exceiption_InvalidKey";
            value = new string[1];
            value[0] = @"Invalid Key.";
            languages.Add(key, value);
            key = @"Cryptography_QuotedPrintable_Exception_InvalidText";
            value = new string[1];
            value[0] = @"Invalid QuotedPrintable text.";
            languages.Add(key, value);
            key = @"CSVReader_Exception_DataCountMismatch";
            value = new string[1];
            value[0] = @"Data count does not equals to header.";
            languages.Add(key, value);
            key = @"CSVReader_Exception_ExceptCharacter";
            value = new string[1];
            value[0] = @"Except character '{0}' on column {1}.";
            languages.Add(key, value);
            key = @"CSVReader_Exception_InvalidCharacter";
            value = new string[1];
            value[0] = @"Invalid character '{0}' found on column {1}.";
            languages.Add(key, value);
            key = @"CSVReader_Exception_InvalidCSVFormat";
            value = new string[1];
            value[0] = @"Invalid CSV File format.";
            languages.Add(key, value);
            key = @"CSVReader_Exception_InvalidRecord";
            value = new string[1];
            value[0] = @"Current record type is not header or data.";
            languages.Add(key, value);
            key = @"CSVReader_Exception_NotRead";
            value = new string[1];
            value[0] = @"Stream has not begun to read.";
            languages.Add(key, value);
            key = @"CSVWriter_Exception_ColumnCountMismatch";
            value = new string[1];
            value[0] = @"Column count mismatch.";
            languages.Add(key, value);
            key = @"DatabaseHelp_Exception_ParametersCountDoesNotMatch";
            value = new string[1];
            value[0] = @"Parameters count does not match.";
            languages.Add(key, value);
            key = @"DatabaseRepository_Exception_NotInContext";
            value = new string[1];
            value[0] = @"Execute does not under context.";
            languages.Add(key, value);
            key = @"DatabaseService_Exception_ForeignKeyNotFound";
            value = new string[1];
            value[0] = @"Foreign key could not be found.";
            languages.Add(key, value);
            key = @"DatabaseService_Exception_InvalidRelationship";
            value = new string[1];
            value[0] = @"Invalid relationship.";
            languages.Add(key, value);
            key = @"DatabaseService_UnknownType";
            value = new string[1];
            value[0] = @"Unknown type.";
            languages.Add(key, value);
            key = @"DataGrid_Exception_ColumnNotFound";
            value = new string[1];
            value[0] = @"Column ""{0}"" could not be found.";
            languages.Add(key, value);
            key = @"DataGrid_Exception_ColumnNotMatch";
            value = new string[1];
            value[0] = @"Item count does not match with columns.";
            languages.Add(key, value);
            key = @"DataHandler_CanNotAccess";
            value = new string[1];
            value[0] = @"DataHandler's internal buffer cannot be accessed.";
            languages.Add(key, value);
            key = @"DataHandler_Exception_ObjectNotSupport";
            value = new string[1];
            value[0] = @"Read/Write Object only support for sequential layout with 1 pack.";
            languages.Add(key, value);
            key = @"DataHandler_OutOfBytes";
            value = new string[1];
            value[0] = @"Not enough bytes space.";
            languages.Add(key, value);
            key = @"DbSelectCommandTextBuilder_Exception_Aggregate";
            value = new string[1];
            value[0] = @"Aggregate select requires only one column.";
            languages.Add(key, value);
            key = @"Directory_Exception_FileInPath";
            value = new string[1];
            value[0] = @"There are one or more files exist in the path.";
            languages.Add(key, value);
            key = @"Directory_Exception_PathIsInvalid";
            value = new string[1];
            value[0] = @"Path is invalid.";
            languages.Add(key, value);
            key = @"EntityCache_Exception_KeyAlreadyExist";
            value = new string[1];
            value[0] = @"Key already exists.";
            languages.Add(key, value);
            key = @"EntityCache_Exception_KeyNameNotFound";
            value = new string[1];
            value[0] = @"Key name could not be found.";
            languages.Add(key, value);
            key = @"EnumerationChooseBox_Exception_ValueIsNotEnum";
            value = new string[1];
            value[0] = @"Value is not an enumeration with flag attribute and drivered from Int32.";
            languages.Add(key, value);
            key = @"Evaluator_Exception_ExpectedType";
            value = new string[1];
            value[0] = @"Expected {0} for default value.";
            languages.Add(key, value);
            key = @"Evaluator_Exception_InvalidRangeExpression";
            value = new string[1];
            value[0] = @"{0} range expression at {1} is invalid.";
            languages.Add(key, value);
            key = @"Evaluator_Exception_InvalidTInput";
            value = new string[1];
            value[0] = @"Invalid input type, only accept {0}.";
            languages.Add(key, value);
            key = @"Evaluator_Exception_InvalidTOutput";
            value = new string[1];
            value[0] = @"Invalid output type, only accept {0}.";
            languages.Add(key, value);
            key = @"Evaluator_Exception_NullRangeExpression";
            value = new string[1];
            value[0] = @"Range expression at {0} is null.";
            languages.Add(key, value);
            key = @"EventTcpClient_Exception_Connected";
            value = new string[1];
            value[0] = @"Client has already connected.";
            languages.Add(key, value);
            key = @"FastSerializerManager_UnsupportType";
            value = new string[1];
            value[0] = @"Unsupport type ""{0}""";
            languages.Add(key, value);
            key = @"FatBinary_Exception_InvalidMachObject";
            value = new string[1];
            value[0] = @"Invalid FatBinary data.";
            languages.Add(key, value);
            key = @"File_Invalid";
            value = new string[1];
            value[0] = @"File Invalid.";
            languages.Add(key, value);
            key = @"File_Null";
            value = new string[1];
            value[0] = @"File should not be null.";
            languages.Add(key, value);
            key = @"FileStream_Exception_DoesNotReadExpectedSize";
            value = new string[1];
            value[0] = @"Read data size does not equal to expected.";
            languages.Add(key, value);
            key = @"FileStream_NeedWriteData";
            value = new string[1];
            value[0] = @"Need write data rights.";
            languages.Add(key, value);
            key = @"FileStream_NotSupportRead";
            value = new string[1];
            value[0] = @"Read operation isn't supported.";
            languages.Add(key, value);
            key = @"FileStream_NotSupportWrite";
            value = new string[1];
            value[0] = @"Write operation isn't supported.";
            languages.Add(key, value);
            key = @"Folder_Invalid";
            value = new string[1];
            value[0] = @"Folder Invalid.";
            languages.Add(key, value);
            key = @"Folder_Null";
            value = new string[1];
            value[0] = @"Folder should not null.";
            languages.Add(key, value);
            key = @"FolderListView_DetailsColumn_RequiresName";
            value = new string[1];
            value[0] = @"Details must display ""Name"" column.";
            languages.Add(key, value);
            key = @"HexCode_Exception_InvalidLiteralCode";
            value = new string[1];
            value[0] = @"Invalid literal code, it should be even length.";
            languages.Add(key, value);
            key = @"HexTextBox_Exception_CouldNotSetFont";
            value = new string[1];
            value[0] = @"Could not set Font for HexTextBox.";
            languages.Add(key, value);
            key = @"HexTextBox_Exception_CouldNotSetMaxLength";
            value = new string[1];
            value[0] = @"Could not set MaxLength for HexTextBox.";
            languages.Add(key, value);
            key = @"HexTextBox_Exception_CouldNotSetShortcutsEnabled";
            value = new string[1];
            value[0] = @"Could not set ShortcutsEnabled for HexTextBox.";
            languages.Add(key, value);
            key = @"HexTextBox_Exception_CouldNotSetText";
            value = new string[1];
            value[0] = @"Could not set Text for HexTextBox.";
            languages.Add(key, value);
            key = @"HttpAddress_Exception_Path_Invalid";
            value = new string[1];
            value[0] = @"Path is invalid.";
            languages.Add(key, value);
            key = @"HttpAddress_Exception_QueryString_Invalid";
            value = new string[1];
            value[0] = @"QueryString is invalid.";
            languages.Add(key, value);
            key = @"IconGroup_CompressionNotSupport";
            value = new string[1];
            value[0] = @"Does not support compression image.";
            languages.Add(key, value);
            key = @"IconGroup_Empty";
            value = new string[1];
            value[0] = @"Icon group is empty.";
            languages.Add(key, value);
            key = @"IconGroup_FormatExist";
            value = new string[1];
            value[0] = @"Format already exists.";
            languages.Add(key, value);
            key = @"IconGroup_InvalidFormat";
            value = new string[1];
            value[0] = @"Invalid format";
            languages.Add(key, value);
            key = @"IconGroup_InvalidHeight";
            value = new string[1];
            value[0] = @"Invalid height.";
            languages.Add(key, value);
            key = @"IconGroup_InvalidImageData";
            value = new string[1];
            value[0] = @"Invalid image data.";
            languages.Add(key, value);
            key = @"IconGroup_InvalidWidth";
            value = new string[1];
            value[0] = @"Invalid width.";
            languages.Add(key, value);
            key = @"IconGroup_PaletteNotMatch";
            value = new string[1];
            value[0] = @"Palette does not match.";
            languages.Add(key, value);
            key = @"IndexModel_Exception_NameEmpty";
            value = new string[1];
            value[0] = @"Index name must not be empty.";
            languages.Add(key, value);
            key = @"InternetPoint_Exception_AddressFamilyNotMatch";
            value = new string[1];
            value[0] = @"Address family must be same.";
            languages.Add(key, value);
            key = @"InternetPoint_Exception_AddressFamilyNotSupport";
            value = new string[1];
            value[0] = @"Address family does not support.";
            languages.Add(key, value);
            key = @"IocContainer_Exception_ComponentNotSupportService";
            value = new string[1];
            value[0] = @"The service ""{0}"" is not assignable from component ""{1}"".";
            languages.Add(key, value);
            key = @"IocContainer_Exception_ConstructNotFound";
            value = new string[1];
            value[0] = @"Construct could not be found.";
            languages.Add(key, value);
            key = @"IocContainer_Exception_InvalidRegisteringStatus";
            value = new string[1];
            value[0] = @"Could not perform Register() in current status.";
            languages.Add(key, value);
            key = @"IocContainer_Exception_InvalidResolvingStatus";
            value = new string[1];
            value[0] = @"Could not perform Resolve() in current status.";
            languages.Add(key, value);
            key = @"IocContainer_Exception_LifeCycleNotSupport";
            value = new string[1];
            value[0] = @"LifeCycle value does not support.";
            languages.Add(key, value);
            key = @"IocContainer_Exception_ServiceExists";
            value = new string[1];
            value[0] = @"Service already exists.";
            languages.Add(key, value);
            key = @"IocContainer_Exception_ServiceNotRegistered";
            value = new string[1];
            value[0] = @"Service is not registered.";
            languages.Add(key, value);
            key = @"IPAddressControl_Exception_IPAddressInvalid";
            value = new string[1];
            value[0] = @"IPAddress is an invalid Internet address(IPv4).";
            languages.Add(key, value);
            key = @"IPAddressManager_Exception_CouldNotConvertToIPv4";
            value = new string[1];
            value[0] = @"Could not convert to IPv4 address";
            languages.Add(key, value);
            key = @"IPAddressManager_Exception_InvalidAddressFamily";
            value = new string[1];
            value[0] = @"Invalid address family.";
            languages.Add(key, value);
            key = @"IPAddressManager_Exception_MustBothIPv4OrIPv6";
            value = new string[1];
            value[0] = @"Begin IP address and End IP address must be both IPv4 or IPv6";
            languages.Add(key, value);
            key = @"IPAddressRange_Exception_AddressFamilyMustBeSame";
            value = new string[1];
            value[0] = @"Address family must be same.";
            languages.Add(key, value);
            key = @"IPAddressRange_Exception_BeginLargerThanEnd";
            value = new string[1];
            value[0] = @"Begin IP larger than end IP.";
            languages.Add(key, value);
            key = @"IPAddressRange_Exception_BytesInvalid";
            value = new string[1];
            value[0] = @"Bytes invalid.";
            languages.Add(key, value);
            key = @"IPAddressRange_Exception_UpperLesserThanLower";
            value = new string[1];
            value[0] = @"End ip address must larger than begin ip address.";
            languages.Add(key, value);
            key = @"IPAddressRangeCollection_Exception_AddressFamilyNotSupport";
            value = new string[1];
            value[0] = @"AddressFamily does not support.";
            languages.Add(key, value);
            key = @"IPAddressRangeCollection_Exception_ItemAddressFamilyNotMatch";
            value = new string[1];
            value[0] = @"Inserting item address family does not match.";
            languages.Add(key, value);
            key = @"IPMapping_Exception_OnlySupportInterNetwork";
            value = new string[1];
            value[0] = @"Only support Inter network (IPv4).";
            languages.Add(key, value);
            key = @"Job_Exception_HasLaunched";
            value = new string[1];
            value[0] = @"Job has launched.";
            languages.Add(key, value);
            key = @"Job_Exception_ThreadExists";
            value = new string[1];
            value[0] = @"Thread already exists.";
            languages.Add(key, value);
            key = @"JSON_ArrayTypeNotSupported";
            value = new string[1];
            value[0] = @"Type '{0}' is not supported for deserialization of an array.";
            languages.Add(key, value);
            key = @"JSON_BadEscape";
            value = new string[1];
            value[0] = @"Unrecognized escape sequence.";
            languages.Add(key, value);
            key = @"JSON_CannotConvertObjectToType";
            value = new string[1];
            value[0] = @"Cannot convert object of type '{0}' to type '{1}'";
            languages.Add(key, value);
            key = @"JSON_CannotCreateListType";
            value = new string[1];
            value[0] = @"Cannot create instance of {0}.";
            languages.Add(key, value);
            key = @"JSON_CircularReference";
            value = new string[1];
            value[0] = @"A circular reference was detected while serializing an object of type '{0}'.";
            languages.Add(key, value);
            key = @"JSON_DepthLimitExceeded";
            value = new string[1];
            value[0] = @"RecursionLimit exceeded.";
            languages.Add(key, value);
            key = @"JSON_DeserializerTypeMismatch";
            value = new string[1];
            value[0] = @"Cannot deserialize object graph into type of '{0}'.";
            languages.Add(key, value);
            key = @"JSON_DictionaryTypeNotSupported";
            value = new string[1];
            value[0] = @"Type '{0}' is not supported for serialization/deserialization of a dictionary, keys must be strings or objects.";
            languages.Add(key, value);
            key = @"JSON_ExpectedOpenBrace";
            value = new string[1];
            value[0] = @"Invalid object passed in, '{' expected.";
            languages.Add(key, value);
            key = @"JSON_IllegalPrimitive";
            value = new string[1];
            value[0] = @"Invalid JSON primitive: {0}.";
            languages.Add(key, value);
            key = @"JSON_InvalidArrayEnd";
            value = new string[1];
            value[0] = @"Invalid array passed in, ']' expected.";
            languages.Add(key, value);
            key = @"JSON_InvalidArrayExpectComma";
            value = new string[1];
            value[0] = @"Invalid array passed in, ',' expected.";
            languages.Add(key, value);
            key = @"JSON_InvalidArrayExtraComma";
            value = new string[1];
            value[0] = @"Invalid array passed in, extra trailing ','.";
            languages.Add(key, value);
            key = @"JSON_InvalidArrayStart";
            value = new string[1];
            value[0] = @"Invalid array passed in, '[' expected.";
            languages.Add(key, value);
            key = @"JSON_InvalidEnumType";
            value = new string[1];
            value[0] = @"Enums based on System.Int64 or System.UInt64 are not JSON-serializable because JavaScript does not support the necessary precision.";
            languages.Add(key, value);
            key = @"JSON_InvalidMaxJsonLength";
            value = new string[1];
            value[0] = @"Value must be a positive integer.";
            languages.Add(key, value);
            key = @"JSON_InvalidMemberName";
            value = new string[1];
            value[0] = @"Invalid object passed in, member name expected.";
            languages.Add(key, value);
            key = @"JSON_InvalidObject";
            value = new string[1];
            value[0] = @"Invalid object passed in, ':' or '}' expected.";
            languages.Add(key, value);
            key = @"JSON_InvalidRecursionLimit";
            value = new string[1];
            value[0] = @"RecursionLimit must be a positive integer.";
            languages.Add(key, value);
            key = @"JSON_MaxJsonLengthExceeded";
            value = new string[1];
            value[0] = @"Error during serialization or deserialization using the JSON JavaScriptSerializer. The length of the string exceeds the value set on the maxJsonLength property.";
            languages.Add(key, value);
            key = @"JSON_NoConstructor";
            value = new string[1];
            value[0] = @"No parameterless constructor defined for type of '{0}'.";
            languages.Add(key, value);
            key = @"JSON_StringNotQuoted";
            value = new string[1];
            value[0] = @"Invalid string passed in, '\""' expected.";
            languages.Add(key, value);
            key = @"JSON_UnterminatedString";
            value = new string[1];
            value[0] = @"Unterminated string passed in.";
            languages.Add(key, value);
            key = @"JSON_ValueTypeCannotBeNull";
            value = new string[1];
            value[0] = @"Cannot convert null to a value type.";
            languages.Add(key, value);
            key = @"Logger_Exception_NotInitialize";
            value = new string[1];
            value[0] = @"Logger does not initialize.";
            languages.Add(key, value);
            key = @"MD5Code_Exception_InvalidBytes";
            value = new string[1];
            value[0] = @"Invalid MD5Code bytes.";
            languages.Add(key, value);
            key = @"MD5Code_Exception_InvalidText";
            value = new string[1];
            value[0] = @"Invalid MD5Code string.";
            languages.Add(key, value);
            key = @"MimeEntity_Exception_UnknownEntity";
            value = new string[1];
            value[0] = @"Unknown entity or invalid entity.";
            languages.Add(key, value);
            key = @"NanoEngine_DatabaseISOpen";
            value = new string[1];
            value[0] = @"Database already open.";
            languages.Add(key, value);
            key = @"NanoEngine_Exception_DatabaseInvalid";
            value = new string[1];
            value[0] = @"Database is invalid.";
            languages.Add(key, value);
            key = @"NanoEngine_Exception_IdNotAlloc";
            value = new string[1];
            value[0] = @"Id does not alloc, call AllocId() first.";
            languages.Add(key, value);
            key = @"NanoEngine_Exception_StatusAreSame";
            value = new string[1];
            value[0] = @"Original status and new status are same.";
            languages.Add(key, value);
            key = @"NanoEngine_Exception_TypeNotSupport";
            value = new string[1];
            value[0] = @"Type does not support.";
            languages.Add(key, value);
            key = @"NanoEngine_IdAllocated";
            value = new string[1];
            value[0] = @"Record id has already allocated.";
            languages.Add(key, value);
            key = @"NanoEngine_IdFull";
            value = new string[1];
            value[0] = @"NanoRecord id has fulled.";
            languages.Add(key, value);
            key = @"NanoEngine_RecordCountOutOfPointerCapacity";
            value = new string[1];
            value[0] = @"Record count out of pointer capacity.";
            languages.Add(key, value);
            key = @"NanoEngine_RecordNotFound";
            value = new string[1];
            value[0] = @"Record with id {0} is not found.";
            languages.Add(key, value);
            key = @"NanoEngine_RecordTypeInvalid";
            value = new string[1];
            value[0] = @"Record type invalid, id {0} does not belong to {1}.";
            languages.Add(key, value);
            key = @"NanoEngine_TypesOutOfRange";
            value = new string[1];
            value[0] = @"Only support 1-255 types";
            languages.Add(key, value);
            key = @"NanoRecord_Exception_RecordHasDeleted";
            value = new string[1];
            value[0] = @"Record has deleted.";
            languages.Add(key, value);
            key = @"NanoRecord_InvalidProperty";
            value = new string[1];
            value[0] = @"NanoRecord contains illegal property which not support.";
            languages.Add(key, value);
            key = @"NanoRecord_OutOfCapacity";
            value = new string[1];
            value[0] = @"NanoRecord has too much data member.";
            languages.Add(key, value);
            key = @"NavigationStrip_Category_BorderStyle";
            value = new string[1];
            value[0] = @"Appearance";
            languages.Add(key, value);
            key = @"NavigationStrip_Category_SelectedIndexChanged";
            value = new string[1];
            value[0] = @"Behavior";
            languages.Add(key, value);
            key = @"NavigationStrip_Description_BorderStyle";
            value = new string[1];
            value[0] = @"Indicates whether the panel should have a border.";
            languages.Add(key, value);
            key = @"NavigationStrip_Description_SelectedIndexChanged";
            value = new string[1];
            value[0] = @"Occurs when the value of the SelectedIndex property changes.";
            languages.Add(key, value);
            key = @"Netpath_Invalid";
            value = new string[1];
            value[0] = @"Netpath invalid.";
            languages.Add(key, value);
            key = @"NetworkCredential_KeyInvalid";
            value = new string[1];
            value[0] = @"A AES key must be 16 bytes(128 bit).";
            languages.Add(key, value);
            key = @"NetworkServer_Exception_HostIsNull";
            value = new string[1];
            value[0] = @"Host is null.";
            languages.Add(key, value);
            key = @"NetworkServer_Exception_InvalidExpression";
            value = new string[1];
            value[0] = @"Invalid Expression.";
            languages.Add(key, value);
            key = @"NetworkServer_Exception_InvalidHost";
            value = new string[1];
            value[0] = @"Host is invalid.";
            languages.Add(key, value);
            key = @"NetworkService_Exception_InvalidPort";
            value = new string[1];
            value[0] = @"Service port is invalid.";
            languages.Add(key, value);
            key = @"NetworkServiceCollection_Exception_ServiceExist";
            value = new string[1];
            value[0] = @"Service already exists.";
            languages.Add(key, value);
            key = @"NetworkEndPoint_Exception_InvalidPort";
            value = new string[1];
            value[0] = @"Port is invalid.";
            languages.Add(key, value);
            key = @"OpenFolderFileDialog_ContextMenu_Display";
            value = new string[1];
            value[0] = @"Display";
            languages.Add(key, value);
            key = @"OpenFolderFileDialog_ContextMenu_Display_File";
            value = new string[1];
            value[0] = @"File";
            languages.Add(key, value);
            key = @"OpenFolderFileDialog_ContextMenu_Display_Folder";
            value = new string[1];
            value[0] = @"Folder";
            languages.Add(key, value);
            key = @"OpenFolderFileDialog_ContextMenu_Display_Hidden";
            value = new string[1];
            value[0] = @"Hidden";
            languages.Add(key, value);
            key = @"OpenFolderFileDialog_ContextMenu_Execute";
            value = new string[1];
            value[0] = @"Execute";
            languages.Add(key, value);
            key = @"OpenFolderFileDialog_ContextMenu_Open";
            value = new string[1];
            value[0] = @"Open";
            languages.Add(key, value);
            key = @"OpenFolderFileDialog_ContextMenu_Parent";
            value = new string[1];
            value[0] = @"Up to Parent";
            languages.Add(key, value);
            key = @"OpenFolderFileDialog_ContextMenu_Refresh";
            value = new string[1];
            value[0] = @"Refresh";
            languages.Add(key, value);
            key = @"OpenFolderFileDialog_ContextMenu_Sort";
            value = new string[1];
            value[0] = @"Sort";
            languages.Add(key, value);
            key = @"OpenFolderFileDialog_ContextMenu_Sort_Ascending";
            value = new string[1];
            value[0] = @"Ascending";
            languages.Add(key, value);
            key = @"OpenFolderFileDialog_ContextMenu_Sort_Descending";
            value = new string[1];
            value[0] = @"Descending";
            languages.Add(key, value);
            key = @"OpenFolderFileDialog_ContextMenu_View";
            value = new string[1];
            value[0] = @"View";
            languages.Add(key, value);
            key = @"OpenFolderFileDialog_ContextMenu_View_Details";
            value = new string[1];
            value[0] = @"Details";
            languages.Add(key, value);
            key = @"OpenFolderFileDialog_ContextMenu_View_LargeIcon";
            value = new string[1];
            value[0] = @"Large Icon";
            languages.Add(key, value);
            key = @"OpenFolderFileDialog_ContextMenu_View_List";
            value = new string[1];
            value[0] = @"List";
            languages.Add(key, value);
            key = @"OpenFolderFileDialog_ContextMenu_View_SmallIcon";
            value = new string[1];
            value[0] = @"Small Icon";
            languages.Add(key, value);
            key = @"OpenFolderFileDialog_Exception_InvalidFilters";
            value = new string[1];
            value[0] = @"Filter string you provided is not valid. The filter string must contain a description of the filter, followed by the vertical bar (|) and the filter pattern. The strings for different filtering options must also be separated by the vertical bar. Example: ""Text files (*.txt)|*.txt|All files (*.*)|*.*""";
            languages.Add(key, value);
            key = @"OpenFolderFileDialog_Filters";
            value = new string[1];
            value[0] = @"Filters:";
            languages.Add(key, value);
            key = @"OpenFolderFileDialog_Paths";
            value = new string[1];
            value[0] = @"Paths:";
            languages.Add(key, value);
            key = @"OpenFolderFileDialog_Text";
            value = new string[1];
            value[0] = @"Open Folder/File";
            languages.Add(key, value);
            key = @"OwnerDrawListView_CanNotSetItemHeight";
            value = new string[1];
            value[0] = @"Can not set item height will handle created.";
            languages.Add(key, value);
            key = @"OwnerDrawTabControl_Close";
            value = new string[1];
            value[0] = @"Close";
            languages.Add(key, value);
            key = @"OwnerDrawTabControl_Exception_NotOwnerDrawTabPage";
            value = new string[1];
            value[0] = @"Only could add OwnerTabPage or its subclass.";
            languages.Add(key, value);
            key = @"PagingControl_TextTemplate_Item";
            value = new string[1];
            value[0] = @"Item: {0}-{1}/{2}";
            languages.Add(key, value);
            key = @"PagingControl_TextTemplate_Page";
            value = new string[1];
            value[0] = @"Page: {0}/{1}";
            languages.Add(key, value);
            key = @"Path_Invalid";
            value = new string[1];
            value[0] = @"Invalid path.";
            languages.Add(key, value);
            key = @"Path_LevelNotEnough";
            value = new string[1];
            value[0] = @"Directory do not contains enough parent level.";
            languages.Add(key, value);
            key = @"PE_Exception_InvalidDotNetPE";
            value = new string[1];
            value[0] = @"Invalid .net PE data";
            languages.Add(key, value);
            key = @"PE_Exception_InvalidPE";
            value = new string[1];
            value[0] = @"Invalid PE data.";
            languages.Add(key, value);
            key = @"PE_Exception_ManifestResourceNotFound";
            value = new string[1];
            value[0] = @"Specified manifest resource could not been found.";
            languages.Add(key, value);
            key = @"PE_Exception_NotSupportedDotNetPE";
            value = new string[1];
            value[0] = @".Net PE contains not supported data.";
            languages.Add(key, value);
            key = @"Picture_Number_Invalid";
            value = new string[1];
            value[0] = @"Invalid literal number.";
            languages.Add(key, value);
            key = @"PortTextBox_Text_NotSupport";
            value = new string[1];
            value[0] = @"Use ""Port"" property instead of ""Text"".";
            languages.Add(key, value);
            key = @"RegexElement_Invalid";
            value = new string[1];
            value[0] = @"Invalid element.";
            languages.Add(key, value);
            key = @"RegexElement_LookbehindAssertion_Invalid";
            value = new string[1];
            value[0] = @"Could not set RegexTerminalAssertion as lookbehind assertion.";
            languages.Add(key, value);
            key = @"RegexQuantifierElement_NotAcceptElements";
            value = new string[1];
            value[0] = @"Not accept RegexLiteralElement or RegexQuantifierElement as matching part.";
            languages.Add(key, value);
            key = @"ReliableUdpClient_Exception_InvalidRing";
            value = new string[1];
            value[0] = @"Invalid ring. ring value must between 1 to 65534";
            languages.Add(key, value);
            key = @"ReliableUdpConsole_Exception_ClientManaged";
            value = new string[1];
            value[0] = @"Client already managed.";
            languages.Add(key, value);
            key = @"ReliableUdpConsole_Exception_ClientNotManaged";
            value = new string[1];
            value[0] = @"Client does not manage.";
            languages.Add(key, value);
            key = @"Server_Exception_IsNotRunning";
            value = new string[1];
            value[0] = @"Server isn't running";
            languages.Add(key, value);
            key = @"Server_Exception_IsRunning";
            value = new string[1];
            value[0] = @"Server is running.";
            languages.Add(key, value);
            key = @"Server_MustRunning";
            value = new string[1];
            value[0] = @"Server not running, please call Start() method first.";
            languages.Add(key, value);
            key = @"Server_Running";
            value = new string[1];
            value[0] = @"Server is running!";
            languages.Add(key, value);
            key = @"ShellImageList_Exception_InvalidExtension";
            value = new string[1];
            value[0] = @"Invalid extension.";
            languages.Add(key, value);
            key = @"SortedCollection_Exception_Insert";
            value = new string[1];
            value[0] = @"Could not insert a item in specified index, please use add instead.";
            languages.Add(key, value);
            key = @"SortedCollection_Exception_SetItem";
            value = new string[1];
            value[0] = @"Could not set item for a SortCollection";
            languages.Add(key, value);
            key = @"StronglyTypedRowGenerator_RequiredColumn";
            value = new string[1];
            value[0] = @"Must at lease contains one column. ";
            languages.Add(key, value);
            key = @"StronglyTypedTableGenerator_RequiredColumn";
            value = new string[1];
            value[0] = @"Must at lease contains one column. ";
            languages.Add(key, value);
            key = @"SwitchControl_Exception_MustBeSwitchPage";
            value = new string[1];
            value[0] = @"Control must be switch panel.";
            languages.Add(key, value);
            key = @"SystemChangeNotifier_ArgumentException_PIDLExist";
            value = new string[1];
            value[0] = @"The PIDL is moniting.";
            languages.Add(key, value);
            key = @"TableModel_Exception_NameEmpty";
            value = new string[1];
            value[0] = @"Table name must not be empty.";
            languages.Add(key, value);
            key = @"TableModel_Exception_SchemaEmpty";
            value = new string[1];
            value[0] = @"Table schema must not be empty.";
            languages.Add(key, value);
            key = @"TableModelCollection_Exception_NameExists";
            value = new string[1];
            value[0] = @"Table name already exists.";
            languages.Add(key, value);
            key = @"TableSet_Exception_ColumnsEmpty";
            value = new string[1];
            value[0] = @"Column could not be empty.";
            languages.Add(key, value);
            key = @"TableSet_Exception_EntityIsNull";
            value = new string[1];
            value[0] = @"Entity could not be null when specify condition columns.";
            languages.Add(key, value);
            key = @"TableSet_Exception_InvalidPropertyName";
            value = new string[1];
            value[0] = @"Invalid property name.";
            languages.Add(key, value);
            key = @"TcpClient_Exception_Connected";
            value = new string[1];
            value[0] = @"Client has already connected.";
            languages.Add(key, value);
            key = @"TcpClient_Exception_NotFoundPattern";
            value = new string[1];
            value[0] = @"Could not find end pattern bytes in buffer.";
            languages.Add(key, value);
            key = @"TcpClient_Exception_ReceiveEmtpy";
            value = new string[1];
            value[0] = @"Not any data has been received.";
            languages.Add(key, value);
            key = @"ThreadHook_HookType_NotSupport";
            value = new string[1];
            value[0] = @"This HookType does not support in ThreadHook. You must consider using a GlobalHook class";
            languages.Add(key, value);
            key = @"TreeListView_Exception_NodeIsNotCollectionContainer";
            value = new string[1];
            value[0] = @"Node is not a collection container.";
            languages.Add(key, value);
            key = @"TreeListViewNode_Exception_DataIsNotCollectionContainer";
            value = new string[1];
            value[0] = @"Data is not a collection container.";
            languages.Add(key, value);
            key = @"Updating_Exception_InvalidEndUpdate";
            value = new string[1];
            value[0] = @"You must call BeginUpdate() Method before.";
            languages.Add(key, value);
            key = @"ValidationInfo_Exception_ExpressionEmpty";
            value = new string[1];
            value[0] = @"Expression is empty.";
            languages.Add(key, value);
            key = @"ValidationItemCollection_Exception_ItemExist";
            value = new string[1];
            value[0] = @"Item exist.";
            languages.Add(key, value);
            key = @"ValidationItemCollection_Exception_ItemNotFound";
            value = new string[1];
            value[0] = @"Item not found.";
            languages.Add(key, value);
            key = @"Validator_ActionList_Tasks";
            value = new string[1];
            value[0] = @"Validator Tasks";
            languages.Add(key, value);
            key = @"Validator_ActionList_Tasks_ClearValidationInformation";
            value = new string[1];
            value[0] = @"Clear Validation Information";
            languages.Add(key, value);
            key = @"Validator_Category_BlinkRate";
            value = new string[1];
            value[0] = @"Error Provider";
            languages.Add(key, value);
            key = @"Validator_Category_BlinkStyle";
            value = new string[1];
            value[0] = @"Error Provider";
            languages.Add(key, value);
            key = @"Validator_Category_CustomValidation";
            value = new string[1];
            value[0] = @"Action";
            languages.Add(key, value);
            key = @"Validator_Category_Icon";
            value = new string[1];
            value[0] = @"Error Provider";
            languages.Add(key, value);
            key = @"Validator_Category_RightToLeft";
            value = new string[1];
            value[0] = @"Error Provider";
            languages.Add(key, value);
            key = @"Validator_Category_Validation";
            value = new string[1];
            value[0] = @"Validation";
            languages.Add(key, value);
            key = @"Validator_ClearWarning";
            value = new string[1];
            value[0] = @"Do you want to clear all validation information from current binding controls?";
            languages.Add(key, value);
            key = @"Validator_DefaultErrorMesage";
            value = new string[1];
            value[0] = @"An error occurred.";
            languages.Add(key, value);
            key = @"Validator_Description_BlinkRate";
            value = new string[1];
            value[0] = @"The rate in milliseconds at which the error icon blinks.";
            languages.Add(key, value);
            key = @"Validator_Description_BlinkStyle";
            value = new string[1];
            value[0] = @"Controls whether the error icon blinks when an error is set.";
            languages.Add(key, value);
            key = @"Validator_Description_CustomValidation";
            value = new string[1];
            value[0] = @"Occurs when validate, it required set ""Custom"" validation type flag.";
            languages.Add(key, value);
            key = @"Validator_Description_Form";
            value = new string[1];
            value[0] = @"Validate all configured validation information controls which lay onto the specified container, when the container is closing.";
            languages.Add(key, value);
            key = @"Validator_Description_Icon";
            value = new string[1];
            value[0] = @"The icon used to indicate an error.";
            languages.Add(key, value);
            key = @"Validator_Description_Mode";
            value = new string[1];
            value[0] = @"Validation mode. This mode should be combined with the follow value. FocusChange: Can change to next control when validation fail. Submit:Validate control when user submit.";
            languages.Add(key, value);
            key = @"Validator_Description_RightToLeft";
            value = new string[1];
            value[0] = @"Indicates whether the component should draw right-to-left for RTL languages.";
            languages.Add(key, value);
            key = @"Validator_Description_Validation_ErrorMesage";
            value = new string[1];
            value[0] = @"Specify a message to been show when control violate regular expression rule(text isn't match regular express).";
            languages.Add(key, value);
            key = @"Validator_Description_Validation_RegularExpression";
            value = new string[1];
            value[0] = @"Specify regular expression for validation. This property only valid if sets RegularExpression validation type. ";
            languages.Add(key, value);
            key = @"Validator_Description_Validation_RegularExpressionOptions";
            value = new string[1];
            value[0] = @"Specify options for regular expression.";
            languages.Add(key, value);
            key = @"Validator_Name";
            value = new string[1];
            value[0] = @"Validator";
            languages.Add(key, value);
            key = @"Validator_RemoveWarning";
            value = new string[1];
            value[0] = @"Do you want to remove validation information associate with this control?";
            languages.Add(key, value);
            key = @"ValueRange_Exception_UpperLesserThanLower";
            value = new string[1];
            value[0] = @"End value must larger than begin value.";
            languages.Add(key, value);
            key = @"Variable16_Exception_SizeUnexpected";
            value = new string[1];
            value[0] = @"Size unexpected, requries 2.";
            languages.Add(key, value);
            key = @"Variable32_Exception_SizeUnexpected";
            value = new string[1];
            value[0] = @"Size unexpected, requries 4.";
            languages.Add(key, value);
            key = @"Variable64_Exception_SizeUnexpected";
            value = new string[1];
            value[0] = @"Size unexpected, requries 8.";
            languages.Add(key, value);
            key = @"Variable8_Exception_SizeUnexpected";
            value = new string[1];
            value[0] = @"Size unexpected, requries 1.";
            languages.Add(key, value);
            key = @"VirtualItemCollection_ItemHasParent";
            value = new string[1];
            value[0] = @"This item belong to another item.";
            languages.Add(key, value);
            key = @"VirtualListView_Category_VirtualListSizeChanged";
            value = new string[1];
            value[0] = @"Property Changed";
            languages.Add(key, value);
            key = @"VirtualListView_Description_VirtualListSizeChanged";
            value = new string[1];
            value[0] = @"Occurs whenever the 'VirtualListSize' property for this ListView changed.";
            languages.Add(key, value);
            key = @"WebClient_Exception_InvalidChunkedBody";
            value = new string[1];
            value[0] = @"Invalid chunked body.";
            languages.Add(key, value);
            return languages;
        }

        public static CultureInfo GetAvailableCulture(CultureInfo culture)
        {
            CultureInfo current = culture;
            while (true)
            {
                if (SR.__columnMappings.ContainsKey(current.LCID))
                {
                    return current;
                }

                current = current.Parent;
            }
        }

        public static string GetString(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return SR.GetStringInternal(name, Thread.CurrentThread.CurrentUICulture);
        }

        public static string GetString(string name, CultureInfo culture)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (culture == null)
            {
                throw new ArgumentNullException(nameof(culture));
            }

            return SR.GetStringInternal(name, culture);
        }

        private static string GetStringInternal(string name, CultureInfo culture)
        {
            if (!SR.__languages.ContainsKey(name))
            {
                return null;
            }
            if (!SR.__columnMappings.ContainsKey(culture.LCID))
            {
                culture = SR.GetAvailableCulture(culture);
            }

            int index = SR.__columnMappings[culture.LCID];
            string text = SR.__languages[name][index];
            if (text == null)
            {
                text = SR.__languages[name][0];
            }

            return text;
        }

        /// <summary>
        /// Index(Offset) must not be negative or not larger than array size.
        /// </summary>
        public static string _Exception_IndexOutOfRange
        {
            get
            {
                return SR.GetStringInternal("_Exception_IndexOutOfRange", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Value '{0}' is out of range.
        /// </summary>
        public static string _Exception_ValueOutOfRange
        {
            get
            {
                return SR.GetStringInternal("_Exception_ValueOutOfRange", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// String must no be empty.
        /// </summary>
        public static string _Exception_StringMustNotEmpty
        {
            get
            {
                return SR.GetStringInternal("_Exception_StringMustNotEmpty", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Array must no be empty.
        /// </summary>
        public static string _Exception_ArrayMustNotEmpty
        {
            get
            {
                return SR.GetStringInternal("_Exception_ArrayMustNotEmpty", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Index(Offset) and length(Count) must refer to a location within the array.
        /// </summary>
        public static string _Exception_IndexLengthOutOfArray
        {
            get
            {
                return SR.GetStringInternal("_Exception_IndexLengthOutOfArray", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Length(Count) must not be negative or not larger than array size.
        /// </summary>
        public static string _Exception_LengthOutOfRange
        {
            get
            {
                return SR.GetStringInternal("_Exception_LengthOutOfRange", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Value must larger than 0.
        /// </summary>
        public static string _Exception_ValueMustPositive
        {
            get
            {
                return SR.GetStringInternal("_Exception_ValueMustPositive", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Value must not be negative
        /// </summary>
        public static string _Exception_ValueMustNotNegative
        {
            get
            {
                return SR.GetStringInternal("_Exception_ValueMustNotNegative", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// The enum value does not define.
        /// </summary>
        public static string _Exception_EnumNotDefine
        {
            get
            {
                return SR.GetStringInternal("_Exception_EnumNotDefine", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Not any data has been received.
        /// </summary>
        public static string Socket_Exception_ReceiveEmtpy
        {
            get
            {
                return SR.GetStringInternal("Socket_Exception_ReceiveEmtpy", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Type provided must be an Interface.
        /// </summary>
        public static string API_Shell_InvalidInterface
        {
            get
            {
                return SR.GetStringInternal("API_Shell_InvalidInterface", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Could not insert itself.
        /// </summary>
        public static string ArrayCollection_CouldNotInsertSelf
        {
            get
            {
                return SR.GetStringInternal("ArrayCollection_CouldNotInsertSelf", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid key data.
        /// </summary>
        public static string ASN1Helper_InvalidData
        {
            get
            {
                return SR.GetStringInternal("ASN1Helper_InvalidData", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Only support data of universal classification.
        /// </summary>
        public static string ASN1Object_ClassificationNotSupport
        {
            get
            {
                return SR.GetStringInternal("ASN1Object_ClassificationNotSupport", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid ASN1 data.
        /// </summary>
        public static string ASN1Object_InvalidData
        {
            get
            {
                return SR.GetStringInternal("ASN1Object_InvalidData", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid size.
        /// </summary>
        public static string ASN1Object_InvalidSize
        {
            get
            {
                return SR.GetStringInternal("ASN1Object_InvalidSize", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Primitive type does not support.
        /// </summary>
        public static string ASN1Object_PrimitiveTypeNotSupport
        {
            get
            {
                return SR.GetStringInternal("ASN1Object_PrimitiveTypeNotSupport", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Data already exist.
        /// </summary>
        public static string BitManager_Exception_DataExist
        {
            get
            {
                return SR.GetStringInternal("BitManager_Exception_DataExist", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Length is out of range.
        /// </summary>
        public static string BitManager_Exception_LengthOutOfRange
        {
            get
            {
                return SR.GetStringInternal("BitManager_Exception_LengthOutOfRange", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid data type.
        /// </summary>
        public static string BPListSerializer_Exception_InvalidDataType
        {
            get
            {
                return SR.GetStringInternal("BPListSerializer_Exception_InvalidDataType", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid integer size.
        /// </summary>
        public static string BPListSerializer_Exception_InvalidIntegerSize
        {
            get
            {
                return SR.GetStringInternal("BPListSerializer_Exception_InvalidIntegerSize", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid length size.
        /// </summary>
        public static string BPListSerializer_Exception_InvalidLengthSize
        {
            get
            {
                return SR.GetStringInternal("BPListSerializer_Exception_InvalidLengthSize", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid real size.
        /// </summary>
        public static string BPListSerializer_Exception_InvalidRealSize
        {
            get
            {
                return SR.GetStringInternal("BPListSerializer_Exception_InvalidRealSize", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid size data.
        /// </summary>
        public static string BPListSerializer_Exception_InvalidSizeData
        {
            get
            {
                return SR.GetStringInternal("BPListSerializer_Exception_InvalidSizeData", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid UID data.
        /// </summary>
        public static string BPListSerializer_Exception_InvalidUIDData
        {
            get
            {
                return SR.GetStringInternal("BPListSerializer_Exception_InvalidUIDData", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Not binary property list data.
        /// </summary>
        public static string BPListSerializer_Exception_NotBinaryPropertyListData
        {
            get
            {
                return SR.GetStringInternal("BPListSerializer_Exception_NotBinaryPropertyListData", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Browse index is out of range of collection.
        /// </summary>
        public static string BrowsableCollection_OutOfRange
        {
            get
            {
                return SR.GetStringInternal("BrowsableCollection_OutOfRange", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Occurs when file browsing
        /// </summary>
        public static string BrowseFileButton_Description_FileBrowsing
        {
            get
            {
                return SR.GetStringInternal("BrowseFileButton_Description_FileBrowsing", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Occurs when SelectedFile changed.
        /// </summary>
        public static string BrowseFileButton_Description_SelectedFileChanged
        {
            get
            {
                return SR.GetStringInternal("BrowseFileButton_Description_SelectedFileChanged", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Set the text for the target control when selected file changed.
        /// </summary>
        public static string BrowseFileButton_Description_Target
        {
            get
            {
                return SR.GetStringInternal("BrowseFileButton_Description_Target", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Occurs when folder browsing.
        /// </summary>
        public static string BrowseFolderButton_Description_FolderBrowsing
        {
            get
            {
                return SR.GetStringInternal("BrowseFolderButton_Description_FolderBrowsing", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Occurs when SelectedPath changed.
        /// </summary>
        public static string BrowseFolderButton_Description_SelectedPathChanged
        {
            get
            {
                return SR.GetStringInternal("BrowseFolderButton_Description_SelectedPathChanged", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Set the text for the target control when selected path changed.
        /// </summary>
        public static string BrowseFolderButton_Description_Target
        {
            get
            {
                return SR.GetStringInternal("BrowseFolderButton_Description_Target", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Byte array is invalid. A valid byte array must be divided by 2 and end with '\0'(0x0000)
        /// </summary>
        public static string Bytes_ConvertToStringArray_Exception_InvalidByteArray
        {
            get
            {
                return SR.GetStringInternal("Bytes_ConvertToStringArray_Exception_InvalidByteArray", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Parameters lists contains null at index of {0}.
        /// </summary>
        public static string Bytes_Exception_BytesContainsNull
        {
            get
            {
                return SR.GetStringInternal("Bytes_Exception_BytesContainsNull", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid hex text.
        /// </summary>
        public static string Bytes_Exception_InvalidHexText
        {
            get
            {
                return SR.GetStringInternal("Bytes_Exception_InvalidHexText", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Fail to parse hex string for '{0}'.
        /// </summary>
        public static string Bytes_Exception_InvalidHexChar
        {
            get
            {
                return SR.GetStringInternal("Bytes_Exception_InvalidHexChar", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Stream length must be non-negative and less than 2^31 - 1 - origin.
        /// </summary>
        public static string Stream_Exception_StreamLength
        {
            get
            {
                return SR.GetStringInternal("Stream_Exception_StreamLength", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// An attempt was made to move the position before the beginning of the stream.
        /// </summary>
        public static string Stream_Exception_SeekBeforeBegin
        {
            get
            {
                return SR.GetStringInternal("Stream_Exception_SeekBeforeBegin", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Capacity was less than the current size.
        /// </summary>
        public static string Stream_Exception_SmallCapacity
        {
            get
            {
                return SR.GetStringInternal("Stream_Exception_SmallCapacity", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Stream was too long.
        /// </summary>
        public static string Stream_Exception_StreamTooLong
        {
            get
            {
                return SR.GetStringInternal("Stream_Exception_StreamTooLong", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Stream does not support writing.
        /// </summary>
        public static string Stream_Exception_WriteNotSupport
        {
            get
            {
                return SR.GetStringInternal("Stream_Exception_WriteNotSupport", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Stream is not expandable.
        /// </summary>
        public static string Stream_Exception_NotExpandable
        {
            get
            {
                return SR.GetStringInternal("Stream_Exception_NotExpandable", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Unable to read beyond the end of the stream.
        /// </summary>
        public static string Stream_Exception_ReadBeyondEOF
        {
            get
            {
                return SR.GetStringInternal("Stream_Exception_ReadBeyondEOF", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// The pattern of the bytes must at least one byte.
        /// </summary>
        public static string CacheTcpClient_Exception_Pattern
        {
            get
            {
                return SR.GetStringInternal("CacheTcpClient_Exception_Pattern", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Percentages contains negative values.
        /// </summary>
        public static string Calculator_Allocate_PercentagesContainsNegative
        {
            get
            {
                return SR.GetStringInternal("Calculator_Allocate_PercentagesContainsNegative", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Sum of percentages must equals 1.
        /// </summary>
        public static string Calculator_Allocate_PercentagesTotalNotEqualsOne
        {
            get
            {
                return SR.GetStringInternal("Calculator_Allocate_PercentagesTotalNotEqualsOne", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Must specifies one or more checking flag(s).
        /// </summary>
        public static string Calculator_EnumContainsFlags_FlagsIsEmpty
        {
            get
            {
                return SR.GetStringInternal("Calculator_EnumContainsFlags_FlagsIsEmpty", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Column name must not be empty.
        /// </summary>
        public static string ColumnModel_Exception_NameEmpty
        {
            get
            {
                return SR.GetStringInternal("ColumnModel_Exception_NameEmpty", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Column name already exists.
        /// </summary>
        public static string ColumnModelCollection_Exception_NameExists
        {
            get
            {
                return SR.GetStringInternal("ColumnModelCollection_Exception_NameExists", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Could not set property due to EngineGroup is not in ready status.
        /// </summary>
        public static string CompletionPort_Group_CouldNotSetPropertyDueToNoReady
        {
            get
            {
                return SR.GetStringInternal("CompletionPort_Group_CouldNotSetPropertyDueToNoReady", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// EngineGroup already disposed.
        /// </summary>
        public static string CompletionPort_Group_Disposed
        {
            get
            {
                return SR.GetStringInternal("CompletionPort_Group_Disposed", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// EngineGroup is not ready.
        /// </summary>
        public static string CompletionPort_Group_IsNotReady
        {
            get
            {
                return SR.GetStringInternal("CompletionPort_Group_IsNotReady", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// EngineGroup  is not running.
        /// </summary>
        public static string CompletionPort_Group_IsNotRunning
        {
            get
            {
                return SR.GetStringInternal("CompletionPort_Group_IsNotRunning", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Specifies whether suppress the system  function after click the button.
        /// </summary>
        public static string ConfirmButton_Description_SuppressDialogResult
        {
            get
            {
                return SR.GetStringInternal("ConfirmButton_Description_SuppressDialogResult", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Set the confirm type in order to show system button text.
        /// </summary>
        public static string ConfirmButton_Description_Type
        {
            get
            {
                return SR.GetStringInternal("ConfirmButton_Description_Type", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid sub type.
        /// </summary>
        public static string ContentType_Exception_InvalidSubType
        {
            get
            {
                return SR.GetStringInternal("ContentType_Exception_InvalidSubType", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid type.
        /// </summary>
        public static string ContentType_Exception_InvalidType
        {
            get
            {
                return SR.GetStringInternal("ContentType_Exception_InvalidType", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Type is not a enumeration type.
        /// </summary>
        public static string ControlManager_Exception_TypeIsNotEnum
        {
            get
            {
                return SR.GetStringInternal("ControlManager_Exception_TypeIsNotEnum", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Contains null array.
        /// </summary>
        public static string CollectionManager_Concatenate_ContainsNullArray
        {
            get
            {
                return SR.GetStringInternal("CollectionManager_Concatenate_ContainsNullArray", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid String.
        /// </summary>
        public static string CRC16_Exception_InvalidString
        {
            get
            {
                return SR.GetStringInternal("CRC16_Exception_InvalidString", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid String.
        /// </summary>
        public static string CRC16List_Exception_InvalidString
        {
            get
            {
                return SR.GetStringInternal("CRC16List_Exception_InvalidString", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid String.
        /// </summary>
        public static string CRC32_Exception_InvalidString
        {
            get
            {
                return SR.GetStringInternal("CRC32_Exception_InvalidString", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid String.
        /// </summary>
        public static string CRC32List_Exception_InvalidString
        {
            get
            {
                return SR.GetStringInternal("CRC32List_Exception_InvalidString", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid IV.
        /// </summary>
        public static string Cryptography_Exceiption_InvalidIV
        {
            get
            {
                return SR.GetStringInternal("Cryptography_Exceiption_InvalidIV", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid Key.
        /// </summary>
        public static string Cryptography_Exceiption_InvalidKey
        {
            get
            {
                return SR.GetStringInternal("Cryptography_Exceiption_InvalidKey", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid QuotedPrintable text.
        /// </summary>
        public static string Cryptography_QuotedPrintable_Exception_InvalidText
        {
            get
            {
                return SR.GetStringInternal("Cryptography_QuotedPrintable_Exception_InvalidText", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Data count does not equals to header.
        /// </summary>
        public static string CSVReader_Exception_DataCountMismatch
        {
            get
            {
                return SR.GetStringInternal("CSVReader_Exception_DataCountMismatch", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Except character '{0}' on column {1}.
        /// </summary>
        public static string CSVReader_Exception_ExceptCharacter
        {
            get
            {
                return SR.GetStringInternal("CSVReader_Exception_ExceptCharacter", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid character '{0}' found on column {1}.
        /// </summary>
        public static string CSVReader_Exception_InvalidCharacter
        {
            get
            {
                return SR.GetStringInternal("CSVReader_Exception_InvalidCharacter", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid CSV File format.
        /// </summary>
        public static string CSVReader_Exception_InvalidCSVFormat
        {
            get
            {
                return SR.GetStringInternal("CSVReader_Exception_InvalidCSVFormat", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Current record type is not header or data.
        /// </summary>
        public static string CSVReader_Exception_InvalidRecord
        {
            get
            {
                return SR.GetStringInternal("CSVReader_Exception_InvalidRecord", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Stream has not begun to read.
        /// </summary>
        public static string CSVReader_Exception_NotRead
        {
            get
            {
                return SR.GetStringInternal("CSVReader_Exception_NotRead", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Column count mismatch.
        /// </summary>
        public static string CSVWriter_Exception_ColumnCountMismatch
        {
            get
            {
                return SR.GetStringInternal("CSVWriter_Exception_ColumnCountMismatch", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Parameters count does not match.
        /// </summary>
        public static string DatabaseHelp_Exception_ParametersCountDoesNotMatch
        {
            get
            {
                return SR.GetStringInternal("DatabaseHelp_Exception_ParametersCountDoesNotMatch", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Execute does not under context.
        /// </summary>
        public static string DatabaseRepository_Exception_NotInContext
        {
            get
            {
                return SR.GetStringInternal("DatabaseRepository_Exception_NotInContext", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Foreign key could not be found.
        /// </summary>
        public static string DatabaseService_Exception_ForeignKeyNotFound
        {
            get
            {
                return SR.GetStringInternal("DatabaseService_Exception_ForeignKeyNotFound", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid relationship.
        /// </summary>
        public static string DatabaseService_Exception_InvalidRelationship
        {
            get
            {
                return SR.GetStringInternal("DatabaseService_Exception_InvalidRelationship", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Unknown type.
        /// </summary>
        public static string DatabaseService_UnknownType
        {
            get
            {
                return SR.GetStringInternal("DatabaseService_UnknownType", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Column "{0}" could not be found.
        /// </summary>
        public static string DataGrid_Exception_ColumnNotFound
        {
            get
            {
                return SR.GetStringInternal("DataGrid_Exception_ColumnNotFound", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Item count does not match with columns.
        /// </summary>
        public static string DataGrid_Exception_ColumnNotMatch
        {
            get
            {
                return SR.GetStringInternal("DataGrid_Exception_ColumnNotMatch", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// DataHandler's internal buffer cannot be accessed.
        /// </summary>
        public static string DataHandler_CanNotAccess
        {
            get
            {
                return SR.GetStringInternal("DataHandler_CanNotAccess", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Read/Write Object only support for sequential layout with 1 pack.
        /// </summary>
        public static string DataHandler_Exception_ObjectNotSupport
        {
            get
            {
                return SR.GetStringInternal("DataHandler_Exception_ObjectNotSupport", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Not enough bytes space.
        /// </summary>
        public static string DataHandler_OutOfBytes
        {
            get
            {
                return SR.GetStringInternal("DataHandler_OutOfBytes", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Aggregate select requires only one column.
        /// </summary>
        public static string DbSelectCommandTextBuilder_Exception_Aggregate
        {
            get
            {
                return SR.GetStringInternal("DbSelectCommandTextBuilder_Exception_Aggregate", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// There are one or more files exist in the path.
        /// </summary>
        public static string Directory_Exception_FileInPath
        {
            get
            {
                return SR.GetStringInternal("Directory_Exception_FileInPath", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Path is invalid.
        /// </summary>
        public static string Directory_Exception_PathIsInvalid
        {
            get
            {
                return SR.GetStringInternal("Directory_Exception_PathIsInvalid", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Key already exists.
        /// </summary>
        public static string EntityCache_Exception_KeyAlreadyExist
        {
            get
            {
                return SR.GetStringInternal("EntityCache_Exception_KeyAlreadyExist", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Key name could not be found.
        /// </summary>
        public static string EntityCache_Exception_KeyNameNotFound
        {
            get
            {
                return SR.GetStringInternal("EntityCache_Exception_KeyNameNotFound", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Value is not an enumeration with flag attribute and drivered from Int32.
        /// </summary>
        public static string EnumerationChooseBox_Exception_ValueIsNotEnum
        {
            get
            {
                return SR.GetStringInternal("EnumerationChooseBox_Exception_ValueIsNotEnum", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Expected {0} for default value.
        /// </summary>
        public static string Evaluator_Exception_ExpectedType
        {
            get
            {
                return SR.GetStringInternal("Evaluator_Exception_ExpectedType", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// {0} range expression at {1} is invalid.
        /// </summary>
        public static string Evaluator_Exception_InvalidRangeExpression
        {
            get
            {
                return SR.GetStringInternal("Evaluator_Exception_InvalidRangeExpression", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid input type, only accept {0}.
        /// </summary>
        public static string Evaluator_Exception_InvalidTInput
        {
            get
            {
                return SR.GetStringInternal("Evaluator_Exception_InvalidTInput", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid output type, only accept {0}.
        /// </summary>
        public static string Evaluator_Exception_InvalidTOutput
        {
            get
            {
                return SR.GetStringInternal("Evaluator_Exception_InvalidTOutput", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Range expression at {0} is null.
        /// </summary>
        public static string Evaluator_Exception_NullRangeExpression
        {
            get
            {
                return SR.GetStringInternal("Evaluator_Exception_NullRangeExpression", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Client has already connected.
        /// </summary>
        public static string EventTcpClient_Exception_Connected
        {
            get
            {
                return SR.GetStringInternal("EventTcpClient_Exception_Connected", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Unsupport type "{0}"
        /// </summary>
        public static string FastSerializerManager_UnsupportType
        {
            get
            {
                return SR.GetStringInternal("FastSerializerManager_UnsupportType", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid FatBinary data.
        /// </summary>
        public static string FatBinary_Exception_InvalidMachObject
        {
            get
            {
                return SR.GetStringInternal("FatBinary_Exception_InvalidMachObject", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// File Invalid.
        /// </summary>
        public static string File_Invalid
        {
            get
            {
                return SR.GetStringInternal("File_Invalid", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// File should not be null.
        /// </summary>
        public static string File_Null
        {
            get
            {
                return SR.GetStringInternal("File_Null", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Read data size does not equal to expected.
        /// </summary>
        public static string FileStream_Exception_DoesNotReadExpectedSize
        {
            get
            {
                return SR.GetStringInternal("FileStream_Exception_DoesNotReadExpectedSize", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Need write data rights.
        /// </summary>
        public static string FileStream_NeedWriteData
        {
            get
            {
                return SR.GetStringInternal("FileStream_NeedWriteData", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Read operation isn't supported.
        /// </summary>
        public static string FileStream_NotSupportRead
        {
            get
            {
                return SR.GetStringInternal("FileStream_NotSupportRead", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Write operation isn't supported.
        /// </summary>
        public static string FileStream_NotSupportWrite
        {
            get
            {
                return SR.GetStringInternal("FileStream_NotSupportWrite", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Folder Invalid.
        /// </summary>
        public static string Folder_Invalid
        {
            get
            {
                return SR.GetStringInternal("Folder_Invalid", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Folder should not null.
        /// </summary>
        public static string Folder_Null
        {
            get
            {
                return SR.GetStringInternal("Folder_Null", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Details must display "Name" column.
        /// </summary>
        public static string FolderListView_DetailsColumn_RequiresName
        {
            get
            {
                return SR.GetStringInternal("FolderListView_DetailsColumn_RequiresName", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid literal code, it should be even length.
        /// </summary>
        public static string HexCode_Exception_InvalidLiteralCode
        {
            get
            {
                return SR.GetStringInternal("HexCode_Exception_InvalidLiteralCode", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Could not set Font for HexTextBox.
        /// </summary>
        public static string HexTextBox_Exception_CouldNotSetFont
        {
            get
            {
                return SR.GetStringInternal("HexTextBox_Exception_CouldNotSetFont", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Could not set MaxLength for HexTextBox.
        /// </summary>
        public static string HexTextBox_Exception_CouldNotSetMaxLength
        {
            get
            {
                return SR.GetStringInternal("HexTextBox_Exception_CouldNotSetMaxLength", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Could not set ShortcutsEnabled for HexTextBox.
        /// </summary>
        public static string HexTextBox_Exception_CouldNotSetShortcutsEnabled
        {
            get
            {
                return SR.GetStringInternal("HexTextBox_Exception_CouldNotSetShortcutsEnabled", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Could not set Text for HexTextBox.
        /// </summary>
        public static string HexTextBox_Exception_CouldNotSetText
        {
            get
            {
                return SR.GetStringInternal("HexTextBox_Exception_CouldNotSetText", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Path is invalid.
        /// </summary>
        public static string HttpAddress_Exception_Path_Invalid
        {
            get
            {
                return SR.GetStringInternal("HttpAddress_Exception_Path_Invalid", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// QueryString is invalid.
        /// </summary>
        public static string HttpAddress_Exception_QueryString_Invalid
        {
            get
            {
                return SR.GetStringInternal("HttpAddress_Exception_QueryString_Invalid", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Does not support compression image.
        /// </summary>
        public static string IconGroup_CompressionNotSupport
        {
            get
            {
                return SR.GetStringInternal("IconGroup_CompressionNotSupport", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Icon group is empty.
        /// </summary>
        public static string IconGroup_Empty
        {
            get
            {
                return SR.GetStringInternal("IconGroup_Empty", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Format already exists.
        /// </summary>
        public static string IconGroup_FormatExist
        {
            get
            {
                return SR.GetStringInternal("IconGroup_FormatExist", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid format
        /// </summary>
        public static string IconGroup_InvalidFormat
        {
            get
            {
                return SR.GetStringInternal("IconGroup_InvalidFormat", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid height.
        /// </summary>
        public static string IconGroup_InvalidHeight
        {
            get
            {
                return SR.GetStringInternal("IconGroup_InvalidHeight", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid image data.
        /// </summary>
        public static string IconGroup_InvalidImageData
        {
            get
            {
                return SR.GetStringInternal("IconGroup_InvalidImageData", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid width.
        /// </summary>
        public static string IconGroup_InvalidWidth
        {
            get
            {
                return SR.GetStringInternal("IconGroup_InvalidWidth", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Palette does not match.
        /// </summary>
        public static string IconGroup_PaletteNotMatch
        {
            get
            {
                return SR.GetStringInternal("IconGroup_PaletteNotMatch", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Index name must not be empty.
        /// </summary>
        public static string IndexModel_Exception_NameEmpty
        {
            get
            {
                return SR.GetStringInternal("IndexModel_Exception_NameEmpty", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Address family must be same.
        /// </summary>
        public static string InternetPoint_Exception_AddressFamilyNotMatch
        {
            get
            {
                return SR.GetStringInternal("InternetPoint_Exception_AddressFamilyNotMatch", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Address family does not support.
        /// </summary>
        public static string InternetPoint_Exception_AddressFamilyNotSupport
        {
            get
            {
                return SR.GetStringInternal("InternetPoint_Exception_AddressFamilyNotSupport", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// The service "{0}" is not assignable from component "{1}".
        /// </summary>
        public static string IocContainer_Exception_ComponentNotSupportService
        {
            get
            {
                return SR.GetStringInternal("IocContainer_Exception_ComponentNotSupportService", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Construct could not be found.
        /// </summary>
        public static string IocContainer_Exception_ConstructNotFound
        {
            get
            {
                return SR.GetStringInternal("IocContainer_Exception_ConstructNotFound", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Could not perform Register() in current status.
        /// </summary>
        public static string IocContainer_Exception_InvalidRegisteringStatus
        {
            get
            {
                return SR.GetStringInternal("IocContainer_Exception_InvalidRegisteringStatus", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Could not perform Resolve() in current status.
        /// </summary>
        public static string IocContainer_Exception_InvalidResolvingStatus
        {
            get
            {
                return SR.GetStringInternal("IocContainer_Exception_InvalidResolvingStatus", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// LifeCycle value does not support.
        /// </summary>
        public static string IocContainer_Exception_LifeCycleNotSupport
        {
            get
            {
                return SR.GetStringInternal("IocContainer_Exception_LifeCycleNotSupport", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Service already exists.
        /// </summary>
        public static string IocContainer_Exception_ServiceExists
        {
            get
            {
                return SR.GetStringInternal("IocContainer_Exception_ServiceExists", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Service is not registered.
        /// </summary>
        public static string IocContainer_Exception_ServiceNotRegistered
        {
            get
            {
                return SR.GetStringInternal("IocContainer_Exception_ServiceNotRegistered", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// IPAddress is an invalid Internet address(IPv4).
        /// </summary>
        public static string IPAddressControl_Exception_IPAddressInvalid
        {
            get
            {
                return SR.GetStringInternal("IPAddressControl_Exception_IPAddressInvalid", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Could not convert to IPv4 address
        /// </summary>
        public static string IPAddressManager_Exception_CouldNotConvertToIPv4
        {
            get
            {
                return SR.GetStringInternal("IPAddressManager_Exception_CouldNotConvertToIPv4", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid address family.
        /// </summary>
        public static string IPAddressManager_Exception_InvalidAddressFamily
        {
            get
            {
                return SR.GetStringInternal("IPAddressManager_Exception_InvalidAddressFamily", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Begin IP address and End IP address must be both IPv4 or IPv6
        /// </summary>
        public static string IPAddressManager_Exception_MustBothIPv4OrIPv6
        {
            get
            {
                return SR.GetStringInternal("IPAddressManager_Exception_MustBothIPv4OrIPv6", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Address family must be same.
        /// </summary>
        public static string IPAddressRange_Exception_AddressFamilyMustBeSame
        {
            get
            {
                return SR.GetStringInternal("IPAddressRange_Exception_AddressFamilyMustBeSame", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Begin IP larger than end IP.
        /// </summary>
        public static string IPAddressRange_Exception_BeginLargerThanEnd
        {
            get
            {
                return SR.GetStringInternal("IPAddressRange_Exception_BeginLargerThanEnd", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Bytes invalid.
        /// </summary>
        public static string IPAddressRange_Exception_BytesInvalid
        {
            get
            {
                return SR.GetStringInternal("IPAddressRange_Exception_BytesInvalid", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// End ip address must larger than begin ip address.
        /// </summary>
        public static string IPAddressRange_Exception_UpperLesserThanLower
        {
            get
            {
                return SR.GetStringInternal("IPAddressRange_Exception_UpperLesserThanLower", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// AddressFamily does not support.
        /// </summary>
        public static string IPAddressRangeCollection_Exception_AddressFamilyNotSupport
        {
            get
            {
                return SR.GetStringInternal("IPAddressRangeCollection_Exception_AddressFamilyNotSupport", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Inserting item address family does not match.
        /// </summary>
        public static string IPAddressRangeCollection_Exception_ItemAddressFamilyNotMatch
        {
            get
            {
                return SR.GetStringInternal("IPAddressRangeCollection_Exception_ItemAddressFamilyNotMatch", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Only support Inter network (IPv4).
        /// </summary>
        public static string IPMapping_Exception_OnlySupportInterNetwork
        {
            get
            {
                return SR.GetStringInternal("IPMapping_Exception_OnlySupportInterNetwork", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Job has launched.
        /// </summary>
        public static string Job_Exception_HasLaunched
        {
            get
            {
                return SR.GetStringInternal("Job_Exception_HasLaunched", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Thread already exists.
        /// </summary>
        public static string Job_Exception_ThreadExists
        {
            get
            {
                return SR.GetStringInternal("Job_Exception_ThreadExists", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Type '{0}' is not supported for deserialization of an array.
        /// </summary>
        public static string JSON_ArrayTypeNotSupported
        {
            get
            {
                return SR.GetStringInternal("JSON_ArrayTypeNotSupported", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Unrecognized escape sequence.
        /// </summary>
        public static string JSON_BadEscape
        {
            get
            {
                return SR.GetStringInternal("JSON_BadEscape", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Cannot convert object of type '{0}' to type '{1}'
        /// </summary>
        public static string JSON_CannotConvertObjectToType
        {
            get
            {
                return SR.GetStringInternal("JSON_CannotConvertObjectToType", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Cannot create instance of {0}.
        /// </summary>
        public static string JSON_CannotCreateListType
        {
            get
            {
                return SR.GetStringInternal("JSON_CannotCreateListType", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// A circular reference was detected while serializing an object of type '{0}'.
        /// </summary>
        public static string JSON_CircularReference
        {
            get
            {
                return SR.GetStringInternal("JSON_CircularReference", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// RecursionLimit exceeded.
        /// </summary>
        public static string JSON_DepthLimitExceeded
        {
            get
            {
                return SR.GetStringInternal("JSON_DepthLimitExceeded", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Cannot deserialize object graph into type of '{0}'.
        /// </summary>
        public static string JSON_DeserializerTypeMismatch
        {
            get
            {
                return SR.GetStringInternal("JSON_DeserializerTypeMismatch", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Type '{0}' is not supported for serialization/deserialization of a dictionary, keys must be strings or objects.
        /// </summary>
        public static string JSON_DictionaryTypeNotSupported
        {
            get
            {
                return SR.GetStringInternal("JSON_DictionaryTypeNotSupported", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid object passed in, '{' expected.
        /// </summary>
        public static string JSON_ExpectedOpenBrace
        {
            get
            {
                return SR.GetStringInternal("JSON_ExpectedOpenBrace", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid JSON primitive: {0}.
        /// </summary>
        public static string JSON_IllegalPrimitive
        {
            get
            {
                return SR.GetStringInternal("JSON_IllegalPrimitive", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid array passed in, ']' expected.
        /// </summary>
        public static string JSON_InvalidArrayEnd
        {
            get
            {
                return SR.GetStringInternal("JSON_InvalidArrayEnd", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid array passed in, ',' expected.
        /// </summary>
        public static string JSON_InvalidArrayExpectComma
        {
            get
            {
                return SR.GetStringInternal("JSON_InvalidArrayExpectComma", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid array passed in, extra trailing ','.
        /// </summary>
        public static string JSON_InvalidArrayExtraComma
        {
            get
            {
                return SR.GetStringInternal("JSON_InvalidArrayExtraComma", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid array passed in, '[' expected.
        /// </summary>
        public static string JSON_InvalidArrayStart
        {
            get
            {
                return SR.GetStringInternal("JSON_InvalidArrayStart", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Enums based on System.Int64 or System.UInt64 are not JSON-serializable because JavaScript does not support the necessary precision.
        /// </summary>
        public static string JSON_InvalidEnumType
        {
            get
            {
                return SR.GetStringInternal("JSON_InvalidEnumType", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Value must be a positive integer.
        /// </summary>
        public static string JSON_InvalidMaxJsonLength
        {
            get
            {
                return SR.GetStringInternal("JSON_InvalidMaxJsonLength", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid object passed in, member name expected.
        /// </summary>
        public static string JSON_InvalidMemberName
        {
            get
            {
                return SR.GetStringInternal("JSON_InvalidMemberName", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid object passed in, ':' or '}' expected.
        /// </summary>
        public static string JSON_InvalidObject
        {
            get
            {
                return SR.GetStringInternal("JSON_InvalidObject", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// RecursionLimit must be a positive integer.
        /// </summary>
        public static string JSON_InvalidRecursionLimit
        {
            get
            {
                return SR.GetStringInternal("JSON_InvalidRecursionLimit", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Error during serialization or deserialization using the JSON JavaScriptSerializer. The length of the string exceeds the value set on the maxJsonLength property.
        /// </summary>
        public static string JSON_MaxJsonLengthExceeded
        {
            get
            {
                return SR.GetStringInternal("JSON_MaxJsonLengthExceeded", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// No parameterless constructor defined for type of '{0}'.
        /// </summary>
        public static string JSON_NoConstructor
        {
            get
            {
                return SR.GetStringInternal("JSON_NoConstructor", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid string passed in, '\"' expected.
        /// </summary>
        public static string JSON_StringNotQuoted
        {
            get
            {
                return SR.GetStringInternal("JSON_StringNotQuoted", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Unterminated string passed in.
        /// </summary>
        public static string JSON_UnterminatedString
        {
            get
            {
                return SR.GetStringInternal("JSON_UnterminatedString", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Cannot convert null to a value type.
        /// </summary>
        public static string JSON_ValueTypeCannotBeNull
        {
            get
            {
                return SR.GetStringInternal("JSON_ValueTypeCannotBeNull", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Logger does not initialize.
        /// </summary>
        public static string Logger_Exception_NotInitialize
        {
            get
            {
                return SR.GetStringInternal("Logger_Exception_NotInitialize", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid MD5Code bytes.
        /// </summary>
        public static string MD5Code_Exception_InvalidBytes
        {
            get
            {
                return SR.GetStringInternal("MD5Code_Exception_InvalidBytes", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid MD5Code string.
        /// </summary>
        public static string MD5Code_Exception_InvalidText
        {
            get
            {
                return SR.GetStringInternal("MD5Code_Exception_InvalidText", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Unknown entity or invalid entity.
        /// </summary>
        public static string MimeEntity_Exception_UnknownEntity
        {
            get
            {
                return SR.GetStringInternal("MimeEntity_Exception_UnknownEntity", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Database already open.
        /// </summary>
        public static string NanoEngine_DatabaseISOpen
        {
            get
            {
                return SR.GetStringInternal("NanoEngine_DatabaseISOpen", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Database is invalid.
        /// </summary>
        public static string NanoEngine_Exception_DatabaseInvalid
        {
            get
            {
                return SR.GetStringInternal("NanoEngine_Exception_DatabaseInvalid", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Id does not alloc, call AllocId() first.
        /// </summary>
        public static string NanoEngine_Exception_IdNotAlloc
        {
            get
            {
                return SR.GetStringInternal("NanoEngine_Exception_IdNotAlloc", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Original status and new status are same.
        /// </summary>
        public static string NanoEngine_Exception_StatusAreSame
        {
            get
            {
                return SR.GetStringInternal("NanoEngine_Exception_StatusAreSame", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Type does not support.
        /// </summary>
        public static string NanoEngine_Exception_TypeNotSupport
        {
            get
            {
                return SR.GetStringInternal("NanoEngine_Exception_TypeNotSupport", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Record id has already allocated.
        /// </summary>
        public static string NanoEngine_IdAllocated
        {
            get
            {
                return SR.GetStringInternal("NanoEngine_IdAllocated", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// NanoRecord id has fulled.
        /// </summary>
        public static string NanoEngine_IdFull
        {
            get
            {
                return SR.GetStringInternal("NanoEngine_IdFull", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Record count out of pointer capacity.
        /// </summary>
        public static string NanoEngine_RecordCountOutOfPointerCapacity
        {
            get
            {
                return SR.GetStringInternal("NanoEngine_RecordCountOutOfPointerCapacity", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Record with id {0} is not found.
        /// </summary>
        public static string NanoEngine_RecordNotFound
        {
            get
            {
                return SR.GetStringInternal("NanoEngine_RecordNotFound", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Record type invalid, id {0} does not belong to {1}.
        /// </summary>
        public static string NanoEngine_RecordTypeInvalid
        {
            get
            {
                return SR.GetStringInternal("NanoEngine_RecordTypeInvalid", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Only support 1-255 types
        /// </summary>
        public static string NanoEngine_TypesOutOfRange
        {
            get
            {
                return SR.GetStringInternal("NanoEngine_TypesOutOfRange", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Record has deleted.
        /// </summary>
        public static string NanoRecord_Exception_RecordHasDeleted
        {
            get
            {
                return SR.GetStringInternal("NanoRecord_Exception_RecordHasDeleted", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// NanoRecord contains illegal property which not support.
        /// </summary>
        public static string NanoRecord_InvalidProperty
        {
            get
            {
                return SR.GetStringInternal("NanoRecord_InvalidProperty", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// NanoRecord has too much data member.
        /// </summary>
        public static string NanoRecord_OutOfCapacity
        {
            get
            {
                return SR.GetStringInternal("NanoRecord_OutOfCapacity", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Appearance
        /// </summary>
        public static string NavigationStrip_Category_BorderStyle
        {
            get
            {
                return SR.GetStringInternal("NavigationStrip_Category_BorderStyle", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Behavior
        /// </summary>
        public static string NavigationStrip_Category_SelectedIndexChanged
        {
            get
            {
                return SR.GetStringInternal("NavigationStrip_Category_SelectedIndexChanged", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Indicates whether the panel should have a border.
        /// </summary>
        public static string NavigationStrip_Description_BorderStyle
        {
            get
            {
                return SR.GetStringInternal("NavigationStrip_Description_BorderStyle", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Occurs when the value of the SelectedIndex property changes.
        /// </summary>
        public static string NavigationStrip_Description_SelectedIndexChanged
        {
            get
            {
                return SR.GetStringInternal("NavigationStrip_Description_SelectedIndexChanged", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Netpath invalid.
        /// </summary>
        public static string Netpath_Invalid
        {
            get
            {
                return SR.GetStringInternal("Netpath_Invalid", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// A AES key must be 16 bytes(128 bit).
        /// </summary>
        public static string NetworkCredential_KeyInvalid
        {
            get
            {
                return SR.GetStringInternal("NetworkCredential_KeyInvalid", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Host is null.
        /// </summary>
        public static string NetworkServer_Exception_HostIsNull
        {
            get
            {
                return SR.GetStringInternal("NetworkServer_Exception_HostIsNull", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid Expression.
        /// </summary>
        public static string NetworkServer_Exception_InvalidExpression
        {
            get
            {
                return SR.GetStringInternal("NetworkServer_Exception_InvalidExpression", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Host is invalid.
        /// </summary>
        public static string NetworkServer_Exception_InvalidHost
        {
            get
            {
                return SR.GetStringInternal("NetworkServer_Exception_InvalidHost", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Service port is invalid.
        /// </summary>
        public static string NetworkService_Exception_InvalidPort
        {
            get
            {
                return SR.GetStringInternal("NetworkService_Exception_InvalidPort", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Service already exists.
        /// </summary>
        public static string NetworkServiceCollection_Exception_ServiceExist
        {
            get
            {
                return SR.GetStringInternal("NetworkServiceCollection_Exception_ServiceExist", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Port is invalid.
        /// </summary>
        public static string NetworkEndPoint_Exception_InvalidPort
        {
            get
            {
                return SR.GetStringInternal("NetworkEndPoint_Exception_InvalidPort", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Display
        /// </summary>
        public static string OpenFolderFileDialog_ContextMenu_Display
        {
            get
            {
                return SR.GetStringInternal("OpenFolderFileDialog_ContextMenu_Display", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// File
        /// </summary>
        public static string OpenFolderFileDialog_ContextMenu_Display_File
        {
            get
            {
                return SR.GetStringInternal("OpenFolderFileDialog_ContextMenu_Display_File", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Folder
        /// </summary>
        public static string OpenFolderFileDialog_ContextMenu_Display_Folder
        {
            get
            {
                return SR.GetStringInternal("OpenFolderFileDialog_ContextMenu_Display_Folder", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Hidden
        /// </summary>
        public static string OpenFolderFileDialog_ContextMenu_Display_Hidden
        {
            get
            {
                return SR.GetStringInternal("OpenFolderFileDialog_ContextMenu_Display_Hidden", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Execute
        /// </summary>
        public static string OpenFolderFileDialog_ContextMenu_Execute
        {
            get
            {
                return SR.GetStringInternal("OpenFolderFileDialog_ContextMenu_Execute", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Open
        /// </summary>
        public static string OpenFolderFileDialog_ContextMenu_Open
        {
            get
            {
                return SR.GetStringInternal("OpenFolderFileDialog_ContextMenu_Open", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Up to Parent
        /// </summary>
        public static string OpenFolderFileDialog_ContextMenu_Parent
        {
            get
            {
                return SR.GetStringInternal("OpenFolderFileDialog_ContextMenu_Parent", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Refresh
        /// </summary>
        public static string OpenFolderFileDialog_ContextMenu_Refresh
        {
            get
            {
                return SR.GetStringInternal("OpenFolderFileDialog_ContextMenu_Refresh", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Sort
        /// </summary>
        public static string OpenFolderFileDialog_ContextMenu_Sort
        {
            get
            {
                return SR.GetStringInternal("OpenFolderFileDialog_ContextMenu_Sort", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Ascending
        /// </summary>
        public static string OpenFolderFileDialog_ContextMenu_Sort_Ascending
        {
            get
            {
                return SR.GetStringInternal("OpenFolderFileDialog_ContextMenu_Sort_Ascending", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Descending
        /// </summary>
        public static string OpenFolderFileDialog_ContextMenu_Sort_Descending
        {
            get
            {
                return SR.GetStringInternal("OpenFolderFileDialog_ContextMenu_Sort_Descending", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// View
        /// </summary>
        public static string OpenFolderFileDialog_ContextMenu_View
        {
            get
            {
                return SR.GetStringInternal("OpenFolderFileDialog_ContextMenu_View", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Details
        /// </summary>
        public static string OpenFolderFileDialog_ContextMenu_View_Details
        {
            get
            {
                return SR.GetStringInternal("OpenFolderFileDialog_ContextMenu_View_Details", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Large Icon
        /// </summary>
        public static string OpenFolderFileDialog_ContextMenu_View_LargeIcon
        {
            get
            {
                return SR.GetStringInternal("OpenFolderFileDialog_ContextMenu_View_LargeIcon", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// List
        /// </summary>
        public static string OpenFolderFileDialog_ContextMenu_View_List
        {
            get
            {
                return SR.GetStringInternal("OpenFolderFileDialog_ContextMenu_View_List", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Small Icon
        /// </summary>
        public static string OpenFolderFileDialog_ContextMenu_View_SmallIcon
        {
            get
            {
                return SR.GetStringInternal("OpenFolderFileDialog_ContextMenu_View_SmallIcon", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Filter string you provided is not valid. The filter string must contain a description of the filter, followed by the vertical bar (|) and the filter pattern. The strings for different filtering options must also be separated by the vertical bar. Example: "Text files (*.txt)|*.txt|All files (*.*)|*.*"
        /// </summary>
        public static string OpenFolderFileDialog_Exception_InvalidFilters
        {
            get
            {
                return SR.GetStringInternal("OpenFolderFileDialog_Exception_InvalidFilters", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Filters:
        /// </summary>
        public static string OpenFolderFileDialog_Filters
        {
            get
            {
                return SR.GetStringInternal("OpenFolderFileDialog_Filters", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Paths:
        /// </summary>
        public static string OpenFolderFileDialog_Paths
        {
            get
            {
                return SR.GetStringInternal("OpenFolderFileDialog_Paths", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Open Folder/File
        /// </summary>
        public static string OpenFolderFileDialog_Text
        {
            get
            {
                return SR.GetStringInternal("OpenFolderFileDialog_Text", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Can not set item height will handle created.
        /// </summary>
        public static string OwnerDrawListView_CanNotSetItemHeight
        {
            get
            {
                return SR.GetStringInternal("OwnerDrawListView_CanNotSetItemHeight", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Close
        /// </summary>
        public static string OwnerDrawTabControl_Close
        {
            get
            {
                return SR.GetStringInternal("OwnerDrawTabControl_Close", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Only could add OwnerTabPage or its subclass.
        /// </summary>
        public static string OwnerDrawTabControl_Exception_NotOwnerDrawTabPage
        {
            get
            {
                return SR.GetStringInternal("OwnerDrawTabControl_Exception_NotOwnerDrawTabPage", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Item: {0}-{1}/{2}
        /// </summary>
        public static string PagingControl_TextTemplate_Item
        {
            get
            {
                return SR.GetStringInternal("PagingControl_TextTemplate_Item", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Page: {0}/{1}
        /// </summary>
        public static string PagingControl_TextTemplate_Page
        {
            get
            {
                return SR.GetStringInternal("PagingControl_TextTemplate_Page", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid path.
        /// </summary>
        public static string Path_Invalid
        {
            get
            {
                return SR.GetStringInternal("Path_Invalid", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Directory do not contains enough parent level.
        /// </summary>
        public static string Path_LevelNotEnough
        {
            get
            {
                return SR.GetStringInternal("Path_LevelNotEnough", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid .net PE data
        /// </summary>
        public static string PE_Exception_InvalidDotNetPE
        {
            get
            {
                return SR.GetStringInternal("PE_Exception_InvalidDotNetPE", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid PE data.
        /// </summary>
        public static string PE_Exception_InvalidPE
        {
            get
            {
                return SR.GetStringInternal("PE_Exception_InvalidPE", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Specified manifest resource could not been found.
        /// </summary>
        public static string PE_Exception_ManifestResourceNotFound
        {
            get
            {
                return SR.GetStringInternal("PE_Exception_ManifestResourceNotFound", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// .Net PE contains not supported data.
        /// </summary>
        public static string PE_Exception_NotSupportedDotNetPE
        {
            get
            {
                return SR.GetStringInternal("PE_Exception_NotSupportedDotNetPE", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid literal number.
        /// </summary>
        public static string Picture_Number_Invalid
        {
            get
            {
                return SR.GetStringInternal("Picture_Number_Invalid", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Use "Port" property instead of "Text".
        /// </summary>
        public static string PortTextBox_Text_NotSupport
        {
            get
            {
                return SR.GetStringInternal("PortTextBox_Text_NotSupport", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid element.
        /// </summary>
        public static string RegexElement_Invalid
        {
            get
            {
                return SR.GetStringInternal("RegexElement_Invalid", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Could not set RegexTerminalAssertion as lookbehind assertion.
        /// </summary>
        public static string RegexElement_LookbehindAssertion_Invalid
        {
            get
            {
                return SR.GetStringInternal("RegexElement_LookbehindAssertion_Invalid", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Not accept RegexLiteralElement or RegexQuantifierElement as matching part.
        /// </summary>
        public static string RegexQuantifierElement_NotAcceptElements
        {
            get
            {
                return SR.GetStringInternal("RegexQuantifierElement_NotAcceptElements", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid ring. ring value must between 1 to 65534
        /// </summary>
        public static string ReliableUdpClient_Exception_InvalidRing
        {
            get
            {
                return SR.GetStringInternal("ReliableUdpClient_Exception_InvalidRing", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Client already managed.
        /// </summary>
        public static string ReliableUdpConsole_Exception_ClientManaged
        {
            get
            {
                return SR.GetStringInternal("ReliableUdpConsole_Exception_ClientManaged", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Client does not manage.
        /// </summary>
        public static string ReliableUdpConsole_Exception_ClientNotManaged
        {
            get
            {
                return SR.GetStringInternal("ReliableUdpConsole_Exception_ClientNotManaged", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Server isn't running
        /// </summary>
        public static string Server_Exception_IsNotRunning
        {
            get
            {
                return SR.GetStringInternal("Server_Exception_IsNotRunning", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Server is running.
        /// </summary>
        public static string Server_Exception_IsRunning
        {
            get
            {
                return SR.GetStringInternal("Server_Exception_IsRunning", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Server not running, please call Start() method first.
        /// </summary>
        public static string Server_MustRunning
        {
            get
            {
                return SR.GetStringInternal("Server_MustRunning", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Server is running!
        /// </summary>
        public static string Server_Running
        {
            get
            {
                return SR.GetStringInternal("Server_Running", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid extension.
        /// </summary>
        public static string ShellImageList_Exception_InvalidExtension
        {
            get
            {
                return SR.GetStringInternal("ShellImageList_Exception_InvalidExtension", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Could not insert a item in specified index, please use add instead.
        /// </summary>
        public static string SortedCollection_Exception_Insert
        {
            get
            {
                return SR.GetStringInternal("SortedCollection_Exception_Insert", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Could not set item for a SortCollection
        /// </summary>
        public static string SortedCollection_Exception_SetItem
        {
            get
            {
                return SR.GetStringInternal("SortedCollection_Exception_SetItem", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Must at lease contains one column. 
        /// </summary>
        public static string StronglyTypedRowGenerator_RequiredColumn
        {
            get
            {
                return SR.GetStringInternal("StronglyTypedRowGenerator_RequiredColumn", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Must at lease contains one column. 
        /// </summary>
        public static string StronglyTypedTableGenerator_RequiredColumn
        {
            get
            {
                return SR.GetStringInternal("StronglyTypedTableGenerator_RequiredColumn", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Control must be switch panel.
        /// </summary>
        public static string SwitchControl_Exception_MustBeSwitchPage
        {
            get
            {
                return SR.GetStringInternal("SwitchControl_Exception_MustBeSwitchPage", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// The PIDL is moniting.
        /// </summary>
        public static string SystemChangeNotifier_ArgumentException_PIDLExist
        {
            get
            {
                return SR.GetStringInternal("SystemChangeNotifier_ArgumentException_PIDLExist", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Table name must not be empty.
        /// </summary>
        public static string TableModel_Exception_NameEmpty
        {
            get
            {
                return SR.GetStringInternal("TableModel_Exception_NameEmpty", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Table schema must not be empty.
        /// </summary>
        public static string TableModel_Exception_SchemaEmpty
        {
            get
            {
                return SR.GetStringInternal("TableModel_Exception_SchemaEmpty", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Table name already exists.
        /// </summary>
        public static string TableModelCollection_Exception_NameExists
        {
            get
            {
                return SR.GetStringInternal("TableModelCollection_Exception_NameExists", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Column could not be empty.
        /// </summary>
        public static string TableSet_Exception_ColumnsEmpty
        {
            get
            {
                return SR.GetStringInternal("TableSet_Exception_ColumnsEmpty", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Entity could not be null when specify condition columns.
        /// </summary>
        public static string TableSet_Exception_EntityIsNull
        {
            get
            {
                return SR.GetStringInternal("TableSet_Exception_EntityIsNull", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid property name.
        /// </summary>
        public static string TableSet_Exception_InvalidPropertyName
        {
            get
            {
                return SR.GetStringInternal("TableSet_Exception_InvalidPropertyName", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Client has already connected.
        /// </summary>
        public static string TcpClient_Exception_Connected
        {
            get
            {
                return SR.GetStringInternal("TcpClient_Exception_Connected", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Could not find end pattern bytes in buffer.
        /// </summary>
        public static string TcpClient_Exception_NotFoundPattern
        {
            get
            {
                return SR.GetStringInternal("TcpClient_Exception_NotFoundPattern", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Not any data has been received.
        /// </summary>
        public static string TcpClient_Exception_ReceiveEmtpy
        {
            get
            {
                return SR.GetStringInternal("TcpClient_Exception_ReceiveEmtpy", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// This HookType does not support in ThreadHook. You must consider using a GlobalHook class
        /// </summary>
        public static string ThreadHook_HookType_NotSupport
        {
            get
            {
                return SR.GetStringInternal("ThreadHook_HookType_NotSupport", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Node is not a collection container.
        /// </summary>
        public static string TreeListView_Exception_NodeIsNotCollectionContainer
        {
            get
            {
                return SR.GetStringInternal("TreeListView_Exception_NodeIsNotCollectionContainer", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Data is not a collection container.
        /// </summary>
        public static string TreeListViewNode_Exception_DataIsNotCollectionContainer
        {
            get
            {
                return SR.GetStringInternal("TreeListViewNode_Exception_DataIsNotCollectionContainer", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// You must call BeginUpdate() Method before.
        /// </summary>
        public static string Updating_Exception_InvalidEndUpdate
        {
            get
            {
                return SR.GetStringInternal("Updating_Exception_InvalidEndUpdate", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Expression is empty.
        /// </summary>
        public static string ValidationInfo_Exception_ExpressionEmpty
        {
            get
            {
                return SR.GetStringInternal("ValidationInfo_Exception_ExpressionEmpty", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Item exist.
        /// </summary>
        public static string ValidationItemCollection_Exception_ItemExist
        {
            get
            {
                return SR.GetStringInternal("ValidationItemCollection_Exception_ItemExist", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Item not found.
        /// </summary>
        public static string ValidationItemCollection_Exception_ItemNotFound
        {
            get
            {
                return SR.GetStringInternal("ValidationItemCollection_Exception_ItemNotFound", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Validator Tasks
        /// </summary>
        public static string Validator_ActionList_Tasks
        {
            get
            {
                return SR.GetStringInternal("Validator_ActionList_Tasks", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Clear Validation Information
        /// </summary>
        public static string Validator_ActionList_Tasks_ClearValidationInformation
        {
            get
            {
                return SR.GetStringInternal("Validator_ActionList_Tasks_ClearValidationInformation", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Error Provider
        /// </summary>
        public static string Validator_Category_BlinkRate
        {
            get
            {
                return SR.GetStringInternal("Validator_Category_BlinkRate", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Error Provider
        /// </summary>
        public static string Validator_Category_BlinkStyle
        {
            get
            {
                return SR.GetStringInternal("Validator_Category_BlinkStyle", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Action
        /// </summary>
        public static string Validator_Category_CustomValidation
        {
            get
            {
                return SR.GetStringInternal("Validator_Category_CustomValidation", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Error Provider
        /// </summary>
        public static string Validator_Category_Icon
        {
            get
            {
                return SR.GetStringInternal("Validator_Category_Icon", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Error Provider
        /// </summary>
        public static string Validator_Category_RightToLeft
        {
            get
            {
                return SR.GetStringInternal("Validator_Category_RightToLeft", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Validation
        /// </summary>
        public static string Validator_Category_Validation
        {
            get
            {
                return SR.GetStringInternal("Validator_Category_Validation", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Do you want to clear all validation information from current binding controls?
        /// </summary>
        public static string Validator_ClearWarning
        {
            get
            {
                return SR.GetStringInternal("Validator_ClearWarning", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// An error occurred.
        /// </summary>
        public static string Validator_DefaultErrorMesage
        {
            get
            {
                return SR.GetStringInternal("Validator_DefaultErrorMesage", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// The rate in milliseconds at which the error icon blinks.
        /// </summary>
        public static string Validator_Description_BlinkRate
        {
            get
            {
                return SR.GetStringInternal("Validator_Description_BlinkRate", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Controls whether the error icon blinks when an error is set.
        /// </summary>
        public static string Validator_Description_BlinkStyle
        {
            get
            {
                return SR.GetStringInternal("Validator_Description_BlinkStyle", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Occurs when validate, it required set "Custom" validation type flag.
        /// </summary>
        public static string Validator_Description_CustomValidation
        {
            get
            {
                return SR.GetStringInternal("Validator_Description_CustomValidation", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Validate all configured validation information controls which lay onto the specified container, when the container is closing.
        /// </summary>
        public static string Validator_Description_Form
        {
            get
            {
                return SR.GetStringInternal("Validator_Description_Form", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// The icon used to indicate an error.
        /// </summary>
        public static string Validator_Description_Icon
        {
            get
            {
                return SR.GetStringInternal("Validator_Description_Icon", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Validation mode. This mode should be combined with the follow value. FocusChange: Can change to next control when validation fail. Submit:Validate control when user submit.
        /// </summary>
        public static string Validator_Description_Mode
        {
            get
            {
                return SR.GetStringInternal("Validator_Description_Mode", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Indicates whether the component should draw right-to-left for RTL languages.
        /// </summary>
        public static string Validator_Description_RightToLeft
        {
            get
            {
                return SR.GetStringInternal("Validator_Description_RightToLeft", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Specify a message to been show when control violate regular expression rule(text isn't match regular express).
        /// </summary>
        public static string Validator_Description_Validation_ErrorMesage
        {
            get
            {
                return SR.GetStringInternal("Validator_Description_Validation_ErrorMesage", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Specify regular expression for validation. This property only valid if sets RegularExpression validation type. 
        /// </summary>
        public static string Validator_Description_Validation_RegularExpression
        {
            get
            {
                return SR.GetStringInternal("Validator_Description_Validation_RegularExpression", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Specify options for regular expression.
        /// </summary>
        public static string Validator_Description_Validation_RegularExpressionOptions
        {
            get
            {
                return SR.GetStringInternal("Validator_Description_Validation_RegularExpressionOptions", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Validator
        /// </summary>
        public static string Validator_Name
        {
            get
            {
                return SR.GetStringInternal("Validator_Name", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Do you want to remove validation information associate with this control?
        /// </summary>
        public static string Validator_RemoveWarning
        {
            get
            {
                return SR.GetStringInternal("Validator_RemoveWarning", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// End value must larger than begin value.
        /// </summary>
        public static string ValueRange_Exception_UpperLesserThanLower
        {
            get
            {
                return SR.GetStringInternal("ValueRange_Exception_UpperLesserThanLower", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Size unexpected, requries 2.
        /// </summary>
        public static string Variable16_Exception_SizeUnexpected
        {
            get
            {
                return SR.GetStringInternal("Variable16_Exception_SizeUnexpected", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Size unexpected, requries 4.
        /// </summary>
        public static string Variable32_Exception_SizeUnexpected
        {
            get
            {
                return SR.GetStringInternal("Variable32_Exception_SizeUnexpected", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Size unexpected, requries 8.
        /// </summary>
        public static string Variable64_Exception_SizeUnexpected
        {
            get
            {
                return SR.GetStringInternal("Variable64_Exception_SizeUnexpected", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Size unexpected, requries 1.
        /// </summary>
        public static string Variable8_Exception_SizeUnexpected
        {
            get
            {
                return SR.GetStringInternal("Variable8_Exception_SizeUnexpected", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// This item belong to another item.
        /// </summary>
        public static string VirtualItemCollection_ItemHasParent
        {
            get
            {
                return SR.GetStringInternal("VirtualItemCollection_ItemHasParent", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Property Changed
        /// </summary>
        public static string VirtualListView_Category_VirtualListSizeChanged
        {
            get
            {
                return SR.GetStringInternal("VirtualListView_Category_VirtualListSizeChanged", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Occurs whenever the 'VirtualListSize' property for this ListView changed.
        /// </summary>
        public static string VirtualListView_Description_VirtualListSizeChanged
        {
            get
            {
                return SR.GetStringInternal("VirtualListView_Description_VirtualListSizeChanged", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Invalid chunked body.
        /// </summary>
        public static string WebClient_Exception_InvalidChunkedBody
        {
            get
            {
                return SR.GetStringInternal("WebClient_Exception_InvalidChunkedBody", Thread.CurrentThread.CurrentUICulture);
            }
        }

    }
}
