using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.IO;

namespace WPFPractice.Model
{
    public class JsonFileService : IFileService
    {
        public List<Parametres> Open(string filename)
        {
            List<Parametres> parametres = new List<Parametres>();
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(List<Parametres>));
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                parametres = jsonSerializer.ReadObject(fs) as List<Parametres>;
            }
            return parametres;
        }

        public void Save(string filename, List<Parametres> parametresList)
        {
            DataContractJsonSerializer jsonFormatter =
             new DataContractJsonSerializer(typeof(List<Parametres>));
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, parametresList);
            }
        }
    }
}
