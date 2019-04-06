using CrackerZIPArchiveWithPassword.Models;
using System.IO;
using System.Xml.Serialization;

namespace CrackerZIPArchiveWithPassword.Utilities
{
    class SerializeHelper
    {
        private static XmlSerializer _formatter;

        static SerializeHelper()
        {
            _formatter = new XmlSerializer(typeof(CrackerState));
        }

        public static void Serialize(CrackerState crackerState, string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                _formatter.Serialize(fs, crackerState);
            }
        }

        public static CrackerState DeSerialize(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                return (CrackerState)_formatter.Deserialize(fs);
            }
        }
    }
}