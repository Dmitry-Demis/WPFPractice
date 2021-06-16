using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using WPFPractice.Interfaces;
using WPFPractice.Model;

namespace WPFPractice.Services
{
    public class JsonFileService : IFileService
    {
        public List<Parameter> Open(string fileName)
        {
            List<Parameter> parameters = new List<Parameter>();
            DataContractJsonSerializer jsonFormatter =
                 new DataContractJsonSerializer(typeof(List<Parameter>));
            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                parameters = jsonFormatter.ReadObject(fs) as List<Parameter>;
            }
            return parameters;
        }

        public void Save(string fileName, List<Parameter> parameters)
        {
            DataContractJsonSerializer jsonFormatter =
               new DataContractJsonSerializer(typeof(List<Parameter>));
            using (var fs = new FileStream(fileName, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, parameters);
            }
        }
    }
}
