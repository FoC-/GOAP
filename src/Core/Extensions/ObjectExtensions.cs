using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Core.Extensions
{
    public static class ObjectExtensions
    {
        public static T DeepClone<T>(this T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            if (ReferenceEquals(source, null))
            {
                return default(T);
            }

            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter
                {
                    Context = new StreamingContext(StreamingContextStates.Clone)
                };

                formatter.Serialize(stream, source);
                stream.Position = 0;

                return (T)formatter.Deserialize(stream);
            }
        }
    }
}
