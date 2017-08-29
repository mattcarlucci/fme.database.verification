using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fme.Library
{
    public class Serializer
    {
        /// <summary>
        /// Serializes the specified path.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">The path.</param>
        /// <param name="files">The files.</param>
        public static void Serialize<T>(String path, T content)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                serializer.Serialize(streamWriter, content);
            }
        }

        /// <summary>
        /// Serializes the specified path.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">The path.</param>
        /// <param name="files">The files.</param>
        public static void Serialize<T>(String path, List<T> content)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));

            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                serializer.Serialize(streamWriter, content);
            }
        }     

        /// <summary>
        /// Des the serialize.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">The path.</param>
        /// <returns>T.</returns>
        public static T DeSerialize<T>(String path) where T : new()
        {
            try
            {
                if (File.Exists(path) == false) return new T();

                XmlSerializer serializer = new XmlSerializer(typeof(T));

                using (StreamReader streamWriter = new StreamReader(path))
                {
                    return (T)serializer.Deserialize(streamWriter);
                }

            }
            catch (Exception)
            {
                return new T();
            }
        }
    }
}
