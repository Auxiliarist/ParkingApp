using MvvmCross.Base;
using ServiceStack.Text;
using System;
using System.IO;

namespace ParkingApp.Services.Implementations
{
    public class MvxJsonConverter : IMvxJsonConverter
    {
        public MvxJsonConverter()
        {
        }

        public T DeserializeObject<T>(Stream stream) => JsonSerializer.DeserializeFromStream<T>(stream);

        public T DeserializeObject<T>(string inputText) => JsonSerializer.DeserializeFromString<T>(inputText);

        public object DeserializeObject(Type type, string inputText) => JsonSerializer.DeserializeFromString(inputText, type);

        public string SerializeObject(object toSerialise) => JsonSerializer.SerializeToString(toSerialise);
    }
}