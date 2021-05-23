using System.Collections.Generic;
using WPFPractice.BindingEnums;

namespace WPFPractice.Model
{
    
    public class Parameter
    {
        public string Name { get; set; }
        public ParameterType SelectedParameterType { get; set; } = ParameterType.SimpleString;
        public List<string> Strings { get; set; }
    }

    //Rem:
    //- комментарии
}
