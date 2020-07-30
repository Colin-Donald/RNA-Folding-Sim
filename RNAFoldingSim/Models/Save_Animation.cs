using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace RNAFoldingSim.Models
{
    public class Save_Animation
    {
        /**
         * void Save(Fold animation, String fileName)
         * params: (Fold) A pre-generated animation; (String) name of file
         */ 
        public static void Save(Fold animation)
        {
            if (animation == null)
            {
                return;
            }
            try
            {
                // Save
                XmlDocument rnaFile = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(typeof(Fold));
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, animation);
                    stream.Position = 0;
                    rnaFile.Load(stream);
                    rnaFile.Save(animation.id.ToString());// TODO: Make a method to distribute proper names
                }
            }
            catch (Exception fsave)
            {
                // TODO: Log this in a file or something
                Console.WriteLine("save failed");
            }
        }
    }
}
