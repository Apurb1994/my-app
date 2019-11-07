using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Text;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using System.Xml;
using System.Xml.Serialization;

namespace ISG_VIG_Brands.Library
{
    public static class SerialiserDeserialiser
    {
        public static string Serialise<T>(this T rootObject)
        {
            try
            {
                return JsonConvert.SerializeObject(rootObject, Newtonsoft.Json.Formatting.Indented);
            }
            catch { }

            return null;
        }

        public static T Deserializer<T>(this string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex) { }

            return default(T);
        }

        public static T DeserializerXml<T>(this string xml)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                StringReader stringReader = new StringReader(xml);
                return (T)xmlSerializer.Deserialize(stringReader);
            }
            catch (Exception ex) { }

            return default(T);
        }

        public static string SerializerXml<T>(this T xml)
        {
            try
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                var stringBuilder = new StringBuilder();
                using (var stringWriter = new StringWriter(stringBuilder))
                {
                    xmlSerializer.Serialize(stringWriter, xml);
                }
                return stringBuilder.ToString();
            }
            catch (Exception ex) { }

            return null;
        }

        public static string XmlToJsonSerialisation(this XmlDocument doc)
        {
            return JsonConvert.SerializeXmlNode(doc);
        }

        public static string XmlToJsonSerialisation(this string data)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);
            return doc.XmlToJsonSerialisation();
        }

        public static XmlDocument JsonToXmlSerialisation(this string json, string rootNode = "root")
        {
            try
            {
                return (XmlDocument)JsonConvert.DeserializeXmlNode(json);
            }
            catch
            {
                try
                {
                    return (XmlDocument)JsonConvert.DeserializeXmlNode(json, rootNode);
                }
                catch { }
            }

            return null;
        }
    }
}
