using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DirSyncro
{
    class Utility
    {
        public static Regex WildcardMatch(String wildcard, bool case_sensitive)
        {
            // Replace the * with an .* and the ? with a dot. Put ^ at the
            // beginning and a $ at the end
            String pattern = "^" + Regex.Escape(wildcard).Replace(@"\*", ".*").Replace(@"\?", ".") + "$";

            // Now, run the Regex as you already know
            Regex regex;
            if (case_sensitive)
                regex = new Regex(pattern);
            else
                regex = new Regex(pattern, RegexOptions.IgnoreCase);

            return regex;
        }

        public static T ReadFromXML<T>(string xmlFile)
        {
            T obj = default(T);

            using (FileStream fs = new FileStream(xmlFile, FileMode.Open))
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));

                obj = (T)xs.Deserialize(fs);
            }

            return obj;
        }

        public static void WriteToXML<T>(string xmlFile, ref T serializeObj)
        {
            using (FileStream fs = new FileStream(xmlFile, FileMode.Create))
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));

                xs.Serialize(fs, serializeObj);
            }
        }
    }
}
