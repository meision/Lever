using System;
using System.Collections.Concurrent;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Meision.Coding
{
    public class JsonSerializer : IDisposable
    {
        private static readonly ConcurrentDictionary<Type, DataContractJsonSerializer> __serializers = new ConcurrentDictionary<Type, DataContractJsonSerializer>();

        private MemoryStream _stream;

        public JsonSerializer()
        {
            this._stream = new MemoryStream();
        }

        ~JsonSerializer()
        {
            this.Dispose(false);
        }
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this._stream != null)
                {
                    this._stream.Dispose();
                    this._stream = null;
                }
            }
        }

        public string Serialize<T>(T value)
        {
            DataContractJsonSerializer serializer = JsonSerializer.__serializers.GetOrAdd(typeof(T), (p) =>
            {
                return new DataContractJsonSerializer(typeof(T));
            });

            this._stream.Seek(0, SeekOrigin.Begin);
            serializer.WriteObject(this._stream, value);
            string text = Encoding.UTF8.GetString(this._stream.GetBuffer(), 0, (int)this._stream.Position);
            return text;
        }

        public T Deserialize<T>(string text)
        {
            ThrowHelper.ArgumentNull((text == null), nameof(text));

            DataContractJsonSerializer serializer = JsonSerializer.__serializers.GetOrAdd(typeof(T), (p) =>
            {
                return new DataContractJsonSerializer(typeof(T));
            });

         //  this._stream.

            serializer.ReadObject(this._stream);

            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream())
            {
                StreamWriter sw = new StreamWriter(ms);
              //  sw.Write(s);
                sw.Flush();
                ms.Seek(0, SeekOrigin.Begin);
                return (T)js.ReadObject(ms);
            }
        }


    }
}
