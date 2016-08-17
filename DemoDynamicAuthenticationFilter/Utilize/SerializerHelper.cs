using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace DemoDynamicAuthenticationFilter.Utilize
{
    public static class SerializerHelper
    {
        public static string JavaScriptSerialize(object obj)
        {
            JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
            var TheJson = TheSerializer.Serialize(obj);
            return TheJson;
        }

        public static T JavaScriptDeserialize<T>(string input)
        {
            JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
            T obj = TheSerializer.Deserialize<T>(input);
            return obj;
        }
        public static T DeserializeJsonObject<T>(string content, Encoding encoding)
        {
            using (Stream stream = new MemoryStream())
            {
                byte[] data = encoding.GetBytes(content);
                stream.Write(data, 0, data.Length);
                stream.Position = 0;
                var deserializer = new DataContractJsonSerializer(typeof(T));
                return (T)deserializer.ReadObject(stream);
            }
        }

        public static string SerializeJsonObject(object obj)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            using (StreamReader reader = new StreamReader(memoryStream))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                serializer.WriteObject(memoryStream, obj);
                memoryStream.Position = 0;
                return reader.ReadToEnd();
            }
        }
    }
}