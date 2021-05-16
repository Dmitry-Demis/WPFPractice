using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPractice.Model
{
    public enum DataType
    {
        SimpleString,
        StringWithHistory,
        ValueFromList,
        SetFromList
    }
    public class Parameter
    {
        public string Name { get; set; }
        public DataType Types { get; set; } = new DataType();
        public List<string> Strings { get; set; }
    }
}
